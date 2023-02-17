namespace SoftwareTesting.Mocking
{
    public interface IStatementGenerator
    {
        string SaveStatement(int houseKeeperOid, string houseKeeperName, DateTime statementDate);
    }

    public class StatementGenerator : IStatementGenerator
    {
        public string SaveStatement(int houseKeeperOid, string houseKeeperName, DateTime statementDate)
        {
            var report = new HouseKeeperStatementReport(houseKeeperOid, statementDate);

            if (!report.HasDate)
                return string.Empty;

            report.CreateDocument();

            var fileName = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                string.Format("Sandpiper Statement {0:yyyy-MM} {1}.pdf", statementDate, houseKeeperOid));

            report.ExportToPdf(fileName);

            return fileName;
        }
    }

    public class HouseKeeperStatementReport
    {
        public int houseKeeperOid { get; set; }
        public bool HasDate { get; set; }
        public DateTime statementDate { get; set; }

        public HouseKeeperStatementReport(int houseKeeperOid, DateTime statementDate)
        {
            this.houseKeeperOid = houseKeeperOid;
            this.statementDate = statementDate;
        }

        public void CreateDocument() { }
        public void ExportToPdf(string fileName) { } 
    }
}

