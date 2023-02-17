namespace SoftwareTesting.Mocking
{
    public interface IEmployeeStorage
    {
        void Delete(int id);
    }

    public class EmployeeStorage : IEmployeeStorage
    {
        public void Delete(int id)
        {
            var _db = new EmployeeContext();
            var employee = _db.Employees.Find(id);
            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }
    }
}