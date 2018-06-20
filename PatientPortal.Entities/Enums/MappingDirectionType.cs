//-----------------------------------------------------------------------
// <copyright file="MappingDirectionType.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the MappingDirectionType.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Entities
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Mapping Direction Type.
    /// </summary>
    public enum MappingDirectionType
    {
        /// <summary>
        /// None mapping direction type.
        /// </summary>
        None,

        /// <summary>
        /// DTO from entity.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "DTO is to be cased in this way only.")]
        DTOFromEntity, // DB (Entity) to UI (DTO)

        /// <summary>
        /// Entity from DTO.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "DTO is to be cased in this way only.")]
        EntityFromDTO, // UI (DTO) to DB (Entity)

        /// <summary>
        /// Both mapping directions.
        /// </summary>
        Both
    }
}