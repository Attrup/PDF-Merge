using System;
using System.IO;

namespace pdf_merge;

public class FileParser {
    public static void FilesFromDirectory(string dirPath, ref List<string> filePaths) {
        // Check that the provided path is a valid path to a directory
        if (!Directory.Exists(dirPath)) {
            throw new IOException($"The provided directory path does not exist!\nPath: '{dirPath}'");
        }

        // Get all files in the directory and copy the PDF file paths
        string[] fileEntries = Directory.GetFiles(dirPath);
        Array.Sort(fileEntries);

        foreach (string file in fileEntries) {
            if (Path.GetExtension(file).Equals(".pdf")) {
                filePaths.Add(file);
            }
        }
    }

}
