//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataEntites.FileTransfer
{
    using System;
    using System.Collections.Generic;
    
    public partial class FileDb
    {
        public int Id { get; set; }
        public Nullable<int> ManagerId { get; set; }
        public string FileType { get; set; }
        public string TextType { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
