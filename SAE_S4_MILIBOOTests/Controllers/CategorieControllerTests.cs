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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_S4_MILIBOO.Controllers.Tests
{
    [TestClass()]
    public class CategorieControllerTests
    {
        private readonly MilibooDBContext _context;
        private readonly CategorieController _controller;
        private IDataRepositoryCategorie<Categorie> _dataRepository;

        public CategorieControllerTests()
        {

            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql("Server=postgresql-philippa.alwaysdata.net;port=5432;Database=philippa_bdapi_milibite; SearchPath=public; uid=philippa; password=postgres"); // Chaine de connexion à mettre dans les ( )
            _context = new MilibooDBContext(builder.Options);
            _dataRepository = new CategorieManager(_context);
            _controller = new CategorieController(_dataRepository);
        }

        [TestMethod()]
        public void GetAllCategoriesPremierNiveauTest()
        {
            var lescategories = _context.Categories.Where<Categorie>(cat => cat.CategorieParentNavigation== null);

            var actionResult = _dataRepository.GetCategoriesPremierNiveau();

            Assert.AreEqual(lescategories, actionResult.Result, "la liste des catégories renvoyées est bonne");
        }

        [TestMethod()]
        public void GetCategorieTest_ReturnsExistingProduct_Moq()
        {
            Categorie ctg = new Categorie
            {
                Categorieid = 1,
                CategorieParentid = null,
                Libelle = "Canapé Fauteuil",
                Description = null,
                PhotoCategorieNavigation = null,
                ProduitsCategorieNavigation = null,
                CategorieParentNavigation = null,
                SousCategoriesNavigation = null
            };


            var mockRepository = new Mock<IDataRepositoryCategorie<Categorie>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(ctg);
            var categorieController = new CategorieController(mockRepository.Object);

            // Act
            var actionResult = categorieController.GetCategorie(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(ctg, actionResult.Value as Categorie);
        }

        [TestMethod()]
        public void GetCategorieTest_ReturnsNull_Moq()
        {
            Categorie ctg = new Categorie
            {
                Categorieid = 1,
                CategorieParentid = null,
                Libelle = "Canapé Fauteuil",
                Description = null,
                PhotoCategorieNavigation = null,
                ProduitsCategorieNavigation = null,
                CategorieParentNavigation = null,
                SousCategoriesNavigation = null
            };


            var mockRepository = new Mock<IDataRepositoryCategorie<Categorie>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(ctg);
            var categorieController = new CategorieController(mockRepository.Object);

            // Act
            var actionResult = categorieController.GetCategorie(2).Result;

            // Assert
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }
    }
}