using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FoodDeliveryTracking.TestProject.IntegrationTests
{
    /// <summary>
    /// This test ensures that there are no pending 'TODO' comments in the codebase.
    /// It scans all .cs files in the main project directory and its subdirectories
    /// to check for 'TODO' comments and fails the test if any are found.
    /// </summary>
    public class TodoCommentsTests
    {
        /// <summary>
        /// This unit test verifies that no pending TODO comments exist in the codebase.
        /// </summary>
        [Fact]
        public void NoPendingTODOCommentsShouldExist()
        {
            // Get the location of the current test assembly
            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.FullName;
            solutionDirectory = solutionDirectory.Replace("\\FoodDeliveryTracking.TestProject\\bin", "");

            // Scan .cs files in the main project directory and its subdirectories
            var syntaxTrees = Directory.GetFiles(solutionDirectory, "*.cs", SearchOption.AllDirectories)
                .Select(file => CSharpSyntaxTree.ParseText(File.ReadAllText(file)));

            var todoComments = syntaxTrees
                .SelectMany(tree => tree.GetRoot()
                    .DescendantTrivia()
                    .Where(trivia => trivia.IsKind(SyntaxKind.SingleLineCommentTrivia) && trivia.ToString().ToLower().Contains("todo")))
                .ToList();

            foreach (var comment in todoComments)
            {
                Console.WriteLine($"TODO comment found: {comment}");
            }

            Assert.Empty(todoComments);
        }

    }
}
