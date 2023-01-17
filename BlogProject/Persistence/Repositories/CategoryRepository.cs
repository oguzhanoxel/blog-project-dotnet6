using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
	public class CategoryRepository : EfRepositoryBase<Category, RepositoryDbContext>, ICategoryRepository
	{
		public CategoryRepository(RepositoryDbContext context) : base(context)
		{
			
		}
	}
}


