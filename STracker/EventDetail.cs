//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STracker
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventDetail
    {
        public int ID { get; set; }
        public int EventID { get; set; }
        public int WhoDid { get; set; }
        public int ActionDone { get; set; }
        public int ToWho { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual EventAction EventAction { get; set; }
        public virtual Person Person { get; set; }
        public virtual Person Person1 { get; set; }
    }
}