using Moq;
using NUnit.Framework;
using System.Net;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    class InstallerHelperUnitTest
    {
        Mock<IFileDownloader> _fileDownloader;
        InstallerHelper _isntallerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _isntallerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_ValidArguments_ReturnTrue()
        {
            var result = _isntallerHelper.DownloadInstaller("customerName", "installerName");

            Assert.That(result, Is.True);
        }

        [Test]
        public void DownloadInstaller_EmptyArguments_ReturnFalse()
        {
            _fileDownloader.Setup(fr =>
                fr.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
            .Throws<WebException>();

            var result = _isntallerHelper.DownloadInstaller("", "");

            Assert.That(result, Is.False);
        }
    }
}
