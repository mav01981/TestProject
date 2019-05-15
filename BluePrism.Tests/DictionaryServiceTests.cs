using BluePrism.Service;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BluePrism.Tests
{
    public class DictionaryServiceTests
    {
        public string[] CreateSUT()
        {
            return new string[]
            {
                "Spin","Spit","Spat","Spot","Span"
            };
        }

        public string[] CreateSUTDuplicatedData()
        {
            return new string[]
            {
                "Spin","Spit","Spit","Spat","Spot","Span"
            };
        }

        public string[] CreateSUTMoreThanOneCharacterDifference()
        {
            return new string[]
            {
                "Spin","Spit","Spit","Sportswriter","Spat","Spot","Span"
            };
        }

        [Theory(DisplayName = "Given 5 or more letter words IsValidNextWord returns false")]
        [InlineData("spin", "spiritual")]
        [InlineData("spot", "splint")]
        public void Given_4_Letter_Words_As_IsValidNextWord_returns_False(string word1, string word2)
        {
            //Arrange
            var validationService = new ValidationService();
            //Act
            bool result = validationService.ISvalidNextWord(word1, word2);
            //Assert
            result.Should().Be(false);
        }

        [Theory(DisplayName = "Given 3 Letter Words As IsValidNextWord Returns False")]
        [InlineData("spin", "spy")]
        [InlineData("spot", "so")]
        public void Given_3_Letter_Words_As_IsValidNextWord_returns_False(string word1, string word2)
        {
            //Arrange
            var validationService = new ValidationService();
            //Act
            bool result = validationService.ISvalidNextWord(word1, word2);
            //Assert
            result.Should().Be(false);
        }

        [Theory(DisplayName = "Given Spit,Spat returns 1 character difference")]
        [InlineData("Spin", "Spit")]
        public void Given_Three_Words_Char_Difference_Two_Words(string word1, string word2)
        {
            //Arrange
            var validationService = new ValidationService();
            //Act
            bool result = validationService.ISvalidNextWord(word1, word2);
            //Assert
            result.Should().Be(true);
        }

        [Fact(DisplayName = "Given No Data in file returns empty list")]
        public void Given_No_Strings_In_File_returns_Empty_Results()
        {
            //Arrange
            var mockedFileService = new Mock<IFileservice>();
            mockedFileService.Setup(x => x.Read(It.IsAny<string>())).Returns(new string[0]);
            var validationService = new ValidationService();
            //Act
            var service = new DictionaryService(mockedFileService.Object, validationService);
            var result = service.CreateResult(It.IsAny<string>(), "Spin", "Spot", It.IsAny<string>());
            //Assert
            result.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Given Spin,Spit,Spat,Spot,Span returns Spin,Spit,Spot")]
        public void Given_StartWord_Spin_EndWord_Spot_Returns_Spin_Spit_Spot()
        {
            //Arrange
            var mockedFileService = new Mock<IFileservice>();
            mockedFileService.Setup(x => x.Read(It.IsAny<string>())).Returns(CreateSUT());
            var validationService = new ValidationService();
            //Act
            var service = new DictionaryService(mockedFileService.Object, validationService);
            var result = service.CreateResult(It.IsAny<string>(), "Spin", "Spot", It.IsAny<string>());
            //Assert
            result.Should().Equal(new List<string>()
            {
                "Spin",
                "Spit",
                "Spot"
            });
        }

        [Fact(DisplayName = "Given Spin,Spit,Spit,Spat,Spot,Span returns Spin,Spit,Spot")]
        public void Given_StartWord_Spin_EndWord_Spot_Add_Word_Not_In_File_Returns_Spin_Spit_Spot()
        {
            //Arrange
            var mockedFileService = new Mock<IFileservice>();
            mockedFileService.Setup(x => x.Read(It.IsAny<string>())).Returns(CreateSUTDuplicatedData());
            var validationService = new ValidationService();
            //Act
            var service = new DictionaryService(mockedFileService.Object, validationService);
            var result = service.CreateResult(It.IsAny<string>(), "Spin", "Spot", It.IsAny<string>());
            //Assert
            result.Should().Equal(new List<string>()
            {
                "Spin",
                "Spit",
                "Spot"
            });
        }

        [Fact(DisplayName = "Given Spin,Spit,Spit,SportsWriter,Spat,Spot,Span returns Spin,Spit,Spot")]
        public void Given_StartWord_Spin_EndWord_Spot_Add_Word_Has_More_Than_One_Char_Different_Returns_Spin_Spit_Spot()
        {
            //Arrange
            var mockedFileService = new Mock<IFileservice>();
            mockedFileService.Setup(x => x.Read(It.IsAny<string>())).Returns(CreateSUTMoreThanOneCharacterDifference());
            var validationService = new ValidationService();
            //Act
            var service = new DictionaryService(mockedFileService.Object, validationService);
            var result = service.CreateResult(It.IsAny<string>(), "Spin", "Spot", It.IsAny<string>());
            //Assert
            result.Should().Equal(new List<string>()
            {
                "Spin",
                "Spit",
                "Spot"
            });
        }

    }
}
