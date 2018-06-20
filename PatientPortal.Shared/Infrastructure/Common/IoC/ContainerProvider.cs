//-----------------------------------------------------------------------
// <copyright file="UnityResolver.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the UnityResolver class.</summary>
//-----------------------------------------------------------------------
using Microsoft.Practices.Unity;


namespace PatientPortal.Shared.Infrastructure.Common.IoC
{

    public static class ContainerProvider
    {
        private static IUnityContainer container;

        /// <summary>
        /// Initializes the unity container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns>Unity container.</returns>
        public static void InitializeContainer(IUnityContainer unityContainer)
        {
            if (container == null)
            {
                container = unityContainer;
            }
        }

        /// <summary>
        /// Resolves with specified parameters.
        /// </summary>
        /// <typeparam name="T">Parameters required by constructor.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Object instance.</returns>
        public static T Resolve<T>(params ResolverOverride[] parameters)
        {
            T instance = default(T);
            try
            {
                instance = container.Resolve<T>(parameters);
            }
            catch
            {
                //// todo: create custom exception "DIException" and throw this instead.
                throw;
            }

            return instance;
        }

        /// <summary>
        /// Cleans up the unity container by calling its 'dispose' method.
        /// </summary>
        public static void CleanUp()
        {
            if (container != null)
            {
                container.Dispose();
            }
        }
    }
}