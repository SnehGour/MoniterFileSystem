using static System.Console;
using System.IO;
public class FileProcessor
{
    private string filePath;
    private static readonly string BackUpDirectoryName = "backup";
    private static readonly string InprogressDirectoryName = "inprogress";
    private static readonly string CompletedDirectoryName = "completed";
    public string InputFilePath { get; }
    public FileProcessor(string filePath)
    {
        InputFilePath = filePath;
    }
    public void Process()
    {
        WriteLine($"Begin Process of {InputFilePath}");

        // Check if file exists
        if(!File.Exists(InputFilePath))
        {
            WriteLine($"Error: file doesnt exists");
        }

        string rootDirectoryPath = new DirectoryInfo(InputFilePath).Parent.Parent.FullName;
        WriteLine($"Root data path is {rootDirectoryPath}");

        // Check if the backup dir exists
        string inputFileDirPath = Path.GetDirectoryName(InputFilePath);
        string backupDirPath = Path.Combine(rootDirectoryPath, BackUpDirectoryName);

        if(!File.Exists(InputFilePath))
        {
            WriteLine($"File not Found in {inputFileDirPath}");
            return;
        }

        if(!Directory.Exists(backupDirPath))
        {
            WriteLine($"Creating {backupDirPath}");
            Directory.CreateDirectory(backupDirPath);
        }


        // Create backup: Copying files to backup directory
        string inputFilePath = Path.GetFileName(InputFilePath);
        string backupFilePath = Path.Combine(backupDirPath, inputFilePath);
        WriteLine($"Copying {InputFilePath} to {backupFilePath}");
        File.Copy(InputFilePath, backupFilePath, true);     // true: Override the file


        // Creating InProgress Directory
        string inProgressDirPath = Path.Combine(rootDirectoryPath,InprogressDirectoryName);
        if(!Directory.Exists(inProgressDirPath))
        {
            WriteLine($"Creating {inProgressDirPath}");
            Directory.CreateDirectory(inProgressDirPath);
        }

        // Move input file to inprogess

        // Check if file already there
        string inprogressFilePath = Path.Combine(inProgressDirPath, inputFilePath);
        if(File.Exists(inprogressFilePath))
        {
            WriteLine($"File is Already Processed !");
            return;
        }

        WriteLine($"Moving File {inprogressFilePath} to {inprogressFilePath}");
        File.Move(InputFilePath,inprogressFilePath);


        // Once process is completed -> moving it to completed dir
        string completedDirPath = Path.Combine(rootDirectoryPath, CompletedDirectoryName);

        if(!Directory.Exists(completedDirPath))
        {
            WriteLine($"Creating {CompletedDirectoryName} at {completedDirPath}");
            Directory.CreateDirectory(completedDirPath);
        }

        string completedFilePath = Path.Combine(completedDirPath, inputFilePath);
        WriteLine($"Moving  file from {inprogressFilePath} to {completedFilePath}");
        File.Move(inprogressFilePath, completedFilePath);

        Directory.Delete(inProgressDirPath , true);
    }
}