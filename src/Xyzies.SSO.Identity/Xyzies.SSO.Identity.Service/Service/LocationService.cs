using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Repository;

namespace Xyzies.SSO.Identity.Services.Service
{
    public class LocationService : ILocaltionService
    {
        private readonly IStateRepository _stateRepo;
        private readonly ICityRepository _cityRepo;

        public LocationService(IStateRepository stateRepo, ICityRepository cityRepo)
        {
            _stateRepo = stateRepo;
            _cityRepo = cityRepo;
        }

        public async Task<List<City>> GetAllCities()
        {
            var cities = await _cityRepo.GetAsync();
            return cities.Include(city => city.State).ToList();
        }

        public async Task<List<City>> GetAllCities(string stateName)
        {
            var state = await _stateRepo.GetByAsync(s => s.Name == stateName || s.ShortName == stateName);
            var cities = await _cityRepo.GetAsync(c => c.State.Id == state.Id);
            return cities.ToList();
        }

        public async Task<List<State>> GetAllStates()
        {
            var state = await _stateRepo.GetAsync();
            return state.ToList();
        }

        public async Task SetCity(string city, string stateName)
        {
            var isCityExists = (await _cityRepo.GetByAsync(x => x.Name == city)) != null;
            if (!isCityExists)
            {
                var state = await _stateRepo.GetByAsync(x => x.Name == stateName || x.ShortName == stateName);
                if (state == null)
                {
                    var newState = new State { Name = stateName, ShortName = stateName };
                    var stateId = await _stateRepo.AddAsync(newState);
                    newState.Id = stateId;
                    await _cityRepo.AddAsync(new City { Name = city, State = state });
                }
                else
                {
                    await _cityRepo.AddAsync(new City { Name = city, State = state });
                }
            }
        }

        public async Task SetCity(List<City> cities)
        {
            var states = await _stateRepo.GetAsync();
            var statesList = states.ToList();
            foreach (var city in cities)
            {
                city.State.Id = (statesList.FirstOrDefault(x => x.Name == city.State.Name)).Id;
                var cityInDb = await _cityRepo.GetByAsync(x => x.Name.ToLower() == city.Name.ToLower());
                if(cityInDb == null)
                {
                    await _cityRepo.AddAsync(city);
                }
            }
        }

        public async Task SetState(string stateName)
        {
            var isStateExists = (await _stateRepo.GetByAsync(s => s.Name == stateName || s.ShortName == stateName)) != null;
            if (!isStateExists)
            {
                await _stateRepo.AddAsync(new State { Name = stateName, ShortName = stateName });
            }
        }

        public async Task SetState(List<State> states)
        {
            foreach (var state in states)
            {
                var stateInDb = await _stateRepo.GetByAsync(x => x.Name.ToLower() == state.Name.ToLower());
                if(stateInDb == null)
                {
                    await _stateRepo.AddAsync(state);
                }
            }
        }
    }
}
