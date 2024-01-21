using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelApp.Models
{
    public class Clients
    {
        [Required]
        public int ClientID { get; set; }
        
        [Display(Name = "Ime")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Prezime")]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "E-mail")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        public string Username { get; set; }


        public string Password { get; set; }


        public Clients()
        {
            ClientID = -1;
            FirstName = "";
            LastName = "";
            Email = "";
            Phone = "";
        }

        public Clients(int id, string username, string password, string fname, string lname, string email, string phone)
        {
            ClientID = id;
            FirstName = fname;
            LastName = lname;
            Email = email;
            Phone = phone;
            Username = username;
            Password = password;
        }
    }
}