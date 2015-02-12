using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccessInterface;
using NUnit.Framework;
using Service.Repository;
using Service.ServiceInterface;

namespace TEST
{
    public class BaseTest : DynamicObject
    {
        protected SampleService SampleService;
        [SetUp]
        public virtual void Setup()
        {
            SampleService = new SampleService(new SampleRepository(new ServiceAccess(), "Fake"));
        }

        [TearDown]
        public virtual void TearDown()
        {
            //Close sessions if needed 
        }
    }
}
