using Microsoft.AspNetCore.Mvc;
using RentApp.Managers;
using RentApp.Models.Interfaces;
using RentApp.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentApp.Controllers
{
    [Route("api/[controller]")]
    public class OfferController : ApiController
    {
        private readonly OfferManager _offerManager;


        public OfferController(OfferManager offerManager)
        {
            _offerManager = offerManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<IOffer>), 200)]
        public IActionResult GetByUserId(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return BadRequest();
            }

            var result = _offerManager.GetByUserId(id);

            return Ok(result);
        }
        [HttpPost, Route("imgCheck")]
        [ProducesResponseType(typeof(List<string>), 200)]
        public IActionResult CheckIfImgsExist([FromBody]List<string> imgSourceList)
        {
            if (imgSourceList == null || !imgSourceList.Any())
            {
                return BadRequest();
            }

            var result = _offerManager.CheckImageExist(imgSourceList);

            return Ok(result);
        }

        [HttpPost, Route("search")]
        public async Task<IActionResult> GetByFilter([FromBody] OfferFilterRequest filter)
        {
            if (filter == null)
            {
                return BadRequest();
            }

            var result = await Task.Factory.StartNew(() => _offerManager.GetByFilter(filter));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOfferRequest item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var result = await Task.Factory.StartNew(() => _offerManager.Create(item));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await Task.Factory.StartNew(() => _offerManager.Delete(id));
            return Ok();
        }
    }
}
