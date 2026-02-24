using Microsoft.Extensions.Hosting;

namespace Server.Controllers
{
    public class BackGroundGameTicks : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                GamesController.Tick();
                await Task.Delay(1, stoppingToken);
            }
        }
    }
}
