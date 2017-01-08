using System;
using System.Diagnostics;
using System.Windows.Threading;
using ClientApp.Helpers;
using ClientApp.ViewModels.TabViewModels;
using ClientApp.ViewModels.TabViewModels.Base;

namespace ClientApp.ViewModels
{
    public class MainViewModel : ModelBase
    {
        private readonly DispatcherTimer _dt;
        private readonly Stopwatch _stopWatch;
        private readonly int _tokenTimeToLive;
        private TabViewModelBase _accountsTabViewModel;
        private TabViewModelBase _historryTabViewModel;
        private TabViewModelBase _payInTabViewModel;
        private TabViewModelBase _payOutTabViewModel;
        private string _remainingTimeSession;
        private TabViewModelBase _transferTabViewModel;

        public MainViewModel()
        {
            _tokenTimeToLive = ServiceBankExecutor.GetTokenTtl();
            _dt = new DispatcherTimer();
            _stopWatch = new Stopwatch();

            _dt.Tick += dt_Tick;
            _dt.Interval = new TimeSpan(0, 0, 0, 0, 10);

            _stopWatch.Start();
            _dt.Start();
        }


        public string RemainingTimeSession
        {
            get
            {
                if (_stopWatch.IsRunning)
                    return $"Your session will expire in {_remainingTimeSession}";
                return "Your session has expired";
            }
            set
            {
                _remainingTimeSession = value;
                NotifyPropertyChanged("RemainingTimeSession");
            }
        }

        public TabViewModelBase AccountsTabViewModel
        {
            get
            {
                return _accountsTabViewModel ??
                       (_accountsTabViewModel = new AccountsTabViewModel {CloseAction = CloseAction});
            }
        }

        public TabViewModelBase TransferTabViewModel
        {
            get
            {
                return _transferTabViewModel ??
                       (_transferTabViewModel = new TransferTabViewModel {CloseAction = CloseAction});
            }
        }

        public TabViewModelBase PayInTabViewModel
        {
            get
            {
                return _payInTabViewModel ?? (_payInTabViewModel = new PayInTabViewModel {CloseAction = CloseAction});
            }
        }

        public TabViewModelBase PayOutTabViewModel
        {
            get
            {
                return _payOutTabViewModel ?? (_payOutTabViewModel = new PayOutTabViewModel {CloseAction = CloseAction});
            }
        }

        public TabViewModelBase HistoryTabViewModel
        {
            get
            {
                return _historryTabViewModel ??
                       (_historryTabViewModel = new HistoryTabViewModel {CloseAction = CloseAction});
            }
        }


        private void dt_Tick(object sender, EventArgs e)
        {
            if (_stopWatch.IsRunning)
            {
                var ts = TimeSpan.FromMilliseconds(_tokenTimeToLive * 1000 - _stopWatch.Elapsed.TotalMilliseconds);
                if (ts.TotalMilliseconds <= 0)
                {
                    _stopWatch.Stop();
                    ts = TimeSpan.FromSeconds(0);
                }

                RemainingTimeSession = $"{ts.Minutes:00}:{ts.Seconds:00}:{ts.Milliseconds / 10:00}";
            }
        }
    }
}