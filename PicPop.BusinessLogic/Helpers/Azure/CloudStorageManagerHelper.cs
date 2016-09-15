using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicPop.BusinessLogic.Models;

namespace PicPop.BusinessLogic
{
    public class CloudStorageManagerHelper
    {
        private static CloudBlobContainer GetContainer(string containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Properties.Settings.Default.StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName.ToLower());
            // Create the container if it doesn't already exist
            container.CreateIfNotExists();
            return container;
        }

        public static BlobFileModel GetFileInfo(BlobFileType fileType, string key, bool withFileData = true)
        {
            CloudBlobContainer container = GetContainer(fileType.ToString());
            CloudBlockBlob blob = container.GetBlockBlobReference(key);
            if (blob == null || !blob.Exists())
                return null;
            blob.FetchAttributes();
            BlobFileModel file = new BlobFileModel(blob, withFileData);
            return file;
        }

        public static BlobFileModel GetFileInfo(BlobFileType fileType, string key, DateTime filerDate,
            bool withFileData = true)
        {
            CloudBlobContainer container = GetContainer(fileType.ToString());
            CloudBlockBlob blob = container.GetBlockBlobReference(key);
            

            if (blob == null || !blob.Exists())
                return null;

            blob.FetchAttributes();

            if (!blob.Metadata.Any() &&
                (blob.Metadata["DtUploaded"] == null || Convert.ToDateTime(blob.Metadata["DtUploaded"]) > filerDate))
            {
                blob.Delete();
                return null;
            }

            BlobFileModel file = new BlobFileModel(blob, withFileData);
            return file;
        }


        public static void InsertFileWithStaticName(BlobFileType fileType, string staticFileName, BlobFileModel file,
            string userId = "")
        {
            CloudBlobContainer container = GetContainer(fileType.ToString());
            CloudBlockBlob blob = container.GetBlockBlobReference(staticFileName);
            file.Data.Position = 0;
            blob.UploadFromStream(file.Data);

            blob.Metadata["FileName"] = file.FileName;
            blob.Metadata["ContentType"] = file.ContentType;
            blob.Metadata["ContentLength"] = file.Data.Length.ToString();
            blob.Metadata["BlobFileType"] = fileType.ToString();
            blob.Metadata["DtUploaded"] = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            blob.SetMetadata();
        }


        public static CloudBlobContainer GetCloudBlobContainer(string containerName)
        {
            CloudBlobContainer container = GetContainer(containerName);
            if (container == null)
                return null;
            container.SetPermissions(new BlobContainerPermissions{PublicAccess = BlobContainerPublicAccessType.Blob});
            return container;
        }

        public static string GetUrlInfo(BlobFile blobFile)
        {
            if (blobFile == null || string.IsNullOrEmpty(blobFile.BlobKey))
                return string.Empty;

            CloudBlobContainer container = GetCloudBlobContainer(((BlobFileType)blobFile.Container).ToString());
            CloudBlockBlob blob = container.GetBlockBlobReference(blobFile.BlobKey);
            if (blob == null || !blob.Exists())
                return string.Empty;
            return blob.Uri.ToString();
        }
    }
}
