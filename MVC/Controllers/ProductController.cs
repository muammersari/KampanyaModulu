using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace MVC.Controllers
{
    public class ProductController : Controller
    {
        string baseUrl = "https://localhost:44371/api/";

        [HttpGet]
        public IActionResult ProductPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            //Ürün Ekleme
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "Product/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseTask = await client.PostAsJsonAsync(
                    baseUrl + "Product/add", product);

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {
                    var readTask = JsonConvert.DeserializeObject<VM<Product>>(await responseTask.Content.ReadAsStringAsync());
                    return Json(readTask);
                }
                else return Json("404");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            //Ürün Güncelleme
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "Product/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseTask = await client.PostAsJsonAsync(
                    baseUrl + "Product/update", product);

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {
                    var readTask = JsonConvert.DeserializeObject<VM<Product>>(await responseTask.Content.ReadAsStringAsync());
                    return Json(readTask);
                }
                else return Json("404");
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            //ürün silme
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "Product/");
                HttpResponseMessage responseTask = await client.DeleteAsync("delete?productId=" + productId);

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {
                    return Json("200");
                }
                else return Json("404");
            }
        }
        public async Task<IActionResult> ProductList()
        {
            // tüm ürünleri listeleme
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(baseUrl + "Product/");
                HttpResponseMessage responseTask = await client.GetAsync("getList");

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {

                    var readTask = JsonConvert.DeserializeObject<VM<List<Product>>>(await responseTask.Content.ReadAsStringAsync());
                    if (HttpContext.Request.Method.ToUpper() == "POST")
                        return Json(readTask);

                    return View(readTask);
                }
                else return Json("404");
            }
        }
    }
}

