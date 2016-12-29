using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xunit;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("SprocketWTW.Tests")]
[assembly: AssemblyTrademark("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("b7abf87d-2a5d-4ee3-8d40-7a59971daa21")]

// Due to the nature of StaticRegistrationCollection and XUnit's ability to run tests in parallel
// for tests within the same assembly, parallel test execution has been turned off here to 
// ensure cache registrations are clear before the next test is executed.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
