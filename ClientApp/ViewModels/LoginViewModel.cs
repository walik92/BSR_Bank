using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ClientApp.Helpers;
using ClientApp.Views;

namespace ClientApp.ViewModels
{
    public class LoginViewModel : ModelBase
    {
        private ICommand _loginCommand;

        public string IdClient { get; set; }


        public ICommand LoginCommand
        {
            get { return _loginCommand ?? (_loginCommand = new CommandHandlerAsync(LoginAction)); }
        }

        private async void LoginAction(object parameter)
        {
            VisibilityProgress = Visibility.Visible;
            VisibilityForm = Visibility.Collapsed;

            var password = ((PasswordBox) parameter).Password;

            try
            {
                int id;
                if (!int.TryParse(IdClient, out id))
                    throw new Exception("The Id Client is incorrect.");

                await ServiceBankExecutor.Login(id, password);

                await Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(ShowMainWindow));

                await Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    CloseAction);
            }
            catch (FaultException ex)
            {
                Error = ex.Message;
            }
            catch (Exception ex)
            {
                //todo log exception
                Error = "Service bank is not available, please try later.";
            }
            VisibilityForm = Visibility.Visible;
            VisibilityProgress = Visibility.Collapsed;
        }

        private void ShowMainWindow()
        {
            var form = new Main();
            form.Show();
        }
    }
}