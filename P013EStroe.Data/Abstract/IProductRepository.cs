﻿using P013EStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace P013EStroe.Data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByIncludeAsync(int id); // bu metot bize ürün marka ve kategori include edilmiş veritabanından 1 tane kayıt getirecek
        Task<List<Product>> GetProductsByIncludeAsync(); // tüm ürünleri marka ve kategorisiyle getirecek
        Task<List<Product>> GetProductsByIncludeAsync(Expression<Func<Product, bool>> expression); // tüm ürünleri marka ve kategorisiyle filtre uygulayarak getirecek

    }
}
