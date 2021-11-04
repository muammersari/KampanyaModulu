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
    public class CampaingService : ICampaingService
    {
        ICampaingDal _campaingDal;
        public CampaingService(ICampaingDal campaingDal)
        {
            _campaingDal = campaingDal;
        }

        public IDataResult<Campaing> Add(Campaing campaing)
        {
            var result = _campaingDal.Add(campaing);
            if (result == null)
                return new ErrorDataResult<Campaing>("Kampanya Eklenemedi");

            return new SuccessDataResult<Campaing>(result); ;
        }

        public IResult Delete(int id)
        {
            var result = _campaingDal.Get(x => x.CampaingId == id);
            if (result == null)
                return new ErrorResult("Kampanya Bulunamadı");

            _campaingDal.Delete(result);
            return new SuccessResult("Kampanya Silindi");
        }

        public IDataResult<Campaing> GetById(int id)
        {
            var result = _campaingDal.Get(p => p.CampaingId == id);
            if (result == null)
                return new ErrorDataResult<Campaing>("Kampanya Bulunamadı");

            return new SuccessDataResult<Campaing>(result);
        }

        public IDataResult<Campaing> GetByName(string campaingName)
        {
            var result = _campaingDal.Get(p => p.CampaingName == campaingName);
            if (result == null)
                return new ErrorDataResult<Campaing>("Kampanya Bulunamadı");

            return new SuccessDataResult<Campaing>(result);
        }

        public IDataResult<List<Campaing>> GetList()
        {
            var result = _campaingDal.GetList().ToList();
            if (result.Count == 0)
                return new ErrorDataResult<List<Campaing>>("Kampanya Bulunamadı");

            return new SuccessDataResult<List<Campaing>>(result);
        }

        public IDataResult<Campaing> Update(Campaing campaing)
        {
            var result = _campaingDal.Update(campaing);
            if (result == null)
                return new ErrorDataResult<Campaing>("Kampanya Güncellenemedi");

            return new SuccessDataResult<Campaing>(result);
        }
    }
}
