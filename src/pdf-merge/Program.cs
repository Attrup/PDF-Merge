using CommandLine;

namespace pdf_merge {
    class Program {
        public class Options {
            [Option('d', "directory", Required = false, HelpText = "Specify a directory from which to retrieve all PDF files (Files are sorted lexicographically before combining).")]
            public string? Directory { get; set; }

            [Option('f', "files", Required = false, HelpText = "Specify PDF file paths individually (Accepts multiple file paths separated by spaces) (Files are combined in the order they are specified).")]
            public IEnumerable<string>? Files { get; set; }

            [Option('o', "output", Required = false, HelpText = "Set the output file name without file extension (Default is 'combined').")]
            public string? Output { get; set; }

            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }

            static void Main(string[] args) {
                Parser.Default.ParseArguments<Options>(args)
                       .WithParsed<Options>(o => {

                       });
            }
        }
    }
}