using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FNC_AB_ORDER_MANAGEMENT.Models
{
    public class InmatningarModel
    {
        public string Ordernr { get; set; }
        public string Kund { get; set; }
        public string Telefonnr { get; set; }
        public string Ort { get; set; }
        public string Adress { get; set; }
        public System.DateTime InDatum { get; set; }
        public System.DateTime UtDatum { get; set; }
        public bool StyckPris { get; set; }
        public decimal Langd { get; set; }
        public decimal Timmar { get; set; }
        public bool Fakturerad { get; set; }
        public string Ovrigt { get; set; }
        public string Status { get; set; }
        public string AID { get; set; }
        public int ID { get; set; }
    
       // public virtual AspNetUsers AspNetUsers { get; set; }
    }
}