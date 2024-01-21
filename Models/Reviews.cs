using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelApp.Models
{
    public class Reviews
    {
        public int ReviewID { get; set; }

        [ForeignKey("Clients")]
        public int ClientID { get; set; }

        [ForeignKey("Destination")]
        public int DestinationID { get; set; }

        public string Rating { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        public virtual Destination Destination { get; set; }

        public virtual Clients Clients { get; set; }
    }
}