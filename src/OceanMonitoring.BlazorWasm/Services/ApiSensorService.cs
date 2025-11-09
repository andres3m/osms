using OceanMonitoring.Core.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;


public class ApiSensorService
{
    private readonly HttpClient _http;
    public ApiSensorService(HttpClient http) => _http = http;


    public async Task<IEnumerable<SensorData>> GetAll() => await _http.GetFromJsonAsync<IEnumerable<SensorData>>("api/sensors");
    public async Task<SensorData> GetLatest(string id) => await _http.GetFromJsonAsync<SensorData>($"api/sensors/{id}");
}