using FoodDeliveryTracking.Services.Logger;
using System.Reflection;

namespace FoodDeliveryTracking.TestProject.IntegrationTests
{
    public class AllMethodsTests
    {
        /// <summary>
        /// This unit test class verifies that all methods in the project that return a Task have names ending with 'Async'.
        /// It scans all classes and methods within the project's assembly and checks method names for the 'Async' suffix.
        /// </summary>
        [Fact]
        public void AllAsyncMethodsShouldEndWithAsync()
        {
            // Get the assembly of your project
            var assembly = Assembly.GetAssembly(typeof(FoodDeliveryTracking.Program));

            // Scan the classes in the assembly
            var types = assembly!.GetTypes();

            foreach (var type in types)
            {
                // Scan public and non-public instance methods of the classes
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var method in methods)
                {
                    // Check if the method returns a Task
                    if (typeof(Task).IsAssignableFrom(method.ReturnType))
                    {
                        // Verify that the method name ends with 'Async'
                        Assert.True(method.Name.EndsWith("Async"), $"Method {method.Name} in class {type.Name} does not end with 'Async'.");
                    }
                }
            }
        }

        /// <summary>
        /// Verifies that custom classes in specified namespaces have an ILoggerManager field.
        /// </summary>
        [Fact]
        public void CustomClassesShouldHaveILoggerManagerField()
        {
            // Get the assembly of your project
            var assembly = Assembly.GetAssembly(typeof(FoodDeliveryTracking.Program));

            // Define namespaces for your custom classes
            var customNamespaces = new List<string>
                {
                    "FoodDeliveryTracking.Config",
                    "FoodDeliveryTracking.Controllers",
                    "FoodDeliveryTracking.Data",
                    "FoodDeliveryTracking.Services"
                };

            // Define namespaces to be excluded
            var excludedNamespaces = new List<string>
                {
                    "FoodDeliveryTracking.Services.Logger",
                    "FoodDeliveryTracking.Services.Models",
                    "FoodDeliveryTracking.Services.Register",
                    "FoodDeliveryTracking.Data.Register",
                    "FoodDeliveryTracking.Data.Models",
                    "FoodDeliveryTracking.Data.Context",
                    "FoodDeliveryTracking.Config",
                };

            // Scan all classes in the assembly
            var types = assembly!.GetTypes();

            List<Type> classesWithoutLogger = new List<Type>();

            foreach (var type in types)
            {
                // Check if the class belongs to one of the custom namespaces
                if (type.IsClass && customNamespaces.Any(ns => type.FullName?.StartsWith(ns) == true) &&
                    !excludedNamespaces.Any(ns => type.FullName?.StartsWith(ns) == true) &&
                    !(type.FullName?.Contains($"+<>c") == true ||
                    type.FullName?.Contains($"d__") == true ||
                    type.FullName?.Contains($"+") == true))
                {
                    // Check if the class contains an ILoggerManager field
                    var loggerManagerField = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                        .FirstOrDefault(field => field.FieldType == typeof(ILoggerManager));

                    if (loggerManagerField == null)
                    {
                        // Add the class to the list of classes without ILoggerManager
                        classesWithoutLogger.Add(type);
                    }
                }
            }

            // Check if there are classes without ILoggerManager and fail if there are any
            if (classesWithoutLogger.Any())
            {
                foreach (var type in classesWithoutLogger)
                {
                    Assert.True(false, $"Class '{type.FullName}' does not have an ILoggerManager field.");
                }
                Assert.True(false, "Some custom classes do not have an ILoggerManager field.");
            }
        }

    }
}
