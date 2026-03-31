using Microsoft.Extensions.Hosting;

namespace Server.Controllers
{
    public class BackGroundGameTicks : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            const int tickRate = 60;
            const double tickIntervalMs = 1000.0 / tickRate;

            var nextTick = DateTime.UtcNow;

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;

                // Если мы отстаём — не накапливаем лаг бесконечно
                if (now > nextTick)
                {
                    nextTick = now;
                }

                GamesController.Tick();

                nextTick = nextTick.AddMilliseconds(tickIntervalMs);

                var delay = nextTick - DateTime.UtcNow;

                if (delay > TimeSpan.Zero)
                {
                    await Task.Delay(delay, stoppingToken);
                }
                else
                {
                    // если не успеваем — просто пропускаем ожидание
                    await Task.Yield();
                }
            }
        }
    }
}
