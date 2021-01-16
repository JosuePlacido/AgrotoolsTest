using System.Threading.Tasks;
using Agrotools.Models;

namespace Agrotools.Data.DAO
{
	public interface IDAOAnswer : IDAO<Answer>
	{
		Task AddAll(Answer[] list);
	}
}
