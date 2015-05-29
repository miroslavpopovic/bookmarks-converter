using System;
using System.IO;
using Fclp;

namespace CodeMind.BookmarksConverter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var parser = new FluentCommandLineParser<ApplicationArguments>();

            parser.Setup(arg => arg.Type)
                .As('t', "type")
                .SetDefault(FormatType.Wdr)
                .WithDescription("Specify the type of the input. By default it's WDR (Web Development Resource HTML file).");

            parser.Setup(arg => arg.File)
                .As('f', "file")
                .Required()
                .WithDescription("Input file to parse and convert. Use either the full or relative path.");

            parser.Setup(arg => arg.Output)
                .As('o', "output")
                .Required()
                .WithDescription("Output JSON file to save the conversion result. Use either the full or relative path.");

            parser.SetupHelp("?", "h", "help")
                .Callback(text => Console.WriteLine(text))
                .UseForEmptyArgs()
                .WithHeader("Converts bookmarks from various formats to the JSON file that can be read by Backmark application.");

            var result = parser.Parse(args);

            if (result.HasErrors)
                Console.WriteLine(result.ErrorText);
            else
            {
                var arguments = parser.Object;

                arguments.File = EnsureFullPath(arguments.File);
                arguments.Output = EnsureFullPath(arguments.Output);
                // TODO: Do the actual conversion here...
            }

            Console.WriteLine("Press Enter to finish");
            Console.ReadLine();
        }

        private static string EnsureFullPath(string fileName)
        {
            if (Path.IsPathRooted(fileName)) return fileName;

            var root = Path.GetDirectoryName(typeof (Program).Assembly.Location);

            // ReSharper disable AssignNullToNotNullAttribute
            return Path.Combine(root, fileName);
            // ReSharper restore AssignNullToNotNullAttribute
        }
    }
}
