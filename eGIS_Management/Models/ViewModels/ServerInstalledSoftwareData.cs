using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eGIS_Management.Models.ViewModels
{
    public class ServerInstalledSoftwareData
    {
        public int Software_ID { get; set; }
        public string Name_Version { get; set; }
        public bool Installed { get; set; }
    }
}