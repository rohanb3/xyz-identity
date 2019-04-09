using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Services.Service
{
    public interface ILocaltionService
    {
        Task<List<string>> GetAllCities();
        Task<List<string>> GetAllCities(string state);
        Task<List<State>> GetAllStates();
        Task SetState(string stateName);
        Task SetState(List<State> state);
        Task SetCity(string city, string stateName);
        Task SetCity(List<City> city);
    }
}
