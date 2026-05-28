using System;
using Xunit;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.Tests
{
    public class TrackValidationTests
    {
        
        [Theory]
        [InlineData(0)]   
        [InlineData(-1)]  
        [InlineData(-150)]
        public void Constructor_ShouldThrowArgumentException_WhenDurationIsInvalid(int invalidDuration)
        {
            
            Assert.Throws<ArgumentException>(() =>
            {
                var badTrack = new PodcastTrack(
                    "id", 
                    "Invalid Track", 
                    invalidDuration,
                    "file.mp3", 
                    "art-1", 
                    "Author"
                );
            });
        }

        [Fact]
        public void Constructor_ShouldSetCorrectProperties_WhenDataIsValid()
        {
            string expectedId = "valid-id";
            string expectedTitle = "Clean Code Lecture";
            int expectedDuration = 320;

            var validTrack = new PodcastTrack(expectedId, expectedTitle, expectedDuration, "file.mp3", "art-1", "Bob");

            Assert.Equal(expectedId, validTrack.Id);
            Assert.Equal(expectedTitle, validTrack.Title);
            Assert.Equal(expectedDuration, validTrack.DurationInSeconds);
        }
    }
}