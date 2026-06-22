using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using testingApp;

namespace testingAppTests
{
    public class PanierTests
    {
        [Theory]
        [MemberData(nameof(PanierCalculerTotal_TestData))]
        public void PanierCalculerTotal_SelonStatus_RetournePrixCorrect(Panier p, Decimal expected)
        {
            decimal total = p.CalculerTotal();

            Assert.Equal(expected, total);
        }

        public static IEnumerable<object[]> PanierCalculerTotal_TestData()
        {
            yield return new object[]
            {
                new Panier
                {
                    Client = new Client("Georges", TypeClient.Standard),
                    Articles = new List<Article>{new Article("Banane", 2.50m), new Article("Pomme", 5), new Article("Raisins", 1.2m)}
                },
                8.7m
            };
            yield return new object[]
            {
                new Panier
                {
                    Client = new Client("Georgette", TypeClient.Premium),
                    Articles = new List<Article>{new Article("Banane", 2.50m), new Article("Pomme", 5), new Article("Raisins", 6m)}
                },
                12.15m
            };
            yield return new object[]
            {
                new Panier
                {
                    Client = new Client("Georgette", TypeClient.VIP),
                    Articles = new List<Article>{new Article("Banane", 12.5m), new Article("Pomme", 5), new Article("Raisins", 6m)}
                },
                18.8m
            };
        }
    }
}
