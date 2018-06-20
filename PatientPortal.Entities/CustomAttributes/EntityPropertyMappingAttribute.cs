//-----------------------------------------------------------------------
// <copyright file="EntityPropertyMappingAttribute.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the EntityPropertyMappingAttribute.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Entities
{
    using System;

    /// <summary>
    /// Contains/Represents/Provides Entity property mapping attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class EntityPropertyMappingAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPropertyMappingAttribute"/> class.
        /// </summary>
        /// <param name="mappingDirection">The mapping direction.</param>
        /// <param name="mappedEntityPropertyName">Name of the mapped entity property.</param>
        public EntityPropertyMappingAttribute(MappingDirectionType mappingDirection, string mappedEntityPropertyName)
            : this(mappingDirection)
        {
            // private set value
            this.MappedEntityPropertyName = mappedEntityPropertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPropertyMappingAttribute"/> class.
        /// </summary>
        /// <param name="mappingDirection">The mapping direction.</param>
        internal EntityPropertyMappingAttribute(MappingDirectionType mappingDirection)
        {
            // private set value
            this.MappingDirection = mappingDirection;
        }

        /// <summary>
        /// Gets the name of the mapped entity property.
        /// </summary>
        /// <value>The name of the mapped entity property.</value>
        public string MappedEntityPropertyName { get; private set; }

        /// <summary>
        /// Gets the mapping direction.
        /// </summary>
        /// <value>The mapping direction.</value>
        public MappingDirectionType MappingDirection { get; private set; }
    }
}