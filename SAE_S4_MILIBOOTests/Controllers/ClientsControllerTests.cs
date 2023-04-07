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
    public class ClientsControllerTests
    {
        private readonly MilibooDBContext _context;
        private readonly ClientsController _controller;
        private IDataRepositoryClient<Client> _dataRepository;

        public ClientsControllerTests()
        {

            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql("Server=postgresql-philippa.alwaysdata.net;port=5432;Database=philippa_bdapi_milibite; SearchPath=public; uid=philippa; password=postgres"); // Chaine de connexion à mettre dans les ( )
            _context = new MilibooDBContext(builder.Options);
            _dataRepository = new ClientManager(_context);
            _controller = new ClientsController(_dataRepository);
        }

        [TestMethod()]
        public void GetClientTest_ReturnsExistingClient_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(clt);
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClient(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(clt, actionResult.Value as Client);
        }

        [TestMethod]
        public void GetClientTest_ReturnsNotFound_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(clt);
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClient(2).Result;
        }

        //    // Assert
        //    Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        //}

        [TestMethod]
        public void GetAllClientsTest_ReturnsAllClients_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            Client clt2 = clt;
            clt2.ClientId = 2;
            clt2.Mail = "tristan.ginet2@gmail.com";

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(new List<Client> { clt, clt2 });
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetAllClients().Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Client> { clt, clt2 }, actionResult.Value as List<Client>);
        }

        [TestMethod]
        public void GetClientByEmailTest_ReturnsExistingClient_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetClientByEmail("tristan.ginet@gmail.com").Result).Returns(clt);
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClientByEmail("tristan.ginet@gmail.com").Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(clt, actionResult.Value as Client);
        }

        [TestMethod]
        public void GetClientByEmailTest_ReturnsNotFound_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetClientByEmail("tristan.ginet2@gmail.com").Result).Returns(new NotFoundResult());
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClientByEmail("tristan.ginet2@gmail.com").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetClientByPortable_ReturnsExistingClient_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetClientByPortable("0606060606").Result).Returns(clt);
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClientByPortable("0606060606").Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(clt, actionResult.Value as Client);
        }

        [TestMethod]
        public void GetClientByPortable_ReturnsNotFound_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetClientByPortable("0606060606").Result).Returns(clt);
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClientByPortable("0707070707").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAllClientsByNomPrenom_ReturnsExistingClient_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            // LA FONCTION GET BY NOM PRENOM TEST LES DEUX INDEPENDAMENT ET RASSEMBLE LES RESULTATS

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetAllClientsByNomPrenom("GINET").Result).Returns(new List<Client> { clt });
            var clientController = new ClientsController(mockRepository.Object);

            var mockRepository2 = new Mock<IDataRepositoryClient<Client>>();
            mockRepository2.Setup(x => x.GetAllClientsByNomPrenom("Tristan").Result).Returns(new List<Client> { clt });

            // Act
            var actionResult = clientController.GetAllClientsByNomPrenom("GINET").Result;
            var actionResult2 = clientController.GetAllClientsByNomPrenom("Tristan").Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(new List<Client> { clt }, actionResult.Value as List<Client>);
            //Assert.AreEqual(new List<Client> { clt }, actionResult2.Value as List<Client>);
        }

        [TestMethod]
        public void GetAllClientsByNomPrenom_ReturnsNotFound_Moq()
        {
            Client clt = new Client
            {
                ClientId = 1,
                Mail = "tristan.ginet@gmail.com",
                Password = "password",
                Nom = "GINET",
                Prenom = "Tristan",
                Portable = "0606060606",
                NewsMiliboo = true,
                NewsPartenaire = true,
                SoldeFidelite = 50,
                DerniereConnexion = new DateTime(2023 - 03 - 10),
                DateCreation = new DateTime(2022 - 03 - 10),
                Civilite = "homme",
                AvisClientsNavigation = null,
                CarteBancaireClientNavigation = null,
                ClientsLivraisonsNavigation = null,
                CommandesClientNavigation = null,
                LignesPanierClientNavigation = null,
                ListesNavigation = null
            };

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetAllClientsByNomPrenom("Ginet").Result).Returns(new List<Client> { clt });
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetAllClientsByNomPrenom("Guyon").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAllClientsNewsletterMTest_ReturnsExistingClient_Moq()
        {
            List<Client> mesClients = new List<Client>();

            for (int i = 1; i <= 5; i++)
            {
                mesClients.Add(
                    new Client
                    {
                        ClientId = i,
                        Mail = "tristan.ginet@gmail.com",
                        Password = "password",
                        Nom = "GINET",
                        Prenom = "Tristan",
                        Portable = "0606060606",
                        NewsMiliboo = true,
                        NewsPartenaire = true,
                        SoldeFidelite = 50,
                        DerniereConnexion = new DateTime(2023 - 03 - 10),
                        DateCreation = new DateTime(2022 - 03 - 10),
                        Civilite = "homme",
                        AvisClientsNavigation = null,
                        CarteBancaireClientNavigation = null,
                        ClientsLivraisonsNavigation = null,
                        CommandesClientNavigation = null,
                        LignesPanierClientNavigation = null,
                        ListesNavigation = null
                    }
                );
            }

            // LA FONCTION GET BY NOM PRENOM TEST LES DEUX INDEPENDAMENT ET RASSEMBLE LES RESULTATS

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetAllClientsNewsletterM().Result).Returns(new List<Client> { mesClients[0], mesClients[1], mesClients[2], mesClients[3], mesClients[4] });
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetAllClientsNewsletterM().Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Client> { mesClients[0], mesClients[1], mesClients[2], mesClients[3], mesClients[4] }, actionResult.Value as List<Client>);
        }

        [TestMethod]
        public void GetAllClientsNewsletterMTest_ReturnsNotFound()
        {
            List<Client> mesClients = new List<Client>();

            for (int i = 1; i < 6; i++)
            {
                mesClients.Add(
                    new Client
                    {
                        ClientId = i,
                        Mail = "tristan.ginet@gmail.com" + i,
                        Password = "password",
                        Nom = "GINET",
                        Prenom = "Tristan",
                        Portable = "0606060606",
                        NewsMiliboo = false,
                        NewsPartenaire = false,
                        SoldeFidelite = 50,
                        DerniereConnexion = new DateTime(2023 - 03 - 10),
                        DateCreation = new DateTime(2022 - 03 - 10),
                        Civilite = "homme",
                        AvisClientsNavigation = null,
                        CarteBancaireClientNavigation = null,
                        ClientsLivraisonsNavigation = null,
                        CommandesClientNavigation = null,
                        LignesPanierClientNavigation = null,
                        ListesNavigation = null
                    }
                );
            }

            // LA FONCTION GET BY NOM PRENOM TEST LES DEUX INDEPENDAMENT ET RASSEMBLE LES RESULTATS

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(mesClients);
            var clientController = new ClientsController(mockRepository.Object);

            //// Act
            var actionResult = clientController.GetAllClientsNewsletterM().Result;

            // Assert
            Assert.AreEqual(404, (actionResult.Result as NotFoundResult).StatusCode);
        }

        [TestMethod]
        public void GetAllClientsNewsletterPTest_ReturnsExistingClient_Moq()
        {
            List<Client> mesClients = new List<Client>();

            for (int i = 1; i <= 5; i++)
            {
                mesClients.Add(
                    new Client
                    {
                        ClientId = i,
                        Mail = "tristan.ginet@gmail.com",
                        Password = "password",
                        Nom = "GINET",
                        Prenom = "Tristan",
                        Portable = "0606060606",
                        NewsMiliboo = true,
                        NewsPartenaire = true,
                        SoldeFidelite = 50,
                        DerniereConnexion = new DateTime(2023 - 03 - 10),
                        DateCreation = new DateTime(2022 - 03 - 10),
                        Civilite = "homme",
                        AvisClientsNavigation = null,
                        CarteBancaireClientNavigation = null,
                        ClientsLivraisonsNavigation = null,
                        CommandesClientNavigation = null,
                        LignesPanierClientNavigation = null,
                        ListesNavigation = null
                    }
                );
            }

            for (int i = 6; i <= 10; i++)
            {
                mesClients.Add(
                    new Client
                    {
                        ClientId = i,
                        Mail = "tristan.ginet@gmail.com",
                        Password = "password",
                        Nom = "GINET",
                        Prenom = "Tristan",
                        Portable = "0606060606",
                        NewsMiliboo = false,
                        NewsPartenaire = false,
                        SoldeFidelite = 50,
                        DerniereConnexion = new DateTime(2023 - 03 - 10),
                        DateCreation = new DateTime(2022 - 03 - 10),
                        Civilite = "homme",
                        AvisClientsNavigation = null,
                        CarteBancaireClientNavigation = null,
                        ClientsLivraisonsNavigation = null,
                        CommandesClientNavigation = null,
                        LignesPanierClientNavigation = null,
                        ListesNavigation = null
                    }
                );
            }

            // LA FONCTION GET BY NOM PRENOM TEST LES DEUX INDEPENDAMENT ET RASSEMBLE LES RESULTATS

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetAllClientsNewsletterP().Result).Returns(new List<Client> { mesClients[0], mesClients[1], mesClients[2], mesClients[3], mesClients[4] });
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetAllClientsNewsletterP().Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(new List<Client> { mesClients[0], mesClients[1], mesClients[2], mesClients[3], mesClients[4] }, actionResult.Value as List<Client>);
        }

        [TestMethod]
        public void GetAllClientsNewsletterPTest_ReturnsNotFound()
        {
            List<Client> mesClients = new List<Client>();

            for (int i = 1; i < 6; i++)
            {
                mesClients.Add(
                    new Client
                    {
                        ClientId = i,
                        Mail = "tristan.ginet@gmail.com",
                        Password = "password",
                        Nom = "GINET",
                        Prenom = "Tristan",
                        Portable = "0606060606",
                        NewsMiliboo = false,
                        NewsPartenaire = false,
                        SoldeFidelite = 50,
                        DerniereConnexion = new DateTime(2023 - 03 - 10),
                        DateCreation = new DateTime(2022 - 03 - 10),
                        Civilite = "homme",
                        AvisClientsNavigation = null,
                        CarteBancaireClientNavigation = null,
                        ClientsLivraisonsNavigation = null,
                        CommandesClientNavigation = null,
                        LignesPanierClientNavigation = null,
                        ListesNavigation = null
                    }
                );
            }

            // LA FONCTION GET BY NOM PRENOM TEST LES DEUX INDEPENDAMENT ET RASSEMBLE LES RESULTATS

            var mockRepository = new Mock<IDataRepositoryClient<Client>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(new List<Client> { mesClients[0], mesClients[1], mesClients[2], mesClients[3], mesClients[4] });
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetAllClientsNewsletterP().Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        public void PostClientTest()
        {
            Assert.Fail();
        }

        public void PutClientTest()
        {
            Assert.Fail();
        }

        public void DeleteClientTest()
        {
            Assert.Fail();
        }
    }
}