using System.Linq;
using Agrotools.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace Agrotools.Data.DAO
{
	public class DAOAnswer : DAOBase<Answer>, IDAOAnswer
	{
		public DAOAnswer(ApplicationDbContext context) : base(context)
		{
		}

		public async Task AddAll(Answer[] list)
		{
			await _context.AddRangeAsync(list);
			await _context.SaveChangesAsync();
		}
	}
}
