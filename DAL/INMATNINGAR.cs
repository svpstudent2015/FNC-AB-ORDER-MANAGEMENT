//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class INMATNINGAR
    {
        public int Id { get; set; }
        public string Ordernr { get; set; }
        public string Kund { get; set; }
        public string Telefonnr { get; set; }
        public string Ort { get; set; }
        public string Adress { get; set; }
        public Nullable<System.DateTime> InDatum { get; set; }
        public Nullable<System.DateTime> UtDatum { get; set; }
        public Nullable<bool> StyckPris { get; set; }
        public Nullable<decimal> Langd { get; set; }
        public Nullable<decimal> Timmar { get; set; }
        public Nullable<bool> Fakturerad { get; set; }
        public string Ovrigt { get; set; }
        public string Status { get; set; }
    }
}
