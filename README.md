# PDF-Merge
![Tests](https://github.com/Attrup/PDF-Merge/actions/workflows/unit-tests.yml/badge.svg)

PDF-Merge is a simple and easy-to-use CLI tool for merging PDF files, created as part of learning the C# language. To use the tool, you must obtain a binary by either downloading the [latest release](https://github.com/Attrup/PDF-Merge/releases) or building it locally and add it to your system path. Follow the instructions under [Installation](#installation) to get started 🙂

## User Guide

Once added to the system path, the tool can be called from anywhere using:

```bash
pdf-merge <arguments>
```

### CLI Arguments

- `-d, --directory`  
  Merge all PDF files from a specified directory (sorted lexicographically before combining).
- `-f, --files`  
  Specify individual PDF file paths (accepts multiple file paths separated by spaces). Files are combined in the order they are listed.
- `-o, --output` (Default: `combined`)  
  Set the output file name (without file extension).
- `-v, --verbose` (Default: `false`)  
  Enable verbose output.
- `--help`  
  Display the help screen.

It is possible to specify both a directory and a list of files. In this case, all files from the directory will be merged first, followed by the specified files in the given order.

### Examples

#### Merge All PDF Files from a Directory  
Merge all PDFs in the current directory:

```bash
pdf-merge --directory . --output MergedFile
pdf-merge -d . -o MergedFile
```

Merge PDFs from another directory:

```bash
pdf-merge --directory ../../PDFSlides/Week_4 --output Week_4_Slides
pdf-merge -d ../../PDFSlides/Week_4 -o Week_4_Slides
```

👉 **Note:** The output file will be saved in the directory where the command is executed.

#### Merge Selected PDF Files  
Merge specific files:

```bash
pdf-merge --files file1.pdf file2.pdf file3.pdf --output Week_1
pdf-merge -f file1.pdf file2.pdf file3.pdf -o Week_1
```

## Program Output

### Standard Output  
Without verbose mode, the tool provides minimal output:

```console
$ pdf-merge --directory . --output MergedFile
PDF-Merge
Merging PDF files   [####################] Done!
```

### Verbose Mode  
With verbose mode enabled, the tool provides detailed feedback:

```console
$ pdf-merge --directory PDFSlides/Week_1
PDF-Merge
Retrieving PDF files from: PDFSlides/Week_1
Rejected file: notes.txt
Added file: day1_slides.pdf
Added file: day2_slides.pdf
Added file: day3_slides.pdf
Added file: day4_slides.pdf
Added file: day5_slides.pdf
SUCCESS: Finished retrieving files from target directory
Merging PDF files   [####################] Done!
SUCCESS: PDF files merged and saved as combined.pdf
```

## Installation

To use PDF-Merge, either download the [latest release](https://github.com/Attrup/PDF-Merge/releases) or build it locally, and add the obtained binary to your system path.

### Build Instructions
#### Requirements
- .NET 8 or newer (older versions may work but are untested).

#### Steps
1. Clone the repository:
   ```bash
   git clone <repository_url>
   cd PDF-Merge
   ```
2. Build a release version:
   ```bash
   dotnet publish src/pdf-merge/pdf-merge.csproj -c Release -o bin/pdf-merge
   ```
3. Locate the built tool in:
   ```
   bin/pdf-merge
   ```
4. Move the `pdf-merge` binary to a permanent location and add it to your system path.

After completing these steps, you can use the `pdf-merge` command from any terminal.
