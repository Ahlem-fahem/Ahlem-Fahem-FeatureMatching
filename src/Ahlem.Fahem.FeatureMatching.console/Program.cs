// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Ahlem.Fahem.FeatureMatching;

static class MyConsoleProgram
{
    static async Task Main(string[] args)
    {
        byte[] img = await File.ReadAllBytesAsync(args[0]);
        
        var imgScenesData = new List<byte[]>();
        foreach (var imgPath in Directory.EnumerateFiles(args[1]))
        {
            var imgBytes = await File.ReadAllBytesAsync(imgPath);
            imgScenesData.Add(imgBytes);
        }

        var objectDetection = new ObjectDetection();

        var featureMatchingResults = await objectDetection.DetectObjectInScenes(img,imgScenesData);
        
        foreach (var objectDetectionResult in featureMatchingResults)
        {
            System.Console.WriteLine($"Points:{JsonSerializer.Serialize(objectDetectionResult.Points)}");
        }
    }
}