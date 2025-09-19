using Infrastructure.Interfaces;
using Infrastructure.Models;
using Moq;

namespace Infrastructure.Tests.Services;

public class FileService_Tests
{
    [Fact]
    public void SaveContentToFile_ShouldReturnTrue_WhenContentSaveToFile()
    {
        // Arrange

        var fileResult = new FileResult { Succeeded = true };

        var fileServiceMock = new Mock<IFileService>();
        var fileService = fileServiceMock.Object;

        fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>(), It.IsAny<string>())).Returns(fileResult);

        // Act

        var result = fileService.SaveContentToFile("", "");

        // Assert

        Assert.True(result.Succeeded);
    }

    [Fact]
    public void SaveContentToFile_ShouldReturnFalseWithError_WhenContentNotSaveToFile()
    {
        // Arrange

        var fileResult = new FileResult { Succeeded = false, Error = "Unable to save contetnt." };

        var fileServiceMock = new Mock<IFileService>();
        var fileService = fileServiceMock.Object;

        fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>(), It.IsAny<string>())).Returns(fileResult);

        // Act

        var result = fileService.SaveContentToFile("", "");

        // Assert

        Assert.False(result.Succeeded);
        Assert.Equal("Unable to save contetnt.", result.Error);
    }

    [Fact]
    public void GetContentFromFile_ShouldReturnTrueAndContentAsJson_WhenFileFound()
    {
        // Arrange

        var jsonContent = "[{\"Id\": \"810937b9-b1bc-4eee-8972-86b91717b58e\", \"Name\" : \"Test Product\", \"Price\" : 100.00}]";
        var fileResult = new FileResult { Succeeded = true, Content = jsonContent };

        var fileServiceMock = new Mock<IFileService>();
        var fileService = fileServiceMock.Object;

        fileServiceMock.Setup(fs => fs.GetContentFromFile(It.IsAny<string>())).Returns(fileResult);

        // Act

        var result = fileService.GetContentFromFile("");

        // Assert

        Assert.True(result.Succeeded);
        Assert.Equal(jsonContent, result.Content);

    }

    [Fact]
    public void GetContentFromFile_ShouldReturnFalseWithError_WhenExceptionOccured()
    {
        // Arrange
        var fileResult = new FileResult { Succeeded = false, Error = "Something went wrong!" };

        var fileServiceMock = new Mock<IFileService>();
        var fileService = fileServiceMock.Object;

        fileServiceMock.Setup(fs => fs.GetContentFromFile(It.IsAny<string>())).Returns(fileResult);

        // Act

        var result = fileService.GetContentFromFile("");

        // Assert

        Assert.False(result.Succeeded);
        Assert.False(string.IsNullOrEmpty(result.Error));

    }
}
