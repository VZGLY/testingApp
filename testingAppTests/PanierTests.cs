using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using testingApp;

namespace testingAppTests
{
    public class PanierTests
    {
        // Helper pour construire rapidement un panier
        private static Panier CreerPanier(TypeClient status, params Article[] articles)
        {
            return new Panier
            {
                Client = new Client("Test", status),
                Articles = new List<Article>(articles)
            };
        }

        // ---------------------------------------------------------------
        // CalculerTotal
        // ---------------------------------------------------------------
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

        [Fact]
        public void CalculerTotal_PanierVide_RetourneZero()
        {
            var panier = CreerPanier(TypeClient.Standard);

            Assert.Equal(0m, panier.CalculerTotal());
        }

        // ---------------------------------------------------------------
        // AjouterArticle
        // ---------------------------------------------------------------
        [Fact]
        public void AjouterArticle_ArticleValide_AjouteAuPanier()
        {
            var panier = CreerPanier(TypeClient.Standard);
            var article = new Article("Banane", 2.50m);

            panier.AjouterArticle(article);

            Assert.Single(panier.Articles);
            Assert.Same(article, panier.Articles[0]);
        }

        [Fact]
        public void AjouterArticle_PlusieursArticles_ConserveOrdreEtNombre()
        {
            var panier = CreerPanier(TypeClient.Standard);

            panier.AjouterArticle(new Article("Banane", 2.50m));
            panier.AjouterArticle(new Article("Pomme", 5m));

            Assert.Equal(2, panier.Articles.Count);
            Assert.Equal("Banane", panier.Articles[0].Nom);
            Assert.Equal("Pomme", panier.Articles[1].Nom);
        }

        [Fact]
        public void AjouterArticle_Null_LeveArgumentNullException()
        {
            var panier = CreerPanier(TypeClient.Standard);

            Assert.Throws<ArgumentNullException>(() => panier.AjouterArticle(null));
        }

        // ---------------------------------------------------------------
        // SupprimerArticle
        // ---------------------------------------------------------------
        [Fact]
        public void SupprimerArticle_ArticleExistant_RetourneTrueEtSupprime()
        {
            var panier = CreerPanier(TypeClient.Standard,
                new Article("Banane", 2.50m),
                new Article("Pomme", 5m));

            bool resultat = panier.SupprimerArticle("Banane");

            Assert.True(resultat);
            Assert.Single(panier.Articles);
            Assert.DoesNotContain(panier.Articles, a => a.Nom == "Banane");
        }

        [Fact]
        public void SupprimerArticle_ArticleInexistant_RetourneFalse()
        {
            var panier = CreerPanier(TypeClient.Standard,
                new Article("Banane", 2.50m));

            bool resultat = panier.SupprimerArticle("Kiwi");

            Assert.False(resultat);
            Assert.Single(panier.Articles);
        }

        [Fact]
        public void SupprimerArticle_PanierVide_RetourneFalse()
        {
            var panier = CreerPanier(TypeClient.Standard);

            Assert.False(panier.SupprimerArticle("Banane"));
        }

        [Fact]
        public void SupprimerArticle_NomsEnDouble_SupprimeUnSeulOccurrence()
        {
            var panier = CreerPanier(TypeClient.Standard,
                new Article("Banane", 2.50m),
                new Article("Banane", 3m));

            bool resultat = panier.SupprimerArticle("Banane");

            Assert.True(resultat);
            Assert.Single(panier.Articles);
            // La premiere occurrence est supprimee, la seconde (prix 3) reste
            Assert.Equal(3m, panier.Articles[0].Prix);
        }

        // ---------------------------------------------------------------
        // NombreArticles
        // ---------------------------------------------------------------
        [Fact]
        public void NombreArticles_PanierVide_RetourneZero()
        {
            var panier = CreerPanier(TypeClient.Standard);

            Assert.Equal(0, panier.NombreArticles());
        }

        [Fact]
        public void NombreArticles_AvecArticles_RetourneNombreCorrect()
        {
            var panier = CreerPanier(TypeClient.Standard,
                new Article("Banane", 2.50m),
                new Article("Pomme", 5m),
                new Article("Raisins", 1.2m));

            Assert.Equal(3, panier.NombreArticles());
        }

        // ---------------------------------------------------------------
        // ArticleLePlusCher
        // ---------------------------------------------------------------
        [Fact]
        public void ArticleLePlusCher_RetourneArticleAvecPrixMaximum()
        {
            var plusCher = new Article("Pomme", 5m);
            var panier = CreerPanier(TypeClient.Standard,
                new Article("Banane", 2.50m),
                plusCher,
                new Article("Raisins", 1.2m));

            var resultat = panier.ArticleLePlusCher();

            Assert.Same(plusCher, resultat);
        }

