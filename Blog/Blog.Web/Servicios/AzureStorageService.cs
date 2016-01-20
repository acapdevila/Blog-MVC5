using System.IO;
using System.Web.Helpers;
using Blog.Web.Configuracion;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Blog.Web.Servicios
{
    public static class AzureStorageService
    {
        public static CloudBlobContainer GetBloobContainer()
        {
                var storageAccount  = CloudStorageAccount.Parse(WebConfigParametro.StorageConnectionString);

                var blobClient = storageAccount.CreateCloudBlobClient();

                var contenedor = blobClient.GetContainerReference(WebConfigParametro.NombreContenedorBlobAzure);

                if (contenedor.CreateIfNotExists())
                {
                    // configure container for public access
                    contenedor.SetPermissions(
                        new BlobContainerPermissions {
                           PublicAccess = BlobContainerPublicAccessType.Container
                         }); 

                }
                return contenedor;
            }

        public static void UploadImage(this CloudBlobContainer storageContainer, string blobName, WebImage image)
        {
            var blob = storageContainer.GetBlockBlobReference(blobName);
            blob.Properties.ContentType = "image/" + image.ImageFormat;

            using (var stream = new MemoryStream(image.GetBytes(), writable: false))
            {
                blob.UploadFromStream(stream);
            }
        }

        public static void CopyBlob(this CloudBlobContainer storageContainer, string sourceBlobName, string targetBlobName)
        {
                var blobSource = storageContainer.GetBlockBlobReference(sourceBlobName);
                if (blobSource.Exists())
                {
                  var blobTarget = storageContainer.GetBlockBlobReference(targetBlobName);
                  blobTarget.StartCopy(blobSource);
                }
         }

        public static void DeleteImage(this CloudBlobContainer storageContainer, string blobName)
        {
            var blockBlob = storageContainer.GetBlockBlobReference(blobName);
            blockBlob.DeleteAsync(); 
        }


        public static string GetBlobName(string folderPath, string filename)
        {
            return $"{folderPath}{filename}".Substring(1, folderPath.Length + filename.Length - 1);
        }

    }
}
