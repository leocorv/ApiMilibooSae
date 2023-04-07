using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class CollectionControllerTests
    {
        private readonly MilibooDBContext _context;
        private readonly CollectionController _controller;
        private IDataRepositoryCollection<Collection> _dataRepository;

        public CollectionControllerTests()
        {

            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql("Server=postgresql-philippa.alwaysdata.net;port=5432;Database=philippa_bdapi_milibite; SearchPath=public; uid=philippa; password=postgres"); // Chaine de connexion à mettre dans les ( )
            _context = new MilibooDBContext(builder.Options);
            _dataRepository = new CollectionManager(_context);
            _controller = new CollectionController(_dataRepository);
        }


        [TestMethod()]
        public void GetCollectionsTest()
        {
            Assert.Fail();
        }
    }
}