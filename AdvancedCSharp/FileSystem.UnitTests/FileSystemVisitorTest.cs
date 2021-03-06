using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileSystem.Interfaces;
using FileSystem.Models;
using FileSystem.Services;
using Moq;
using Xunit;

namespace FileSystem.UnitTests
{
    public class FileSystemVisitorTest
    {
        private IFileSystemVisitor _fileSystemVisitor;
        private readonly Mock<IFileSystemProvider> _fileSystemProviderMock;

        public FileSystemVisitorTest()
        {
            _fileSystemProviderMock = new Mock<IFileSystemProvider>();
        }

        [Fact]
        public void GetSystemTheeItems_WhenOnlyFilesExist_ReturnFiles()
        {
            // Arrange
            const string path = "C:/test";
            var files = new[]
            {
                new FileInfo("file.txt"),
                new FileInfo("picture.png")
            };
            _fileSystemProviderMock.Setup(e => e.GetFiles(It.IsAny<string>())).Returns(() => files);
            _fileSystemVisitor = new FileSystemVisitor(_fileSystemProviderMock.Object);

            // Act
            var actualResult = _fileSystemVisitor.GetSystemTreeItems(path).ToList();

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(files[0].Name, actualResult[0].Name);
            Assert.Equal(files[1].Name, actualResult[1].Name);
        }

        [Fact]
        public void GetSystemTheeItems_WhenFoldersExist_ReturnFolders()
        {
            // Arrange
            const string path = "C:/test";
            var folders = new[]
            {
                new DirectoryInfo("C:/folder1"),
                new DirectoryInfo("C:/folder2")
            };

            _fileSystemProviderMock.Setup(e => e.GetDirectories(path)).Returns(() => folders);
            _fileSystemVisitor = new FileSystemVisitor(_fileSystemProviderMock.Object);

            // Act
            var actualResult = _fileSystemVisitor.GetSystemTreeItems(path).ToList();

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(folders[0].Name, actualResult[0].Name);
            Assert.Equal(folders[1].Name, actualResult[1].Name);
        }

        [Fact]
        public void GetSystemTheeItems_WhenFilesFoldersExist_ReturnFilesFolders()
        {
            // Arrange
            const string path = "C:/test";

            var files = new[]
            {
                new FileInfo("file.txt"),
                new FileInfo("picture.png")
            };

            var folders = new[]
            {
                new DirectoryInfo("C:/folder1"),
                new DirectoryInfo("C:/folder2")
            };

            _fileSystemProviderMock.Setup(e => e.GetFiles(path)).Returns(() => files);
            _fileSystemProviderMock.Setup(e => e.GetDirectories(path)).Returns(() => folders);
            _fileSystemVisitor = new FileSystemVisitor(_fileSystemProviderMock.Object);

            // Act
            var actualResult = _fileSystemVisitor.GetSystemTreeItems(path).ToList();

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(files[0].Name, actualResult[0].Name);
            Assert.Equal(files[1].Name, actualResult[1].Name);
            Assert.Equal(folders[0].Name, actualResult[2].Name);
            Assert.Equal(folders[1].Name, actualResult[3].Name);
        }

        [Fact]
        public void GetSystemTheeItems_WhenFilesInSubFolders_ReturnFiles()
        {
            // Arrange
            const string path = "C:/folder";
            const string pathSubFolder = "C:/folder/subfolder";

            var filesFolder = new[]
            {
                new FileInfo("C:/folder/video.avi"),
            };

            var filesSubFolder = new[]
            {
                new FileInfo("C:/folder/subfolder/test.txt"),
                new FileInfo("C:/folder/subfolder/photo.png")
            };

            var directory = new[]
            {
                new DirectoryInfo("C:/folder/subfolder")
            };

            _fileSystemProviderMock.SetupSequence(e => e.GetFiles(path)).Returns(filesFolder).Returns(filesSubFolder);
            _fileSystemProviderMock.Setup(e => e.GetDirectories(path)).Returns(() => directory);
            _fileSystemVisitor = new FileSystemVisitor(_fileSystemProviderMock.Object);

            // Act
            var actualResult = _fileSystemVisitor.GetSystemTreeItems(path).ToList();

            // Assert
            Assert.NotNull(actualResult);
        }