        [Fact]
        public void ArticleLePlusCher_UnSeulArticle_RetourneCetArticle()
        {
            var article = new Article("Banane", 2.50m);
            var panier = CreerPanier(TypeClient.Standard, article);

            Assert.Same(article, panier.ArticleLePlusCher());
        }

        [Fact]
        public void ArticleLePlusCher_PanierVide_ReturnNull()
        {
            var panier = CreerPanier(TypeClient.Standard);

            Assert.Null(panier.ArticleLePlusCher());
        }

        // ---------------------------------------------------------------
        // PrixMoyen
        // ---------------------------------------------------------------
        [Fact]
        public void PrixMoyen_RetourneMoyenneCorrecte()
        {
            var panier = CreerPanier(TypeClient.Standard,
                new Article("Banane", 2m),
                new Article("Pomme", 4m),
                new Article("Raisins", 6m));

            Assert.Equal(4m, panier.PrixMoyen());
        }

        [Fact]
        public void PrixMoyen_UnSeulArticle_RetourneSonPrix()
        {
            var panier = CreerPanier(TypeClient.Standard, new Article("Banane", 3.5m));

            Assert.Equal(3.5m, panier.PrixMoyen());
        }

        [Fact]
        public void PrixMoyen_PanierVide_RetourneZero()
        {
            var panier = CreerPanier(TypeClient.Standard);

            Assert.Equal(0,panier.PrixMoyen());
        }

        // ---------------------------------------------------------------
        // CalculerTotalAvecTVA
        // ---------------------------------------------------------------
        [Fact]
        public void CalculerTotalAvecTVA_ClientStandard_AjouteLaTVA()
        {
            // Total HT = 100, TVA 20% => 120
            var panier = CreerPanier(TypeClient.Standard,
                new Article("Article", 100m));

            decimal total = panier.CalculerTotalAvecTVA(20m);

            Assert.Equal(120m, total);
        }

        [Fact]
        public void CalculerTotalAvecTVA_TauxZero_RetourneTotalSansTVA()
        {
            var panier = CreerPanier(TypeClient.Standard,
                new Article("Article", 50m));

            Assert.Equal(50m, panier.CalculerTotalAvecTVA(0m));
        }

        [Fact]
        public void CalculerTotalAvecTVA_ClientVIP_TVACalculeeSurPrixHTSansReduction()
        {
            // Sous-total = 100. Reduction VIP 20% => CalculerTotal = 80.
            // TVA 20% calculee sur le sous-total brut (100) => 20.
            // Total attendu = 80 + 20 = 100.
            var panier = CreerPanier(TypeClient.VIP,
                new Article("Article", 100m));

            decimal total = panier.CalculerTotalAvecTVA(20m);

            Assert.Equal(100m, total);
        }

        // ---------------------------------------------------------------
        // AppliquerCodePromo
        // ---------------------------------------------------------------
        [Theory]
        [InlineData("PROMO10", 90)]
        [InlineData("PROMO20", 80)]
        [InlineData("INCONNU", 100)]
        [InlineData("", 100)]
        [InlineData(null, 100)]
        public void AppliquerCodePromo_AppliqueLaReductionAttendue(string code, decimal expected)
        {
            // Client Standard, total = 100
            var panier = CreerPanier(TypeClient.Standard,
                new Article("Article", 100m));

            decimal total = panier.AppliquerCodePromo(code);

            Assert.Equal(expected, total);
        }

        [Fact]
        public void AppliquerCodePromo_SeCumuleAvecReductionStatut()
        {
            // Client Premium : total = 100 * 0.9 = 90, puis PROMO10 => 90 * 0.9 = 81
            var panier = CreerPanier(TypeClient.Premium,
                new Article("Article", 100m));

            Assert.Equal(81m, panier.AppliquerCodePromo("PROMO10"));
        }

        // ---------------------------------------------------------------
        // EstEligibleLivraisonGratuite
        // ---------------------------------------------------------------
        [Fact]
        public void EstEligibleLivraisonGratuite_TotalSuperieurA50_RetourneTrue()
        {
            var panier = CreerPanier(TypeClient.Standard, new Article("Article", 60m));

            Assert.True(panier.EstEligibleLivraisonGratuite());
        }

        [Fact]
        public void EstEligibleLivraisonGratuite_TotalInferieurA50_RetourneFalse()
        {
            var panier = CreerPanier(TypeClient.Standard, new Article("Article", 40m));

            Assert.False(panier.EstEligibleLivraisonGratuite());
        }

        [Fact]
        public void EstEligibleLivraisonGratuite_TotalEgalA50_RetourneFalse()
        {
            // La condition est stricte (> 50), donc 50 n'est pas eligible
            var panier = CreerPanier(TypeClient.Standard, new Article("Article", 50m));

            Assert.False(panier.EstEligibleLivraisonGratuite());
        }
    }
}
