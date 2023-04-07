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
    public class CommandesControllerTests
    {
        private readonly MilibooDBContext _context;
        private readonly CommandesController _controller;
        private IDataRepositoryCommande<Commande> _dataRepository;

        public CommandesControllerTests()
        {

            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql("Server=postgresql-philippa.alwaysdata.net;port=5432;Database=philippa_bdapi_miliboo; SearchPath=public; uid=philippa; password=postgres");
            _dataRepository = new CommandeManager(_context);
            _controller = new CommandesController(_dataRepository);
        }

        [TestMethod()]
        public void GetCommandeTest_ReturnsExistingClient_Moq()
        {
            Commande cmd = new Commande
            {
                CommandeId = 1,
                ClientId = 1,
                AdresseId = 1,
                EtatId = 1,
                Instructions = "Fonctionne stp",
                Collecte = true,
                Express = true,
                PointsFideliteUtilises = 50,
                AdresseCommandeNavigation = null,
                EtatCommandeNavigation = null,
                ClientCommandeNavigation = null,
                LigneCommandeCommandeNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryCommande<Commande>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns( cmd );
            var commandeController = new CommandesController(mockRepository.Object);

            // Act
            var actionResult = commandeController.GetCommande(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(cmd, actionResult.Value as Commande);
        }

        [TestMethod()]
        public void GetCommandesTest_ReturnsNull_Moq()
        {
            Commande cmd = new Commande
            {
                CommandeId = 1,
                ClientId = 1,
                AdresseId = 1,
                EtatId = 1,
                Instructions = "Fonctionne stp",
                Collecte = true,
                Express = true,
                PointsFideliteUtilises = 50,
                AdresseCommandeNavigation = null,
                EtatCommandeNavigation = null,
                ClientCommandeNavigation = null,
                LigneCommandeCommandeNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryCommande<Commande>>();
            mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(new NotFoundResult());
            var commandeController = new CommandesController(mockRepository.Object);

            // Act
            var actionResult = commandeController.GetCommandes().Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Commande> { cmd }, (List<Commande>)actionResult.Value as List<Commande>);
        }

        [TestMethod()]
        public void GetCommandesByIdClient_ReturnsExistingClient_Moq()
        {
            Commande cmd = new Commande
            {
                CommandeId = 1,
                ClientId = 1,
                AdresseId = 1,
                EtatId = 1,
                Instructions = "Fonctionne stp",
                Collecte = true,
                Express = true,
                PointsFideliteUtilises = 50,
                AdresseCommandeNavigation = null,
                EtatCommandeNavigation = null,
                ClientCommandeNavigation = null,
                LigneCommandeCommandeNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryCommande<Commande>>();
            mockRepository.Setup(x => x.GetAllCommandeByClientId(1).Result).Returns(new List<Commande> { cmd });
            var commandeController = new CommandesController(mockRepository.Object);

            // Act
            var actionResult = commandeController.GetCommandesByIdClient(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Commande> { cmd }, (List<Commande>)actionResult.Value as List<Commande>);
        }

        [TestMethod()]
        public void GetCommandesByEtat_ReturnsExistingClient_Moq()
        {
            Commande cmd = new Commande
            {
                CommandeId = 1,
                ClientId = 1,
                AdresseId = 1,
                EtatId = 1,
                Instructions = "Fonctionne stp",
                Collecte = true,
                Express = true,
                PointsFideliteUtilises = 50,
                AdresseCommandeNavigation = null,
                EtatCommandeNavigation = null,
                ClientCommandeNavigation = null,
                LigneCommandeCommandeNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryCommande<Commande>>();
            mockRepository.Setup(x => x.GetAllCommandeByEtat(1).Result).Returns(new List<Commande> { cmd });
            var commandeController = new CommandesController(mockRepository.Object);

            // Act
            var actionResult = commandeController.GetCommandesByEtat(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Commande> { cmd }, (List<Commande>)actionResult.Value as List<Commande>);
        }

        [TestMethod()]
        public void GetCommandeTest_ReturnsNotFound_Moq()
        {
            Commande cmd = new Commande
            {
                CommandeId = 1,
                ClientId = 1,
                AdresseId = 1,
                EtatId = 1,
                Instructions = "Fonctionne stp",
                Collecte = true,
                Express = true,
                PointsFideliteUtilises = 50,
                AdresseCommandeNavigation = null,
                EtatCommandeNavigation = null,
                ClientCommandeNavigation = null,
                LigneCommandeCommandeNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryCommande<Commande>>();
            mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(new NotFoundResult());
            var commandeController = new CommandesController(mockRepository.Object);

            // Act
            var actionResult = commandeController.GetCommande(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetCommandesByIdClient_ReturnsNotFound_Moq()
        {
            Commande cmd = new Commande
            {
                CommandeId = 1,
                ClientId = 1,
                AdresseId = 1,
                EtatId = 1,
                Instructions = "Fonctionne stp",
                Collecte = true,
                Express = true,
                PointsFideliteUtilises = 50,
                AdresseCommandeNavigation = null,
                EtatCommandeNavigation = null,
                ClientCommandeNavigation = null,
                LigneCommandeCommandeNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryCommande<Commande>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(new List<Commande> { cmd });
            var commandeController = new CommandesController(mockRepository.Object);

            // Act
            var actionResult = commandeController.GetCommandesByIdClient(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetCommandesByEtat_ReturnsNotFound_Moq()
        {
            Commande cmd = new Commande
            {
                CommandeId = 1,
                ClientId = 1,
                AdresseId = 1,
                EtatId = 1,
                Instructions = "Fonctionne stp",
                Collecte = true,
                Express = true,
                PointsFideliteUtilises = 50,
                AdresseCommandeNavigation = null,
                EtatCommandeNavigation = null,
                ClientCommandeNavigation = null,
                LigneCommandeCommandeNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryCommande<Commande>>();
            mockRepository.Setup(x => x.GetAllCommandeByEtat(2).Result).Returns(new NotFoundResult());
            var commandeController = new CommandesController(mockRepository.Object);

            // Act
            var actionResult = commandeController.GetCommandesByEtat(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }
    }
}