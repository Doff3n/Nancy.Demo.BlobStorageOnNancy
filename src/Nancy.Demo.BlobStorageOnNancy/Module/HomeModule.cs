using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Module
{
    public class HomeModule : Nancy.NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => View["Index.cshtml"];

        }


    }
}