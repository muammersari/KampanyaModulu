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
    public class ScheduleService : IScheduleService
    {
        IScheduleDal _scheduleDal;
        public ScheduleService(IScheduleDal scheduleDal)
        {
            _scheduleDal = scheduleDal;
        }

        public IDataResult<Schedule> Add(Schedule schedule)
        {
            var result = _scheduleDal.Add(schedule);
            if (result == null)
                return new ErrorDataResult<Schedule>("Tarife Eklenemedi");

            return new SuccessDataResult<Schedule>(result);
        }

        public IResult Delete(int id)
        {
            var result = _scheduleDal.Get(x => x.ScheduleId == id);
            if (result == null)
            {
                return new SuccessResult("Tarife Bulunamadı");
            }
            _scheduleDal.Delete(result);
            return new SuccessResult("Tarife Silindi");
        }

        public IDataResult<Schedule> GetById(int id)
        {
            var result = _scheduleDal.Get(p => p.ScheduleId == id);
            if (result == null)
                return new ErrorDataResult<Schedule>("Ürün Bulunamadı");

            return new SuccessDataResult<Schedule>(result);
        }

        public IDataResult<Schedule> GetByName(string scheduleName)
        {
            var result = _scheduleDal.Get(p => p.Name == scheduleName);
            if (result == null)
                return new ErrorDataResult<Schedule>("Tarife Bulunamadı");

            return new SuccessDataResult<Schedule>(result);
        }

        public IDataResult<List<Schedule>> GetList()
        {
            var result = _scheduleDal.GetList().ToList();
            if (result.Count == 0)
                return new ErrorDataResult<List<Schedule>>("Tarife Bulunamadı");

            return new SuccessDataResult<List<Schedule>>(result);
        }

        public IDataResult<Schedule> Update(Schedule schedule)
        {
            var result = _scheduleDal.Update(schedule);
            if (result == null)
                return new ErrorDataResult<Schedule>("Tarife Güncellenemedi");

            return new SuccessDataResult<Schedule>(result);
        }
    }
}
