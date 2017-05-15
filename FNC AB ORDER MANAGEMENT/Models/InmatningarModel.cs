using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FNC_AB_ORDER_MANAGEMENT.Models
{
    public class InmatningarModel
    {
        public string Ordernr { get; set; }
        public string KundNamn { get; set; }
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
        public string AID { get; set; }
        public int ID { get; set; }
        public Nullable<int> KundID { get; set; }

        public List<InmatningarModel> InmatningsLista;
        public List<KundModel> KundLista;
        public List<String> statuslista;
        public List<String> styckPrislista;

        public InmatningarModel()
        {
            styckPrislista = new List<string>();
            styckPrislista.Add("Ja");
            styckPrislista.Add("Nej");
            InmatningsLista = new List<InmatningarModel>();
            KundLista = new List<KundModel>();
            statuslista = new List<string>();
            statuslista.Add("Pågående");
            statuslista.Add("Ej genomförbar");
            statuslista.Add("Slutförd");
            statuslista.Add("Ej slutförd");
        }
 
    
    }

  
}