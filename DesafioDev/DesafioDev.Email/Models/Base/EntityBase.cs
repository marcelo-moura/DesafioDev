namespace DesafioDev.Email.Models.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public bool Ativo { get; set; }

        public EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
