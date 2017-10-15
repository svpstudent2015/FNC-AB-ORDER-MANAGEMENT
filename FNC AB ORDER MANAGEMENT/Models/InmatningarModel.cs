using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FNC_AB_ORDER_MANAGEMENT.Models
{
    public class InmatningarModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} måste vara minst {2} tecken långt .", MinimumLength = 1)]
        public string Ordernr { get; set; }

        [DisplayName("Kund")]
        
        public string KundNamn { get; set; }

        [StringLength(15, ErrorMessage = "{0} kan inte var längre än 15 tecken")]
        public string Telefonnr { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} kan inte var längre än 50 tecken")]
        public string Ort { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} kan inte var längre än 50 tecken")]
        public string Adress { get; set; }

        [DisplayName("Start Datum")]
        [DataType(DataType.Date)]
        [Required]
        public Nullable<System.DateTime> InDatum { get; set; }

        [DisplayName("Slut Datum")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> UtDatum { get; set; }

        [DisplayName("Etablerad kostnad")]
        public Nullable<bool> StyckPris { get; set; }

        [DisplayName("Längd (m)")]
        [RegularExpression(@"^[0-9]\d*(\,\d+)?$", ErrorMessage = "Du måste använda siffror")]
        public Nullable<decimal> Langd { get; set; }

        [RegularExpression(@"^[0-9]\d*(\.\d+)?$", ErrorMessage = "Du måste använda siffror")]
        public Nullable<decimal> Timmar { get; set; }

        public Nullable<bool> Fakturerad { get; set; }

        [DisplayName("Övrigt")]
        [StringLength(200, ErrorMessage = "{0} kan inte var längre än 200 tecken")]
        public string Ovrigt { get; set; }

        public string Status { get; set; }

        public string Color { get; set; }
        public string AID { get; set; }

        [DisplayName("Användare")]
        public string Anvandrare { get; set; }

        public int ID { get; set; }

        public Nullable<bool> Exporterad { get; set; }

        public Nullable<int> KundID { get; set; }

        public List<InmatningarModel> InmatningsLista;
        public List<KundModel> KundLista;
        public List<String> statuslista;
        public List<String> styckPrislista;
        public List<String> fakturaLista;
        public List<String> ExporteradLista;

        public InmatningarModel()
        {
            InmatningsLista = new List<InmatningarModel>();
            KundLista = new List<KundModel>();


            // StyckprisLista
            styckPrislista = new List<string>();
            styckPrislista.Add("Ja");
            styckPrislista.Add("Nej");          

            // StatusLista
            statuslista = new List<string>();
            statuslista.Add("Pågående");
            statuslista.Add("Ej genomförbar");
            statuslista.Add("Slutförd");
            statuslista.Add("Ej slutförd");

            // FakturaLista
            fakturaLista = new List<string>();
            fakturaLista.Add("Ja");
            fakturaLista.Add("Nej");

            // ExporteradLista
            ExporteradLista = new List<string>();
            ExporteradLista.Add("Ja");
            ExporteradLista.Add("Nej");
        }
 
    
    }

  
}