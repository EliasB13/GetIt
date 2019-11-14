﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APZ_BACKEND.Presentation.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class ItemTakingController : Controller
	{
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		[HttpPost("take-item")]
		public async Task<IActionResult> TakeItem(int itemId)
		{
			return Ok();
		}

		[HttpPost("return-item")]
		public async Task<IActionResult> ReturnItem(int itemId)
		{
			return Ok();
		}

		// PUT api/<controller>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
