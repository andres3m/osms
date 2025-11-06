using OceanMonitoring.Core.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


namespace OceanMonitoring.Infrastructure.Services
{
    public interface ISensorRepository
    {
        IEnumerable<SensorData> GetLatestAll();
        SensorData GetLatest(string sensorId);
        void Add(SensorData data);
    }


    public class SensorRepository : ISensorRepository
    {
        private readonly ConcurrentDictionary<string, SensorData> _latest = new();


        public IEnumerable<SensorData> GetLatestAll() => _latest.Values.OrderBy(x => x.SensorId);
        public SensorData GetLatest(string sensorId) => _latest.TryGetValue(sensorId, out var d) ? d : null;
        public void Add(SensorData data) => _latest[data.SensorId] = data;
    }
}