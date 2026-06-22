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

        public void AjouterArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            Articles.Add(article);
        }

        public bool SupprimerArticle(string nom)
        {
            for (int i = 0; i < Articles.Count; i++)
            {
                if (Articles[i].Nom == nom)
                {
                    Articles.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public int NombreArticles()
        {
            return Articles.Count;
        }

        public Article ArticleLePlusCher()
        {
            Article plusCher = Articles[0];

            foreach (var article in Articles)
            {
                if (article.Prix < plusCher.Prix)
                {
                    plusCher = article;
                }
            }

            return plusCher;
        }

        public decimal PrixMoyen()
        {
            decimal total = 0;

            foreach (var article in Articles)
            {
                total += article.Prix;
            }

            return total / Articles.Count;
        }

        public decimal CalculerTotalAvecTVA(decimal tauxTVA)
        {
            decimal sousTotal = 0;

            foreach (var article in Articles)
            {
                sousTotal += article.Prix;
            }

            decimal tva = sousTotal * tauxTVA / 100;

            return CalculerTotal() + tva;
        }

        public decimal AppliquerCodePromo(string code)
        {
            decimal total = CalculerTotal();

            switch (code)
            {
                case "PROMO10":
                    total -= total * 0.10m;
                    break;
                case "PROMO20":
                    total -= total * 0.20m;
                    break;
            }

            return total;
        }

        public bool EstEligibleLivraisonGratuite()
        {
            return CalculerTotal() > 50m;
        }
    }
}
