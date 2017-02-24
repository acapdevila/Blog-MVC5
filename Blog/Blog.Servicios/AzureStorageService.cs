using System.IO;
using System.Web.Helpers;
using Blog.Servicios.Configuracion;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Blog.Servicios
{
    public static class AzureStorageService
    {
        public static CloudBlobContainer ObtenerBlobContainer()
        {
                CloudStorageAccount storageAccount  = CloudStorageAccount.Parse(WebConfigParametro.StorageConnectionString);

                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                CloudBlobContainer contenedor = blobClient.GetContainerReference(WebConfigParametro.NombreContenedorBlobAzure);

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

        public static void SubirImagen(this CloudBlobContainer storageContainer, string blobName, WebImage image)
        {
            CloudBlockBlob blob = storageContainer.GetBlockBlobReference(blobName);
            blob.Properties.ContentType = "image/" + image.ImageFormat;

            using (var stream = new MemoryStream(image.GetBytes(), writable: false))
            {
                blob.UploadFromStream(stream);
            }
        }

        public static string GetBlobName(string folderPath, string filename)
        {
            return $"{folderPath}{filename}".Substring(1, folderPath.Length + filename.Length - 1);
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
    }
}
