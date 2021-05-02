using System.Threading.Tasks;

namespace FreeParser.Services.Workers
{
	public interface ICategoryWorker
	{
		Task DoWork();
	}
}