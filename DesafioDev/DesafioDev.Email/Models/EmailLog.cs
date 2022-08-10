using DesafioDev.Email.Models.Base;

namespace DesafioDev.Email.Models
{
    public class EmailLog : Entity
    {
        public string Email { get; private set; }
        public string Log { get; private set; }

        protected EmailLog() { }

        public EmailLog(string email, string log)
        {
            Email = email;
            Log = log;
        }
    }
}
