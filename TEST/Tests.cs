using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.Models.DataModels;
using NUnit.Framework;

namespace TEST
{
    [TestFixture]
    public class Tests :BaseTest
    {
        [Test]
        public void getCustomers()
        {
            var customers = SampleService.ListCustomers(new CustomerFilter());
            Assert.True(customers.TotalCount == 4);
        }

        [Test]
        public void getColors()
        {
            //Check the error message that is returned...  
            var colors = SampleService.ListColors(new DBBColorFilter());
        }
    }
}
