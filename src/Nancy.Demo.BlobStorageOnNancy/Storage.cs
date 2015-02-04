using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Service.Models;

namespace Service
{
    public class Storage : IStorage
    {


        private CloudBlobClient _blobClient;
        private CloudBlobContainer _container;
        private IEnumerable<CloudBlobContainer> _containers;
        public Storage()
        {
            var storageAccount = CloudStorageAccount.Parse(
               ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            _blobClient = storageAccount.CreateCloudBlobClient();
            _containers = _blobClient.ListContainers();
        }
        public Storage(string container)
        {
            var storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            _blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a Container. 
            _container = _blobClient.GetContainerReference(container);

            // Create the Container if it doesn't already exist.
            _container.CreateIfNotExists();

            _container.SetPermissions(
             new BlobContainerPermissions
            {
                PublicAccess =
                    BlobContainerPublicAccessType.Blob
            });
           
        }

        public Containers Containers()
        {
            var stringList = _containers.Select(x=>x.Name).ToList();
            var containerList = stringList.Select(item => new Container {Title = item}).ToList();
            var containers = new Containers{List = containerList};
            
            return containers;
        }

  
        public Documents Documents()
        {
            var stringList =
                _container.ListBlobs(null, true).OfType<CloudBlockBlob>().Select(blob => blob.Name).ToList();
            var documentList = stringList.Select(item => new Document {Title = item}).ToList();
            var documents = new Documents
            {
                List =documentList
            };
            return documents;
        }

        public void Upload(string name, Stream source)
        {
            var cloudBlockBlob = _container.GetBlockBlobReference(name);
            cloudBlockBlob.UploadFromStream(source);

        }

        public void Delete(string name)
        {
            var cloudBlockBlob = _container.GetBlockBlobReference(name);
            cloudBlockBlob.Delete();
        }

        public void DeleteContainer(string name)
        {
            var container = _blobClient.GetContainerReference(name);
            container.Delete();
        }


        public byte[] Download(string name)
        {
            var cloudBlockBlob = _container.GetBlockBlobReference(name);
            using (var memoryStream = new MemoryStream())
            {
                cloudBlockBlob.DownloadToStream(memoryStream);
                return memoryStream.ToArray();
            }
         
        }
    }
}