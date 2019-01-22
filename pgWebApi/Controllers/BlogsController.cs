using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pgWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pgWebApi.Controllers
{
	[Route("api/[controller]")]
	public class BlogsController : Controller
	{
		private readonly DbContextOptions<BloggingContext> _options;

		public BlogsController(DbContextOptions<BloggingContext> options)
		{
			_options = options;
		}

		// GET: api/<controller>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			List<string> urls = new List<string>();
			using (var db = new BloggingContext(_options))
			{
				Trace.WriteLine("All blogs in database:");
				foreach (var blog in db.Blogs)
				{
					Trace.WriteLine($" - {blog.Url}");
					urls.Add(blog.Url);
				}
			}

			return urls;
		}

		// GET api/<controller>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<controller>
		[HttpPost]
		public void Post([FromBody]string url)
		{
			using (var db = new BloggingContext(_options))
			{
				db.Blogs.Add(new Blog { Url = url });
				var count = db.SaveChanges();
				Trace.WriteLine($"{count} records saved to database");
				Trace.WriteLine("All blogs in database:");
				foreach (var blog in db.Blogs)
				{
					Trace.WriteLine(" - {0}", blog.Url);
				}
			}
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
