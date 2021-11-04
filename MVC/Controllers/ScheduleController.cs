using Entities.Concrete;
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
    public class ScheduleController : Controller
    {
        string baseUrl = "https://localhost:44371/api/";

        public IActionResult SchedulePage()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddSchedule(Schedule schedule)
        {
            //Tarife Ekleme
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "Schedule/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseTask = await client.PostAsJsonAsync(
                    baseUrl + "Schedule/add", schedule);

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {
                    var readTask = JsonConvert.DeserializeObject<VM<Schedule>>(await responseTask.Content.ReadAsStringAsync());
                    return Json(readTask);
                }
                else return Json("404");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateSchedule(Schedule schedule)
        {
            //Tarife Güncelleme
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "Schedule/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseTask = await client.PostAsJsonAsync(
                    baseUrl + "Schedule/update", schedule);

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {
                    var readTask = JsonConvert.DeserializeObject<VM<Schedule>>(await responseTask.Content.ReadAsStringAsync());
                    return Json(readTask);
                }
                else return Json("404");
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteSchedule(int scheduleId)
        {
            //Tarife silme
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "Schedule/");
                HttpResponseMessage responseTask = await client.DeleteAsync("delete?scheduleId=" + scheduleId);

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {
                    return Json("200");
                }
                else return Json("404");
            }
        }
        public async Task<IActionResult> ScheduleList()
        {
            //Tüm tarifeler getirilir
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(baseUrl + "Schedule/");
                HttpResponseMessage responseTask = await client.GetAsync("getList");

                if (responseTask.StatusCode == HttpStatusCode.OK)
                {

                    var readTask = JsonConvert.DeserializeObject<VM<List<Schedule>>>(await responseTask.Content.ReadAsStringAsync());
                    if (HttpContext.Request.Method.ToUpper() == "POST")
                        return Json(readTask);

                    return View(readTask);
                }
                else return Json("404");
            }
        }
    }
}
