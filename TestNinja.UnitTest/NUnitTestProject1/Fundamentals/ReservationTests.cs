using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();
            // Act
            var cancelled = reservation.CanBeCancelledBy(new User { IsAdmin = true });
            // Assert
            Assert.IsTrue(cancelled);
        }

        [Test]
        public void CanBeCancelledBy_UserIsTheSame_ReturnsTrue()
        {
            var user = new User();
            var reservation = new Reservation() { MadeBy = user };

            var cancelled = reservation.CanBeCancelledBy(user);
            
            Assert.IsTrue(cancelled);
        }


        [Test]
        public void CanBeCancelledBy_UserIsDifferent_ReturnsFalse()
        {
            var userThatReserved = new User();
            var userThatCancelled = new User();
            var reservation = new Reservation() { MadeBy = userThatReserved };
            
            var cancelled = reservation.CanBeCancelledBy(userThatCancelled);

            Assert.IsFalse(cancelled);
        }
    }
}
