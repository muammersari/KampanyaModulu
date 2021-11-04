using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //register ettiğimiz interface lerimiz burada yer alıyor ve yönetimi buradan yapılıyor
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<ScheduleService>().As<IScheduleService>();
            builder.RegisterType<EfScheduleDal>().As<IScheduleDal>();

            builder.RegisterType<CampaingService>().As<ICampaingService>();
            builder.RegisterType<EfCampaingDal>().As<ICampaingDal>();

            builder.RegisterType<CampaingAndProductService>().As<ICampaingAndProductService>();
            builder.RegisterType<EfCampaingAndProductDal>().As<ICampaingAndProductDal>();
        }
    }
}
