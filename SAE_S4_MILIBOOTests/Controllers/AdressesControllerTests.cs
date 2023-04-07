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
    public class AdressesControllerTests
    {
        private readonly MilibooDBContext _context;
        private readonly AdressesController _controller;
        private IDataRepositoryAdresse<Adresse> _dataRepository;

        public AdressesControllerTests()
        {

            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql("Server=postgresql-philippa.alwaysdata.net;port=5432;Database=philippa_bdapi_milibite; SearchPath=public; uid=philippa; password=postgres"); // Chaine de connexion à mettre dans les ( )
            _context = new MilibooDBContext(builder.Options);
            _dataRepository = new AdresseManager(_context);
            _controller = new AdressesController(_dataRepository);
        }

        [TestMethod()]
        public void GetAdresseTest_Moq()
        {
            Adresse adresse = new Adresse
            {
                AdresseId = 2,
                Rue = "Rue Dictum Av",
                Numero = "47",
                Cp = "75000",
                Ville = "Paris",
                Pays = "France",
                TelFixe = "08 93 84 71 32",
                Remarque = null,
                AdressesClientsNavigation = null,
                CommandeAdresseNavigation = null
            };


            var mockRepository = new Mock<IDataRepositoryAdresse<Adresse>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(adresse);
            var adresseController = new AdressesController(mockRepository.Object);

            //Act
            var actionResult = adresseController.GetAdresse(1).Result;

             //Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(adresse, actionResult.Value as Adresse);
        }
    }
}