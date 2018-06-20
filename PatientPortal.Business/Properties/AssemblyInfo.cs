//-----------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the AssemblyInfo.cs file.</summary>
//-----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// private set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("PatientPortal.Business")]
[assembly: AssemblyDescription("This assembly constains implementation for Data Access, DB Access, Facade, Model & Business Domain")]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("524272a7-07d7-48c7-bc25-f5da170d489e")]
[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Justification = "Done intentially.")]
[module: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Justification = "Assemblies Should Have Valid Strong Names")]
[module: SuppressMessage("Microsoft.Design", "CA1014:MarkAssembliesWithClsCompliant", Justification = "Mark Assemblies With Cls Compliant")]
[module: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "General Exception is handled for wrapping to more specific exception")]
[assembly: NeutralResourcesLanguageAttribute("en")]
[assembly: AssemblyCompanyAttribute("Microsoft")]
[assembly: AssemblyProductAttribute("PatientPortal.Business")]
[assembly: AssemblyCopyrightAttribute("Copyright © Microsoft 2013")]
[assembly: AssemblyVersionAttribute("1.0.0.0")]
[assembly: AssemblyFileVersionAttribute("1.0.0.0")]
[assembly: ComVisible(false)]