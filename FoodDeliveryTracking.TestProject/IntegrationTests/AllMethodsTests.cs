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
    }

}
