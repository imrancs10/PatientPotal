//-----------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the AssemblyInfo file.</summary>
//-----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("PatientPortal.Shared")]
[assembly: AssemblyDescription("Shared Library consisting Framework components and contracts")]
[assembly: ComVisible(false)]
[assembly: Guid("a104cbbb-fbdd-42f1-b2b5-607e41df1309")]
[module:
    SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames",
        Justification = "Assemblies Should Have Valid Strong Names")]
[module:
    SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "namespace",
        Target = "PatientPortal.Shared", MessageId = "Shared", Justification = "Identifiers Should Not Match Keywords")]
[module:
    SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Scope = "namespace",
        Target = "PatientPortal.Shared.Factories", MessageId = "Shared",
        Justification = "Identifiers Should Not Match Keywords")]
[assembly: AssemblyCompanyAttribute("Microsoft")]
[assembly: AssemblyProductAttribute("PatientPortal.Shared")]
[assembly: AssemblyCopyrightAttribute("Copyright © Microsoft 2013")]
[assembly: AssemblyVersionAttribute("1.0.0.0")]
[assembly: AssemblyFileVersionAttribute("1.0.0.0")]
[module: SuppressMessage("Microsoft.Design", "CA1014:MarkAssembliesWithClsCompliant", Justification = "ClsCompliant")]