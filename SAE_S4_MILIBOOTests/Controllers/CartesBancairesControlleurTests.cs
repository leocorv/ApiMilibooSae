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
    public class CartesBancairesControlleurTests
    {
        private readonly MilibooDBContext _context;
        private readonly CartesBancairesControlleur _controller;
        private IDataRepositoryCarteBancaire<CarteBancaire> _dataRepository;

        public CartesBancairesControlleurTests()
        {

            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql("Server=postgresql-philippa.alwaysdata.net;port=5432;Database=philippa_bdapi_milibite; SearchPath=public; uid=philippa; password=postgres"); // Chaine de connexion à mettre dans les ( )
            _context = new MilibooDBContext(builder.Options);
            _dataRepository = new CarteBancaireManager(_context);
            _controller = new CartesBancairesControlleur(_dataRepository);
        }

        [TestMethod()]
        public void GetCarteBancaireTest_ReturnExistingItem_Moq()
        {
            CarteBancaire cb = new CarteBancaire
            {
                CarteBancaireId = 1,
                ClientId = 1,
                NumeroCarte = "5969172594088170",
                CryptoCarte = "391",
                DateExpiration = "24/02",
                Nom = "GINET",
                Prenom = "Tristan",
                ClientCarteBancaireNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryCarteBancaire<CarteBancaire>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(cb);
            var cbController = new CartesBancairesControlleur(mockRepository.Object);

            // Act
            var actionResult = cbController.GetCarteBancaire(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(cb, actionResult.Value);
        }

        [TestMethod()]
        public void GetAllCartesBancairesByClientId_ReturnExistingItem_Moq()
        {
            CarteBancaire cb = new CarteBancaire
            {
                CarteBancaireId = 1,
                ClientId = 1,
                NumeroCarte = "5969172594088170",
                CryptoCarte = "391",
                DateExpiration = "24/02",
                Nom = "GINET",
                Prenom = "Tristan",
                ClientCarteBancaireNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryCarteBancaire<CarteBancaire>>();
            mockRepository.Setup(x => x.GetAllCartesBancairesByClientId(1).Result).Returns(new List<CarteBancaire>() { cb });
            var cbController = new CartesBancairesControlleur(mockRepository.Object);

            // Act
            var actionResult = cbController.GetAllCartesBancairesByClientId(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<CarteBancaire>() { cb }, actionResult.Value as List<CarteBancaire>);
        }

        [TestMethod()]
        public void GetCarteBancaireTest_ReturnsNotFound_Moq()
        {
            CarteBancaire cb = new CarteBancaire
            {
                CarteBancaireId = 1,
                ClientId = 1,
                NumeroCarte = "5969172594088170",
                CryptoCarte = "391",
                DateExpiration = "24/02",
                Nom = "GINET",
                Prenom = "Tristan",
                ClientCarteBancaireNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryCarteBancaire<CarteBancaire>>();
            mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(new NotFoundResult());
            var cbController = new CartesBancairesControlleur(mockRepository.Object);

            // Act
            var actionResult = cbController.GetCarteBancaire(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAllCartesBancairesByClientId_ReturnsNotFound_Moq()
        {
            CarteBancaire cb = new CarteBancaire
            {
                CarteBancaireId = 1,
                ClientId = 1,
                NumeroCarte = "5969172594088170",
                CryptoCarte = "391",
                DateExpiration = "24/02",
                Nom = "GINET",
                Prenom = "Tristan",
                ClientCarteBancaireNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryCarteBancaire<CarteBancaire>>();
            mockRepository.Setup(x => x.GetAllCartesBancairesByClientId(2).Result).Returns(new NotFoundResult());
            var cbController = new CartesBancairesControlleur(mockRepository.Object);

            // Act
            var actionResult = cbController.GetAllCartesBancairesByClientId(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }
    }
}