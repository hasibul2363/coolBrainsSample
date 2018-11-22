using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Test.Models;
using SimpleInjector.Lifestyles;
using SuitSupply.Infrastructure.Repository.Contracts;
using System;

namespace Repository.Test
{
    [TestClass]
    public class RepositoryTest:TestBase
    {

        public IRepository Repository { get; set; }
        [TestInitialize]
        public void Init()
        {
            InitContainer();
            

           
        }

        [TestMethod]
        public void AddMustSucceed()
        {

            using (AsyncScopedLifestyle.BeginScope(Container))
            {
                Repository = Container.GetInstance<IRepository>();
                var category = new ProductCategory
                {
                    CategoryCode = "2363",
                    CategoryName = "BALL",
                    Id = Guid.NewGuid()
                };
                Repository.Add(category);
                Repository.SaveChanges();
                var categories = Repository.GetItems<ProductCategory>();
            }

            
        }
    }
}
