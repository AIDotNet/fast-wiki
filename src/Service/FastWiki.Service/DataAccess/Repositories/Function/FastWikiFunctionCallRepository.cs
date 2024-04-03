using FastWiki.Service.Domain.Function.Aggregates;
using FastWiki.Service.Domain.Function.Repositories;

namespace FastWiki.Service.DataAccess.Repositories.Function;

public sealed class FastWikiFunctionCallRepository : Repository<WikiDbContext, FastWikiFunctionCall, long>,
    IFastWikiFunctionCallRepository
{
    public FastWikiFunctionCallRepository(WikiDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }
}