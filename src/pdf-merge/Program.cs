using CommandLine;

namespace pdf_merge {
    class Program {
        public class Options {
            [Option('d', "directory", Required = false, HelpText = "Use this to specify a directory from which to retrieve all PDF files.")]
            public string? Directory { get; set; }

            [Option('f', "files", Required = false, HelpText = "Use this to specify PDF file paths individually (accepts multiple file paths separated by spaces).")]
            public IEnumerable<string>? Files { get; set; }

            [Option('o', "output", Required = false, HelpText = "Use this to specify the output file name.")]
            public string? Output { get; set; }


            static void Main(string[] args) {
                Parser.Default.ParseArguments<Options>(args)
                       .WithParsed<Options>(o => {

                       });
            }
        }
    }
}