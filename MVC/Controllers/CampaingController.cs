using Entities.Concrete;
using Entities.Concrete.DTO;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class CampaingController : Controller
    {
       
        string baseUrl = "https://localhost:44371/api/";

        public IActionResult CampaingPage()
        {
            return View();
        }

        //Kampanya bilgilerini ekliyoruz
        [HttpPost]
        public async Task<ActionResult> AddCampaing(Campaing campaing)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "Campaing/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseTask = await client.PostAsJsonAsync(
                    baseUrl + "Campaing/add", campaing);

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {
                    var readTask = JsonConvert.DeserializeObject<VM<Campaing>>(await responseTask.Content.ReadAsStringAsync());
                    return Json(readTask);
                }
                else return Json("404");
            }
        }

        //Kampanya bilgilerini Güncelliyoruz
        [HttpPost]
        public async Task<ActionResult> UpdateCampaing(Campaing campaing)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "Campaing/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseTask = await client.PostAsJsonAsync(
                    baseUrl + "Campaing/update", campaing);

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {
                    var readTask = JsonConvert.DeserializeObject<VM<Campaing>>(await responseTask.Content.ReadAsStringAsync());
                    return Json(readTask);
                }
                else return Json("404");
            }
        }

        //Kampanyanın ürünlerini kampanya ürünler tablosuna ekliyoruz
        [HttpPost]
        public async Task<ActionResult> AddCampaingAndProduct(CampaingAndProduct campaingAndProduct)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "CampaingAndProduct/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseTask = await client.PostAsJsonAsync(
                    baseUrl + "CampaingAndProduct/add", campaingAndProduct);

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {
                    var readTask = JsonConvert.DeserializeObject<VM<Campaing>>(await responseTask.Content.ReadAsStringAsync());
                    return Json(readTask);
                }
                else return Json("404");
            }
        }
        int sayac = 0;
        //Kampanyanın ürünlerini güncelliyoruz
        [HttpPost]
        public async Task<ActionResult> UpdateCampaingAndProduct(CampaingAndProduct campaingAndProduct)
        {
            if (sayac == 0) // metoda ilk girildiğinde ürünleri siliyoruz. her seferinde silinmemesi için sayaç tanımladık
            {
                using (var client = new HttpClient())
                {
                    //Kampanyanın ürünlerini güncellerken eski ürünlerin hepsini siliyoruz.
                    client.BaseAddress = new Uri(baseUrl + "CampaingAndProduct/");
                    HttpResponseMessage responseTask = await client.DeleteAsync("deleteRange?campaingId=" + campaingAndProduct.CampaingId);
                    if (responseTask.StatusCode == HttpStatusCode.OK)
                    {
                        sayac++;
                    }
                }
            }

            using (var client = new HttpClient())
            {
                //Yeni ürünlerimizi ekliyoruz
                client.BaseAddress = new Uri(baseUrl + "CampaingAndProduct/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseTask = await client.PostAsJsonAsync(
                    baseUrl + "CampaingAndProduct/add", campaingAndProduct);

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {
                    var readTask = JsonConvert.DeserializeObject<VM<Campaing>>(await responseTask.Content.ReadAsStringAsync());
                    return Json(readTask);
                }
                else return Json("404");
            }
        }

        //Kampanya yı id sine göre siliyoruz
        [HttpPost]
        public async Task<ActionResult> DeleteCampaing(int campaingId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "Campaing/");
                HttpResponseMessage responseTask = await client.DeleteAsync("delete?campaingId=" + campaingId);

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {
                    return Json("200");
                }
                else return Json("404");
            }
        }

        //Kampanyalara ait tüm bilgileri çekiyoruz
        public async Task<IActionResult> CampaingAndProductList()
        {
            //Product-Schedule-Campaing-CampaingAndProduct tablolarının içinde  bulunduğu view modelimiz
            CampaingAndProductViewModel vm = new CampaingAndProductViewModel();
            VM<List<Campaing>> readTask = new VM<List<Campaing>>();
            using (var client = new HttpClient())
            {
                //Tüm kampanyaları çekiyoruz
                client.BaseAddress = new Uri(baseUrl + "Campaing/");
                HttpResponseMessage responseTask = await client.GetAsync("getList");
                if (responseTask.StatusCode == HttpStatusCode.OK)
                {
                    readTask = JsonConvert.DeserializeObject<VM<List<Campaing>>>(await responseTask.Content.ReadAsStringAsync());
                    vm.Campaings = readTask.data;
                }
                else return Json("404");

            }
            //Tüm Bilgileri viewvModel imizde tutuyoruz.
            VM<List<CampaingAndProduct>> readTask1 = new VM<List<CampaingAndProduct>>();
            List<CampaingAndProduct> campaingAndProducts = new List<CampaingAndProduct>();
            foreach (var item in readTask.data)
            {
                //Kampanyalara ait ürünleri çekiyoruz
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl + "CampaingAndProduct/");
                    HttpResponseMessage responseTask = await client.GetAsync("getByCampaingId?campaingId=" + item.CampaingId);
                    if (responseTask.StatusCode == HttpStatusCode.OK)
                    {
                        readTask1 = JsonConvert.DeserializeObject<VM<List<CampaingAndProduct>>>(await responseTask.Content.ReadAsStringAsync());
                        foreach (var item1 in readTask1.data)
                        {
                            campaingAndProducts.Add(item1);
                        }
                    }
                    else return Json("404");
                }
            }
            vm.CampaingAndProducts = campaingAndProducts;


            var liste = campaingAndProducts.Select(x => x.ProductId).Distinct().ToList();
            List<Product> products = new List<Product>();
            foreach (var item in liste)
            {
                //Kampanyalara ait ürünlerin tüm bilgilerini alıyoruz
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl + "Product/");
                    HttpResponseMessage responseTask = await client.GetAsync("getByProductId?productId=" + item);
                    if (responseTask.StatusCode == HttpStatusCode.OK)
                    {
                        var readTask2 = JsonConvert.DeserializeObject<VM<Product>>(await responseTask.Content.ReadAsStringAsync());
                        products.Add(readTask2.data);
                    }
                    else return Json("404");
                }
            }
            vm.Products = products;

            List<Schedule> schedules = new List<Schedule>();
            foreach (var item in readTask.data)
            {
                //Kampanyalara ait tüm tarife bilgilerini alıyoruz
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl + "Schedule/");
                    HttpResponseMessage responseTask = await client.GetAsync("getByScheduleId?scheduleId=" + item.ScheduleId);
                    if (responseTask.StatusCode == HttpStatusCode.OK)
                    {
                        var readTask2 = JsonConvert.DeserializeObject<VM<Schedule>>(await responseTask.Content.ReadAsStringAsync());
                        schedules.Add(readTask2.data);
                    }
                    else return Json("404");
                }
            }
            vm.Schedules = schedules;
            return Json(vm);


        }
    }
}
