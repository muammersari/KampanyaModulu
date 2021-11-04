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
    public class CampaingAndProductService : ICampaingAndProductService
    {
        ICampaingAndProductDal _campaingAndProductDal;
        public CampaingAndProductService(ICampaingAndProductDal campaingAndProductDal)
        {
            _campaingAndProductDal = campaingAndProductDal;
        }

        public IDataResult<CampaingAndProduct> Add(CampaingAndProduct campaingAndProduct)
        {
            var result = _campaingAndProductDal.Add(campaingAndProduct);
            if (result == null)
                return new ErrorDataResult<CampaingAndProduct>("Kampanyaya Ürün Eklenemedi");

            return new SuccessDataResult<CampaingAndProduct>(result);
        }

        public IResult Delete(int campaingId, int productId)
        {
            var result = _campaingAndProductDal.Get(x => x.ProductId == productId && x.CampaingId == campaingId);
            if (result == null)
            {
                return new ErrorResult("Kampanyada Ürün Bulunamadı");
            }
            _campaingAndProductDal.Delete(result);
            return new SuccessResult("Kampanyadan Ürün Silindi");
        }

        public IResult DeleteRange(int id)
        {
            var result = _campaingAndProductDal.GetList(x => x.CampaingId == id).ToList();
            if (result == null)
            {
                return new ErrorResult("Kampanyada Ürün Bulunamadı");
            }
            _campaingAndProductDal.DeleteRange(result);
            return new SuccessResult("Kampanyadan Tüm Ürün Silindi");
        }

        public IDataResult<List<CampaingAndProduct>> GetByCampaingId(int id)
        {
            var result = _campaingAndProductDal.GetList(p => p.CampaingId == id).ToList();
            if (result == null)
                return new ErrorDataResult<List<CampaingAndProduct>>("Kampanyada Bulunamadı");

            return new SuccessDataResult<List<CampaingAndProduct>>(result);
        }

        public IDataResult<CampaingAndProduct> Update(CampaingAndProduct campaingAndProduct)
        {
            var result = _campaingAndProductDal.Update(campaingAndProduct);
            if (result == null)
                return new ErrorDataResult<CampaingAndProduct>("Kampanyada Güncellenemedi");

            return new SuccessDataResult<CampaingAndProduct>(result);
        }
    }
}
