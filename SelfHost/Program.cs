using System;
using System.Configuration;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using Autofac;
using Autofac.Integration.Wcf;
using log4net;
using SelfHost.Services;
using SelfHost.Services.Interfaces;

namespace SelfHost
{
    /// <summary>
    ///     Hostowanie usług
    /// </summary>
    internal class Program
    {
        private static ServiceHost _serviceBankHost;
        private static ServiceHost _serviceTransferHost;
        private static ServiceHost _serviceAdminHost;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string _baseAddress;
        private static IContainer _containerIoC;

        private static void Main(string[] args)
        {
            Log.Debug("Run hosting services");

            try
            {
                _baseAddress = $"http://{ConfigurationManager.AppSettings["BaseAddress"]}";

                _containerIoC = ContainerConfig.Configure();

                StartHostingServiceBank();
                StartHostingServiceTransfer();
                StartHostingServiceAdmin();

                Console.ReadLine();

                _serviceBankHost.Close();
                _serviceTransferHost.Close();
                _serviceAdminHost.Close();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private static void StartHostingServiceBank()
        {
            var uri = new Uri($"{_baseAddress}/serviceBank");
            _serviceBankHost = new ServiceHost(typeof(ServiceBank), uri);
            _serviceBankHost.AddDependencyInjectionBehavior<IServiceBank>(_containerIoC);
            _serviceBankHost.AddServiceEndpoint(typeof(IServiceBank), new BasicHttpBinding(), string.Empty);
            _serviceBankHost.Description.Behaviors.Add(new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                HttpGetUrl = uri
            });
            _serviceBankHost.Open();
            Console.WriteLine(
                $"The host service bank has been opened. Address: {_serviceBankHost.Description.Endpoints[0].Address}");
        }

        private static void StartHostingServiceTransfer()
        {
            var uri = new Uri($"{_baseAddress}");
            _serviceTransferHost = new WebServiceHost(typeof(ServiceTransfer), uri);
            _serviceTransferHost.AddDependencyInjectionBehavior<IServiceTransfer>(_containerIoC);
            _serviceTransferHost.AddServiceEndpoint(typeof(IServiceTransfer), new WebHttpBinding(), string.Empty);
            var stp = _serviceTransferHost.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;
            _serviceTransferHost.Open();
            Console.WriteLine(
                $"The host service transfer has been opened. Address: {_serviceTransferHost.Description.Endpoints[0].Address}");
        }

        private static void StartHostingServiceAdmin()
        {
            var uri = new Uri($"{_baseAddress}/admin");
            _serviceAdminHost = new WebServiceHost(typeof(ServiceAdmin), uri);
            _serviceAdminHost.AddDependencyInjectionBehavior<IServiceAdmin>(_containerIoC);
            _serviceAdminHost.AddServiceEndpoint(typeof(IServiceAdmin), new WebHttpBinding(), string.Empty);
            var stp = _serviceAdminHost.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;
            _serviceAdminHost.Open();
            Console.WriteLine(
                $"The host service admin has been opened. Address: {_serviceAdminHost.Description.Endpoints[0].Address}");
        }
    }
}