# PDF-Merge
PDF-Merge is a simple and easy-to-use CLI tool for merging PDF files, created as part of learning the C# language. To use the tool, you must build it locally and add it to your system path. Follow the instructions under [Installation](README.md#installation) to get started 🙂

## User Guide
Once installed and added to the system path, the tool can be called from anywhere using:

```bash
pdf-merge <arguments>
```
### CLI Arguments
- `-d, --directory`: Set the directory to retrieve all PDF files from (Files are sorted lexicographically before combining).
- `-f, --files`: Specify individual PDF file paths (Accepts multiple file paths separated by spaces) (Files are combined in the order they are specified).
- `-o, --output`       (Default: combined) Set output file name without file extension.
- `-v, --verbose`      (Default: false) Set output to verbose messages.
- `--help`             Display help screen.

It is possible to specify both a directory and a list of files to combine. In this case all the files from the directory will be merged first with the specified files being appended in the order they are specified.

### Example uses:
#### Merge All PDF Files from Directory:  
All PDF files from the current directory can be merged using either of the two equivalent commands:
```bash
pdf-merge --directory . -output MergedFile
pdf-merge --d. -o MergedFile

```
Alternatively the PDF files from another directory can be merged using either of the two equivalent commands:
```bash
pdf-merge --directory ../../PDFSlides/Week_4 -output Week_4_Slides
pdf-merge --d ../../PDFSlides/Week_4 -o Week_4_Slides
```
Note that in the above example the combined PDF will be saved at the location of the terminal when the command is executed.

#### Merge Selected PDF Files: 
Merging only a selection of PDF files can be accomplished using either of the two equivalent commands:
```bash
pdf-merge --files file1.pdf file2.pdf file3.pdf -output Week_1
pdf-merge --f file1.pdf file2.pdf file3.pdf -o Week_1
```

### Program Output
When calling the tool without verbose mode, the output will be kept to a minium, simply showing the progress of merging the files in a fashion similar to the one shown below:
```console
$ pdf-merge --directory . -output MergedFile
PDF-Merge
Merging PDF files   [####################] Done!
```

In case verbose mode is enabled, the program will provide significantly more output to inform of the current status and what files have been detected as shown below:
```console
$ pdf-merge --directory PDFSlides/Week_1
PDF-Merge
Retrieving PDF files from: PDFSlides/Week_1
Rejected file notes.txt
Added file day1_slides.pdf
Added file day2_slides.pdf
Added file day3_slides.pdf
Added file day4_slides.pdf
Added file day5_slides.pdf
SUCCESS: Finished retrieving files from target directory
Merging PDF files   [####################] Done!
SUCCESS: PDF files merged and saved as combined.pdf
```

## Installation
This tool must be built locally and added to your system path in order to be called from anywhere as a command. The project requires .NET 8 or newer, but way work with older versions by downgrading the project, however this has not been tested!

To build the project, clone the repository to a directory of your choice, and build a Release version of the project by running the following command while in the project root:
```bash
dotnet publish -c Release
```

Following this command, a release version of the tool can be found in the folder `src/pdf-merge/bin/Release/publish`. To add the tool to your system path, simply copy the publish folder to an appropriate permanent location on your system, and rename it to pdf-merge. then add the pdf-merge folder to your system path. Following this the `pdf-merge` command will be available anywhere in your terminal of choice.
