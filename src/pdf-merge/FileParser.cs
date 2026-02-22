namespace pdf_merge;

public class FileParser {

    /// <summary>
    /// Retrieves all PDF files from a specified directory and adds them to the referenced filePaths list.  
    /// Throws an exception in case the directory path is invalid.
    /// Prints a warning to the CLI in case the specified directory is empty.
    /// </summary>
    /// <param name="dirPath">Path to the directory to retrieve files from</param>
    /// <param name="filePaths">List to extend with the valid PDF file paths</param>
    /// <param name="enableVerbose">Enables verbose mode to print extra messages to console</param>
    /// <exception cref="IOException"></exception>
    public static void FilesFromDirectory(string dirPath, ref List<string> filePaths, bool enableVerbose) {
        // Check that the provided path is a valid path to a directory
        if (!Directory.Exists(dirPath)) {
            throw new IOException($"Cannot find directory: '{dirPath}'");
        }

        if (enableVerbose) ConsoleOutput.Print($"Retrieving PDF files from: {Path.GetDirectoryName(dirPath)}");

        // Get all files in the directory, sort them and copy only the PDF file paths
        string[] fileEntries = Directory.GetFiles(dirPath);
        Array.Sort(fileEntries);

        if (fileEntries.Length == 0) ConsoleOutput.Warning("No files found in directory");

        foreach (string file in fileEntries) {
            if (Path.GetExtension(file).Equals(".pdf")) {
                filePaths.Add(file);

                if (enableVerbose) ConsoleOutput.Print($"Added file {Path.GetFileName(file)}");
            }
            else {
                if (enableVerbose) ConsoleOutput.Print($"Rejected file {Path.GetFileName(file)}");
            }
        }

        if (enableVerbose) ConsoleOutput.Success($"Finished retrieving files from target directory");
    }

    /// <summary>
    /// Get the 15 most recently created PDFs in the specified directory.
    /// Throws an exception in case the directory path is invalid.
    /// </summary>
    /// <param name="dirPath">Path to the directory to retrieve files from</param>
    /// <param name="enableVerbose">Enables verbose mode to print extra messages to console</param>
    /// <returns>Ordered dictionary containing the PDFs. Key = 0 is the most recently created PDF and Key = 14 the oldest</returns>
    /// <exception cref="IOException"></exception>
    public static Dictionary<int, string> GetRecentPDFsFromDirectory(string dirPath, bool enableVerbose) {
        // Find all PDFs in the specified directory
        if (!Directory.Exists(dirPath)) {
            throw new IOException($"Cannot find directory: '{dirPath}'");
        }

        if (enableVerbose) ConsoleOutput.Print($"Retrieving PDF files from: {Path.GetDirectoryName(dirPath)}");
        IEnumerable<string> fileEntries = Directory.GetFiles(dirPath).OrderByDescending(d => new FileInfo(d).CreationTime).Reverse();

        if (fileEntries.Count() == 0) ConsoleOutput.Warning("No files found in directory");

        // Retrieve only PDFs and build dictionary
        var orderedPaths = new Dictionary<int, string>();
        int pdfCounter = 0;
        foreach (string file in fileEntries) {
            // Add if PDF
            if (Path.GetExtension(file).Equals(".pdf")) {
                orderedPaths.Add(pdfCounter, file);

                if (enableVerbose) ConsoleOutput.Print($"Added file {Path.GetFileName(file)}");

                // Increment file counter and break when 15 files have been fetched
                pdfCounter += 1;
                if (pdfCounter == 15) break;
            }
            else {
                if (enableVerbose) ConsoleOutput.Print($"Rejected file {Path.GetFileName(file)}");
            }
        }

        return orderedPaths;
    }

    /// <summary>
    /// Retrieve files from the ordered dictionary based on user input.
    /// </summary>
    /// <param name="orderedFilesById">Ordered dictionary containing the PDFs by their age</param>
    /// <param name="filePaths">List to extend with the valid PDF file paths</param>
    public static void FilesFromID(Dictionary<int, string> orderedFilesById, ref List<string> filePaths) {
        string? selectedFileIDs = Console.ReadLine();
        if (!string.IsNullOrEmpty(selectedFileIDs)) {
            foreach (string fileId in selectedFileIDs.Split(' ')) {
                int fileInt;

                // Error handling
                if (!int.TryParse(fileId, out fileInt)) {
                    ConsoleOutput.Warning($"Unknown ID: '{fileId}', skipping...");
                    continue;
                }

                if (fileInt > orderedFilesById.Count || fileInt < 0) {
                    ConsoleOutput.Warning($"ID: {fileInt} is out of bounds, skipping...");
                    continue;
                }

                // Add file
                filePaths.Add(orderedFilesById[fileInt]);
            }
        }
    }

    /// <summary>
    /// Validates that a list of specified files are PDF format and that the paths are valid.
    /// Throws an exception if a specified file is not PDF or if a path is invalid.
    /// </summary>
    /// <param name="files">List of file paths to validate</param>
    /// <param name="filePaths">List to extend with the valid PDF file paths</param>
    /// <param name="enableVerbose">Enables verbose mode to print extra messages to console</param>
    /// <exception cref="IOException"></exception>
    public static void FilesFromList(IEnumerable<string> files, ref List<string> filePaths, bool enableVerbose) {
        foreach (string file in files) {
            // Check that the file is PDF and exists
            if (!Path.GetExtension(file).Equals(".pdf")) {
                throw new IOException($"'{Path.GetFileName(file)}' is not a PDF file");
            }

            if (!File.Exists(file)) {
                throw new IOException($"Cannot find file: '{Path.GetFileName(file)}'");
            }

            // Save file path
            filePaths.Add(file);

            if (enableVerbose) ConsoleOutput.Print($"Added file {Path.GetFileName(file)}");
        }

        if (enableVerbose) ConsoleOutput.Success($"Retrieved all specified files");
    }

}
