using Allup.Domain.Entities;
using Allup.Persistence.Context;
using Allup.Persistence.Repositories.Abstraction;
using Core.Persistence.Repositories;

namespace Allup.Persistence.Repositories.Implementations;

public class ProductTranslationRepository : EfRepositoryBase<ProductTranslation, AppDbContext>, IProductTranslationRepository
{
    public ProductTranslationRepository(AppDbContext context) : base(context)
    {
    }
}
