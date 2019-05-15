using BluePrism.Service;
using FluentAssertions;
using System;
using System.IO;
using Xunit;

namespace BluePrism.Tests
{
    public class FileServiceTests
    {
        [Fact(DisplayName = "")]
        public void Given_A_File_Path_Load_Text_Into_List()
        {
            //Arrange
            var filePath = Environment.CurrentDirectory + @"\words-english.txt";
            var service = new FileService();
            //Act
            var dictionary = service.Read(filePath);
            //Assert
            dictionary.Should().NotBeEmpty("Because the dictionary file has been loaded.");
        }

        [Fact(DisplayName = "")]
        public void Given_A_File_Path_That_Does_Not_Exist_Throws_FileNotFoundException()
        {
            //Arrange
            var filePath = Environment.CurrentDirectory + @"\name-not-found.txt";
            var service = new FileService();
            //Act
            Action action = () => service.Read(filePath);
            //Assert
            action.Should().Throw<FileNotFoundException>();
        }

        [Fact(DisplayName = "")]
        public void Given_A_File_Path_Save_Results_To_Txt_File()
        {
            //Arrange
            var filePath = Environment.CurrentDirectory + @"\results.txt";
            var results = new string[] { "Spin", "Spit", "Spot" };
            var service = new FileService();
            //Act
            var fileSaved = service.Save(filePath, results);
            //Assert
            fileSaved.Should().Be(fileSaved);
        }

        [Fact(DisplayName = "")]
        public void Given_A_File_Path_Save_Results_To_Txt_File_In_Location_That_Does_Not_Exist_Throws_UnauthorizedAccessException()
        {
            //Arrange
            var filePath =  @"\file-does-not-exist.txt";
            var results = new string[] { "Spin", "Spit", "Spot" };
            var service = new FileService();
            //Act
            Action action = () => service.Save(filePath, results);
            //Assert
            action.Should().Throw<UnauthorizedAccessException>();
        }
    }
}
