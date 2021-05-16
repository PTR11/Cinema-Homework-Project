using CinemaHW.Persistence;
using CinemaHW.Persistence.DTO;
using CinemaHW.Persistence.Services;
using CinemaHW.WebApi.Controllers;
using CinemaHW.WebApi2.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace CinemaHW.WebAPI.Tests
{
    public class ControllerTest : IDisposable
    {
        private readonly CinemaHWDbContext _context;
        private readonly CinemaHWService _service;
        private readonly MoviesController _controller1;
        private readonly ActorsController _controller2;
        private readonly ProgramsController _controller3;
        private readonly RentsController _controller4;
        private readonly PlacesController _controller5;
        public ControllerTest()
        {
            var option = new DbContextOptionsBuilder<CinemaHWDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            _context = new CinemaHWDbContext(option);
            _service = new CinemaHWService(_context,null);
            _controller1 = new MoviesController(_service);
            _controller2 = new ActorsController(_service);
            _controller3 = new ProgramsController(_service);
            _controller4 = new RentsController(_service);
            _controller5 = new PlacesController(_service);

            TestDbInitializer.Initialize(_context);


            var userManager = new UserManager<Users>(
                new UserStore<Users>(_context), null,
                new PasswordHasher<Users>(), null, null, null, null, null, null);

            var user = new Users { UserName = "testName", Id = "testId" };
            userManager.CreateAsync(user, "testPassword").Wait();

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testName"),
                new Claim(ClaimTypes.NameIdentifier, "testId"),
            });
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _controller4.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        #region Movies
        [Fact]
        public void GetMoviesTest()
        {
            
            // Act
            var result = _controller1.GetMovies();

            // Assert
            var content = Assert.IsAssignableFrom<IEnumerable<MovieDto>>(result.Value);
            Assert.Equal(3, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetMoviesByIdTest(Int32 id)
        {
            
            // Act
            var result = _controller1.GetMovie(id);

            // Assert
            var content = Assert.IsAssignableFrom<MovieDto>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidMovieTest()
        {
            
            // Arrange
            var id = 4;

            // Act
            var result = _controller1.GetMovie(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PostMovieTest()
        {
            // Arrange
            var newList = new MovieDto { Title = "Random Movie", Director = "Of course me", Length = 69, Description = "What did you except?", UploadTime = DateTime.Now,  };
            var count = _context.Movies.Count();

            // Act
            var result = _controller1.PostMovie(newList);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<Movie>(objectResult.Value);
            Assert.Equal(count + 1, _context.Movies.Count());
        }
        #endregion

        #region Actors

        [Fact]
        public void GetActorsTest()
        {
            
            // Act
            var result = _controller2.GetActors(1);

            // Assert
            var content = Assert.IsAssignableFrom<List<ActorDto>>(result.Value);
            Assert.Equal(5, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetActorByIdTest(Int32 id)
        {
            
            // Act
            var result = _controller2.GetActor(id);

            // Assert
            var content = Assert.IsAssignableFrom<ActorDto>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidActorTest()
        {
            
            // Arrange
            var id = 111;

            // Act
            var result = _controller2.GetActor(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PostActorTest()
        {
            
            // Arrange
            var newList = new ActorDto { Name = "Barnák Péter" };
            var count = _context.Actors.Count();

            // Act
            var result = _controller2.PostActor(newList);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<ActorDto>(objectResult.Value);
            Assert.Equal(count + 1, _context.Actors.Count());
        }

        #endregion

        #region Programs
        [Fact]
        public void GetProgramsTest()
        {
            
            // Act
            var result = _controller3.GetPrograms();

            // Assert
            var content = Assert.IsAssignableFrom<List<ProgramDto>>(result.Value);
            Assert.Equal(4, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetProgramByIdTest(Int32 id)
        {
            
            // Act
            var result = _controller3.GetProgram(id);

            // Assert
            var content = Assert.IsAssignableFrom<ProgramDto>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidProgramTest()
        {
            
            // Arrange
            var id = 111;

            // Act
            var result = _controller3.GetProgram(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PostProgramTest()
        {
            
            // Arrange
            var newList = new ActorDto { Name = "Barnák Péter" };
            var count = _context.Actors.Count();

            // Act
            var result = _controller2.PostActor(newList);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<ActorDto>(objectResult.Value);
            Assert.Equal(count + 1, _context.Actors.Count());
        }
        #endregion

        #region Rents


        [Fact]
        public void GetRentsTest()
        {

            // Act
            var result = _controller4.GetRents();

            // Assert
            var content = Assert.IsAssignableFrom<List<RentDto>>(result.Value);
            Assert.Equal(0, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetRentByIdTest(Int32 id)
        {

            // Act
            var result = _controller4.GetRentsByProgramId(id);

            // Assert
            var content = Assert.IsAssignableFrom<List<RentDto>>(result.Value);
            Assert.Equal(0, content.Count);
        }

        [Fact]
        public void GetInvalidRentTest()
        {

            // Arrange
            var id = 111;

            // Act
            var result = _controller4.GetRentById(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        #endregion

        #region Places
        [Fact]
        public void GetInvalidPlaceTest()
        {

            // Arrange
            var id = 4;

            // Act
            var result = _controller5.GetPlacesByRentId(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }
        #endregion
    }
}
