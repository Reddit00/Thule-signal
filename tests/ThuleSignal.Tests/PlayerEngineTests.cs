using System;
using Xunit;
using Moq; 
using ThuleSignal.App.Services;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.App.Patterns.Observer;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.Tests
{
    public class PlayerEngineTests
    {
        [Fact]
        public void PlayTrack_ShouldCallAudioOutputAndTriggerEvent_WhenTrackIsCorrect()
        {
            var mockAudioOutput = new Mock<IAudioOutput>();
            
            var player = new PlayerEngine(mockAudioOutput.Object);
            
            var track = new PodcastTrack("test-id", "Test Title", 100, "test.mp3", "art-1", "Host");
            
            bool eventWasRaised = false;
            string raisedState = string.Empty;

            player.PlayerStateChanged += (sender, args) =>
            {
                eventWasRaised = true;
                raisedState = args.State;
            };

            player.PlayTrack(track);
            Assert.True(eventWasRaised, "Подія PlayerStateChanged не була викликана!");
            Assert.Equal("Playing", raisedState);

            mockAudioOutput.Verify(audio => audio.PlayStream(track), Times.Once());
        }
    }
}