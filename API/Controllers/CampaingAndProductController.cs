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
    public class CampaingAndProductController : ControllerBase
    {
        ICampaingAndProductService _campaingAndProductService;
        public CampaingAndProductController(ICampaingAndProductService campaingAndProductService)
        {
            _campaingAndProductService = campaingAndProductService;
        }

        [HttpPost("add")]
        public ActionResult Add(CampaingAndProduct campaingAndProduct)
        {
            var result = _campaingAndProductService.Add(campaingAndProduct);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int campaingId, int productId)
        {
            var result = _campaingAndProductService.Delete(campaingId, productId);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("deleteRange")]
        public IActionResult DeleteRange(int campaingId)
        {
            var result = _campaingAndProductService.DeleteRange(campaingId);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getByCampaingId")]
        public ActionResult GetByCampaingId(int campaingId)
        {
            var result = _campaingAndProductService.GetByCampaingId(campaingId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
