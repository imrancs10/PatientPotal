//-----------------------------------------------------------------------
// <copyright file="TestBase.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the TestBase class.</summary>
//-----------------------------------------------------------------------
//this.UnityContainer = UnityDependencyProvider.InitializeContainer(AppConstants.UnityContainers.TestContainer);

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace PatientPortal.Shared.Infrastructure.UnitTest
{
    /// <summary>
    /// Base class for Unit Test Cases.
    /// </summary>
    [TestClass()]
    public abstract class Test
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get;
            set;
        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void TestInitializeHandler()
        {
            this.OnTestInitialize();
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void TestCleanupHandler()
        {
            this.OnTestCleanup();
        }

        /// <summary>
        /// This method executes before each test is run.
        /// Use this method to revert the state of variables on which every test depends upon.
        /// </summary>
        protected virtual void OnTestInitialize()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clean ups operations after a test ends.
        /// </summary>
        protected virtual void OnTestCleanup()
        {
            throw new NotImplementedException();
        }
    }
}
