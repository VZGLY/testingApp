using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace testingApp
{
    public enum TypeClient
    {
        Standard,
        Premium,
        VIP,
        GuideSupreme
    }

    public class Article
    {
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        public Article(string nom, decimal prix)
        {
            Nom = nom;
            Prix = prix;
        }
    }

    public class Client
    {
        public string Nom { get; set; }
        public TypeClient Status { get; set; }
        public Client(string nom, TypeClient status)
        {
            Nom = nom;
            Status = status;
        }
    }

    public class Panier
    {
        public Client Client { get; set; }

        public List<Article> Articles { get; set; } = new List<Article>();

        public decimal CalculerTotal()
        {
            decimal total = 0;

            foreach (var article in Articles)
            { 
                total += article.Prix;
            }

            switch (Client.Status)
            {
                case TypeClient.Premium:
                    total *= 0.9m; // 10% de réduction
                    break;
                case TypeClient.VIP:
                    total *= 0.8m; // 20% de réduction
                    break;
            }

            return total;
        }
    }
}
