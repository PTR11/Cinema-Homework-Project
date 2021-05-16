using CinemaHW.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Persistence.Services
{
    public class CinemaHWService : ICinemaHWService
    {
        private readonly CinemaHWDbContext context;
        private readonly UserManager<Users> userManager;
        public CinemaHWService(CinemaHWDbContext ct, UserManager<Users> uM)
        {
            context = ct;
            userManager = uM;
        }

        public List<Movie> GetLastUploadedMovies()
        {
            return context.Movies
                .OrderByDescending(m => m.UploadTime).Take(5).ToList();
        }

        public List<Movie> GetMovies(String name = null)
        {
            return context.Movies
                .Where(m => m.Title.Contains(name ?? ""))
                .ToList();
        }

        public List<Movie> GetMoviesByDate(DateTime date)
        {
            List<Programs> todayProgram = context.Program
                .Where(r => r.Date.Date == date.Date).ToList();
            return todayProgram.Select(p => p.Movie).Distinct().ToList();
        }

        public List<Actors> GetActorsByMovie(int movieId)
        {
            return context.Actors.Where(a => a.MovieId == movieId).ToList();
        }

        public Actors GetActor(int actorId)
        {
            return context.Actors.Single(a => a.Id == actorId);
        }

        public Movie GetMovieById(int id)
        {
            return context.Movies
                .Include(m => m.Actors)
                .Single(m => m.Id == id);
        }

        public List<Programs> GetProgramByMovieId(int id, DateTime date)
        {
            return context.Program
                .Where(p => p.MovieId == id && p.Date.Date == date.Date)
                .ToList();
        }

        public List<Programs> GetPrograms()
        {
            return context.Program
                .OrderByDescending(p => p.Date)
                .ToList();
        }

        public Programs GetProgramById(int id)
        {
            return context.Program.Single(p => p.Id == id);
        }

        public Byte[] GetMovieImage(int id)
        {

            Byte[] imageContent = context.MoviesImages
                .Where(i => i.MovieId == id).Select(r => r.Image).FirstOrDefault();
            return imageContent;
        }

        public List<Places> GetReservedPlaces(int programId)
        {
            List<Rent> rents = context.Rent.Where(p => p.ProgramId == programId).ToList();
            List<Places> result = new List<Places>();
            foreach (Rent item in rents)
            {
                result.AddRange(item.RentPlaces);
            }
            return result;
        }
        public List<Places> GetReservedPlacesByRentId(int id)
        {
            List<Rent> rents = context.Rent.Where(p => p.Id == id).ToList();
            List<Places> result = new List<Places>();
            foreach (Rent item in rents)
            {
                result.AddRange(item.RentPlaces);
            }
            return result;
        }

        public Int32 GetLineSizeOfRoom(int programId)
        {
            Programs program = context.Program.FirstOrDefault(r => r.Id == programId);
            Room room = program.Room;
            return room.Line;
        }

        public Int32 GetColSizeOfRoom(int programId)
        {
            Programs program = context.Program.FirstOrDefault(r => r.Id == programId);
            Room room = program.Room;
            return room.Column;
        }

        public RentViewModel NewRent(int programId)
        {
            Programs program = context.Program
                .Include(p => p.Room)
                .FirstOrDefault(p => p.Id == programId);
            RentViewModel rent = new RentViewModel();
            rent.Program = program;
            rent.Col = this.GetColSizeOfRoom(programId);
            rent.Row = this.GetLineSizeOfRoom(programId);
            rent.ReservedPlaces = this.GetReservedPlaces(programId);
            return rent;
        }

        public List<Room> GetRooms()
        {
            return context.Room.ToList();
        }

        public List<Rent> GetRents()
        {
            return context.Rent.ToList();
        }

        public Rent GetRentById(int id)
        {
            return context.Rent.Single(r => r.Id ==id);
        }

        public Users GetUserById(String id)
        {
            return context.Users.FirstOrDefault(u => u.Id.Equals(id));
        }
        public Users GetUserByName(String name)
        {
            return context.Users.FirstOrDefault(u => u.UserName.Equals(name));
        }
        public Programs FindProgramById(int id)
        {
            Programs program = context.Program
                .Include(p => p.Room)
                .FirstOrDefault(p => p.Id == id);
            return program;
        }

        public async Task<Boolean> SaveRentAsync(int programid, String userName, List<Places> reservedPlaces, RentViewModel rent)
        {
            if (programid == 0 || rent == null)
                return false;
            Users guest = await userManager.FindByNameAsync(userName);

            if (guest == null)
            {
                return false;
            }
            List<Places> tmpPlaces = this.GetReservedPlaces(programid);
            foreach (Places p in tmpPlaces)
            {
                Debug.WriteLine(p.Line + " " + p.Column);

                if (reservedPlaces.FirstOrDefault(r => r.Line == p.Line && r.Column == p.Column) != null)
                {
                    return false;
                }
            }
            context.Places.AddRange(reservedPlaces);


            context.Rent.Add(new Rent
            {
                User = guest,
                Program = this.FindProgramById(programid),
                RentPlaces = reservedPlaces
            });

            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #region Movies
        public bool UpdateMovie(Movie movie)
        {
            try
            {
                context.Update(movie);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }

        public bool DeleteMovie(int id)
        {
            var movie = context.Movies.Find(id);
            try
            {
                context.Remove(movie);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }

        public Movie CreateMovie(Movie movie)
        {
            try
            {
                context.Add(movie);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return movie;
        }
        #endregion

        #region Images
        public List<MoviesImage> GetImages()
        {
            return context.MoviesImages.ToList();
        }

        public MoviesImage GetImageByMovieId(int movieId)
        {
            return context.MoviesImages.FirstOrDefault(m => m.MovieId == movieId);
        }

        public bool UpdateImage(MoviesImage image)
        {
            try
            {
                context.Update(image);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }

        public MoviesImage CreateImage(MoviesImage image)
        {
            try
            {
                context.Add(image);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return image;
        }

        public bool DeleteImage(int id)
        {
            var image = context.MoviesImages.Find(id);
            try
            {
                context.Remove(image);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }


        #endregion

        #region Actors
        public Actors CreateActor(Actors actor)
        {
            try
            {
                context.Actors.Add(actor);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException e)
            {
                return null;
            }
            return actor;
        }

        public bool DeleteActor(int id)
        {
            var actor = context.Actors.Find(id);
            try
            {
                context.Remove(actor);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }

        public bool UpdateActor(Actors actor)
        {
            try
            {
                context.Update(actor);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Proram
        public Programs CreateProgram(Programs program)
        {
            try
            {
                context.Program.Add(program);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return program;
        }

        public bool DeleteProgram(int id)
        {
            var actor = context.Program.FirstOrDefault(p => p.Id == id);
            try
            {

                context.Remove(actor);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }

        public bool UpdateProgram(Programs program)
        {
            try
            {
                var item = context.Set<Programs>().Local.FirstOrDefault(entry => entry.Id.Equals(program.Id));
                if (item != null)
                {
                    // detach
                    context.Entry(item).State = EntityState.Detached;
                }
                context.Entry(program).State = EntityState.Modified;

                //context.Update(program);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }

        public bool IsRoomFree(Programs program)
        {
            var programLength = (context.Movies
                    .FirstOrDefault(m => m.Id == program.MovieId)
                    .Length + 15) * (-1);
            foreach (var item in context.Program)
            {
                if (item.RoomId == program.RoomId && item.Id != program.Id)
                {
                    var itemDur = context.Movies
                        .FirstOrDefault(m => m.Id == item.MovieId)
                        .Length + 15;
                    var itemInterval = item.Date.AddMinutes(itemDur);

                    var screeningInterval = item.Date.AddMinutes(programLength);

                    if (program.Date < itemInterval && program.Date > screeningInterval)
                    {
                        throw new Exception();
                    }
                }
            }
            return true;
        }
        #endregion

        #region Rent
        public List<Rent> GetRentsById(int pid)
        {
            return context.Rent.Where(r => r.ProgramId == pid).ToList();
        }
        #endregion

        #region Place
        public async Task<Rent> CreateRent(Rent rent)
        {
            Rent r = context.Rent.FirstOrDefault(t => t.UserId.Equals(rent.UserId) && t.ProgramId == rent.ProgramId);
            if (r == null)//Eladó
            {
                r = new Rent
                {
                    UserId = rent.UserId,
                    ProgramId = rent.ProgramId
                };
                context.Add(r);
            }
            
            foreach (Places place in rent.RentPlaces)
            {
                Places find = context.Places.FirstOrDefault(p => p.Line == place.Line && p.Column == place.Column);
                if(find != null)
                {
                    var entity = context.Places.Attach(find);
                    find.Status = 1;
                    find.RentId = r.Id;
                    context.Entry(find).State = EntityState.Modified;
                    r.RentPlaces.Add(find);
                }
                else
                {
                    place.Rent = r;
                    place.RentId = r.Id;
                    context.Places.Add(place);
                }
                
            }
            
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return null;
            }
            return r;
        }

        public void RefreshRentsList()
        {
            List<Rent> copy = new List<Rent>(context.Rent.ToList());
            foreach(Rent rent in copy)
            {
                if(rent.RentPlaces.Count == 0)
                {
                    context.Rent.Remove(rent);
                }
            }
            context.SaveChanges();
        }
        #endregion
    }
}
