﻿using Allup.Domain.Entities;
using Allup.Persistence.Context;
using Allup.Persistence.Repositories.Abstraction;
using Core.Persistence.Repositories;

namespace Allup.Persistence.Repositories.Implementations;

public class ProductImageRepository : EfRepositoryBase<ProductImage, AppDbContext>, IProductImageRepository
{
    public ProductImageRepository(AppDbContext context) : base(context)
    {
    }
}