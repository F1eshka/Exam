using EkzamenADO.Converters;
using System.Globalization;

public class ImagePathConverterTests
{
    private readonly ImagePathConverter _converter;

    public ImagePathConverterTests()
    {
        ImagePathConverter.TestMode = true; 
        _converter = new ImagePathConverter();
    }

    [Fact]
    public void Convert_ShouldReturnPath_WhenFileExists()
    {
        string fileName = "test-img.png";
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", fileName);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        File.WriteAllBytes(fullPath, new byte[] { 0 }); 

        var result = _converter.Convert(fileName, typeof(string), null, CultureInfo.InvariantCulture);

        Assert.Equal(fullPath, result);

        File.Delete(fullPath);
    }

    [Fact]
    public void Convert_ShouldReturnDefault_WhenFileNotExists()
    {
        var result = _converter.Convert("not-found.png", typeof(string), null, CultureInfo.InvariantCulture);
        Assert.Equal("pack://application:,,,/Images/no-image.png", result);
    }

    [Fact]
    public void Convert_ShouldReturnDefault_WhenNull()
    {
        var result = _converter.Convert(null, typeof(string), null, CultureInfo.InvariantCulture);
        Assert.Equal("pack://application:,,,/Images/no-image.png", result);
    }
}
