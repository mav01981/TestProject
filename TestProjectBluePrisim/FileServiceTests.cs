using BluePrism.Service;
using FluentAssertions;
using System;
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
    }
}
