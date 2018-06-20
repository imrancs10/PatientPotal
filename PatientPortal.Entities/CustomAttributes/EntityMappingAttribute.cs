//-----------------------------------------------------------------------
// <copyright file="EntityMappingAttribute.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the EntityMappingAttribute.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Entities
{
    using System;

    /// <summary>
    /// Contains/Represents/Provides Entity mapping attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class EntityMappingAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMappingAttribute"/> class.
        /// </summary>
        /// <param name="mappedEntityTypeFullName">Full name of the mapped entity type.</param>
        /// <param name="mappingType">Type of the mapping.</param>
        public EntityMappingAttribute(string mappedEntityTypeFullName, MappingType mappingType)
        {
            // private set value
            this.MappedEntityTypeFullName = mappedEntityTypeFullName;
            this.MappingType = mappingType;
        }

        /// <summary>
        /// Gets the full name of the mapped entity type.
        /// </summary>
        /// <value>The full name of the mapped entity type.</value>
        public string MappedEntityTypeFullName { get; private set; }

        /// <summary>
        /// Gets the type of the mapping.
        /// </summary>
        /// <value>The type of the mapping.</value>
        public MappingType MappingType { get; private set; }
    }
}