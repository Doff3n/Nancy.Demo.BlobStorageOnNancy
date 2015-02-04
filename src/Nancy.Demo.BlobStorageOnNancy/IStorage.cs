using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Models;

namespace Service
{
    interface IStorage
    {
        void Upload(string name, Stream source);
        void Delete(string name);
        byte[] Download(string name);
        Documents Documents();
        Containers Containers();
        void DeleteContainer(string container);
    }
}
