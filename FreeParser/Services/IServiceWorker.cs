using System.Threading.Tasks;
using DBL.Controllers;

namespace FreeParser.Services
{
	public interface IServiceWorker
	{
		Task DoWork(DBController db);
	}
}