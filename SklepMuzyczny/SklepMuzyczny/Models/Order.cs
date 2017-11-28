using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.Models
{
    public class Order
    {   

        public int OrderId { get; set; }

        [StringLength(50,ErrorMessage = "Zbyt długię imię")]
        [Required(ErrorMessage = "Podaj swoje imię")]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [StringLength(50,ErrorMessage ="Za długie nazwisko")]
        [Required(ErrorMessage = "Podaj swoje nazwisko")]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Podaj swoju kraj")]
        [Display(Name = "Kraj")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Podaj kod-pocztowy")]
        [Display(Name = "Kod-pocztowy")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Podaj adres")]
        [Display(Name = "Adres")]
        public string AdressLine { get; set; }

        [Display(Name = "Województwo")]
        [Required(ErrorMessage = "Podaj województwo")]
        public string State { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Podaj Miasto")]
        public string City { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Telefon")]
        public string Phone{ get; set; }
       
        [EmailAddress(ErrorMessage ="Podaj prawidłowy email")]
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Podaj email")]
        public string Email{ get; set; }

        [Display(Name="Komentarz")]
        public string Comment { get; set; }

        public DateTime DateCreated { get; set; }
        public virtual List<OrderPosition> SongOrdered { get; set; }
    }
}