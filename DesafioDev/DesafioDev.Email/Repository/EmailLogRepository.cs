using DesafioDev.Email.Context;
using DesafioDev.Email.InterfacesRepository;
using DesafioDev.Email.Models;
using DesafioDev.Email.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace DesafioDev.Email.Repository
{
    public class EmailLogRepository : RepositoryBase<EmailLog>, IEmailLogRepository
    {
        public EmailLogRepository(DbContextOptions<DesafioDevEmailContext> context) : base(context)
        {
        }
    }
}
