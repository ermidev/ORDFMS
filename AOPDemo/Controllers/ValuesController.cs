using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AOPDemo.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		private readonly AOPDemo.Repo.IRepository _repository;

		public ValuesController(AOPDemo.Repo.IRepository repository)
		{
			_repository = repository;
		}

		// GET api/values/5
		[HttpGet("{message}")]
		public string EchoMessage(string message)
		{
			return _repository.MethodA(message);
		}
	}
}
