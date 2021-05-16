using CinemaHW.Desktop.Model;
using CinemaHW.Persistence;
using CinemaHW.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace CinemaHW.Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly CinemaHWApiService _service;

        private ObservableCollection<MovieViewModel> _movies = new ObservableCollection<MovieViewModel>();

        private ObservableCollection<ProgramViewModel> _programs = new ObservableCollection<ProgramViewModel>();

        private ObservableCollection<RoomDto> _rooms;

        private List<RentDto> _rents;
        private ObservableCollection<PlaceViewModel> _places;
        private ObservableCollection<PlaceViewModel> _sellingPlaces = new ObservableCollection<PlaceViewModel>();

        private MovieViewModel _selectedMovie;
        private ActorViewModel _selectedActor;
        private MovieViewModel _editableMovie;
        private ActorViewModel _editableActor;
        private ProgramViewModel _selectedProgram;
        private ProgramViewModel _edittableProgram;
        private PlaceViewModel _selectedPlace;
        private UserViewModel _selectedUser;
        private PlaceViewModel _selectedPlaceInSelling;

        private List<MovieImageDto> _moviesImages;

        public ObservableCollection<PlaceViewModel> SellingPlaces
        {
            get { return _sellingPlaces; }
            set { _sellingPlaces = value; OnPropertyChanged(); }
        }
        public ObservableCollection<PlaceViewModel> Places
        {
            get { return _places; }
            set { _places = value; OnPropertyChanged(); }
        }
        public ObservableCollection<RoomDto> Rooms
        {
            get { return _rooms; }
            set { _rooms = value; OnPropertyChanged(); }
        }
        public ObservableCollection<MovieViewModel> Movies
        {
            get { return _movies; }
            set { _movies = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ActorViewModel> _actors;

        public ObservableCollection<ActorViewModel> Actors
        {
            get { return _actors; }
            set { _actors = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ProgramViewModel> Programs
        {
            get { return _programs; }
            set { _programs = value; OnPropertyChanged(); }
        }
        public UserViewModel SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; OnPropertyChanged(); }
        }
        public PlaceViewModel SelectedPlaceInSelling
        {
            get { return _selectedPlaceInSelling; }
            set { _selectedPlaceInSelling = value; OnPropertyChanged(); }
        }
        public ActorViewModel SelectedActor
        {
            get { return _selectedActor; }
            set { _selectedActor = value; OnPropertyChanged(); }
        }
        public PlaceViewModel SelectedPlace
        {
            get { return _selectedPlace; }
            set { _selectedPlace = value; OnPropertyChanged(); }
        }

        public MovieViewModel SelectedMovie
        {
            get { return _selectedMovie; }
            set { _selectedMovie = value; OnPropertyChanged(); }
        }
        public ProgramViewModel SelectedProgram
        {
            get { return _selectedProgram; }
            set { _selectedProgram = value; OnPropertyChanged(); }
        }

        public MovieViewModel EditTableMovie
        {
            get { return _editableMovie; }
            set { _editableMovie = value; OnPropertyChanged(); }
        }
        public ActorViewModel EditTableActor
        {
            get { return _editableActor; }
            set { _editableActor = value; OnPropertyChanged(); }
        }

        public ProgramViewModel EditTableProgram
        {
            get { return _edittableProgram; }
            set { _edittableProgram = value; OnPropertyChanged(); }
        }

        public DelegateCommand RefreshListsCommand { get; private set; }

        public DelegateCommand SelectCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }
        public DelegateCommand AddMovieCommand { get; set; }
        public DelegateCommand EditMovieCommand { get; set; }
        public DelegateCommand DeleteMovieCommand { get; set; }

        public DelegateCommand AddActorCommand { get; set; }
        public DelegateCommand EditActorCommand { get; set; }
        public DelegateCommand DeleteActorCommand { get; set; }

        public DelegateCommand AddProgramCommand { get; set; }
        public DelegateCommand EditProgramCommand { get; set; }
        public DelegateCommand DeleteProgramCommand { get; set; }

        public DelegateCommand TicketSellCommand { get; set; }


        public DelegateCommand SaveMovieEditCommand { get;  set; }
        public DelegateCommand CancelMovieEditCommand { get; set; }

        public DelegateCommand ChangeImageCommand { get; set; }

        public DelegateCommand SaveActorEditCommand { get; set; }
        public DelegateCommand SaveProgramEditCommand { get; set; }
        public DelegateCommand CancelProgramEditCommand { get; set; }

        public DelegateCommand SelectPlaceCommand { get; set; }
        public DelegateCommand DeletePlaceFromSelling { get; set; }
        public DelegateCommand SellAllPlacesCommand { get; set; }

        public event EventHandler LogoutSucceeded;

        public event EventHandler StartingMovieEdit;
        public event EventHandler StartingProgramEdit;

        public event EventHandler FinishingMovieEdit;
        public event EventHandler FinishingActorEdit;
        public event EventHandler FinishingProgramEdit;

        public event EventHandler StartingImageChange;
        public event EventHandler StartingActorEdit;

        public event EventHandler StartingProgramTicketsEdit;

        public MainViewModel(CinemaHWApiService service)
        {
            _service = service;
            RefreshListsCommand = new DelegateCommand(_ => LoadInit());
            SelectCommand = new DelegateCommand(_ => LoadActorsAsync(SelectedMovie));
            LogoutCommand = new DelegateCommand(_ => LogoutAsync());

            AddMovieCommand = new DelegateCommand(_ => AddMovie());
            EditMovieCommand = new DelegateCommand(_ => !(SelectedMovie is null), _ => SEditMovie());
            DeleteMovieCommand =new DelegateCommand(_ => !(SelectedMovie is null), _ => DeleteMovie(SelectedMovie));

            AddActorCommand = new DelegateCommand(_ => AddActor());
            EditActorCommand = new DelegateCommand(_ => !(SelectedActor is null), _ => SEditActor());
            DeleteActorCommand = new DelegateCommand(_ => !(SelectedActor is null), _ => DeleteActorAsync(SelectedActor));

            AddProgramCommand = new DelegateCommand(_ => AddProgram());
            EditProgramCommand = new DelegateCommand(_ => !(SelectedProgram is null), _ => SEditProgram());
            DeleteProgramCommand = new DelegateCommand(_ => !(SelectedProgram is null), _ => DeleteProgram(SelectedProgram));

            SaveMovieEditCommand = new DelegateCommand(_ => SaveMovieEdit());
            CancelMovieEditCommand = new DelegateCommand(_ => CancelMovieEdit());
            ChangeImageCommand = new DelegateCommand(_ => StartingImageChange?.Invoke(this, EventArgs.Empty));

            SaveActorEditCommand = new DelegateCommand(_ => SaveActorEditAsync());

            SaveProgramEditCommand = new DelegateCommand(_ => SaveProgramEdit());
            CancelProgramEditCommand = new DelegateCommand(_ => CancelProgramEdit());

            SelectPlaceCommand = new DelegateCommand(_ => ShowRentId(SelectedPlace));
            DeletePlaceFromSelling = new DelegateCommand(_ => DeleteSellingSeat());

            SellAllPlacesCommand = new DelegateCommand(_=>(SellingPlaces.Count != 0), _ => SellAllPlacesAsync());;
        }

        private async void SellAllPlacesAsync()
        {
            int i = 0;
            var newRent = new RentViewModel
            {
                ProgramId = SelectedProgram.Id,
                Places = new List<PlaceDto>(SellingPlaces.Select(list => (PlaceDto)list).ToList())        
            };


            var rentDto = (RentDto)newRent;
            try
            {
                await _service.GetRent(rentDto);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");return;
            }
            SellingPlaces.Clear();
            SellTicketAsync();

        }

        private void DeleteSellingSeat()
        {
            SellingPlaces.Remove(SelectedPlaceInSelling);
        }

        private async void ShowRentId(PlaceViewModel selectedPlace)
        {
            if(selectedPlace != null)
            {
                if (selectedPlace.Status == 0)
                {
                    String userId = _rents.FirstOrDefault(r => r.Places.Contains((PlaceDto)selectedPlace)).UserId;
                    SelectedUser = (UserViewModel)(await _service.GetUser(userId));
                }
                else
                {
                    SelectedUser = new UserViewModel
                    {
                        FullName = "",
                        PhoneNumber = ""
                    };
                }
            }
            
        }


        #region Login/out
        private async void LogoutAsync()
        {
            try
            {
                await _service.LogoutAsync();
                LogoutSucceeded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
        #endregion

        #region LoadStartingWindow

        private async void LoadInit()
        {
            List<MovieViewModel> tmpMovies = new List<MovieViewModel>();
            List<ProgramViewModel> tmpPrograms = new List<ProgramViewModel>();
            try
            {
                tmpMovies = new List<MovieViewModel>((await _service.LoadMoviesAsync())
                    .Select(movie => (MovieViewModel)movie));
                _moviesImages = new List<MovieImageDto>((await _service.LoadImages())
                    .Select(image => (MovieImageDto)image));
                tmpPrograms = new List<ProgramViewModel>((await _service.LoadProgramsAsync())
                    .Select(ac => (ProgramViewModel)ac));
                _rooms = new ObservableCollection<RoomDto>((await _service.LoadRooms())
                    .Select(room => (RoomDto)room));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            foreach(ProgramViewModel program in tmpPrograms)
            {
                var movie = tmpMovies.Find(m => m.Id == program.MovieId);
                if (movie != null)
                {
                    program.TicketSellCommand = new DelegateCommand(_ => SellTicketAsync());
                    program.MovieTitle = movie.Title;
                    var endTime = program.Date.AddMinutes(movie.Length + 15);
                    if(endTime.Day != program.Date.Day)
                        program.Interval = program.Date.ToString("yyyy/MM/dd hh:mm:ss tt  ") + " - " + endTime.ToString("MM/dd hh:mm:ss tt");
                    else
                        program.Interval = program.Date.ToString("yyyy/MM/dd hh:mm:ss tt") + " - " + endTime.ToString("hh:mm:ss tt");
                }
                else
                {
                    program.MovieTitle = "Ismeretlen";
                }
            }
            Programs = new ObservableCollection<ProgramViewModel>(tmpPrograms);
            Movies = new ObservableCollection<MovieViewModel>(tmpMovies);
        }


        private async void LoadActorsAsync(MovieViewModel movieDto)
        {
            if (movieDto is null)
            {
                return;
            }
            try
            {
                Actors = new ObservableCollection<ActorViewModel>((await _service.LoadActorsAsync(movieDto.Id))
                    .Select(ac => (ActorViewModel)ac));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
        #endregion

        #region Movie

        private async void DeleteMovie(MovieViewModel movie)
        {
            try
            {
                await _service.DeleteMovieAsync(movie.Id);
                Movies.Remove(SelectedMovie);
                SelectedMovie = null;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private void SEditMovie()
        {
            EditTableMovie = SelectedMovie.ShallowClone();
            MovieImageDto imageDto = _moviesImages.Find(i => i.MovieId == SelectedMovie.Id);
            if(imageDto != null)
            {
                EditTableMovie.Image = imageDto.Image;
                EditTableMovie.ImageId = imageDto.Id;
            }
                
            StartingMovieEdit?.Invoke(this, EventArgs.Empty);
        }

        private async void AddMovie()
        {
            var newMovie = new MovieViewModel
            {
                Title = "Új film"
            };

            var movieDto = (MovieDto)newMovie;
            try
            {
                await _service.CreateMovieAsync(movieDto);
                newMovie.Id = movieDto.Id;
                Movies.Add(newMovie);
                SelectedMovie = newMovie;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private void CancelMovieEdit()
        {
            EditTableMovie = null;
            FinishingMovieEdit?.Invoke(this, EventArgs.Empty);
        }

        private async void SaveMovieEdit()
        {
            try
            {
                if(EditTableMovie.Title.Length == 0 || EditTableMovie.Length <= 0 || EditTableMovie.Image == null || EditTableMovie.UploadTime == null || EditTableMovie.Director.Length == 0 || EditTableMovie.Description.Length == 0)
                {
                    OnMessageApplication("Please give every data correctly!");
                    return;
                }
                SelectedMovie.CopyFrom(EditTableMovie);
                await _service.UpdateMoviesAsync((MovieDto)SelectedMovie);
                MovieImageDto mvd = new MovieImageDto
                {
                    Id = EditTableMovie.ImageId,
                    MovieId = SelectedMovie.Id,
                    Image = EditTableMovie.Image
                };
                await _service.UpdateImageAsync(mvd);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            FinishingMovieEdit?.Invoke(this, EventArgs.Empty);
            LoadInit();
        }

        #endregion

        #region Actors
        private async void DeleteActorAsync(ActorViewModel actor)
        {
            try
            {
                await _service.DeleteActorAsync(actor.Id);
                Actors.Remove(actor);
                SelectedActor = null;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private void SEditActor()
        {
            EditTableActor = SelectedActor.ShallowClone();
            StartingActorEdit?.Invoke(this, EventArgs.Empty);
        }

        private async void AddActor()
        {
            var newActor = new ActorViewModel
            {
                Name = "Új színész",
                MovieId = SelectedMovie.Id
            };

            var actorDto = (ActorDto)newActor;

            try
            {
                await _service.CreateActorAsync(actorDto);
                newActor.Id = actorDto.Id;
                Actors.Add(newActor);
                SelectedActor = newActor;
            }

            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void SaveActorEditAsync()
        {
            try
            {
                if(EditTableActor.Name.Length == 0)
                {
                    OnMessageApplication("Please give every data correctly!");
                    return;
                }
                SelectedActor.CopyFrom(EditTableActor);
                await _service.UpdateActorAsync((ActorDto)SelectedActor);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            LoadActorsAsync(SelectedMovie);
            FinishingActorEdit?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Program
        private async void DeleteProgram(ProgramViewModel program)
        {
            try
            {
                await _service.DeleteProgramAsync(program.Id);
                Movies.Remove(SelectedMovie);
                SelectedMovie = null;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            LoadInit();
        }

        private void SEditProgram()
        {
            EditTableProgram = SelectedProgram.ShallowClone();
            StartingProgramEdit?.Invoke(this, EventArgs.Empty);
        }


        private async void SellTicketAsync()
        {
            try
            {
                _rents = new List<RentDto>((await _service.LoadRents(SelectedProgram.Id))
                    .Select(ac => (RentDto)ac));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            RoomDto room = _rooms.FirstOrDefault(r => r.Id == SelectedProgram.RoomId);

            List<PlaceViewModel> tmpPlaces = new List<PlaceViewModel>();
            for(int i = 0; i < room.Line; i++)
            {
                for(int j = 0; j< room.Column; j++)
                {
                    tmpPlaces.Add(new PlaceViewModel
                    {
                        Column = j,
                        Line = i,
                        Status = 1,
                        StatusName = "Szabad",
                        AddSellingListCommand = new DelegateCommand(_ => AddSellingList())
                    }) ;
                }
            }

            try
            {
                List<PlaceViewModel> tmp = new List<PlaceViewModel>();
                foreach(RentDto rent in _rents)
                {
                    tmp = new List<PlaceViewModel>((await _service.LoadPlaces(rent.Id))
                    .Select(ac => (PlaceViewModel)ac));
                    foreach(PlaceViewModel place in tmp)
                    {
                        int index = place.Column * place.Line;
                        var tmpdata = tmpPlaces.Find(p => p.Column == place.Column && p.Line == place.Line);
                        tmpdata.Status = place.Status;
                        if (tmpdata.Status == 0)
                        {
                            tmpdata.StatusName = "Foglalt";
                        }
                        else
                        {
                            tmpdata.StatusName = "Eladott";
                        }

                    }
                }
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }

            Places = new ObservableCollection<PlaceViewModel>(tmpPlaces);

            EditTableProgram = SelectedProgram.ShallowClone();
            StartingProgramTicketsEdit?.Invoke(this, EventArgs.Empty);
        }

        private void AddSellingList()
        {
            if(SelectedPlace.Status == 0 || SelectedPlace.Status == 1)
            {
                ObservableCollection<PlaceViewModel> tmp = new ObservableCollection<PlaceViewModel>();
                if (SellingPlaces.Count != 0)
                {
                    tmp = SellingPlaces;
                }

                if (!tmp.Contains(SelectedPlace))
                {
                    if (!SelectedPlace.StatusName.Equals("Eladott"))
                    {
                        tmp.Add(SelectedPlace);
                        SellingPlaces = tmp;
                    }
                    else
                    {
                        OnMessageApplication($"Cant sell a sold ticket");
                    }
                }
                    
            }
        }

        private async void AddProgram()
        {
            var newProgram = new ProgramViewModel
            {
                MovieTitle = "Új program",
                MovieId = 1,
                RoomId = 1,
            };

            var programDto = (ProgramDto)newProgram;
            try
            {
                await _service.CreateProgramAsync(programDto);
                newProgram.Id = programDto.Id;
                Programs.Add(newProgram);
                SelectedProgram = newProgram;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private void CancelProgramEdit()
        {
            EditTableMovie = null;
            FinishingProgramEdit?.Invoke(this, EventArgs.Empty);
        }

        private async void SaveProgramEdit()
        {
            try
            {
                if(EditTableProgram.MovieTitle == null || EditTableProgram.RoomId == null || EditTableProgram.Date == null)
                {
                    OnMessageApplication("Please give every data correctly!");
                    return;
                }
                SelectedProgram.CopyFrom(EditTableProgram);
                var movie = Movies.FirstOrDefault(r => r.Id == SelectedProgram.MovieId);
                await _service.UpdateProgramAsync((ProgramDto)SelectedProgram);
                
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            LoadInit();
            FinishingProgramEdit?.Invoke(this, EventArgs.Empty);
            
        }
        #endregion
    }
}
