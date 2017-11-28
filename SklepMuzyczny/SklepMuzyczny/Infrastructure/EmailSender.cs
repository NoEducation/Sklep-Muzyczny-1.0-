using SklepMuzyczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SklepMuzyczny.Infrastructure
{
    public class EmailSender
    {
        private MailMessage mail;
        private SmtpClient smptClient;
        private const string EmailAddressSender = "mistrzowymistrz@gmail.com";
        private const string PasswordSender = "heros888";

        public EmailSender()
        {
            mail = new MailMessage();
            smptClient = new SmtpClient("smtp.gmail.com", 587);
        }
        public void SendEmailToCustomer(Order order)
        {
            mail.To.Clear();
            mail.To.Add(order.Email);
            mail.From = new MailAddress(EmailAddressSender);
            mail.Subject = "Potwierdzenie zamówienia";
            mail.Body = SetMessageBodyCustomer(order);
            smptClient.Port = 587;
            smptClient.Credentials = new System.Net.NetworkCredential(EmailAddressSender, PasswordSender);
            smptClient.EnableSsl = true;
            smptClient.Send(mail);

        }
        public void SendEmailToAdmin(Order order)
        {
            mail.To.Clear();
            mail.To.Add(EmailAddressSender);
            mail.From = new MailAddress(EmailAddressSender);
            mail.Subject = "Nowe zamówienie";
            mail.Body = SetMessageBodyAdmin(order);
            smptClient.Port = 587;
            smptClient.Credentials = new System.Net.NetworkCredential(EmailAddressSender, PasswordSender);
            smptClient.EnableSsl = true;
            smptClient.Send(mail);
        }
        private string SetMessageBodyCustomer(Order order)
        {
            string result = "Dziekujemy za złozenie" +
                " zamówienia w naszym sklepie " + order.Name + "\n" +
                "Numer zamówenia: "+ order.OrderId + " z dnia " + order.DateCreated + "\n" +
                "Zamówione piosenki: \n";
            
            foreach(var orderLine in order.SongOrdered)
            {
                result += "Nazwa: " + orderLine.Song.NameSong + "\n" +
                    "Ilość: " + orderLine.Quantity + "\n" +
                    "Koszt: " + orderLine.PricePurchase + "\n\n";
            }

            result += "Do zapłaty: " + order.SongOrdered.Sum(x => x.PricePurchase) + " zł \n" +
                "Wpłate pieniedzy należy dokonać na rachunek <Tutaj bedzie nr rachunku> \n" +
                "Z wyrażami szacunku administracja strony Fajna nuta";

            return result;
        }
        private string SetMessageBodyAdmin(Order order)
        {
            string result = "Nowe zamówienie nr:" + order.OrderId + "\n" +
                  " z dnia " + order.DateCreated + "\n" +
                 "Piosenki zamówione: \n";

            foreach (var orderLine in order.SongOrdered)
            {
                result += "Nazwa: " + orderLine.Song.NameSong + "\n" +
                    "Ilość: " + orderLine.Quantity + "\n" +
                    "Koszt: " + orderLine.PricePurchase + "\n\n";
            }
            result += "Szczegóły dotyczące zamówienia:\n" +
                "Imię: " + order.Name + "\n" +
                "Nazwisko: " + order.Surname + "\n" +
                "E-mail: " + order.Email + "\n" +
                "Telefon: " + order.Phone + "\n" +
                "Kraj: " + order.Country + "\n" +
                "Województwo: " + order.State + "\n" +
                "Miasto: " + order.City + "\n" +
                "Adres: " + order.AdressLine + "\n" +
                "Kod-pocztowy: " + order.Zip + "\n" +
                "Dodatkowe informacje: " + order.Comment != null ? order.Comment : "brak" + "\n\n" +
                "Zamówienie musi zostać potwierdzone przez wysłanie pieniedzy na konto";

            return result;
        }
    }
}