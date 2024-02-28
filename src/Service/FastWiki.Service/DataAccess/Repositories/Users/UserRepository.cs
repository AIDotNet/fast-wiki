using FastWiki.Service.Domain.Users.Repositories;

namespace FastWiki.Service.DataAccess.Repositories.Users;

public class UserRepository : Repository<WikiDbContext,User,Guid>,IUserRepository
{
    public UserRepository(WikiDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }
}