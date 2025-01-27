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
                ConsoleOutput.Print("PDF-Merge");
                List<string> filePaths = [];

                Parser.Default.ParseArguments<Options>(args)
                       .WithParsed<Options>(o => {
                           // If directory is specified, add the PDF files to list of file paths
                           if (o.Directory != null) {
                               try {
                                   FileParser.FilesFromDirectory(o.Directory, ref filePaths, o.Verbose);
                               }
                               catch (Exception e) {
                                   ConsoleOutput.Error(e.Message);
                                   return;
                               }
                           }

                           // If files are specified, add them to the list of file paths
                           if (o.Files != null && o.Files.Any()) {
                               try {
                                   FileParser.FilesFromList(o.Files, ref filePaths, o.Verbose);
                               }
                               catch (Exception e) {
                                   ConsoleOutput.Error(e.Message);
                                   return;
                               }
                           }
                       });
            }
        }
    }
}