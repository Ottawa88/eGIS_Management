//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eGIS_Management.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NetworkShare
    {
        public short ID { get; set; }
        public string ShareName { get; set; }
        public string Network_Path { get; set; }
        public int ServerID { get; set; }
        public string Description { get; set; }
    
        public virtual GIS_Server GIS_Server { get; set; }
    }
}
