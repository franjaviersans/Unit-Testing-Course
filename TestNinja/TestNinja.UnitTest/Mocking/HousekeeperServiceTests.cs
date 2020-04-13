using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking;
using System.Linq;
using System;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    class HousekeeperServiceTests
    {
        Mock<IHousekeeperStore> _housekeeperStore;
        Mock<IEmailFileSender> _emailFileSender;
        Mock<IHousekeeperStatementSaver> _statementSaver;
        Mock<IXtraMessageBox> _xtraMessageBox;
        Housekeeper _housekeeper;
        string _fileName;
        DateTime _statementDate = DateTime.Now;

        [SetUp]
        public void SetUp()
        {
            _xtraMessageBox = new Mock<IXtraMessageBox>();
            _emailFileSender = new Mock<IEmailFileSender>();

            _housekeeperStore = new Mock<IHousekeeperStore>();
            _housekeeper = new Housekeeper
            {
                Email = "housekeeper1@gmail.com",
                Oid = 1,
                FullName = "Hosekeeper1",
                StatementEmailBody = "Email Body"
            };

            _housekeeperStore.Setup(fr => fr.GetHousekeepers()).Returns(
                new List<Housekeeper>
                {
                    _housekeeper
                }.AsQueryable()
            );

            _statementSaver = new Mock<IHousekeeperStatementSaver>();
            _fileName = "Statement.txt";
            _statementSaver.Setup(sS => sS.SaveStatement(
                _housekeeper.Oid,
                _housekeeper.FullName,
                _statementDate
                )).Returns(() => _fileName);
        }

        [Test]
        public void SendStatementEmails_StatementIsNullOrWhiteSpace_DontSendEmail()
        {
            HousekeeperService.SendStatementEmails(_statementDate,
                _housekeeperStore.Object, _emailFileSender.Object, 
                _statementSaver.Object, _xtraMessageBox.Object);
        }

        [Test]
        public void SendStatementEmails_EmailIsNotNull_SaveStatement()
        {
            HousekeeperService.SendStatementEmails(_statementDate,
                _housekeeperStore.Object, _emailFileSender.Object,
                _statementSaver.Object, _xtraMessageBox.Object);

            _statementSaver.Verify(sS => sS.SaveStatement(
                _housekeeper.Oid,
                _housekeeper.FullName,
                _statementDate
                ));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_EmailIsNull_DontSaveStatement(string email)
        {
            _housekeeper.Email = email;

            HousekeeperService.SendStatementEmails(_statementDate,
                _housekeeperStore.Object, _emailFileSender.Object,
                _statementSaver.Object, _xtraMessageBox.Object);

            // _statementSaver.VerifyNoOtherCalls();
            _statementSaver.Verify(sS => sS.SaveStatement(
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()
                ), Times.Never);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementIsNullOrEmpty_DontSendEmail(string fileName)
        {
            _fileName = fileName;
            HousekeeperService.SendStatementEmails(_statementDate,
                _housekeeperStore.Object, _emailFileSender.Object,
                _statementSaver.Object, _xtraMessageBox.Object);

            _emailFileSender.Verify(eFS => eFS.EmailFile(
                It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }


        [Test]
        public void SendStatementEmails_WhenCalled_SendsEmail()
        {
            HousekeeperService.SendStatementEmails(_statementDate,
                _housekeeperStore.Object, _emailFileSender.Object,
                _statementSaver.Object, _xtraMessageBox.Object);

            _emailFileSender.Verify(eFS => eFS.EmailFile(
                _housekeeper.Email, _housekeeper.StatementEmailBody, 
                _fileName, It.IsAny<string>()));
        }

        [Test]
        public void SendStatementEmails_WhenSendEmailException_ShowsMessageBox()
        {
            _emailFileSender.Setup(eFS => eFS.EmailFile(
                _housekeeper.Email, _housekeeper.StatementEmailBody,
                _fileName, It.IsAny<string>())).Throws<Exception>();

            HousekeeperService.SendStatementEmails(_statementDate,
                _housekeeperStore.Object, _emailFileSender.Object,
                _statementSaver.Object, _xtraMessageBox.Object);

            _xtraMessageBox.Verify(xMB => xMB.Show(
                It.IsAny<string>(), 
                It.IsAny<string>(),
                MessageBoxButtons.OK));
        }
    }
}
