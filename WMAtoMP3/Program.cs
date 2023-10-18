using System;
using NAudio.Wave;
using System.IO;
using NAudio.Lame;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the path to the folder containing WMA files: ");
        string inputFolder = Console.ReadLine();

        Console.Write("Enter the path to the output folder for MP3 files: ");
        string outputFolder = Console.ReadLine();

        ConvertWmaToMp3(inputFolder, outputFolder);

        Console.WriteLine("Conversion completed.");
    }

    static void ConvertWmaToMp3(string inputFolder, string outputFolder)
    {
        Directory.CreateDirectory(outputFolder);

        foreach (string wmaFilePath in Directory.GetFiles(inputFolder, "*.wma", SearchOption.AllDirectories))
        {
            string outputFilePath = Path.Combine(outputFolder, Path.ChangeExtension(Path.GetFileName(wmaFilePath), ".mp3"));

            using (var reader = new MediaFoundationReader(wmaFilePath))
            using (var writer = new LameMP3FileWriter(outputFilePath, reader.WaveFormat, 128))
            {
                reader.CopyTo(writer);
            }

            Console.WriteLine($"Converted: {wmaFilePath} -> {outputFilePath}");
        }
    }
}
