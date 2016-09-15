using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using PicPop.BusinessLogic;
using PicPop.BusinessLogic.Models;
using PicPop.BusinessLogic.Utils;

namespace PicPop.Helper
{
    public class PicPopImagesHelper
    {
        /// <summary>
        /// insert File in the Blob.
        /// Modify the image to add a watermark and add to a Blob.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="file"></param>
        /// <returns>return List of blobs relationship with the image</returns>
        public List<BlobFile> CreateImage(string userId, HttpPostedFileBase file)
        {
            BlobFileModel blobOriginal = new BlobFileModel(file);
            BlobFile blobFile = new BlobFile
            {
                Container = (int)BlobFileType.ImagesOriginal,
                CreatedBy = userId,
                Filename = blobOriginal.FileName,
                BlobKey = Common.GetGuid(),
                dtCreated = DateTime.Now
            };

            CloudStorageManagerHelper.InsertFileWithStaticName(BlobFileType.ImagesOriginal, blobFile.BlobKey, blobOriginal, userId);

            List<BlobFile> lst = new List<BlobFile>();
            lst.Add(blobFile);
            lst.Add(AddWatermark(userId,blobFile, file));

            return lst;
        }

        public BlobFile AddWatermark(string userId, BlobFile blobFileOriginal,  HttpPostedFileBase file)
        {
            Stream stream = AddWaterMarkToImage(file);
            


            BlobFileModel blobWatermark = new BlobFileModel(stream, string.Format("{0}", blobFileOriginal.Filename), "image/png");
            BlobFile blobFile = new BlobFile
            {
                Container = (int)BlobFileType.ImagesWaterMark,
                CreatedBy = userId,
                Filename = blobWatermark.FileName,
                BlobKey = Common.GetGuid(),
                dtCreated = DateTime.Now
            };

            CloudStorageManagerHelper.InsertFileWithStaticName((BlobFileType)blobFile.Container, blobFile.BlobKey, blobWatermark, userId);

            return blobFile;
            
        }

        private Stream AddWaterMarkToImage(HttpPostedFileBase file)
        {
            Bitmap bmp = new Bitmap(file.InputStream);
            Graphics canvas = Graphics.FromImage(bmp);

            Bitmap bmpNew = new Bitmap(bmp.Width, bmp.Height);
            canvas = Graphics.FromImage(bmpNew);
            canvas.DrawImage(bmp, new Rectangle(0, 0,
                bmpNew.Width, bmpNew.Height), 0, 0, bmp.Width, bmp.Height,
                GraphicsUnit.Pixel);
            bmp = bmpNew;


            // Here replace "Text" with your text and you also can assign Font Family, Color, Position Of Text etc. 
            canvas.DrawString("PIC POP WEB", new Font("Verdana", 16, FontStyle.Bold), new SolidBrush(Color.Gray), (bmp.Width / 2), (bmp.Height / 2));

            //canvas.DrawString("Text", new Font("Verdana", 14, FontStyle.Bold), new SolidBrush(Color.Beige), (bmp.Width / 2), (bmp.Height / 2));

            //bmp.Save(System.Web.HttpContext.Current.Server.MapPath("~/WaterMarkImages/ ") + file.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            ms.Position = 0;
            Stream stream = ms;

            return stream;
            //Bitmap image1 = (Bitmap)Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("~/WaterMarkImages/ ") + file.FileName, true);
            //return (Image)image1;
        }

        //public byte[] ConvertImageToByteArray(Image imageToConvert)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        ImageFormat format;
        //        //switch (imageToConvert.RawFormat)
        //        //{
        //        //    case "image/png":
        //        //        format = ImageFormat.Png;
        //        //        break;
        //        //    case "image/gif":
        //        //        format = ImageFormat.Gif;
        //        //        break;
        //        //    default:
        //        //        format = ImageFormat.Jpeg;
        //        //        break;
        //        //}

        //        imageToConvert.Save(ms, format);
        //        return ms.ToArray();
        //    }
        //}

    }
}