using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IScheduleService
    {
        IDataResult<Schedule> Add(Schedule schedule); // Tarife Ekleme
        IDataResult<List<Schedule>> GetList(); // Tüm Tarifeleri listeleme
        IDataResult<Schedule> GetById(int id); // İd sine Göre Tarife Getir
        IDataResult<Schedule> GetByName(string scheduleName); // Adına Göre Tarife Getir
        IDataResult<Schedule> Update(Schedule schedule); // Tarife Güncelleme
        IResult Delete(int id); // Tarife Silme
    }
}
