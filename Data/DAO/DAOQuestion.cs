using System.Linq;
using Agrotools.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace Agrotools.Data.DAO
{
	public class DAOQuestion : DAOBase<Question>, IDAOQuestion
	{
		public DAOQuestion(ApplicationDbContext context) : base(context)
		{
		}

		public async Task AddAll(Question[] questions)
		{
			await _context.Questions.AddRangeAsync(questions);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAllWithId(string[] removedQuestions)
		{
			if (removedQuestions != null && removedQuestions.Length > 0)
			{
				_context.Questions.RemoveRange(await _context.Questions
					.Where(q => removedQuestions.Contains(q.Id)).ToArrayAsync());
				await _context.SaveChangesAsync();
			}
		}

		public async Task UpdateAll(Question[] questions)
		{
			_context.Questions.UpdateRange(questions);
			await _context.SaveChangesAsync();
		}
	}
}