        [Fact]
        public void GetSystemTreeItems_WhenPassedPredicate_ReturnFilteredFiles()
        {
            // Arrange
            const string path = "C:/";
            var filesFolder = new[]
            {
                new FileInfo("C:/video.avi"),
                new FileInfo("C:/document.txt"),
                new FileInfo("C:/photo.png"),
                new FileInfo("C:/cv.txt")
            };

            _fileSystemProviderMock.Setup(e => e.GetFiles(path)).Returns(() => filesFolder);
            _fileSystemVisitor = new FileSystemVisitor(_fileSystemProviderMock.Object, file => file.EndsWith(".txt"));

            // Act
            var actualResult = _fileSystemVisitor.GetSystemTreeItems(path).ToList();

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(filesFolder[1].Name, actualResult[0].Name);
            Assert.Equal(filesFolder[3].Name, actualResult[1].Name);
        }

        [Fact]
        public void GetSystemTreeItems_WhenProcessInterrupted_ReturnEmptyFiles()
        {
            // Arrange
            const string path = "C:/";
            var files = new[]
            {
                new FileInfo("C:/video.avi"),
                new FileInfo("C:/document.txt"),
            };

            _fileSystemProviderMock.Setup(e => e.GetFiles(path)).Returns(() => files);
            _fileSystemVisitor = new FileSystemVisitor(_fileSystemProviderMock.Object, file => file.EndsWith(".txt"));

            // Act
            var actualResult = _fileSystemVisitor.GetSystemTreeItems(path, isInterrupted: true).ToList();

            // Assert
            Assert.NotNull(actualResult);
            Assert.Empty(actualResult);
        }

        [Fact]
        public void GetSystemTreeItems_WhenDeleteFilePassed_ReturnOnlyFolders()
        {
            // Arrange
            const string path = "C:/";
            var files = new[]
            {
                new FileInfo("C:/video.avi"),
                new FileInfo("C:/document.txt"),
            };

            var folders = new[]
            {
                new DirectoryInfo("C:/folder1"),
            };

            _fileSystemProviderMock.Setup(e => e.GetFiles(path)).Returns(() => files);
            _fileSystemProviderMock.Setup(e => e.GetDirectories(path)).Returns(() => folders);
            _fileSystemVisitor = new FileSystemVisitor(_fileSystemProviderMock.Object);

            // Act
            var actualResult = _fileSystemVisitor.GetSystemTreeItems(path, isDeleteFile: true).ToList();

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(folders.Length, actualResult.Count);
            Assert.Equal(folders[0].Name, actualResult[0].Name);
        }

        [Fact]
        public void GetSystemTreeItems_WhenDeleteFoldersPassed_ReturnOnlyFolders()
        {
            // Arrange
            const string path = "C:/";
            var files = new[]
            {
                new FileInfo("C:/video.avi"),
            };

            var folders = new[]
            {
                new DirectoryInfo("C:/folder1"),
                new DirectoryInfo("C:/document.txt"),
            };

            _fileSystemProviderMock.Setup(e => e.GetFiles(path)).Returns(() => files);
            _fileSystemProviderMock.Setup(e => e.GetDirectories(path)).Returns(() => folders);
            _fileSystemVisitor = new FileSystemVisitor(_fileSystemProviderMock.Object);

            // Act
            var actualResult = _fileSystemVisitor.GetSystemTreeItems(path, isDeleteFolder: true).ToList();

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(files.Length, actualResult.Count);
            Assert.Equal(files[0].Name, actualResult[0].Name);
        }
    }
}