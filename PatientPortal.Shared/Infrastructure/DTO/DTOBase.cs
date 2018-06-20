//-----------------------------------------------------------------------
// <copyright file="DTOBase.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the DTOBase class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Represents the abstract base class for all Data Transfer Objects.
    /// </summary>
    [Serializable]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public abstract class DTOBase : IDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DTOBase"/> class.
        /// </summary>
        protected DTOBase()
        {
        }

        /// <summary>
        /// Property Changed event. It is fired after a property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Property changing event. This event fires when a property is about to be changed. It fires before property change happens.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;
      
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            ms.Position = 0;
            object obj = bf.Deserialize(ms);
            ms.Close();
            return obj;
        }

        /// <summary>
        /// Event handler for property change event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Event handler to handle the property changing event.
        /// </summary>
        /// <param name="propertyName">Name of property.</param>
        protected void OnPropertyChanging(string propertyName)
        {
            if (this.PropertyChanging != null)
            {
                this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
         
        /// <summary>
        /// To get Error Message
        /// </summary>
        public abstract int ErrorCode { get; set; }

    }
}