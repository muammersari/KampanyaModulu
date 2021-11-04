using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductService : IProductService
    {
        private IProductDal _productDal;
        public ProductService(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<Product> Add(Product product)
        {
            var result = _productDal.Add(product);
            if (result == null)
                return new ErrorDataResult<Product>("Ürün Eklenemedi");

            return new SuccessDataResult<Product>(result);
        }

        public IResult Delete(int id)
        {
            var result = _productDal.Get(x => x.ProductId == id);
            if (result == null)
            {
                return new ErrorResult("Ürün Bulunamadı");
            }
            _productDal.Delete(result);
            return new SuccessResult("Ürün Silindi");
        }

        public IDataResult<Product> GetById(int id)
        {
            var result = _productDal.Get(p => p.ProductId == id);
            if (result == null)
                return new ErrorDataResult<Product>("Ürün Bulunamadı");

            return new SuccessDataResult<Product>(result);
        }

        public IDataResult<Product> GetByName(string productName)
        {
            var result = _productDal.Get(p => p.Name == productName);
            if (result == null)
                return new ErrorDataResult<Product>("Ürün Bulunamadı");

            return new SuccessDataResult<Product>(result);
        }

        public IDataResult<List<Product>> GetList()
        {
            var result = _productDal.GetList().ToList();
            if (result.Count == 0)
                return new ErrorDataResult<List<Product>>("Ürün Bulunamadı");

            return new SuccessDataResult<List<Product>>(result);
        }

        public IDataResult<Product> Update(Product product)
        {
            var result = _productDal.Update(product);
            if (result == null)
                return new ErrorDataResult<Product>("Ürün Güncellenemedi");

            return new SuccessDataResult<Product>(result);

        }
    }
}
