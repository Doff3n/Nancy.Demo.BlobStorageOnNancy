**Nancy + Azure Blob storage demo**

Azure has a Blob storage which is a data service for storing big amounts of unstructured data,
such as text or binary data(images, videos etc), the data can be accessed worldwide via Azure's content via HTTP or HTTPS.
You can use Blob storage to expose data publicly to the world, or to store private data:
http://azure.microsoft.com/en-us/documentation/services/storage/

NancyFx is a very lightweight web framework which is used in this project to serve the service
and to provide content negotiation almost out of the box:
http://nancyfx.org/

In order to use this project you need to:
- 1. Log into your Azure account and create a new storage data service
- 2. Edit the ConnectionStrings.config in this project. Add your own Azure storage account
credentials which can be found when managing keys in the newly created azure storage data service. 
The account name is the name of your storage.

Example of connection string: DefaultEndpointsProtocol=https;AccountName=storagesample;AccountKey=KWPLd0rpW2T0U7K2pVpF8rYr1BgYtR7wYQk33AYiXeUoquiaY6o0TWqduxmPHlqeCNZ3LU0DHptbeIHy5l/Yhg==

Create containers dynamically:
http://localhost:65260/document/[container]

Content negotiation on containers and base path/document:
http://localhost:65260/document/[container].json
http://localhost:65260/document.xml

