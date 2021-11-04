using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICampaingService
    {
        IDataResult<Campaing> Add(Campaing campaing); // Kampanya Ekleme
        IDataResult<List<Campaing>> GetList(); // Tüm Kampanyaları listeleme
        IDataResult<Campaing> GetById(int id); // İd sine Göre Kampanya Getir
        IDataResult<Campaing> GetByName(string campaingName); // Adına Göre Kampanya Getir
        IDataResult<Campaing> Update(Campaing campaing); // Kampanya Güncelleme
        IResult Delete(int id); // Kampanya Silme
    }
}
