using Moq;
using SoftwareTesting.Fundamentals;
using SoftwareTesting.Mocking;

namespace SoftwareTesting.UnitTests
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _employeeStorage;
        private EmployeeController _employeeController;
        [SetUp]
        public void SetUp()
        {
            _employeeStorage = new Mock<IEmployeeStorage>();
            _employeeController = new EmployeeController(_employeeStorage.Object);
        }
        [Test]
        public void DeleteEmployee_WhenCalled_ReturnActionResult()
        {
            var resut = _employeeController.DeleteEmployee(1);

            Assert.That(resut, Is.InstanceOf<ActionResult>());
        }
        
        [Test]
        public void GetCustomer_WhenCalled_InvokeTheIStorageDeleteMethod()
        {
            _employeeStorage.Setup(e => e.Delete(It.IsAny<int>()));

            var resut = _employeeController.DeleteEmployee(1);

            _employeeStorage.Verify(e => e.Delete(1), Times.Once);
           
        }
    }
}

