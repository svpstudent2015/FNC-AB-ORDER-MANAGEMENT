using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FNC_AB_ORDER_MANAGEMENT.Models
{
    public class KundModel
    {
        public int ID { get; set; }
        public string Namn { get; set; }
        public string Email { get; set; }
        public string Telefonnr { get; set; }
        public string Status { get; set; }

        public List<KundModel> KundLista;

        public KundModel()
        {
            
            KundLista = new List<KundModel>();
        }
    }
}