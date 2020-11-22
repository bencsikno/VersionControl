using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
     public class AccountControllerTestFixture
    {
        [Test]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            var accountController = new AccountController();

            
            var actualResult = accountController.ValidateEmail(email);

            
            Assert.AreEqual(expectedResult, actualResult);

        }



    }

}
