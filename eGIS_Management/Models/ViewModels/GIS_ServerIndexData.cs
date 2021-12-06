using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eGIS_Management.Models;

namespace eGIS_Management.Models.ViewModels
{
    public class GIS_ServerIndexData
    {
        public IEnumerable<GIS_Server> GIS_Servers { get; set; }
        public IEnumerable<Software> Softwares { get; set; }
    }
}