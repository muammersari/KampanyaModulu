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
    public class CampaingController : ControllerBase
    {
        ICampaingService _campaingService;
        ICampaingAndProductService _campaingAndProductService;
        public CampaingController(ICampaingService campaingService, ICampaingAndProductService campaingAndProductService)
        {
            _campaingService = campaingService;
            _campaingAndProductService = campaingAndProductService;
        }

        [HttpPost("add")]
        public ActionResult Add(Campaing campaing)
        {
            var result = _campaingService.Add(campaing);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int campaingId)
        {
            var result = _campaingService.Delete(campaingId);
            if (result.Success)
            {
                var result1 = _campaingAndProductService.DeleteRange(campaingId);
                if (result1.Success)
                {
                    return Ok(result.Message);
                }
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getByCampaingId")]
        public ActionResult GetByCampaingId(int campaingId)
        {
            var result = _campaingService.GetById(campaingId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getByCampaingName")]
        public ActionResult GetByCampaingName(string campaingName)
        {
            var result = _campaingService.GetByName(campaingName);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getList")]
        public ActionResult GetList()
        {
            var result = _campaingService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public ActionResult Update(Campaing campaing)
        {
            var result = _campaingService.Update(campaing);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
