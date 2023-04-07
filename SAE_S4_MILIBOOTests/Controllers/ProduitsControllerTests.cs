using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE_S4_MILIBOO.Controllers;
using SAE_S4_MILIBOO.Models.DataManager;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SAE_S4_MILIBOO.Controllers.Tests
{
    [TestClass()]
    public class ProduitsControllerTests
    {
        private readonly MilibooDBContext _context;
        private readonly ProduitsController _controller;
        private IDataRepositoryProduits<Produit> _dataRepository;

        public ProduitsControllerTests()
        {

            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql("Server=localhost;port=5432;Database=MilibooDB; uid=postgres;password=postgres;"); // Chaine de connexion à mettre dans les ( )
            _context = new MilibooDBContext(builder.Options);
            _dataRepository = new ProduitManager(_context);
            _controller = new ProduitsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetProduitsTest()
        {
            ActionResult<IEnumerable<Produit>> users = await _controller.GetProduits();
            CollectionAssert.AreEqual(_context.Produits.ToList(), users.Value.ToList(), "La liste renvoyée n'est pas la bonne.");
        }

        [TestMethod]
        public void GetProduitById_ReturnsExistingProduct_Moq()
        {
            // Arrange
            Produit prd = new Produit
            {
                IdProduit = 1,
                CategorieId = 15,
                CollectionId = null,
                Libelle = "Canapé 3 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = null,
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(null, (decimal)65.4, null),
                DimDossier = new TDimensions(null, null, null),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false
            };

            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetProduitById(1).Result).Returns(prd);
            var produitController = new ProduitsController(mockRepository.Object);

            // Act
            var actionResult = produitController.GetProduit(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(prd, actionResult.Value as Produit);
        }

        [TestMethod]
        public void GetProduitById_ReturnsNull_Moq()
        {
            // Arrange
            Produit prd = new Produit
            {
                IdProduit = 1,
                CategorieId = 15,
                CollectionId = null,
                Libelle = "Canapé 3 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = null,
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(null, (decimal)65.4, null),
                DimDossier = new TDimensions(null, null, null),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false
            };

            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetProduitById(1).Result).Returns(prd);

            var produitController = new ProduitsController(mockRepository.Object);

            // Act
            var actionResult = produitController.GetProduit(2).Result;

            // Assert
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetProduitByIdCouleur_ReturnsExistingProduct_Moq()
        {
            Produit prd = new Produit
            {
                IdProduit = 1,
                CategorieId = 15,
                CollectionId = null,
                Libelle = "Canapé 3 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = null,
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(null, (decimal)65.4, null),
                DimDossier = new TDimensions(null, null, null),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false,
                VariantesProduitNavigation = null
            };

            Variante vrt_with_goodColor = new Variante
            {
                IdVariante = 1,
                IdProduit = 1,
                IdCouleur = 1,
                DateCreation = DateTime.Now,
                Stock = 5,
                Prix = 0,
                Promo = 1,
                ProduitVarianteNavigation = prd
            };

            //prd.VariantesProduitNavigation = new List<Variante>() { vrt_with_goodColor };

            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetAllByPageByCouleur(1, 1, new List<int> { 1 }).Result).Returns(new List<Produit> { prd });

            var mockVarianteRepository = new Mock<IDataRepositoryVariante<Variante>>();
            mockVarianteRepository.Setup(x => x.GetAll().Result)
                .Returns(new List<Variante> { vrt_with_goodColor });

            var produitController = new ProduitsController(mockRepository.Object);
            var varianteController = new VariantesController(mockVarianteRepository.Object);

            // Act
            var actionResult = produitController.GetByAllByPageAndCouleur(1, 1, new[] { 1 }).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Produit> { prd }, (List<Produit>)actionResult.Value as List<Produit>);
        }

        [TestMethod]
        public void GetProduitByIdCouleur_ReturnsNotFound_Moq()
        {
            Produit prd = new Produit
            {
                IdProduit = 1,
                CategorieId = 15,
                CollectionId = null,
                Libelle = "Canapé 3 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = null,
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(null, (decimal)65.4, null),
                DimDossier = new TDimensions(null, null, null),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false,
                VariantesProduitNavigation = null
            };

            Variante vrt_with_goodColor = new Variante
            {
                IdVariante = 1,
                IdProduit = 1,
                IdCouleur = 1,
                DateCreation = DateTime.Now,
                Stock = 5,
                Prix = 0,
                Promo = 1,
                ProduitVarianteNavigation = prd
            };

            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetAllByPageByCouleur(1, 1, new List<int> { 1 }).Result).Returns(new List<Produit> { prd });

            var mockVarianteRepository = new Mock<IDataRepositoryVariante<Variante>>();
            mockVarianteRepository.Setup(x => x.GetAll().Result).Returns(new List<Variante> { vrt_with_goodColor });

            var produitController = new ProduitsController(mockRepository.Object);
            var varianteController = new VariantesController(mockVarianteRepository.Object);

            // Act
            var actionResult = produitController.GetByAllByPageAndCouleur(1, 1, new[] { 2 }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetProduitByPrixMin_ReturnsExistingProduct_Moq()
        {
            Produit prd = new Produit
            {
                IdProduit = 1,
                CategorieId = 15,
                CollectionId = null,
                Libelle = "Canapé 3 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = null,
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(null, (decimal)65.4, null),
                DimDossier = new TDimensions(null, null, null),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false,
                VariantesProduitNavigation = null
            };

            // IF THESES ARE TWO VARIANTES OF SAME PRODUCT WITH ONLY ONE HAVING A PRICE > 750, THIS SHOULD STILL RETURN THE PRODUCT !

            Variante vrt_with_prix_min_800 = new Variante
            {
                IdVariante = 1,
                IdProduit = 1,
                IdCouleur = 1,
                DateCreation = DateTime.Now,
                Stock = 5,
                Prix = 800,
                Promo = 1,
                ProduitVarianteNavigation = prd
            };

            Variante vrt_with_prix_min_700 = new Variante
            {
                IdVariante = 1,
                IdProduit = 1,
                IdCouleur = 1,
                DateCreation = DateTime.Now,
                Stock = 5,
                Prix = 700,
                Promo = 1,
                ProduitVarianteNavigation = prd
            };

            //prd.VariantesProduitNavigation = new List<Variante>() { vrt_with_goodColor };

            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetAllByPageByPrixMini(1, 1, 750).Result).Returns(new List<Produit> { prd });

            var mockVarianteRepository = new Mock<IDataRepositoryVariante<Variante>>();
            mockVarianteRepository.Setup(x => x.GetAll().Result)
                .Returns(new List<Variante> { vrt_with_prix_min_700, vrt_with_prix_min_800 });

            var produitController = new ProduitsController(mockRepository.Object);
            var varianteController = new VariantesController(mockVarianteRepository.Object);

            // Act
            var actionResult = produitController.GetProduitsByPageAndPrixMini(1, 1, 750).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Produit> { prd }, (List<Produit>)actionResult.Value as List<Produit>);
        }

        [TestMethod]
        public void GetProduitByPrixMin_ReturnsNotFound_Moq()
        {
            Produit prd = new Produit
            {
                IdProduit = 1,
                CategorieId = 15,
                CollectionId = null,
                Libelle = "Canapé 3 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = null,
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(null, (decimal)65.4, null),
                DimDossier = new TDimensions(null, null, null),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false,
                VariantesProduitNavigation = null
            };

            Variante vrt_with_prix_700 = new Variante
            {
                IdVariante = 1,
                IdProduit = 1,
                IdCouleur = 1,
                DateCreation = DateTime.Now,
                Stock = 5,
                Prix = 700,
                Promo = 1,
                ProduitVarianteNavigation = prd
            };

            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetAllByPageByPrixMini(1, 1, 750).Result).Returns(new List<Produit>());

            var mockVarianteRepository = new Mock<IDataRepositoryVariante<Variante>>();
            mockVarianteRepository.Setup(x => x.GetAll().Result).Returns(new List<Variante> { vrt_with_prix_700 });

            var produitController = new ProduitsController(mockRepository.Object);
            var varianteController = new VariantesController(mockVarianteRepository.Object);

            // Act
            var actionResult = produitController.GetProduitsByPageAndPrixMini(1, 1, 750).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetProduitByPrixMax_ReturnsExistingProduct_Moq()
        {
            Produit prd = new Produit
            {
                IdProduit = 1,
                CategorieId = 15,
                CollectionId = null,
                Libelle = "Canapé 3 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = null,
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(null, (decimal)65.4, null),
                DimDossier = new TDimensions(null, null, null),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false,
                VariantesProduitNavigation = null
            };

            // IF THESES ARE TWO VARIANTES OF SAME PRODUCT WITH ONLY ONE HAVING A PRICE < 750, THIS SHOULD STILL RETURN THE PRODUCT !

            Variante vrt_with_prix_max_700 = new Variante
            {
                IdVariante = 1,
                IdProduit = 1,
                IdCouleur = 1,
                DateCreation = DateTime.Now,
                Stock = 5,
                Prix = 700,
                Promo = 1,
                ProduitVarianteNavigation = prd
            };

            Variante vrt_with_prix_max_800 = new Variante
            {
                IdVariante = 1,
                IdProduit = 1,
                IdCouleur = 1,
                DateCreation = DateTime.Now,
                Stock = 5,
                Prix = 800,
                Promo = 1,
                ProduitVarianteNavigation = prd
            };

            //prd.VariantesProduitNavigation = new List<Variante>() { vrt_with_goodColor };

            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetAllByPageByPrixMaxi(1, 1, 750).Result).Returns(new List<Produit> { prd });

            var mockVarianteRepository = new Mock<IDataRepositoryVariante<Variante>>();
            mockVarianteRepository.Setup(x => x.GetAll().Result)
                .Returns(new List<Variante> { vrt_with_prix_max_700, vrt_with_prix_max_800 });

            var produitController = new ProduitsController(mockRepository.Object);
            var varianteController = new VariantesController(mockVarianteRepository.Object);

            // Act
            var actionResult = produitController.GetProduitsByPageAndPrixMaxi(1, 1, 750).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Produit> { prd }, (List<Produit>)actionResult.Value as List<Produit>);
        }


        [TestMethod]
        public void GetProduitByPrixMax_ReturnsNotFound_Moq()
        {
            Produit prd = new Produit
            {
                IdProduit = 1,
                CategorieId = 15,
                CollectionId = null,
                Libelle = "Canapé 3 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = null,
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(null, (decimal)65.4, null),
                DimDossier = new TDimensions(null, null, null),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false,
                VariantesProduitNavigation = null
            };

            Variante vrt_with_prix_800 = new Variante
            {
                IdVariante = 1,
                IdProduit = 1,
                IdCouleur = 1,
                DateCreation = DateTime.Now,
                Stock = 5,
                Prix = 800,
                Promo = 1,
                ProduitVarianteNavigation = prd
            };

            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetAllByPageByPrixMaxi(1, 1, 750).Result).Returns(new List<Produit>());

            var mockVarianteRepository = new Mock<IDataRepositoryVariante<Variante>>();
            mockVarianteRepository.Setup(x => x.GetAll().Result).Returns(new List<Variante> { vrt_with_prix_800 });

            var produitController = new ProduitsController(mockRepository.Object);
            var varianteController = new VariantesController(mockVarianteRepository.Object);

            // Act
            var actionResult = produitController.GetProduitsByPageAndPrixMaxi(1, 1, 750).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetProduitByCollection_ReturnsExistingProduct_Moq()
        {
            Produit prd1_col1 = new Produit
            {
                IdProduit = 1,
                CategorieId = 15,
                CollectionId = 1,
                Libelle = "Canapé 3 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = null,
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(null, (decimal)65.4, null),
                DimDossier = new TDimensions(null, null, null),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false,
                VariantesProduitNavigation = null
            };

            Produit prd2_col1 = new Produit
            {
                IdProduit = 2,
                CategorieId = 15,
                CollectionId = 1,
                Libelle = "Canapé 4 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = "Lin",
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(100, (decimal)65.4, null),
                DimDossier = new TDimensions(186, 50, 30),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false,
                VariantesProduitNavigation = null
            };

            Collection collection = new Collection
            {
                CollectionId = 1,
                CollectionLibelle = "Collection 1",
                CollectionPrix = 1999,
                CollectionPromo = 1,
                ProduitsCollectionNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetAllByPageByCollection(1, 1).Result).Returns(new List<Produit> { prd1_col1, prd2_col1 });

            var mockCollectionRepository = new Mock<IDataRepositoryCollection<Collection>>();
            mockCollectionRepository.Setup(x => x.GetAll().Result)
                .Returns(new List<Collection> { collection });

            var produitController = new ProduitsController(mockRepository.Object);
            var collectionController = new CollectionController(mockCollectionRepository.Object);

            // Act
            var actionResult = produitController.GetByAllByPageAndCollection(1, 1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Produit> { prd1_col1, prd2_col1 }, (List<Produit>)actionResult.Value as List<Produit>);
        }

        [TestMethod]
        public void GetProduitByCollection_ReturnNotFound_Moq()
        {
            Produit prd1_col1 = new Produit
            {
                IdProduit = 1,
                CategorieId = 15,
                CollectionId = 1,
                Libelle = "Canapé 3 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = null,
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(null, (decimal)65.4, null),
                DimDossier = new TDimensions(null, null, null),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false,
                VariantesProduitNavigation = null
            };

            Produit prd2_col1 = new Produit
            {
                IdProduit = 2,
                CategorieId = 15,
                CollectionId = 1,
                Libelle = "Canapé 4 places éco-responsable en tissu recyclé naturel FOREST",
                Description = null,
                InscructionsEntretien = null,
                HauteurPieds = 186,
                Revetement = null,
                Matiere = "Lin",
                MatierePieds = null,
                TypeMousseAssise = null,
                TypeMousseDossier = null,
                DensiteAssise = null,
                PoidsColis = null,
                DimTotale = new TDimensions(186, 105, 65),
                DimAssise = new TDimensions(100, (decimal)65.4, null),
                DimDossier = new TDimensions(186, 50, 30),
                DimColis = new TDimensions(null, null, null),
                DimDeplie = new TDimensions(null, null, null),
                DimAccoudoir = new TDimensions(null, null, null),
                MadeInFrance = false,
                VariantesProduitNavigation = null
            };

            Collection collection = new Collection
            {
                CollectionId = 1,
                CollectionLibelle = "Collection 1",
                CollectionPrix = 1999,
                CollectionPromo = 1,
                ProduitsCollectionNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetAllByPageByCollection(1, 1).Result).Returns(new List<Produit> { prd1_col1, prd2_col1 });

            var mockCollectionRepository = new Mock<IDataRepositoryCollection<Collection>>();
            mockCollectionRepository.Setup(x => x.GetAll().Result)
                .Returns(new List<Collection> { collection });

            var produitController = new ProduitsController(mockRepository.Object);
            var collectionController = new CollectionController(mockCollectionRepository.Object);

            // Act
            var actionResult = produitController.GetByAllByPageAndCollection(1, 2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetNumberPage_ProduitByIdCouleur_Moq()
        {
            // INSERTION DE 18 PRODUITS, 5 PAGES PAR PRODUITS, DEVRAIT RETOURNER CEILING(18 / 5) = 4

            List<Produit> mesProduits = new List<Produit>();
            for (int i = 1; i <= 18; i++)
            {
                mesProduits.Add(
                    new Produit
                    {
                        IdProduit = i,
                        CategorieId = 15,
                        CollectionId = null,
                        Libelle = "Canapé 3 places éco-responsable en tissu recyclé naturel FOREST" + i.ToString(),
                        Description = null,
                        InscructionsEntretien = null,
                        HauteurPieds = 186,
                        Revetement = null,
                        Matiere = null,
                        MatierePieds = null,
                        TypeMousseAssise = null,
                        TypeMousseDossier = null,
                        DensiteAssise = null,
                        PoidsColis = null,
                        DimTotale = new TDimensions(186, 105, 65),
                        DimAssise = new TDimensions(null, (decimal)65.4, null),
                        DimDossier = new TDimensions(null, null, null),
                        DimColis = new TDimensions(null, null, null),
                        DimDeplie = new TDimensions(null, null, null),
                        DimAccoudoir = new TDimensions(null, null, null),
                        MadeInFrance = false,
                        VariantesProduitNavigation = null
                    }
                );
            }

            List<Variante> mesVariantes = new List<Variante>();
            for (int i = 1; i <= 18; i++)
            {
                mesVariantes.Add(new Variante
                {
                    IdVariante = 1,
                    IdProduit = 1,
                    IdCouleur = 1,
                    DateCreation = DateTime.Now,
                    Stock = 5,
                    Prix = 0,
                    Promo = 1,
                    ProduitVarianteNavigation = mesProduits[i - 1]
                }
                );
            }

            var mockRepository = new Mock<IDataRepositoryProduits<Produit>>();
            mockRepository.Setup(x => x.GetNumberPagesByCouleur(1, new List<int> { 1 }).Result).Returns(4);

            var mockVarianteRepository = new Mock<IDataRepositoryVariante<Variante>>();
            mockVarianteRepository.Setup(x => x.GetAll().Result)
                .Returns(mesVariantes);

            var produitController = new ProduitsController(mockRepository.Object);
            var varianteController = new VariantesController(mockVarianteRepository.Object);

            // Act
            var actionResult = produitController.GetNumberPagesByCouleur(1, new[] { 1 }).Result;

            //// Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(4, actionResult.Value);
        }
    }
}