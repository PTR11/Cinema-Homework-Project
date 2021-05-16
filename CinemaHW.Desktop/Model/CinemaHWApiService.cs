using CinemaHW.Desktop.ViewModel;
using CinemaHW.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace CinemaHW.Desktop.Model
{
    public class CinemaHWApiService
    {
        private readonly HttpClient _client;

        public CinemaHWApiService(string baseAddress)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public async Task<bool> LoginAsync(String userName, String password)
        {
            LoginDto user = new LoginDto
            {
                UserName = userName,
                Password = password
            };
            var response = await _client.PostAsJsonAsync("api/Account/Login", user);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return false;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }
        public async Task LogoutAsync()
        {
            var response = await _client.PostAsync("api/Account/Logout", null);
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        
        

        #region Movies
        public async Task<IEnumerable<MovieDto>> LoadMoviesAsync()
        {
            var response = await _client.GetAsync("api/Movies");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<MovieDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task CreateMovieAsync(MovieDto movie)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Movies/", movie);
            movie.Id = (await response.Content.ReadAsAsync<MovieDto>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task UpdateMoviesAsync(MovieDto movie)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/Movies/{movie.Id}", movie);
           

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task UpdateImageAsync(MovieImageDto movie)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/MoviesImages/{movie.Id}", movie);


            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task DeleteMovieAsync(Int32 movieId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/Movies/{movieId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task<IEnumerable<MovieImageDto>> LoadImages()
        {
            var response = await _client.GetAsync("api/MoviesImages/");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<MovieImageDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        #endregion

        #region Actors
        public async Task<IEnumerable<ActorDto>> LoadActorsAsync(int movieId)
        {
            var response = await _client.GetAsync($"api/Actors/{movieId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<ActorDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task CreateActorAsync(ActorDto actor)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Actors/", actor);
            actor.Id = (await response.Content.ReadAsAsync<ActorDto>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task UpdateActorAsync(ActorDto actor)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/Actors/{actor.Id}", actor);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }
        public async Task DeleteActorAsync(Int32 actorId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/Actors/{actorId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }



        #endregion

        #region Programs
        public async Task<IEnumerable<ProgramDto>> LoadProgramsAsync()
        {
            var response = await _client.GetAsync("api/Programs");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<ProgramDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task CreateProgramAsync(ProgramDto program)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Programs/", program);
            program.Id = (await response.Content.ReadAsAsync<ProgramDto>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task UpdateProgramAsync(ProgramDto program)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/Programs/{program.Id}", program);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed)
                {
                    throw new NetworkException("Nem sikerült a foglalás");

                }
                else
                {
                    throw new NetworkException("Service returned response: " + response.StatusCode);
                }
            }

        }

        public async Task DeleteProgramAsync(Int32 programId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/Programs/{programId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }
        #endregion

        #region Room
        public async Task<IEnumerable<RoomDto>> LoadRooms()
        {
            var response = await _client.GetAsync("api/Room/");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<RoomDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }


        public bool IsRoomFree(List<MovieViewModel> movies,List<ProgramViewModel> programs, ProgramViewModel act,int roomId,DateTime start, int length)
        {
            foreach(ProgramDto program in programs)
            {
                Debug.WriteLine("faszom");
                if (program.RoomId == roomId && (!program.Date.Equals(act.Date)))
                {
                    
                    var movie = movies.Find(r => r.Id == program.MovieId);
                    var endTime = program.Date.AddMinutes(movie.Length + 15);
                    if(start.CompareTo(program.Date) >= 0 &&  endTime.CompareTo(start.AddMinutes(length)) < 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        #region Rent
        public async Task<IEnumerable<RentDto>> LoadRents(int id)
        {
            var response = await _client.GetAsync($"api/Rents/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<RentDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task<RentDto> GetRent(RentDto rent)
        {
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/Rents/", rent);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<RentDto>();
                }

                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        #endregion

        #region User
        public async Task<UserDto> GetUser(String id)
        {
            {
                var response = await _client.GetAsync($"api/Users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<UserDto>();
                }

                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

       
        #endregion

        #region Place
        public async Task<IEnumerable<PlaceDto>> LoadPlaces(int id)
        {
            var response = await _client.GetAsync($"api/Places/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<PlaceDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }
        #endregion
    }
}
