using System.Net;
using System.Net.Mail;
using System.Text;

namespace SoftwareTesting.Mocking
{
    public class HouseKeeperService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStatementGenerator _statementGenerator;
        private readonly IEmailSender _emailSender;
        private readonly IXtraMessageBox _xtraMessageBox;

        public HouseKeeperService(
            IUnitOfWork unitOfWork,
            IStatementGenerator statementGenerator,
            IEmailSender emailSender,
            IXtraMessageBox XtraMessageBox)
        {
            this._statementGenerator = statementGenerator;
            _emailSender = emailSender;
            _xtraMessageBox = XtraMessageBox;
            this._unitOfWork = unitOfWork;
        }
        public bool SendStatementEmails(DateTime statementDate)
        {
            var houseKeepers = _unitOfWork.Query<HouseKeeper>();

            foreach (var houseKeeper in houseKeepers)
            {
                if (string.IsNullOrWhiteSpace(houseKeeper.Email))
                    continue;

                var statementFilename = _statementGenerator.SaveStatement(houseKeeper.Oid, houseKeeper.FullName, statementDate);

                if (string.IsNullOrWhiteSpace(statementFilename))
                    continue;

                var emailAddress = houseKeeper.Email;
                var emailBody = houseKeeper.StatementEmailBody;

                try
                {
                    _emailSender.EmailFile(emailAddress, statementFilename,
                        string.Format("Sandipiper Statement {0:yyyy-MM} {1}", statementDate), houseKeeper.StatementEmailBody);
                }
                catch (Exception e)
                {
                    _xtraMessageBox.Show(e.Message, string.Format("Email failure: {0}", emailAddress),
                        MessageBoxButtons.OK);
                }
            }
            return true;
        }
    }

    public class HouseKeeper
    {

        public int Oid { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string StatementEmailBody { get; set; }
    }
    
    public enum MessageBoxButtons
    {
        OK
    }

    public interface IXtraMessageBox
    {
        void Show(string s, string HouseKeeperStatements, MessageBoxButtons ok);
    }

    public class XtraMessageBox : IXtraMessageBox
    {
        public void Show(string s, string HouseKeeperStatements, MessageBoxButtons ok)
        {

        }
    }

    public class MainForm
    {
        public bool HouseKeeperStatementsSending { get; set; }
    }

    public class DateForm
    {
        public DateForm(string statementDate, object endOfLastMonth)
        {
            
        }
    }
}
