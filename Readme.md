# Nancy.Demo.BlobStorageOnNancy

**Nancy + Azure Blob storage sample**

## About
Azure has a Blob storage which is a data service for storing big amounts of unstructured data,
such as text or binary data(images, videos etc), the data can be accessed worldwide via via HTTP or HTTPS.
You can use Blob storage to expose data publicly to the world, or to store private data:
http://azure.microsoft.com/en-us/documentation/services/storage/

NancyFx is a very lightweight web framework which is used in this project to serve the service
and to provide content negotiation almost out of the box:
http://nancyfx.org/

This demo shows how to use Azure storage on NancyFx resulting in a lightweight service ontop of Azure

###In order to use this project you need to:
1. Log into your Azure account and create a new storage data service
2. Edit the ConnectionStrings.config in this project. Add your own Azure storage account
credentials which can be found when managing keys in the newly created azure storage data service. 
The account name is the name of your storage.
3. Build and run the web application
4. Create containers dynamically:
- *http://localhost:65260/document/{containername}*
5. Uploading of files can be done via http Post to:
http://localhost:65260/documents/upload/{container} or via the web interface
6. Consume data from a mobile app or your new web app on containers and base path/document:
- *http://localhost:65260/document/{containername}.json*
- *http://localhost:65260/document.xml*

Example of connection string: 
```xml
<connectionStrings>
  <add name="StorageConnectionString" connectionString="DefaultEndpointsProtocol=https;AccountName=storagesample;AccountKey=KWPLd0r[...]DHptbeIHy5l/Yhg==" />
</connectionStrings>
```





