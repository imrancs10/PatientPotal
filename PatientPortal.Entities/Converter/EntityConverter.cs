//-----------------------------------------------------------------------
// <copyright file="EntityConverter.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the EntityConverter.cs file.</summary>
//-----------------------------------------------------------------------
namespace PatientPortal.Entities
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using PatientPortal.Shared;    

    /// <summary>
    /// Represents entity converter.
    /// </summary>
    public sealed class EntityConverter
    {
        // Todo: Remove singleton access and make the class static
        // Note: ThreadStatic is added over Field here to ensure that each thread
        // has its unique instance of this class.
        // Since multiple simultaneous calls to this class may come, it is safe
        // to have a seperate copy of this functionality class
        [ThreadStatic]

        /// <summary>
        /// Instance of the entity converter class.
        /// </summary>
        private static EntityConverter instance;

        /// <summary>
        /// Instance to lock.
        /// </summary>
        private static string instanceLock = "LOCK";

        /// <summary>
        /// Prevents a default instance of the <see cref="EntityConverter"/> class from being created.
        /// </summary>
        private EntityConverter()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static EntityConverter Instance
        {
            get
            {
                // create object if not available
                if (EntityConverter.instance == null)
                {
                    lock (EntityConverter.instanceLock)
                    {
                        if (EntityConverter.instance == null)
                        {
                            EntityConverter.instance = new EntityConverter();
                        }
                    }
                }

                // return
                return EntityConverter.instance;
            }
        }

        /// <summary>
        /// Fills the DTO from entity.
        /// </summary>
        /// <param name="fromEntity">From entity.</param>
        /// <param name="dto">DTO to be filled.</param>
        public static void FillDtoFromEntity(EntityObject fromEntity, IDTO dto)
        {
            FillData(dto, fromEntity, false);
        }

        /// <summary>
        /// Fills the entity from DTO.
        /// </summary>
        /// <param name="fromDto">DTO from which the entity is to be filled.</param>
        /// <param name="toEntity">Entity to be filled.</param>
        public static void FillEntityFromDto(IDTO fromDto, EntityObject toEntity)
        {
            FillData(fromDto, toEntity, true);
        }

        /// <summary>
        /// Fills the data in entity from dto or in dto from entity
        /// depending on entityFromDto flag.
        /// </summary>
        /// <param name="dto">The dto that is filled by entity or is used to fill the entity.</param>
        /// <param name="entity">The entity that is filled from dto or is used to fill the dto.</param>
        /// <param name="entityFromDto">If private set to <c>true</c> [entity from dto].</param>
        private static void FillData(IDTO dto, EntityObject entity, bool entityFromDto)
        {
            var dtoType = dto.GetType();
            var entityType = entity.GetType();
            MappingType mappingType;

            if (!VerifyForEntityType(entityType, dtoType, out mappingType))
            {
                throw new EntityConversionException(string.Format(Thread.CurrentThread.CurrentCulture, "Entity type '{0}' must match with type specified in EntityMappingAttribute on type '{1}' !", entityType.ToString(), dtoType.ToString()));
            }

            var properties = dtoType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                bool skipThisProperty = false;
                object[] customAttributes = property.GetCustomAttributes(typeof(EntityPropertyMappingAttribute), false);
                if (customAttributes.Length == 0)
                {
                    continue;
                }

                foreach (object customAttribute in customAttributes)
                {
                    EntityPropertyMappingAttribute entityPropertyMappingAttribute = (EntityPropertyMappingAttribute)customAttribute;
                    if (entityPropertyMappingAttribute.MappingDirection == MappingDirectionType.None)
                    {
                        skipThisProperty = true;
                        break;
                    }
                }

                if (skipThisProperty)
                {
                    continue;
                }

                var entityPropertyName = GetEntityPropertyName(property, mappingType, entityFromDto);
                if (!string.IsNullOrEmpty(entityPropertyName))
                {
                    var entityProperty = entityType.GetProperty(entityPropertyName);

                    if (entityProperty == null)
                    {
                        throw new EntityConversionException(entityPropertyName, entity);
                    }

                    var sourceProperty = entityFromDto ? property : entityProperty;
                    var destinationProperty = entityFromDto ? entityProperty : property;
                    var sourceObject = entityFromDto ? (dto as object) : (entity as object);
                    var destinationObject = entityFromDto ? (entity as object) : (dto as object);
                    var sourceValue = sourceProperty.GetValue(sourceObject, null);
                    if (destinationProperty.CanWrite)
                    {
                        destinationProperty.SetValue(destinationObject, sourceValue, null);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the name of the entity property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="mappingType">Type of the mapping.</param>
        /// <param name="entityFromDTO">If set to <c>true</c> [entity from DTO].</param>
        /// <returns>Name of the property.</returns>
        private static string GetEntityPropertyName(PropertyInfo property, MappingType mappingType, bool entityFromDTO)
        {
            string entityPropertyName = string.Empty;
            var attribute =
                        (EntityPropertyMappingAttribute)
                        Attribute.GetCustomAttribute(property, typeof(EntityPropertyMappingAttribute));

            bool skipMapping = false;

            if (attribute != null)
            {
                if (entityFromDTO)
                {
                    skipMapping = !(attribute.MappingDirection == MappingDirectionType.EntityFromDTO || attribute.MappingDirection == MappingDirectionType.Both);
                }
                else
                {
                    skipMapping = !(attribute.MappingDirection == MappingDirectionType.DTOFromEntity || attribute.MappingDirection == MappingDirectionType.Both);
                }
            }

            switch (mappingType)
            {
                case MappingType.TotalExplicit:
                    if (attribute == null)
                    {
                        throw new EntityConversionException(string.Format(Thread.CurrentThread.CurrentCulture, "Property '{0}' should have EntityPropertyMappingAttribute !"), entityPropertyName);
                    }

                    if (skipMapping)
                    {
                        entityPropertyName = string.Empty;
                    }
                    else
                    {
                        entityPropertyName = attribute.MappedEntityPropertyName;
                    }

                    break;

                case MappingType.TotalImplicit:
                    if (attribute != null && skipMapping)
                    {
                        entityPropertyName = string.Empty;
                    }
                    else
                    {
                        entityPropertyName = property.Name;
                    }

                    break;

                case MappingType.Hybrid:
                    if (attribute == null)
                    {
                        entityPropertyName = property.Name;
                    }
                    else if (skipMapping)
                    {
                        entityPropertyName = string.Empty;
                    }
                    else
                    {
                        entityPropertyName = attribute.MappedEntityPropertyName;
                    }

                    break;

                default:
                    break;
            }

            return entityPropertyName;
        }

        /// <summary>
        /// Verifies the type of for entity.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="dtoType">Type of the DTO.</param>
        /// <param name="mappingType">Type of the mapping.</param>
        /// <returns>True or false.</returns>
        private static bool VerifyForEntityType(Type entityType, Type dtoType, out MappingType mappingType)
        {
            var attributes = dtoType.GetCustomAttributes(typeof(EntityMappingAttribute), false);
            if (attributes.Count() == 1)
            {
                var mappingAttribute = (EntityMappingAttribute)attributes[0];
                mappingType = mappingAttribute.MappingType;
                return mappingAttribute.MappedEntityTypeFullName.Equals(entityType.FullName);
            }
            else
            {
                throw new EntityConversionException("Only one EntityMappingAttribute can be applied on type '{0}' !", dtoType.ToString());
            }
        }
    }
}