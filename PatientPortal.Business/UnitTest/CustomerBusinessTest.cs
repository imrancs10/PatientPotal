using Company.Project.Shared;
using Company.Project.Shared.Infrastructure.Common.Logging;
using Company.Project.Shared.Infrastructure.UnitTest;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using Company.Project.Shared.Infrastructure.Common.IoC;

namespace WOB.ERMS.Infrastructure.Facade.UnitTests
{
    /// <summary>
    /// Class responsible to unit test User facade class.
    /// </summary>
    [TestClass]
    public class CustomerBusinessTest : Test
    {
        static IUnityContainer container;

        [ClassInitialize]
        // This method once before the first test in the class is run.
        // This method should be used to setup the state of the test class.
        // This includes variables that are shared in all tests, but the values 
        // of which either does not change or the changing of their values will not impact a test.
        public static void Initialize(TestContext testContext)
        {
            //container = ContainerProvider.InitializeContainer(AppConstants.UnityContainers.TestContainer);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            ContainerProvider.CleanUp();
        }

        /// <summary>
        /// Clean ups operations after a test ends.
        /// </summary>
        protected override void OnTestCleanup()
        {
        }

        /// <summary>
        /// Test_s the if_ a_ method_ was called_ on_ user app service.
        /// </summary>
        [TestMethod]
        public void Test_AddCustomer()
        {
            // ARRANGE
            var logger = ContainerProvider.Resolve<ILogger>();
          
            // ACT
          
            // ASSERT
          
            Assert.IsTrue(true);
        }
    }
}
