using SoftwareTesting.Fundamentals;

namespace SoftwareTesting.UnitTests
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_InputSpeedIsOutOfRange_ThrowArgumentOutOfRangeException(int speed)
        {
            var demeritPoints = new DemeritPointsCalculator();

            Assert.That(() => demeritPoints.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }
        
        [Test]
        [TestCase(73, 1)]
        [TestCaseSource(nameof(DemeritTestsCases))]
        public void CalculateDemeritPoints_WhenCalled_ReturnsDemeritPoint(int speed, int expectedResult)
        {
            var demeritPoints = new DemeritPointsCalculator();

            var resul = demeritPoints.CalculateDemeritPoints(speed);
            Assert.That(resul, Is.EqualTo(expectedResult));
        }

        static object[] DemeritTestsCases = 
        {
            new object[] {70, 1},
            new object[] {80, 3},
            new object[] {75, 2},
            new object[] {0, 0},
            new object[] {64, 0},
            new object[] {65, 0},
            new object[] {75, 2},
            new object[] {66, 0}
        };
    }
}

