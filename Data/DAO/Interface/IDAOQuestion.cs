using System.Threading.Tasks;
using Agrotools.Models;

namespace Agrotools.Data.DAO
{
	public interface IDAOQuestion : IDAO<Question>
	{
		Task UpdateAll(Question[] questions);
		Task AddAll(Question[] questions);
		Task DeleteAllWithId(string[] removedQuestions);
	}
}
