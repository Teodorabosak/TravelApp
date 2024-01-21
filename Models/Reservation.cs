using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelApp.Models
{
    public class Reservation
    {
        [Required]
        public int ReservationID { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientID { get; set; }

        [ForeignKey("Destination")]
        [Required]
        public int DestinationID { get; set; }

        // Navigaciona svojstva - opciono, ali korisno za pristup podacima povezanih entiteta
        public virtual Clients Client { get; set; }

        public virtual Destination Destination { get; set; }
    }
}