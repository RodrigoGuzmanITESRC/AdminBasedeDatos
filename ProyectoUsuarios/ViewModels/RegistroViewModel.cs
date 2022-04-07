using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using ProyectoUsuarios.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ProyectoUsuarios.Views;
using ProyectoUsuarios.Repositories;
using GalaSoft.MvvmLight.Command;

namespace ProyectoUsuarios.ViewModels
{
    public class RegistroViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        private RegistrarUsuarioView r;
        private EditarUsuarioView editarDialog;
        private AdministrarView administrarView;
        

        public ICommand VerRegistrarCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand VerEditarCommnad { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand CerrarCommand { get; set; }

        public ICommand CerrardosCommand { get; set; }


       


        private string error;

        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Error));
            }
        }

        private Usuario usuario;
        public Usuario Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Usuario"));
            }
        }
        private Usuario usuarioLogin;

        public Usuario UsuarioLogin
        {
            get { return usuarioLogin; }
            set
            {
                usuarioLogin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(usuarioLogin)));
            }
        }


        public void VerRegistro()
        {
            Error = "";
            r = new RegistrarUsuarioView
            {
                DataContext = this
            };
            Usuario = new Usuario();
            r.ShowDialog();
        }

        UsuariosRepository repos = new UsuariosRepository();

        private void Cerrar()
        {
            editarDialog.Close();

        }
        private void Cerrardos()
        {

            r.Close();
        }

        private void Registrar()
        {
            Error ="";
            try
            {
                if (repos.Validate(Usuario))
                {
                    repos.Insert(Usuario);
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

        private void Editar()
        {
            Error = "";
            try
            {
                if (repos.Validate(Usuario))
                {
                    repos.Update(Usuario);
                    Usuario = new Usuario();
                    editarDialog.Close();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

        public void VerEditar()
        {
            editarDialog = new EditarUsuarioView
            {
                DataContext = this
            };
            editarDialog.ShowDialog();
        }

        LoginRepository repos2 = new LoginRepository();
        public void Login()
        {
            if (repos2.ValidateLogin(UsuarioLogin))
            {
                repos2.Login(UsuarioLogin);
                Usuario = repos.GetById(UsuarioLogin);
                administrarView = new AdministrarView
                {
                    DataContext = this
                };
                administrarView.ShowDialog();
            }
        }

        

        public RegistroViewModel()
        {
            VerRegistrarCommand = new RelayCommand(VerRegistro);
            RegistrarCommand = new RelayCommand(Registrar);

            usuarioLogin = new Usuario();
            LoginCommand = new RelayCommand(Login);

            

            VerEditarCommnad = new RelayCommand(VerEditar);
            EditarCommand = new RelayCommand(Editar);

            CerrarCommand = new RelayCommand(Cerrar);
            CerrardosCommand = new RelayCommand(Cerrardos);
        }
    }
}
