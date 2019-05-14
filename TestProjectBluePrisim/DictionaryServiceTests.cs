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

        [Theory(DisplayName = "Given Spin, Spat returns 2 character difference")]
        [InlineData("Spin", "Spat")]
        public void Given_Two_Words_Char_Difference_Two_Words(string word1, string word2)
        {
            //Arrange
            //Act
            int charDiff = 0;
            foreach (var c in word1.ToCharArray())
            {
                if (word2.IndexOf(c) == -1)
                {
                    charDiff += 1;
                }
            }
            //Assert
            charDiff.Should().Be(2);
        }

        [Theory(DisplayName = "Given Spit,Spat returns 1 character difference")]
        [InlineData("Spin", "Spit")]
        public void Given_Three_Words_Char_Difference_Two_Words(string word1, string word2)
        {
            //Arrange
            //Act
            int charDiff = 0;
            foreach (var c in word1.ToCharArray())
            {
                if (word2.IndexOf(c) == -1)
                {
                    charDiff += 1;
                }
            }
            //Assert
            charDiff.Should().Be(1);
        }

        [Fact(DisplayName = "Given No Data in file returns Empty results")]
        public void Given_No_Strings_In_File_returns_Empty_Results()
        {
            //Arrange
            var mockedFileService = new Mock<IFileservice>();
            mockedFileService.Setup(x => x.Read(It.IsAny<string>())).Returns(new string[0]);

            //Act
            var service = new DictionaryService(mockedFileService.Object);
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

            //Act
            var service = new DictionaryService(mockedFileService.Object);
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

            //Act
            var service = new DictionaryService(mockedFileService.Object);
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

            //Act
            var service = new DictionaryService(mockedFileService.Object);
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
