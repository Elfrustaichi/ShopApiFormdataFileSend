using ShopNT.Core.Entities;
using ShopNT.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNT.Data.Repositories
{
    public class ProductRepository:Repository<Product>, IProductRepository
    {
        public ProductRepository(ShopNTDbContext context):base(context)
        {
            
        }
    }
}
