using System.Threading.Tasks;
using Agrotools.Models;

namespace Agrotools.Data.DAO
{
	public interface IDAOQuiz : IDAO<Quiz>
	{
		Task<Quiz> GetByIdWithQuestionsAndAnswers(string id);
		Task<Quiz> GetByIdWithUserAndQuestions(string id);
		Task<Quiz[]> GetAllFromUser(string user);
		Task<Quiz[]> GetAllWithUserAndQuestions();
	}
}
