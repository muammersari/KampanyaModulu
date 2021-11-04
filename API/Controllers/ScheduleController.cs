using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private IScheduleService _scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost("add")]
        public ActionResult Add(Schedule schedule)
        {
            var result = _scheduleService.Add(schedule);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int scheduleId)
        {
            var result = _scheduleService.Delete(scheduleId);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getByScheduleId")]
        public ActionResult GetByScheduleId(int scheduleId)
        {
            var result = _scheduleService.GetById(scheduleId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("getByScheduleName")]
        public ActionResult GetByScheduleName(string scheduleName)
        {
            var result = _scheduleService.GetByName(scheduleName);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getList")]
        public ActionResult GetList()
        {
            var result = _scheduleService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public ActionResult Update(Schedule schedule)
        {
            var result = _scheduleService.Update(schedule);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
