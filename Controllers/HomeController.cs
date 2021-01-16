using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Agrotools.Models;
using Agrotools.Data.DAO;
using Microsoft.EntityFrameworkCore;

namespace Agrotools.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IDAOQuiz _dao;
		private readonly IDAOAnswer _daoAnswer;

		public HomeController(ILogger<HomeController> logger, IDAOQuiz dao, IDAOAnswer daoAnswer)
		{
			_logger = logger;
			_dao = dao;
			_daoAnswer = daoAnswer;
		}
		public async Task<IActionResult> Index()
		{
			var v = await _dao.GetAllWithUserAndQuestions();
			return View(v);
		}
		public async Task<IActionResult> Respond(string id)
		{
			return View(await _dao.GetByIdWithUserAndQuestions(id));
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Respond(RespondViewModel obj)
		{
			if (ModelState.IsValid && obj.Answer.Length == obj.QuestionId.Length)
			{
				Answer[] list = new Answer[obj.Answer.Length];
				for (int x = 0; x < list.Length; x++)
				{
					list[x] = new Answer
					{
						Date = DateTime.Now,
						Lat = obj.Lat[x],
						Long = obj.Long[x],
						QuestionId = obj.QuestionId[x],
						Text = obj.Answer[x]
					};
				}
				try
				{
					await _daoAnswer.AddAll(list);
				}
				catch (DbUpdateConcurrencyException)
				{
					return Problem();
				}
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction("Respond", new { id = obj.Quiz });
		}
		public IActionResult Privacy()
		{
			return View();
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
