using CommandLine;

namespace pdf_merge {
    class Program {
        public class Options {
            // Define command line arguments
            [Option('d', "directory", Required = false, HelpText = "Set the directory to retrieve all PDF files from (Files are sorted lexicographically before combining).")]
            public string? Directory { get; set; }

            [Value(0, MetaName = "directory", Required = false, Hidden = true)]
            public string? PositionalDirectory { get; set; }

            [Option('f', "files", Required = false, HelpText = "Specify individual PDF file paths (Accepts multiple file paths separated by spaces). Files are combined in the order they are specified.")]
            public IEnumerable<string>? Files { get; set; }

            [Option('o', "output", Default = "combined", Required = false, HelpText = "Set output file name without file extension.")]
            public string? Output { get; set; }

            [Option('r', "recent", Default = false, Required = false, HelpText = "List the recently modified PDFs in the current directory (up to 15), and select which ones to merge.")]
            public bool Recent { get; set; }

            [Option('v', "verbose", Default = false, Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }


            static void Main(string[] args) {
                // Print help in case no args are passed
                if (args.Length == 0) {
                    args = ["--help"];
                }

                Parser
                    .Default
                    .ParseArguments<Options>(args)
                    .WithParsed<Options>(o => {
                        ConsoleOutput.Print("PDF-Merge");
                        List<string> filePaths = [];
                        try {
                            // If the "Recent" option is used, let user choose what to merge from recent files
                            if (o.Recent) {
                                // Retrieve and list the recently modified PDFs from the current directory
                                Dictionary<int, string> filesById = FileParser.GetRecentPDFsFromDirectory(".", o.Verbose);

                                if (filesById.Count != 0) {
                                    ConsoleOutput.PrintFileDictionary(filesById);
                                    FileParser.FilesFromID(filesById, ref filePaths);
                                }
                            }

                            // If a directory is specified, add the PDFs from the specified (or current, if no argument is specified) to list of file paths
                            string? dir = o.PositionalDirectory ?? o.Directory;
                            if (dir != null) {
                                FileParser.FilesFromDirectory(dir, ref filePaths, o.Verbose);
                            }

                            // If files are specified, add them to the list of file paths
                            if (o.Files != null && o.Files.Any()) {
                                FileParser.FilesFromList(o.Files, ref filePaths, o.Verbose);
                            }
                        }
                        catch (Exception e) {
                            ConsoleOutput.Error(e.Message);
                            return;
                        }

                        // Finally, combine all PDFs
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