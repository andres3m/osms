using System;

namespace OceanMonitoring.Core.Models
{
    public record SensorData
    {
        public string SensorId { get; init; }
        public DateTime Timestamp { get; init; }
        public double DepthMeters { get; init; }
        public double TemperatureC { get; init; }
        public double SalinityPpt { get; init; }
        public double NoiseDb { get; init; }
    }
}