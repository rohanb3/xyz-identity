using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Services.Service
{
    public interface ILocaltionService
    {
        Task<List<City>> GetAllCities();
        Task<List<City>> GetAllCities(List<Guid> ids);
        Task<List<City>> GetAllCities(string state);
        Task<List<State>> GetAllStates();
        Task<List<State>> GetAllStates(List<Guid> ids);
        Task SetState(string stateName);
        Task SetState(List<State> state);
        Task SetCity(string city, string stateName);
        Task SetCity(List<City> city);
    }
}
