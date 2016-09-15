using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PicPop.BusinessLogic.Models
{
    [DataContract]
    public class BlobFileModel
    {
        #region Property
        [DataMember]
        public string BlobKey { get; set; }
        [DataMember]
        public BlobFileType BlobFileType { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public Stream Data { get; set; }
        [DataMember]
        public string ContentType { get; set; }
        [DataMember]
        public int ContentLength { get; set; }
        [DataMember]
        public DateTime UploadedDate { get; set; }
        #endregion

        #region PUBLIC
        public BlobFileModel(HttpPostedFileBase file)
        {


            if (file == null)
                return;

            int length = (int)file.InputStream.Length;
            if (length > 0)
            {
                Data = file.InputStream;
                Data.Position = 0;
            }

            FileName = System.IO.Path.GetFileName(file.FileName);
            ContentType = file.ContentType;
            ContentLength = file.ContentLength;
        }

        public BlobFileModel(string fileContent, string fileName, string contentType)
        {
            if (string.IsNullOrEmpty(fileContent) || string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(contentType))
                return;

            BlobKey = fileName;
            FileName = fileName;
            ContentType = contentType;
            Data = new MemoryStream();
            StreamWriter writer = new StreamWriter(Data);
            writer.Write(fileContent);
            writer.Flush();
        }

        public BlobFileModel(Stream stream, string fileName, string contentType)
        {
            if (stream.Length == 0 || string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(contentType))
                return;
            stream.Position = 0;
            FileName = fileName;
            ContentType = contentType;
            Data = stream;
        }

        public BlobFileModel(CloudBlockBlob blob, bool withFileData)
        {
            InitFormBlob(blob, withFileData);
        }
        #endregion

        #region PRIVATE
        private void InitFormBlob(CloudBlockBlob blob, bool withFileData)
        {
            if (blob == null || !blob.Exists())
                return;

            BlobKey = blob.Name;
            FileName = Uri.UnescapeDataString(blob.Metadata["FileName"]);
            ContentType = Uri.UnescapeDataString(blob.Metadata["ContentType"]);
            int contentLength;
            if (int.TryParse(Uri.UnescapeDataString(blob.Metadata["ContentLength"]), out contentLength))
                ContentLength = contentLength;

            BlobFileType type;
            if (Enum.TryParse<BlobFileType>(Uri.UnescapeDataString(blob.Metadata["BlobFileType"]), true, out type))
                BlobFileType = type;

            DateTime dtUploaded;
            if (DateTime.TryParse(Uri.UnescapeDataString(blob.Metadata["DtUploaded"]), out dtUploaded))
                UploadedDate = dtUploaded;

            if (withFileData)
            {
                Data = new MemoryStream();
                blob.DownloadToStream(Data);
                Data.Position = 0;
            }
        }
        #endregion
    }
}
