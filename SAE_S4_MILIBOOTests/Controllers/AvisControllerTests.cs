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
    public class AvisControllerTests
    {
        private readonly MilibooDBContext _context;
        private readonly AvisController _controller;
        private IDataRepositoryAvis<Avis> _dataRepository;

        public AvisControllerTests()
        {

            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql("Server=postgresql-philippa.alwaysdata.net;port=5432;Database=philippa_bdapi_milibite; SearchPath=public; uid=philippa; password=postgres"); // Chaine de connexion à mettre dans les ( )
            _context = new MilibooDBContext(builder.Options);
            _dataRepository = new AvisManager(_context);
            _controller = new AvisController(_dataRepository);
        }

        [TestMethod()]
        public void GetAllAvisTest_ReturnExistingItem_Moq()
        {
            // Arrange

            List<Avis> allAvis = new List<Avis>();
            for (int i=1; i<10; i++)
            {
                allAvis.Add(new Avis
                {
                    AvisId = i,
                    ClientId = 1,
                    VarianteId = 1,
                    AvisDate = new DateTime(2022 - 06 - 06),
                    AvisNote = 5,
                    AvisTitre = "SUPER !",
                    AvisTexte = "SUPER !",
                    VarianteAvisNavigation = null,
                    ClientsAvisNavigation = null,
                    PhotosAvisNavigation = null,
                });
            }


            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(allAvis);
            var avisController = new AvisController(mockRepository.Object);

            // Act
            var actionResult = avisController.GetAllAvis().Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(allAvis, actionResult.Value as List<Avis>);
        }

        [TestMethod()]
        public void GetAvisTest_ReturnExistingItem_Moq()
        {
            Avis avis = new Avis
            {
                AvisId = 1,
                ClientId = 1,
                VarianteId = 1,
                AvisDate = new DateTime(2022 - 06 - 06),
                AvisNote = 5,
                AvisTitre = "SUPER !",
                AvisTexte = "SUPER !",
                VarianteAvisNavigation = null,
                ClientsAvisNavigation = null,
                PhotosAvisNavigation = null,
            };

            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            mockRepository.Setup(x => x.GetAvisById(1).Result).Returns(avis);
            var avisController = new AvisController(mockRepository.Object);

            // Act
            var actionResult = avisController.GetAvis(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(avis, actionResult.Value);
        }

        [TestMethod()]
        public void GetAvisByProduitTest_ReturnExistingItem_Moq()
        {
            Avis avis = new Avis
            {
                AvisId = 1,
                ClientId = 1,
                VarianteId = 1,
                AvisDate = new DateTime(2022 - 06 - 06),
                AvisNote = 5,
                AvisTitre = "SUPER !",
                AvisTexte = "SUPER !",
                VarianteAvisNavigation = null,
                ClientsAvisNavigation = null,
                PhotosAvisNavigation = null,
            };

            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            mockRepository.Setup(x => x.GetAvisByProduit(1).Result).Returns(new List<Avis>() { avis });
            var avisController = new AvisController(mockRepository.Object);

            // Act
            var actionResult = avisController.GetAvisByProduit(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Avis>() { avis }, actionResult.Value as List<Avis>);
        }

        [TestMethod()]
        public void GetAvisByVarianteTest_ReturnExistingItem_Moq()
        {
            Avis avis = new Avis
            {
                AvisId = 1,
                ClientId = 1,
                VarianteId = 1,
                AvisDate = new DateTime(2022 - 06 - 06),
                AvisNote = 5,
                AvisTitre = "SUPER !",
                AvisTexte = "SUPER !",
                VarianteAvisNavigation = null,
                ClientsAvisNavigation = null,
                PhotosAvisNavigation = null,
            };

            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            mockRepository.Setup(x => x.GetAvisByVariante(1).Result).Returns(new List<Avis>() { avis });
            var avisController = new AvisController(mockRepository.Object);

            // Act
            var actionResult = avisController.GetAvisByVariante(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Avis>() { avis }, actionResult.Value as List<Avis>);
        }

        [TestMethod()]
        public void GetAvisByClientTest_ReturnExistingItem_Moq()
        {
            Avis avis = new Avis
            {
                AvisId = 1,
                ClientId = 1,
                VarianteId = 1,
                AvisDate = new DateTime(2022 - 06 - 06),
                AvisNote = 5,
                AvisTitre = "SUPER !",
                AvisTexte = "SUPER !",
                VarianteAvisNavigation = null,
                ClientsAvisNavigation = null,
                PhotosAvisNavigation = null,
            };

            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            mockRepository.Setup(x => x.GetAvisByClient(1).Result).Returns(new List<Avis>() { avis });
            var avisController = new AvisController(mockRepository.Object);

            // Act
            var actionResult = avisController.GetAvisByClient(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Avis>() { avis }, actionResult.Value as List<Avis>);
        }

        [TestMethod()]
        public void GetAvisTest_ReturnNotFound_Moq()
        {
            Avis avis = new Avis
            {
                AvisId = 1,
                ClientId = 1,
                VarianteId = 1,
                AvisDate = new DateTime(2022 - 06 - 06),
                AvisNote = 5,
                AvisTitre = "SUPER !",
                AvisTexte = "SUPER !",
                VarianteAvisNavigation = null,
                ClientsAvisNavigation = null,
                PhotosAvisNavigation = null,
            };

            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            mockRepository.Setup(x => x.GetAvisById(2).Result).Returns(new NotFoundResult());
            var avisController = new AvisController(mockRepository.Object);

            // Act
            var actionResult = avisController.GetAvis(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAvisByProduitTest_ReturnNotFound_Moq()
        {
            Avis avis = new Avis
            {
                AvisId = 1,
                ClientId = 1,
                VarianteId = 1,
                AvisDate = new DateTime(2022 - 06 - 06),
                AvisNote = 5,
                AvisTitre = "SUPER !",
                AvisTexte = "SUPER !",
                VarianteAvisNavigation = null,
                ClientsAvisNavigation = null,
                PhotosAvisNavigation = null,
            };

            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            mockRepository.Setup(x => x.GetAvisByProduit(2).Result).Returns(new NotFoundResult());
            var avisController = new AvisController(mockRepository.Object);

            // Act
            var actionResult = avisController.GetAvisByProduit(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAvisByVarianteTest_ReturnNotFound_Moq()
        {
            Avis avis = new Avis
            {
                AvisId = 1,
                ClientId = 1,
                VarianteId = 1,
                AvisDate = new DateTime(2022 - 06 - 06),
                AvisNote = 5,
                AvisTitre = "SUPER !",
                AvisTexte = "SUPER !",
                VarianteAvisNavigation = null,
                ClientsAvisNavigation = null,
                PhotosAvisNavigation = null,
            };

            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            mockRepository.Setup(x => x.GetAvisByVariante(2).Result).Returns(new NotFoundResult());
            var avisController = new AvisController(mockRepository.Object);

            // Act
            var actionResult = avisController.GetAvisByVariante(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAvisByClientTest_ReturnNotFound_Moq()
        {
            Avis avis = new Avis
            {
                AvisId = 1,
                ClientId = 1,
                VarianteId = 1,
                AvisDate = new DateTime(2022 - 06 - 06),
                AvisNote = 5,
                AvisTitre = "SUPER !",
                AvisTexte = "SUPER !",
                VarianteAvisNavigation = null,
                ClientsAvisNavigation = null,
                PhotosAvisNavigation = null,
            };

            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            mockRepository.Setup(x => x.GetAvisByClient(2).Result).Returns(new NotFoundResult());
            var avisController = new AvisController(mockRepository.Object);

            // Act
            var actionResult = avisController.GetAvisByClient(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }
    }
}