using CinemaHW.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CinemaHW.WebAPI.Tests
{
    internal class TestDbInitializer
    {
        internal static void Initialize(CinemaHWDbContext context)
        {
            if (context.Movies.Any())
            {
                return;
            }
            var actors = new List<Actors> {
                new Actors()
                {
                    Id = 1,
                    Name = "Mark Hamil"
                }, new Actors()
                {
                    Id = 2,
                    Name = "Harrison Ford"
                }, new Actors()
                {
                    Id = 3,
                    Name = "Carrie Fisher"
                }, new Actors()
                {
                    Id = 4,
                    Name = "Billy Dee Williams"
                }, new Actors()
                {
                    Id = 5,
                    Name = "Anthony Daniels"
                }, new Actors()
                {
                    Id = 6,
                    Name = "François Cluzet"
                }, new Actors()
                {
                    Id = 7,
                    Name = "Omar Sy"
                }, new Actors()
                {
                    Id = 8,
                    Name = "Tom Hanks"
                }, new Actors()
                {
                    Id = 9,
                    Name = "Robin Wright"
                }, new Actors()
                {
                    Id = 10,
                    Name = "Gary Sinise"
                }
            };
            var movies = new List<Movie>
            {
                new Movie()
                {
                    Id = 1,
                    Title = "Star Wars: A birodalom visszavág",
                    Length = 127,
                    Director = "George Lucas",
                    UploadTime = DateTime.Now,
                    Actors = new List<Actors>
                    {
                        actors[0],actors[1],actors[2],actors[3],actors[4],
                    },
                    Description = "A film három évvel a rettegett Halálcsillag elpusztítása után játszódik. Luke Skywalker, Han Solo, Leia Organa hercegnő és a Lázadó Szövetség nyomukban a Galaktikus Birodalom Darth Vader vezette erőivel menekülni kényszerülnek. Luke elszakad barátaitól és egy félreeső bolygóra utazik, ahol Yoda Jedi mestertől megtanulja használni az Erőt. Közben Vader titokban csapdát állít Luke-nak, ami egy kegyetlen párbajhoz, majd egy megdöbbentő szembesítéshez vezet."
                },
                new Movie()
                {
                    Id = 2,
                    Title="Életrevalók",
                    Length = 113,
                    Director = "Olivier Nakache",
                    UploadTime = DateTime.Now,
                    Actors = new List<Actors>
                    {
                        actors[5],actors[6]
                    },
                    Description = "Az ejtőernyős baleset után tolószékbe kerülő gazdag arisztokrata, Philippe felfogadja otthoni segítőnek a külvárosi gettóból jött Drisst. Azt az embert, aki most szabadult a börtönből, és talán a legkevésbé alkalmas a feladatra. Két világ találkozik és ismeri meg egymást, és az őrült, vicces és meghatározó közös élmények nyomán kapcsolatukból meglepetésszerűen barátság lesz, amely szinte érinthetetlenné teszi őket a külvilág számára."
                },
                new Movie()
                {
                    Id = 3,
                    Title="Forest Gump",
                    Length = 142,
                    Director = "Robert Zemeckis",
                    UploadTime = DateTime.Now,
                    Actors = new List<Actors>
                    {
                        actors[7],actors[8],actors[9]
                    },
                    Description = "A georgiai Savannah városka árnyas buszmegállójában különös mesemondó üldögél. Forrest Gump mindent látott és mindent átélt, de nem mindent értett. Nem éppen a legfényesebb elme. De hát az anyja is mindig azt mondta: 'Csak az a hülye, aki hülyeséget csinál.' Forrest Gump pedig semmi egyebet nem csinált, mint jelen volt a XX. század minden fontos eseményén a focipályától a harctérig, az elnökök klubjától a médiavitákig, míg végül meg nem pihent egyetlen igaz szerelme karjában. Forrest Gump IQ-ja nem szárnyal az egekig, de rendkívül becsületes és jólelkű fiú. Különös véletlenek azonban hozzásegítik, hogy az 1950-es évektől 1970-ig Amerika minden jelentős eseményén részt vegyen, és minden jelentős személyiségével találkozzon, köztük: Elvis Presley-vel, JFK-vel, Lyndon Johnsonnal, Richard Nixonnal. Forrest elvégzi a főiskolát, harcol Vietnamban, élsportoló lesz, az egyetlen probléma csak az, hogy túl buta ahhoz, hogy megértse ezen fontos események jelentőségét."
                }
            };
            var random = new Random();
            var programs = new List<Programs>
            {
                new Programs()
                {
                    Id = 1,
                    Date = new DateTime(2021,05,13,12,43,13),
                    Movie = movies[0]
                },
                new Programs()
                {
                    Id = 2,
                    Date = new DateTime(2021,04,11,22,04,10),
                    Movie = movies[1]
                },
                new Programs()
                {
                    Id = 3,
                    Date = new DateTime(2021,04,08,14,45,58),
                    Movie = movies[1]
                },
                new Programs()
                {
                    Id = 4,
                    Date = new DateTime(2021,04,08,07,30,50),
                    Movie = movies[2]
                },
            };

            var rooms = new List<Room>
            {
                new Room()
                {
                    Id = 1,
                    Line = 10,
                    Column = 5,
                    RoomPrograms = new List<Programs>
                    {
                        programs[0],
                        programs[1]
                    }
                },new Room()
                {
                    Id = 2,
                    Line = 12,
                    Column = 8,
                    RoomPrograms = new List<Programs>
                    {
                        programs[2],
                        programs[3]
                    }
                }
            };
            context.Actors.AddRange(actors);
            context.Movies.AddRange(movies);
            context.Room.AddRange(rooms);
            context.Program.AddRange(programs);
            context.SaveChanges();
        }
    }
}