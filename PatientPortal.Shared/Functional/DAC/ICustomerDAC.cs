//-----------------------------------------------------------------------
// <copyright file="ICustomerDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ICustomerDAC class.</summary>
//-----------------------------------------------------------------------

namespace Company.Project.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for Department DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ICustomerDAC : IDataAccessComponent
    {
        /// <summary>
        /// Get list of customers.
        /// </summary>
        /// <returns>List of customer data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        IList<ICustomerDTO> GetAll();

        /// <summary>
        /// Get customer data by customer id.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns>Customer data component.</returns>
        ICustomerDTO GetById(int id);

        /// <summary>
        /// Add a customer.
        /// </summary>
        /// <param name="customer">Customer data component.</param>
        /// <returns>True or false.</returns>
        bool Add(ICustomerDTO customer);

        /// <summary>
        /// Update a customer.
        /// </summary>
        /// <param name="customer">Customer data component.</param>
        /// <returns>True or false.</returns>
        bool Update(ICustomerDTO customer);

        /// <summary>
        /// Delete a customer.
        /// </summary>
        /// <param name="customer">Customer data component.</param>
        /// <returns>True or false.</returns>
        bool Delete(ICustomerDTO customer);

        /// <summary>
        /// Delete a customer.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns>True or false.</returns>
        bool DeleteById(int id);
    }
}