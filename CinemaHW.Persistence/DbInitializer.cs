using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Persistence
{
    public class DbInitializer
    {
        private static CinemaHWDbContext context;
        private static UserManager<Users> userManager;
        private static RoleManager<IdentityRole<String>> roleManager;

        private static void SeedUsers()
        {
            var adminUser = new Users
            {
                UserName = "admin",
                FullName = "Adminisztrátor",
                Email = "admin@example.com",
                PhoneNumber = "+36123456789"
            };
            //adminUser.Id = Guid.NewGuid().ToString();
            var adminPassword = "Almafa123";
            var adminRole = new IdentityRole<String>("administrator");
            adminRole.Id = Guid.NewGuid().ToString();

            var result1 = userManager.CreateAsync(adminUser, adminPassword).Result;
            var result2 = roleManager.CreateAsync(adminRole).Result;
            var result3 = userManager.AddToRoleAsync(adminUser, adminRole.Name).Result;
        }

        public static void Initialize(IServiceProvider serviceProvider, string imageDirectory)
        {
            
            context = serviceProvider.GetRequiredService<CinemaHWDbContext>();
            userManager = serviceProvider.GetRequiredService<UserManager<Users>>();
            roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<String>>>();


            context.Database.EnsureCreated();
            if (context.Movies.Any())
            {
                return;
            }

            SeedUsers();

            var actors = new List<Actors> {
                new Actors()
                {
                    Name = "Mark Hamil"
                }, new Actors()
                {
                    Name = "Harrison Ford"
                }, new Actors()
                {
                    Name = "Carrie Fisher"
                }, new Actors()
                {
                    Name = "Billy Dee Williams"
                }, new Actors()
                {
                    Name = "Anthony Daniels"
                }, new Actors()
                {
                    Name = "François Cluzet"
                }, new Actors()
                {
                    Name = "Omar Sy"
                }, new Actors()
                {
                    Name = "Tom Hanks"
                }, new Actors()
                {
                    Name = "Robin Wright"
                }, new Actors()
                {
                    Name = "Gary Sinise"
                }, new Actors()
                {
                    Name = "Jay Baruchel"
                }, new Actors()
                {
                    Name = "Gerald Butler"
                }, new Actors()
                {
                    Name = "Jonah Hill"
                }, new Actors()
                {
                    Name = "Keanu Reeves"
                }, new Actors()
                {
                    Name = "Laurence Fishburne"
                }, new Actors()
                {
                    Name = "Carrie-Anne Moss"
                }, new Actors()
                {
                    Name = "Hugo Weaving"
                }, new Actors()
                {
                    Name = "Matthew Broderick"
                }, new Actors()
                {
                    Name = "James Earl Jones"
                }, new Actors()
                {
                    Name = "Jeremy Irons"
                }, new Actors()
                {
                    Name = "Moira Kelly"
                }, new Actors()
                {
                    Name = "Tim Robbins"
                }, new Actors()
                {
                    Name = "Morgan Freeman"
                }, new Actors()
                {
                    Name = "Bob Gunton"
                }, new Actors()
                {
                    Name = "Christian Bale"
                }, new Actors()
                {
                    Name = "Heath Ledger"
                }, new Actors()
                {
                    Name = "Michael Caine"
                }, new Actors()
                {
                    Name = "Michael J. Fox"
                }, new Actors()
                {
                    Name = "Christopher Lloyd"
                }
            };
            var movies = new List<Movie>
            {
                new Movie()
                {
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
                    Title="Forest Gump",
                    Length = 142,
                    Director = "Robert Zemeckis",
                    UploadTime = DateTime.Now,
                    Actors = new List<Actors>
                    {
                        actors[7],actors[8],actors[9]
                    },
                    Description = "A georgiai Savannah városka árnyas buszmegállójában különös mesemondó üldögél. Forrest Gump mindent látott és mindent átélt, de nem mindent értett. Nem éppen a legfényesebb elme. De hát az anyja is mindig azt mondta: 'Csak az a hülye, aki hülyeséget csinál.' Forrest Gump pedig semmi egyebet nem csinált, mint jelen volt a XX. század minden fontos eseményén a focipályától a harctérig, az elnökök klubjától a médiavitákig, míg végül meg nem pihent egyetlen igaz szerelme karjában. Forrest Gump IQ-ja nem szárnyal az egekig, de rendkívül becsületes és jólelkű fiú. Különös véletlenek azonban hozzásegítik, hogy az 1950-es évektől 1970-ig Amerika minden jelentős eseményén részt vegyen, és minden jelentős személyiségével találkozzon, köztük: Elvis Presley-vel, JFK-vel, Lyndon Johnsonnal, Richard Nixonnal. Forrest elvégzi a főiskolát, harcol Vietnamban, élsportoló lesz, az egyetlen probléma csak az, hogy túl buta ahhoz, hogy megértse ezen fontos események jelentőségét."
                },
                new Movie()
                {
                    Title="Így neveld a sárkányodat",
                    Length = 85,
                    Director = "Chris Sanders",
                    UploadTime = DateTime.Now,
                    Actors = new List<Actors>
                    {
                        actors[10],actors[11],actors[12]
                    },
                    Description = "Hibbant-szigeten vikingek élnek. Életüket gyakorta sárkányok veszélyeztetik, amik felperzselik otthonaikat és ellopják állataikat, ezért ádázan harcolnak ellenük. A kétbalkezes főhős, Hablaty (az eredetiben: Hiccup, vagyis „Csuklás” a neve) a falu vezérének, Pléhpofának a fia, a kovácsműhelyben segédkezik. Harcolni nem engedik, mivel nincs kiképezve a sárkányok elleni harcra, ráadásul folytonos csetlés-botlásaival mindig csak bajt okoz. Ő azonban feltalál egy gépezetet, amivel a legrettegettebb, leggyorsabb sárkányt, az „Éjfúriát” titokban sikerül lelőnie. Ez a vikingek történetében még senkinek sem sikerült. Ám a vikingek nem hisznek neki, mert ki hinné, hogy egy Hablaty nevű satnya, mindig csak bajt okozó viking lőtte le azt a sárkányt, amit 300 év alatt senkinek sem sikerült."
                },
                new Movie()
                {
                    Title="Mátrix",
                    Length = 136,
                    Director = "The Wachowskis",
                    UploadTime = DateTime.Now,
                    Actors = new List<Actors>
                    {
                        actors[13],actors[14],actors[15],actors[16]
                    },
                    Description = "A film egy olyan disztopikus jövőt fest meg, melyben az érzékelt világ valójában a Mátrix – egy szimulált valóság, amelyet a mesterséges intelligenciával rendelkező gépek alkottak meg és kibernetikus csatlakozókkal kapcsolják rá az emberek agyát – amelyben az embereket a gépek tenyésztik, azzal a céllal, hogy saját energiájuk forrásaként hasznosítsák őket. A film számos utalást tartalmaz a cyberpunk és hacker szubkultúrákra; filozófiai és vallási elméleteket is felvonultat, úgymint buddhista, Védánta, messianizmus, dualizmus, szókratészi és platóni ideológiák. Mindemellett fejet hajt az Alice Csodaországban, a hongkongi akciófilmek (a koreográfiát Yuen Woo-ping készítette) és a japán animáció előtt."
                },
                new Movie()
                {
                    Title="Oroszlánkirály",
                    Length = 88,
                    Director = "Roger Allers",
                    UploadTime = DateTime.Now,
                    Actors = new List<Actors>
                    {
                        actors[17],actors[18],actors[19],actors[20]
                    },
                    Description = "Főszereplője egy fiatal oroszlán, Szimba, aki az afrikai szavannákon megtanulja, hol a helye az „élet körforgásában”, mialatt számos akadállyal megküzd, hogy ő lehessen a törvényes király. Egyes vélekedések szerint Az oroszlánkirály története Tezuka Oszamu az 1960-as években készült Kimba, a fehér oroszlán című animációs sorozatából merít, azonban a készítők tagadják ezen állításokat. Mindazonáltal, a film bizonyos elemeiben párhuzamot mutat Shakespeare Hamlet című drámájával, a bibliai József és Mózes történetével, illetve az 1942-es Bambi című Disney-alkotással. A 2003-as DVD-kiadás tartalmaz egy új jelenetet, amelyben Mufasa magyar hangja Vass Gábor, a kölyök Simbáé pedig Baradlay Viktor."
                },
                new Movie()
                {
                    Title="Remény rabjai",
                    Length = 144,
                    Director = "Frank Darabont",
                    UploadTime = DateTime.Now,
                    Actors = new List<Actors>
                    {
                        actors[21],actors[22],actors[23]
                    },
                    Description = "1946-ban egy Andy Dufresne nevű bankárt - noha makacsul hangoztatja ártatlanságát - kettős gyilkosság elkövetése miatt életfogytiglani börtönbüntetésre ítélnek. Dufresne egy Maine állambeli büntetés-végrehajtó intézetbe kerül és hamar meg kell ismerkedjen a börtönélet kegyetlen mindennapjaival, a szadista börtönszemélyzettel, a szinte elállatiasodott rabokkal. Azonban Andy nem törik meg. A bankéletben szerzett tapasztalatai segítségével elnyeri az őrök kegyét és azzal, hogy elvállalja egyik rabtársa illegális akcióiból származó bevételeinek könyvelését, kivívja 'társai' elismerését is. Cserébe viszont lehetőséget kap a börtön könyvtár fejlesztésére, ezzel némi emberi méltóságot csempészve a keserű körülmények között élő rabok mindennapjaiba."
                },
                new Movie()
                {
                    Title="Sötét lovag",
                    Length = 152,
                    Director = "Christopher Nolan",
                    UploadTime = DateTime.Now,
                    Actors = new List<Actors>
                    {
                        actors[24],actors[25],actors[26]
                    },
                    Description = "A sötét lovag (The Dark Knight) egy 2008-as amerikai–brit szuperhős-film, amelynek társírója és rendezője Christopher Nolan. A DC Comics képregénykiadó Batman szereplőjén alapuló film a Batman: Kezdődik! (2005) folytatása. A címszerepet ismét Christian Bale alakítja. A filmben Batman megoldandó nehézségei közé tartozik küzdelme a törvényes rend fő ellenségévé előlépett Jokerrel (Heath Ledger), amiben szövetségesei James Gordon rendőrfőkapitány (Gary Oldman) és az újonnan megválasztott kerületi ügyész, Harvey Dent (Aaron Eckhart)."
                },
                new Movie()
                {
                    Title="Vissza a jövőbe",
                    Length = 116,
                    Director = "Robert Zemeckis",
                    UploadTime = DateTime.Now,
                    Actors = new List<Actors>
                    {
                        actors[27],actors[28]
                    },
                    Description = "A film alapötlete Bob Gale-től jött, aki elgondolkozott azon, milyen lett volna, ha a saját apjával egyszerre járt volna iskolába. A forgatókönyv azonban egyik stúdiót sem érdekelte, egészen Zemeckis 1984-es filmje, A smaragd románca sikeréig. Ekkor csatlakozott producerként a stábhoz Spielberg is. A főszerepre már eredetileg is Michael J. Foxot szánták, azonban mivel őt szerződés kötötte a Családi kötelékek című tv-sorozathoz, így kezdetben nem vállalhatta a forgatást. Ezért aztán az első néhány hétben Eric Stoltzcal forgattak, de néhány hét elteltével rájöttek, hogy nem ő a tökéletes választás, és visszatértek Michael J. Foxhoz. Ehhez olyan rugalmas forgatási rendet kellett kitalálniuk, amelybe mindkét szerep belefért."
                }
            };
            var random = new Random();
            var programs = new List<Programs>
            {
                new Programs()
                {
                    Date = new DateTime(2021,05,13,12,43,13),
                    Movie = movies[random.Next(0,8)]
                },
                new Programs()
                {
                    Date = new DateTime(2021,04,11,22,04,10),
                    Movie = movies[random.Next(0,8)]
                },
                new Programs()
                {
                    Date = new DateTime(2021,04,08,14,45,58),
                    Movie = movies[random.Next(0,8)]
                },
                new Programs()
                {
                    Date = new DateTime(2021,04,08,07,30,50),
                    Movie = movies[random.Next(0,8)]
                },
                //
                new Programs()
                {
                    Date = new DateTime(2021,04,08,16,17,54),
                    Movie = movies[random.Next(0,8)]
                },
                new Programs()
                {
                    Date = new DateTime(2021,04,09,07,24,05),
                    Movie = movies[random.Next(0,8)]
                },
                new Programs()
                {
                    Date = new DateTime(2021,04,11,16,36,26),
                    Movie = movies[random.Next(0,8)]
                },
                new Programs()
                {
                    Date = new DateTime(2021,04,09,11,30,38),
                    Movie = movies[random.Next(0,8)]
                },
                //
                new Programs()
                {
                    Date = new DateTime(2021,04,11,20,48,42),
                    Movie = movies[random.Next(0,8)]
                },
                new Programs()
                {
                    Date = new DateTime(2021,04,10,19,30,34),
                    Movie = movies[random.Next(0,8)]
                },
                new Programs()
                {
                    Date = new DateTime(2021,04,10,23,12,47),
                    Movie = movies[random.Next(0,8)]
                },
                new Programs()
                {
                    Date = new DateTime(2021,04,11,14,58,10),
                    Movie = movies[random.Next(0,8)]
                },
                //
                new Programs()
                {
                    Date = new DateTime(2021,04,10,22,12,59),
                    Movie = movies[random.Next(0,8)]
                },
                new Programs()
                {
                    Date = new DateTime(2021,04,08,01,59,53),
                    Movie = movies[random.Next(0,8)]
                },
                new Programs()
                {
                    Date = new DateTime(2021,04,08,03,44,54),
                    Movie = movies[random.Next(0,8)]
                }
            };

            var rooms = new List<Room>
            {
                new Room()
                {
                    Line = 10,
                    Column = 5,
                    RoomPrograms = new List<Programs>
                    {
                        programs[0],
                        programs[1],
                        programs[2],
                        programs[3],
                    }
                },new Room()
                {
                    Line = 12,
                    Column = 8,
                    RoomPrograms = new List<Programs>
                    {
                        programs[4],
                        programs[5],
                        programs[6],
                        programs[7],
                    }
                },new Room()
                {
                    Line = 18,
                    Column = 9,
                    RoomPrograms = new List<Programs>
                    {
                        programs[8],
                        programs[9],
                        programs[10],
                        programs[11],
                        programs[12],
                        programs[13],
                        programs[14]
                    }
                }
            };
            context.Actors.AddRange(actors);
            context.Movies.AddRange(movies);
            context.Room.AddRange(rooms);

            if (Directory.Exists(imageDirectory))
            {
                var images = new List<MoviesImage>();
                var image = Path.Combine(imageDirectory, "birodalomvisszavag.jpg");
                if (File.Exists(image))
                {
                    images.Add(new MoviesImage
                    {
                        Movie = movies[0],
                        Image = File.ReadAllBytes(image)
                    });
                }

                image = Path.Combine(imageDirectory, "eletrevalok.jpg");
                if (File.Exists(image))
                {
                    images.Add(new MoviesImage
                    {
                        Movie = movies[1],
                        Image = File.ReadAllBytes(image)
                    });
                }

                image = Path.Combine(imageDirectory, "forestgump.jpg");
                if (File.Exists(image))
                {
                    images.Add(new MoviesImage
                    {
                        Movie = movies[2],
                        Image = File.ReadAllBytes(image)
                    });
                }

                image = Path.Combine(imageDirectory, "igyneveldasarkanyodat.jpg");
                if (File.Exists(image))
                {
                    images.Add(new MoviesImage
                    {
                        Movie = movies[3],
                        Image = File.ReadAllBytes(image)
                    });
                }
                image = Path.Combine(imageDirectory, "matrix.jpg");
                if (File.Exists(image))
                {
                    images.Add(new MoviesImage
                    {
                        Movie = movies[4],
                        Image = File.ReadAllBytes(image)
                    });
                }
                image = Path.Combine(imageDirectory, "oroszlankiraly.jpg");
                if (File.Exists(image))
                {
                    images.Add(new MoviesImage
                    {
                        Movie = movies[5],
                        Image = File.ReadAllBytes(image)
                    });
                }
                image = Path.Combine(imageDirectory, "rremenyrabjai.jpg");
                if (File.Exists(image))
                {
                    images.Add(new MoviesImage
                    {
                        Movie = movies[6],
                        Image = File.ReadAllBytes(image)
                    });
                }
                image = Path.Combine(imageDirectory, "sotetlovag.jpg");
                if (File.Exists(image))
                {
                    images.Add(new MoviesImage
                    {
                        Movie = movies[7],
                        Image = File.ReadAllBytes(image)
                    });
                }
                image = Path.Combine(imageDirectory, "visszaajovobe.jpg");
                if (File.Exists(image))
                {
                    images.Add(new MoviesImage
                    {
                        Movie = movies[8],
                        Image = File.ReadAllBytes(image)
                    });
                }
                context.MoviesImages.AddRange(images);
            }
            context.SaveChanges();
        }
        
    }
}
