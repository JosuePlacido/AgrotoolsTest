using System.Linq;
using Agrotools.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace Agrotools.Data.DAO
{
	public class DAOQuiz : DAOBase<Quiz>, IDAOQuiz
	{
		public DAOQuiz(ApplicationDbContext context) : base(context)
		{
		}

		public async Task<Quiz[]> GetAllFromUser(string user)
		{
			return await _context.Quizs.Where(q => q.OwnerId == user).ToArrayAsync();
		}

		public async Task<Quiz[]> GetAllWithUserAndQuestions()
		{
			return await _context.Quizs.Include(q => q.Owner).Include(q => q.Questions).ToArrayAsync();
		}

		public override async Task<Quiz> GetById(string id)
		{
			var list = await _context.Quizs.Where(q => q.Id == id).FirstOrDefaultAsync();
			list.Questions = await _context.Questions.Where(q => q.QuizId == id)
				.OrderBy(q => q.Number).ToListAsync();
			return list;
		}

		public async Task<Quiz> GetByIdWithQuestionsAndAnswers(string id)
		{
			var list = await _context.Quizs.Where(q => q.Id == id).FirstOrDefaultAsync();
			list.Questions = await _context.Questions.Where(q => q.QuizId == id)
				.Include(q => q.Answers)
				.OrderBy(q => q.Number).ToListAsync();
			return list;
		}

		public async Task<Quiz> GetByIdWithUserAndQuestions(string id)
		{
			var list = await _context.Quizs.Where(q => q.Id == id)
				.Include(q => q.Owner).FirstOrDefaultAsync();
			list.Questions = await _context.Questions.Where(q => q.QuizId == id)
				.OrderBy(q => q.Number).ToListAsync();
			return list;
		}
	}
}
