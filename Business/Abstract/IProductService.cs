using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<Product> Add(Product product); //Ürün Ekleme
        IDataResult<List<Product>> GetList(); // Tüm Ürünleri listeleme
        IDataResult<Product> GetById(int id); // İd sine Göre Ürün listeleme
        IDataResult<Product> GetByName(string productName); // Adına Göre Ürün Listeleme
        IDataResult<Product> Update(Product product); // Ürün Güncelleme
        IResult Delete(int id); // Ürün Silme
    }
}
