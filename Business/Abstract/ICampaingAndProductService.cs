using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICampaingAndProductService
    {
        IDataResult<CampaingAndProduct> Add(CampaingAndProduct campaingAndProduct); // Kampanyaya ürün Ekleme
        IDataResult<List<CampaingAndProduct>> GetByCampaingId(int id); // İd sine Göre ürün listesini Getir
        IDataResult<CampaingAndProduct> Update(CampaingAndProduct campaingAndProduct); // Kampanya ürünleri Güncelleme
        IResult DeleteRange(int id); // Kampanya Silme
        IResult Delete(int campaingId, int productId); // Kampanya dan 1 tane ürün silme
    }
}
