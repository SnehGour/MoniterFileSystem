using static System.Console;
public class Program
{
    public static void Main(string[]args)
    {
        WriteLine("Parsing COmmand Line Options");

        var commands = args[0];
        if(commands == "--file")
        {
            var filePath = args[1];
            WriteLine($"Single file {filePath} selected");
            ProcessSingleFile(filePath);
        }
        else if(commands == "--dir")
        {
            var directoryPath = args[1];
            var fileType = args[2];
            WriteLine($"Directory Path {directoryPath} selected for {fileType} files");
            ProcessDirectory(directoryPath, fileType);
        }
        else
        {
            WriteLine("Invalid Command line options");
        }
        WriteLine("Press enter to quit");
        ReadLine();
    }
    private static void ProcessSingleFile(string filePath)
    {
        var fileProcessor = new FileProcessor(filePath);
        fileProcessor.Process();
    }
    private static void ProcessDirectory(string directoryPath,string filePath)
    {
        var allFiles = Directory.GetFiles(directoryPath);
    }

}