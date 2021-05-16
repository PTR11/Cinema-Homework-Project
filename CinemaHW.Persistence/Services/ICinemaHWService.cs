using CinemaHW.Persistence;
using CinemaHW.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Persistence.Services
{
    public interface ICinemaHWService
    {
        List<Movie> GetMovies(String name = null);
        List<Actors> GetActorsByMovie(int movieId);
        List<Movie> GetLastUploadedMovies();
        List<Movie> GetMoviesByDate(DateTime date);
        Actors GetActor(int actorId);
        Movie GetMovieById(int id);
        List<Programs> GetProgramByMovieId(int id, DateTime date);
        List<Places> GetReservedPlaces(int programId);
        Int32 GetLineSizeOfRoom(int programId);
        Int32 GetColSizeOfRoom(int programId);
        RentViewModel NewRent(int programId);
        Byte[] GetMovieImage(int id);
        Task<Boolean> SaveRentAsync(int programid, String userName, List<Places> reservedPlaces, RentViewModel rent);
        Programs FindProgramById(int id);
        bool UpdateMovie(Movie movie);
        bool DeleteMovie(int id);
        Movie CreateMovie(Movie movie);
        Actors CreateActor(Actors actor);
        MoviesImage GetImageByMovieId(int movieId);
        bool DeleteActor(int id);
        bool UpdateActor(Actors actor);
        Programs CreateProgram(Programs program);
        bool DeleteProgram(int id);
        List<Programs> GetPrograms();
        Programs GetProgramById(int id);
        List<MoviesImage> GetImages();
        bool UpdateImage(MoviesImage image);
        MoviesImage CreateImage(MoviesImage image);
        bool DeleteImage(int id);
        bool UpdateProgram(Programs movie);
        List<Room> GetRooms();
        List<Rent> GetRentsById(int pid);
        bool IsRoomFree(Programs program);
        List<Places> GetReservedPlacesByRentId(int id);
        Rent GetRentById(int id);
        Users GetUserById(String id);
        Users GetUserByName(String name);
        List<Rent> GetRents();
        Task<Rent> CreateRent(Rent rent);
        void RefreshRentsList();
    }
}
