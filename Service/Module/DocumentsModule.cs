using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nancy;

namespace Service.Module
{
    public class DocumentsModule : Nancy.NancyModule
    {
        private IStorage storage;
        public DocumentsModule()
        {

            Get["/documents/"] = _ =>
            {
                storage = new Storage();
                var containers = storage.Containers();
                
                return containers;
            };

            Get["/documents/{container}"] = _ =>
            {
                var container = (string)_.container;
                storage = new Storage(_.container);
                var documents = storage.Documents();
                documents.Container = _.container;
                return documents;
            };

            Get["/documents/delete/{container}"] = _ =>
            {
                storage = new Storage();
                storage.DeleteContainer(_.container);
                
                  return View["index.cshtml"];
            };

            Get["/documents/delete/{container}/{name}"] = _ =>
            {
                storage = new Storage(_.container);
                storage.Delete(_.name);
                var documents = storage.Documents();
                documents.Container = _.container;
                return documents;
            };

            Get["/documents/image/{container}/{name}"] = _ =>
            {
                storage = new Storage(_.container);
                var documents = storage.Documents();
                documents.Container = _.container;
                return documents;
            };

            Get["/documents/{container}/{name}", true] = async (ctx, ct) =>
            {
                storage = new Storage(ctx.container);
                var documentName = (string) ctx.name;
                var documentByteArray = storage.Download(documentName);
                var contentType= MimeTypes.GetMimeType(documentName);
                var response = new Response();
                response.Headers.Add(new KeyValuePair<string, string>("Content-Disposition", "attachment; filename=" + documentName));
                response.ContentType = contentType;
               
                response.Contents = destinationStream =>
                       {
                           using (var stream = new MemoryStream(documentByteArray))
                           {
                              
                               stream.CopyTo(destinationStream);
                           }
                       };

                return response;
            };

            Post["/documents/upload/{container}"] = x =>
            {
                storage = new Storage(x.container);
                var firstOrDefault = Request.Files.FirstOrDefault();
                
                if (firstOrDefault != null)
                {
                    var documentName = firstOrDefault.Name.ToLower();
                    var content = firstOrDefault.Value;
                    storage.Upload(documentName, content);
                    return 200;
                }
               
                return 500;
            };




        }

    
    }
}