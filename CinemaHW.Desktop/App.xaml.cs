using CinemaHW.Desktop.Model;
using CinemaHW.Desktop.View;
using CinemaHW.Desktop.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaHW.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private CinemaHWApiService _service;
        private MainViewModel _mainViewModel;
        private MainWindow _view;
        private LoginViewModel _loginViewModel;
        private LoginWindow _loginView;
        private MovieEditorWindow _movieEditorWindow;
        private ActorEditorWindow _actorEditorWindow;
        private ProgramEditorWindow _programEditorWindow;
        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new CinemaHWApiService(ConfigurationManager.AppSettings["baseAddress"]);

            _mainViewModel = new MainViewModel(_service);
            _mainViewModel.LogoutSucceeded += _mainViewModel_LogoutSucceeded;
            _mainViewModel.StartingMovieEdit += _mainViewModel_StartingMovieEdit;
            _mainViewModel.FinishingMovieEdit += _mainViewModel_FnishingMovieEdit;
            _mainViewModel.FinishingActorEdit += _mainViewModel_FnishingActorEdit;
            _mainViewModel.FinishingProgramEdit += _mainViewModel_FnishingProgramEdit;
            _mainViewModel.StartingImageChange += _mainViewModel_StartingImageChangeAsync;
            _mainViewModel.StartingActorEdit += _mainViewModel_StartingActorEdit;
            _mainViewModel.StartingProgramEdit += _mainViewModel_StartingProgramEdit;
            _mainViewModel.MessageApplication += MessageApplication;

            _loginViewModel = new LoginViewModel(_service);
            _loginViewModel.LoginSucceeded += _loginViewModel_LoginSucceeded;
            _loginViewModel.LoginFailed += _loginViewModel_LoginFailed;
            _loginViewModel.MessageApplication += MessageApplication;

            _loginView = new LoginWindow
            {
                DataContext = _loginViewModel
            };

            _view = new MainWindow
            {
                DataContext = _mainViewModel
            };
            _loginView.Show();
        }

        private void _mainViewModel_FnishingProgramEdit(object sender, EventArgs e)
        {
            if (_programEditorWindow.IsActive)
            {
                _programEditorWindow.Close();
            }
        }

        private void _mainViewModel_StartingProgramEdit(object sender, EventArgs e)
        {
            _programEditorWindow = new ProgramEditorWindow
            {
                DataContext = _mainViewModel
            };
            _programEditorWindow.ShowDialog();
        }

        private void _mainViewModel_FnishingActorEdit(object sender, EventArgs e)
        {
            if (_actorEditorWindow.IsActive)
            {
                _actorEditorWindow.Close();
            }
        }

        private void _mainViewModel_StartingActorEdit(object sender, EventArgs e)
        {
            _actorEditorWindow = new ActorEditorWindow
            {
                DataContext = _mainViewModel
            };
            _actorEditorWindow.ShowDialog();
        }

        private async void _mainViewModel_StartingImageChangeAsync(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Images|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png;",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dialog.ShowDialog(_movieEditorWindow).GetValueOrDefault(false))
            {
                _mainViewModel.EditTableMovie.Image = await File.ReadAllBytesAsync(dialog.FileName);
                int a = 0;
            }
        }

        private void _mainViewModel_FnishingMovieEdit(object sender, EventArgs e)
        {
            if (_movieEditorWindow.IsActive)
            {
                _movieEditorWindow.Close();
            }
        }

        private void _mainViewModel_StartingMovieEdit(object sender, EventArgs e)
        {
            _movieEditorWindow = new MovieEditorWindow
            {
                DataContext = _mainViewModel
            };
            _movieEditorWindow.ShowDialog();
        }

        private void _mainViewModel_LogoutSucceeded(object sender, EventArgs e)
        {
            _view.Hide();
            _loginView.Show();
            
        }

        private void _loginViewModel_LoginSucceeded(object sender, EventArgs e)
        {
            _loginView.Hide();
            _view.Show();
        }

        private void _loginViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Login failed", "CinemaHW", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        

        private void MessageApplication(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "CinemaHW", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
