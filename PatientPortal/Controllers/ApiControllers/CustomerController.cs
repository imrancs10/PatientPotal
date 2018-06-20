//-----------------------------------------------------------------------
// <copyright file="CustomerController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the CustomerController.cs file.</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YaleNexTouch.Shared;

namespace YaleNexTouch.Web
{
    public class CustomerController : BaseApiController
    {

        public CustomerController()
        {
            
        }

        public CustomerController(ICustomerFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public CustomerController(ICustomerFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private ICustomerFacade Facade { get; set; }


        /// <summary>
        /// This method will use to get the complete customer list.
        /// </summary>
        /// <returns></returns>
        public IList<ICustomerDTO> Get()
        {
            var customers = this.Facade.GetCustomerList();
            if (customers.IsValid())
            {
                return customers.Data;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will use to get the particular customer by passing its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICustomerDTO Get(int id)
        {
            var customers = this.Facade.GetCustomerById(id);
            if (customers.IsValid())
            {
                return customers.Data;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will use to insert the customer.
        /// </summary>
        /// <param name="customer"></param>
        public void Post([FromBody]YaleNexTouch.DTOModel.CustomerDTO customer)
        {
            this.Facade.InsertCustomer(customer);
        }


        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
