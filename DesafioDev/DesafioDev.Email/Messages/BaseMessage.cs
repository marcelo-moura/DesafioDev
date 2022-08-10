namespace DesafioDev.Email.Messages
{
    public abstract class BaseMessage
    {
        public DateTime MessageCreated { get; set; }

        protected BaseMessage()
        {
            MessageCreated = DateTime.Now;
        }
    }
}