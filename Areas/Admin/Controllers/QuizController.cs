using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agrotools.Data;
using Agrotools.Models;
using Agrotools.Data.DAO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Agrotools.Areas.Admin.Data;

namespace Agrotools.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class QuizController : Controller
	{
		private readonly IDAOQuiz _dao;
		private readonly IDAOQuestion _daoQuestion;

		public QuizController(IDAOQuiz dao, IDAOQuestion daoQuestion)
		{
			_dao = dao;
			_daoQuestion = daoQuestion;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _dao.GetAllFromUser(User.FindFirstValue(ClaimTypes.NameIdentifier)));
		}

		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var quiz = await _dao.GetByIdWithQuestionsAndAnswers(id);
			if (quiz == null)
			{
				return NotFound();
			}

			return View(quiz);
		}

		public IActionResult Create()
		{
			return View(new Quiz());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Quiz quiz)
		{
			if (ModelState.IsValid)
			{
				quiz.CreatedAt = DateTime.UtcNow;
				quiz.OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				quiz.Questions = quiz.Questions.Select((q, index) =>
				{
					q.Number = index + 1;
					return q;
				}).ToList();
				await _dao.Add(quiz);
				return RedirectToAction(nameof(Index));
			}
			return View(quiz);
		}

		// GET: Admin/Quiz/Edit/5
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var quiz = await _dao.GetById(id);
			if (quiz == null)
			{
				return NotFound();
			}
			return View(quiz);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, EditModel quizModel)
		{
			Quiz quiz = (Quiz)quizModel;
			quiz.OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (id != quiz.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					using (var transcation = await _dao.BeginTtransaction())
					{
						await _dao.Update(quiz);
						await _daoQuestion.DeleteAllWithId(quizModel.RemovedQuestions);
						await _daoQuestion.UpdateAll(quiz.Questions.Where(q => q.Id != null)
							.Select((q, index) =>
							{
								q.QuizId = quiz.Id;
								q.Number = index + 1;
								return q;
							}).ToArray());
						await _daoQuestion.AddAll(quiz.Questions.Where(q => q.Id == null)
							.Select((q, index) =>
							{
								q.QuizId = quiz.Id;
								q.Number = index + 1;
								return q;
							}).ToArray());
						transcation.Commit();
					}
				}
				catch (DbUpdateConcurrencyException)
				{
					return Problem();
				}
				return RedirectToAction(nameof(Index));
			}
			return View(quiz);
		}

		public async Task<IActionResult> Delete(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var quiz = await _dao.GetById(id);
			if (quiz == null)
			{
				return NotFound();
			}
			return View(quiz);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			Quiz quiz = await _dao.GetById(id);
			if (quiz == null)
			{
				return NotFound();
			}
			await _dao.Delete(quiz);
			return RedirectToAction(nameof(Index));
		}
	}
}
