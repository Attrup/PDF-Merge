using System;
using System.IO;

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
