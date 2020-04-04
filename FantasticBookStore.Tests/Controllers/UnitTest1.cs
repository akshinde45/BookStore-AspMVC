using Microsoft.VisualStudio.TestTools.UnitTesting;
using FantasticBookStore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FantasticBookStore.Controllers.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void CreateUserTest()
        {
            UserController controller = new UserController();

            // Act
            ViewResult result = controller.CreateUser() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

       
    }
}