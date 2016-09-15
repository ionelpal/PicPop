using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using PicPop.BusinessLogic.Models;
using PicPop.BusinessLogic.Utils;

namespace PicPop.BusinessLogic
{
    public class ShoppingCartHelper
    {
        /// <summary>
        /// get the shopping Cart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static ShoppingCartModel GetCart(string userId)
        {
            string key = string.Format("{0}_Cart", userId);

            // Get the blob from azure
            var blob = CloudStorageManagerHelper.GetFileInfo(BlobFileType.Cart, key, DateTime.Now.AddHours(-1), true);

            // Check if exist
            if (blob == null)
                return null;

            // Get the data from the blob and deserialize to Shopping Cart model
            string json = Common.StreamToString(blob.Data);

            var serializer = new JavaScriptSerializer();
            var shopCart = serializer.Deserialize<ShoppingCartModel>(json);

            return shopCart;
        }

        /// <summary>
        /// Save the Shopping Cart Data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="shopCart"></param>
        private static void SaveCart(string userId, ShoppingCartModel shopCart)
        {
            // Serialize the object Shopping Cart model to json
            var serializer = new JavaScriptSerializer();
            string content = serializer.Serialize(shopCart);
            
            string key = string.Format("{0}_Cart", userId);


            // Create the blob model 
            BlobFilesHelper blobFileHelper = new BlobFilesHelper();
            BlobFileModel blob = new BlobFileModel(content, key, "String");
            BlobFile blobFile = null;

            if (blobFileHelper.Exist(key))
            {
                // Get the blob File
                blobFile = blobFileHelper.GetByKey(key);

                blobFileHelper.Update(blobFile);
            }
            else
            {
                // Create the blob File
                blobFile = new BlobFile
                {
                    Container = (int)BlobFileType.Cart,
                    CreatedBy = userId,
                    Filename = blob.FileName,
                    BlobKey = blob.FileName,
                    dtCreated = DateTime.Now
                };
                blobFileHelper.Add(blobFile);
            }
            
            // Save the Blob model in Azure Storage
            CloudStorageManagerHelper.InsertFileWithStaticName((BlobFileType)blobFile.Container, key, blob);
        }
        
        /// <summary>
        /// Add Item to Shopping Cart
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns>Num Items inside of Shopping Cart</returns>
        public int Add(int id, string userId)
        {
            // Get the cart from a blob if doesnt exist create a object
            var cart = GetCart(userId) ?? new ShoppingCartModel();

            // get the image to Add in the cart
            var image = new PicPopImagesHelper().FindById(id);

            // Check if the image exist
            if (image == null)
                return cart.ListItems.Count;

            // Check if doesnt exist the image inside of the shopping cart
            if (!cart.ListItems.Any(x => x.Id.Equals(id)))
                cart.ListItems.Add(new ShoppingCartItemModel()
                {
                    Id = id,
                    Name = image.Name,
                    Amount = image.Amount
                });

            cart.TotalAmount = cart.ListItems.Sum(x => x.Amount);
            //TODO: Necesito calcular el Total Amount
            // save shopping cart
            SaveCart(userId, cart);
            
            return cart.ListItems.Count;
        }

        public void Delete(int id, string userId)
        {
            // Get the cart from a blob if doesnt exist create a object
            var cart = GetCart(userId) ?? new ShoppingCartModel();
            
            // Check if there is listitem inside of the cart
            if (cart.ListItems.Count==0)
                return;

            // Remove the Item from the cart
            cart.ListItems.Remove(cart.ListItems.FirstOrDefault(x => x.Id.Equals(id)));
            
            // calculate TotalAmount
            cart.TotalAmount = cart.ListItems.Sum(x => x.Amount);

            // save shopping cart
            SaveCart(userId, cart);
        }

        public void DeleteAll(string userId)
        {
            SaveCart(userId, new ShoppingCartModel());
        }


        public static int NumElements(string userId)
        {
            // Get the cart from a blob if doesnt exist create a object
            var cart = GetCart(userId) ?? new ShoppingCartModel();

            return cart.ListItems.Count;
        }
    }
}
