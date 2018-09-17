using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ShareMe.SignalR.Server
{
	public class ReportsPublisher : Hub
	{
		public Task PublishReport(string reportName)
		{
			return Clients.All.InvokeAsync("OnReportPublished", reportName);
		}
	}
}
