using System;
using NUnit.Framework;
using StatsViewer.Models;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StatsRequestProcessCanBeCreatedWithValidUri()
        {
            var uri = new Uri("https://stackoverflow.com/users/16587/george-stocker");
            var statsRequest = new StatsRequestProcess(new StatsRequest(uri));
            Assert.AreEqual(16587, statsRequest.UserId);
            Assert.AreEqual("stackoverflow.com", statsRequest.SiteName);
        }

        [Test]
        public void StatsRequestProcessWithNonHttpsSchemeGetsReplacedWithHttpsScheme()
        {
            var uri = new Uri("gopher://stackoverflow.com/users/16587/george-stocker");
            var statsRequest = new StatsRequestProcess(new StatsRequest(uri));
            Assert.AreEqual(15687, statsRequest.UserId);
            Assert.AreEqual("stackoverflow.com", statsRequest.SiteName);
        }
        [Test]
        public void StatsRequestProcessCanBeCreatedWithTrailingSlashLeftOffButStillValidUri()
        {
            var uri = new Uri("https://stackoverflow.com/users/16587");
            var statsRequest = new StatsRequestProcess(new StatsRequest(uri));
            Assert.AreEqual(16587, statsRequest.UserId);
            Assert.AreEqual("stackoverflow.com", statsRequest.SiteName);
        }
        [Test]
        public void StatsRequestProcessCanBeCreatedWithTrailingSlashOnButNoUserNameFollowing()
        {
            var uri = new Uri("https://stackoverflow.com/users/16587/");
            var statsRequest = new StatsRequestProcess(new StatsRequest(uri));
            Assert.AreEqual(16587, statsRequest.UserId);
            Assert.AreEqual("stackoverflow.com", statsRequest.SiteName);
        }
        [Test]
        public void StatsRequestProcessCanBeCreatedWithNoSchemeButHasHostName()
        {
            var statsRequest = new StatsRequestProcess(new StatsRequest("stackoverflow.com/users/16587/"));
            Assert.AreEqual(16587, statsRequest.UserId);
            Assert.AreEqual("stackoverflow.com", statsRequest.SiteName);
        }

        [Test]
        public void InvalidUriDoesNotGenerateAValidStatsRequestProcess()
        {
            var statsRequest = new StatsRequestProcess(new StatsRequest("SELECT 1 = 1"));
            Assert.NotNull(statsRequest);
            Assert.That(!statsRequest.IsValid());
        }
    }
}