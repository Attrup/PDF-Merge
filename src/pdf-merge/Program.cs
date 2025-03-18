using System;
using System.Linq;
using CommandLine;
using CommandLine.Text;

namespace pdf_merge {
    class Program {
        public class Options {
            // Define command line arguments
            [Option('d', "directory", Required = false, HelpText = "Set the directory to retrieve all PDF files from (Files are sorted lexicographically before combining).")]
            public string? Directory { get; set; }

            [Value(0, MetaName = "directory", Required = false, Hidden = true)]
            public string? PositionalDirectory { get; set; }

            [Option('f', "files", Required = false, HelpText = "Specify individual PDF file paths (Accepts multiple file paths separated by spaces) (Files are combined in the order they are specified).")]
            public IEnumerable<string>? Files { get; set; }

            [Option('o', "output", Default = "combined", Required = false, HelpText = "Set output file name without file extension.")]
            public string? Output { get; set; }

            [Option('v', "verbose", Default = false, Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }


            static void Main(string[] args) {
                // Print help in case no args are passed
                if (args.Length == 0) {
                    args = ["--help"];
                }

                Parser.Default.ParseArguments<Options>(args)
                       .WithParsed<Options>(o => {
                           ConsoleOutput.Print("PDF-Merge");
                           List<string> filePaths = [];

                           // If a directory is specified, add the PDF files to list of file paths
                           string? dir = o.PositionalDirectory ?? o.Directory;
                           if (dir != null) {
                               try {
                                   FileParser.FilesFromDirectory(dir, ref filePaths, o.Verbose);
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

                           // Combine PDF files
                           if (filePaths.Count != 0) {
                               string fileName = o.Output != null ? o.Output : "combined";
                               FileMerger.MergeFiles(filePaths, fileName, o.Verbose);
                           }
                           else {
                               ConsoleOutput.Error("No PDF files found");
                           }
                       });


            }
        }
    }
}