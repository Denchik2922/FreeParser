using System.Threading.Tasks;

namespace FreeParser.Services.Workers
{
	public interface ISendOrderWorker
	{
		Task DoWork();
	}
}