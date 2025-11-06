using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
private readonly IHubContext<SensorHub> _hub; // forward reference, we'll define hub in API


private readonly Random _rnd = new();


public SensorSimulatorHostedService(ILogger<SensorSimulatorHostedService> logger, ISensorRepository repo, IHubContext<SensorHub> hub)
{
    _logger = logger;
    _repo = repo;
    _hub = hub;
}


protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    // Create a few sensors
    var sensors = new[] { "B1", "B2", "B3", "B4" };
    double t = 0;


    while (!stoppingToken.IsCancellationRequested)
    {
        t += 1.0;
        foreach (var id in sensors)
        {
            var depth = 100 + 50 * Math.Sin(t / 10.0 + id.GetHashCode() % 10) + _rnd.NextDouble() * 5;
            var temp = 4 + 10 * Math.Exp(-depth / 1000.0) + (_rnd.NextDouble() - 0.5) * 0.5;
            var sal = 34 + (_rnd.NextDouble() - 0.5) * 0.5;
            var noise = 80 + _rnd.NextDouble() * 40;


            var data = new SensorData
            {
                SensorId = id,
                Timestamp = DateTime.UtcNow,
                DepthMeters = Math.Round(depth, 2),
                TemperatureC = Math.Round(temp, 2),
                SalinityPpt = Math.Round(sal, 2),
                NoiseDb = Math.Round(noise, 2)
            };


            _repo.Add(data);


            // Broadcast via SignalR hub
            await _hub.Clients.All.SendAsync("SensorUpdate", data, stoppingToken);
        }


        await Task.Delay(1000, stoppingToken);
    }
}
}
}