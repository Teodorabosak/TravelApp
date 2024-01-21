using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;

namespace TravelApp.Models
{
    public class Destination 
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Naziv")]
        [Required]
        public string Name { get; set; }
       
        [Display(Name = "Opis putovanja")]
        [Required] 
        public string Description { get; set; }

       
        [Display(Name = "Polazak")]
        public DateTime DateGo { get; set; }

       
        [Display(Name = "Povratak")]
        public DateTime DateBack { get; set; }

        [Display (Name = "Cena aranžmana u € ")]
        public int Price { get; set; }

        public Destination()
        {
            Id = -1;
            Name = "";
            Description = "";
            DateGo = DateTime.Now;
            DateBack = DateTime.Now;
            Price = 000;
        }

        public Destination(int id, string name, string description, DateTime datego, DateTime dateback, int price)
        {
            Id = id;
            Name = name;
            Description = description;
            DateGo = datego;
            DateBack = dateback;
            Price = price;
        }

    }
}