using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using PicPop.BusinessLogic.Helpers.Extensions;
using PicPop.BusinessLogic.Models;

namespace PicPop.BusinessLogic.Utils
{
    public class ImagesUtils
    {
        public static void SaveImagesBlob(string userId, HttpPostedFileBase upImage)
        {
            #region Create Blob Original Image
            BlobFileModel blobOriginal = new BlobFileModel(upImage);
            BlobFile blobFile = new BlobFile
            {
                Container = (int)BlobFileType.ImagesOriginal,
                CreatedBy = userId,
                Filename = blobOriginal.FileName,
                BlobKey = Common.GetGuid(),
                dtCreated = DateTime.Now
            };
            #endregion
            CloudStorageManagerHelper.InsertFileWithStaticName(BlobFileType.ImagesOriginal, blobFile.BlobKey, blobOriginal, userId);












            BlobFileModel blobWaterMark = CloudStorageManagerHelper.GetFileInfo(BlobFileType.ImagesOriginal, "Watermark.png");

            Bitmap imageWithWaterMark= AddWaterMark(blobOriginal.Data);

            byte[] data = imageWithWaterMark.ImageToByteArray();

            BlobFileModel blobImageWithWaterMark = new BlobFileModel(imageWithWaterMark.ToStream(ImageFormat.Png), string.Format("{0}_WaterMark", blobOriginal.FileName),
                "Image");
            BlobFile blobFileWithWaterMark = new BlobFile
            {
                Container = (int)BlobFileType.ImagesWaterMark,
                CreatedBy = userId,
                Filename = blobImageWithWaterMark.FileName,
                BlobKey = Common.GetGuid(),
                dtCreated = DateTime.Now
            };

            CloudStorageManagerHelper.InsertFileWithStaticName((BlobFileType)blobFileWithWaterMark.Container, blobFileWithWaterMark.BlobKey, blobImageWithWaterMark, userId);

            var blobHelper = new BlobFilesHelper();
            blobHelper.Add(blobFileWithWaterMark);


        }






        
        //http://www.aspsnippets.com/Articles/Create-Add-Watermark-Text-to-Images-Photo-in-ASPNet-using-C-and-VBNet.aspx
        public static Bitmap AddWaterMark(Stream stream)
        {
            string watermarkText = "Pic Pop";

            // Read the File into a Bitmap.
            using (Bitmap bmp = new Bitmap(stream, false))
            {
                using (Graphics grp = Graphics.FromImage(bmp))
                {
                    //Set the Color of the Watermark text.
                    Brush brush = new SolidBrush(Color.Gray);

                    //Set the Font and its size.
                    Font font = new System.Drawing.Font("Arial", 30, FontStyle.Bold, GraphicsUnit.Pixel);

                    //Determine the size of the Watermark text.
                    SizeF textSize = new SizeF();
                    textSize = grp.MeasureString(watermarkText, font);

                    //Position the text and draw it on the image.
                    Point position = new Point((bmp.Width - ((int) textSize.Width + 10)),
                        (bmp.Height - ((int) textSize.Height + 10)));
                    grp.DrawString(watermarkText, font, brush, position);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        //Save the Watermarked image to the MemoryStream.
                        bmp.Save(memoryStream, ImageFormat.Png);
                    }
                    return bmp;
                }
            }
            return null;




            //using (Bitmap image = new Bitmap(stream))
            //using (Bitmap watermarkImage = new Bitmap(stream2))
            ////using(Bitmap watermarkImage = Image.FromFile("/Images/PicPop.jpg") as Bitmap)
            //using (Graphics imageGraphics = Graphics.FromImage(image))
            //{
            //    watermarkImage.SetResolution(imageGraphics.DpiX, imageGraphics.DpiY);
            //    int x = ((image.Width - watermarkImage.Width) / 2);
            //    int y = ((image.Height - watermarkImage.Height) / 2);

            //    imageGraphics.DrawImage(watermarkImage, x, y, watermarkImage.Width, watermarkImage.Height);
            //    //TODO: CAMBIAR ESTO PARA QUE ACEPTE OTROS TIPOS DE IMAGEN 
            //    return image.ToStream(format);

            //    //return ImageToByteArray(image);
            //}
        }
    }
}
