using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace FoodDeliveryTracking.TestProject.IntegrationTests
{
    /// <summary>
    /// This class is a test class for the nomenclature of controllers.
    /// </summary>
    public class ControllersTests
    {
        /// <summary>
        /// This method checks that all classes that inherit from `Controller` end in "Controller".
        /// </summary>
        [Fact]
        public void ControllersNaming()
        {
            // Get the assembly of your project
            var assembly = Assembly.GetAssembly(typeof(FoodDeliveryTracking.Program));

            // Scan all classes in the assembly
            int? controllerTypesCount = assembly?.GetTypes().Count(type => typeof(Controller).IsAssignableFrom(type) && type.IsPublic);
            int? filesControllersCount = assembly?.GetTypes().Count(file => file.FullName!.EndsWith("Controller"));

            if (controllerTypesCount.HasValue && filesControllersCount.HasValue)
            {
                Assert.True(controllerTypesCount.Value.Equals(filesControllersCount.Value), $"All classes that inherit from Controller are " +
                    $"identified as Controller at the end of their name.");
            }
            else
            {
                Assert.Fail($"The test for checking class names that inherit from Controller could not be completed.");
            }
        }

        /// <summary>
        /// Verifies that all methods in controllers have the correct attributes.
        /// </summary>
        [Fact]
        public void MethodsControllersAtributes()
        {
            // Get the assembly of your project
            var assembly = Assembly.GetAssembly(typeof(FoodDeliveryTracking.Program));

            // Scan all classes in the assembly
            var controllerTypes = assembly!.GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && type.IsPublic);

            foreach (var controllerType in controllerTypes)
            {
                // Scan all public methods in the controller class
                var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(method => method.DeclaringType == controllerType);

                foreach (var method in methods)
                {
                    // Check if the method has any Http attribute (HttpGet, HttpPost, etc.)
                    var hasHttpAttribute = method.GetCustomAttributes().Any(attr => IsHttpAttribute(attr.GetType()));

                    // Check if the method has the [Route] attribute
                    var hasRouteAttribute = method.GetCustomAttributes().OfType<RouteAttribute>().Any();

                    // Verify that both attributes are present
                    Assert.True(hasHttpAttribute && hasRouteAttribute, $"Method {method.Name} in controller {controllerType.Name} must have an Http attribute and [Route].");
                }
            }
        }

        private bool IsHttpAttribute(Type attributeType)
        {
            // Check if the attribute type is a derived class of HttpMethodAttribute
            return typeof(HttpMethodAttribute).IsAssignableFrom(attributeType);
        }

        /// <summary>
        /// Verifies that all methods in controllers have return type is Task<IActionResult>.
        /// </summary>
        [Fact]
        public void AllEndpointsShouldHaveReturnTaskIActionResult()
        {
            // Get the assembly of your project
            var assembly = Assembly.GetAssembly(typeof(FoodDeliveryTracking.Program));

            // Scan all classes in the assembly
            var controllerTypes = assembly!.GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && type.IsPublic);

            foreach (var controllerType in controllerTypes)
            {
                // Scan all public methods in the controller class
                var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(method => method.DeclaringType == controllerType);

                foreach (var method in methods)
                {
                    // Check the return type of the method
                    var returnType = method.ReturnType;

                    // Ensure that the return type is Task<IActionResult>
                    Assert.True(returnType == typeof(Task<IActionResult>), $"Method {method.Name} in controller {controllerType.Name} must have the return type Task<IActionResult>.");
                }
            }
        }
    }
}
