using System;
using Microsoft.AspNetCore.Mvc;

namespace AOPDemo.Controllers
{
	[Route("api/[controller]")]
	public class CustomersController : Controller
	{
        public AOPDemo.Repo.IRepository _repository { get; set; }


		// GET api/values/5
		[HttpGet("{message}")]
		public string EchoMessage(string message)
		{
			return _repository.MethodA(message);
		}
	}
}
