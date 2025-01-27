using System;
using System.Linq;
using CommandLine;

namespace pdf_merge {
    class Program {
        public class Options {
            // Define command line arguments
            [Option('d', "directory", Required = false, HelpText = "Set directory to retrieve all PDF files from (Files are sorted lexicographically before combining).")]
            public string? Directory { get; set; }

            [Option('f', "files", Required = false, HelpText = "Specify individual PDF file paths (Accepts multiple file paths separated by spaces) (Files are combined in the order they are specified).")]
            public IEnumerable<string>? Files { get; set; }

            [Option('o', "output", Required = false, HelpText = "Set output file name without file extension (Default is 'combined').")]
            public string? Output { get; set; }

            [Option('v', "verbose", Default = true, Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }


            static void Main(string[] args) {
                List<string> filePaths = [];

                Parser.Default.ParseArguments<Options>(args)
                       .WithParsed<Options>(o => {
                           if (o.Directory != null) {
                               FileParser.FilesFromDirectory(o.Directory, ref filePaths);
                           }
                           if (o.Files != null && o.Files.Any()) {
                               FileParser.FilesFromList(o.Files, ref filePaths);
                           }

                           System.Console.WriteLine(o.Output);
                       });
            }
        }
    }
}