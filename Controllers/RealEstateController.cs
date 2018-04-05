﻿using Microsoft.AspNetCore.Mvc;
using RentApp.Managers;
using RentApp.Models.DbModels;
using RentApp.Models.Structs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentApp.Controllers
{
    [Route("api/[controller]")]
    public class RealEstateController : ApiController
    {
        private readonly RealEstateManager _realEstateManager;

        public RealEstateController(RealEstateManager realEstateManager)
        {
            _realEstateManager = realEstateManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<RealEstateObject>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Task.Factory.StartNew(() => _realEstateManager.GetAll());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var item = _realEstateManager.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RealEstateObject item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var result = await Task.Factory.StartNew(() => _realEstateManager.Create(item));
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RealEstateObject item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var result = await Task.Factory.StartNew(() => _realEstateManager.Update(item));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = _realEstateManager.GetById(id);
            if (item == null)
            {
                return BadRequest();
            }

            var result = await Task.Factory.StartNew(() => _realEstateManager.Remove(id));
            return Ok(result);
        }
    }
}
