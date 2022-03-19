using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Ahlem.Fahem.FeatureMatching.Tests;

public class FeatureMatchingUnitTest
{
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath();
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in
                 Directory.EnumerateFiles(Path.Combine(executingPath, "Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }
        var objectImageData = await File.ReadAllBytesAsync(Path.Combine(executingPath, "Ahlem-Fahem-object.jpg"));
        var detectObjectInScenesResults = await new ObjectDetection().DetectObjectInScenes(objectImageData, imageScenesData);
        Assert.Equal("[{\"X\":5290,\"Y\":-16355},{\"X\":-3611,\"Y\":-998},{\"X\":810,\"Y\":5678},{\"X\":15286,\"Y\":2473}]",JsonSerializer.Serialize(detectObjectInScenesResults[0].Points));
        Assert.Equal("[{\"X\":10662,\"Y\":-4568},{\"X\":1781,\"Y\":-961},{\"X\":1476,\"Y\":1792},{\"X\":4683,\"Y\":3577}]",JsonSerializer.Serialize(detectObjectInScenesResults[1].Points));
    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath =
            Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}