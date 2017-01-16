using System;
using System.Configuration;
using System.Net;
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
    internal class Program
    {
        private static ServiceHost _serviceBankHost;
        private static ServiceHost _serviceTransferHost;
        private static ServiceHost _serviceAdminHost;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main(string[] args)
        {
            Log.Debug("Start hosting");

            try
            {
                var baseAddress = $"http://{ConfigurationManager.AppSettings["BaseAddress"]}";

                var container = ContainerConfig.Configure();

                StartHostServiceBank(baseAddress, container);
                StartHostServiceTransfer(baseAddress, container);
                StartHostServiceAdmin(baseAddress, container);

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

        private static void StartHostServiceBank(string address, IContainer container)
        {
            var uri = new Uri($"{address}/serviceBank");
            _serviceBankHost = new ServiceHost(typeof(ServiceBank), uri);
            _serviceBankHost.AddDependencyInjectionBehavior<IServiceBank>(container);
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

        private static void StartHostServiceTransfer(string address, IContainer container)
        {
            var uri = new Uri($"{address}");
            _serviceTransferHost = new WebServiceHost(typeof(ServiceTransfer), uri);
            _serviceTransferHost.AddDependencyInjectionBehavior<IServiceTransfer>(container);

            _serviceTransferHost.AddServiceEndpoint(typeof(IServiceTransfer), new WebHttpBinding(), string.Empty);
            var stp = _serviceTransferHost.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;
            _serviceTransferHost.Open();
            Console.WriteLine(
                $"The host service transfer has been opened. Address: {_serviceTransferHost.Description.Endpoints[0].Address}");
        }

        private static void StartHostServiceAdmin(string address, IContainer container)
        {
            var uri = new Uri($"{address}/admin");
            _serviceAdminHost = new WebServiceHost(typeof(ServiceAdmin), uri);
            _serviceAdminHost.AddDependencyInjectionBehavior<IServiceAdmin>(container);

            _serviceAdminHost.AddServiceEndpoint(typeof(IServiceAdmin), new WebHttpBinding(), string.Empty);
            var stp = _serviceAdminHost.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;
            _serviceAdminHost.Open();
            Console.WriteLine(
                $"The host service admin has been opened. Address: {_serviceAdminHost.Description.Endpoints[0].Address}");
        }

        private static bool CheckIPValid(string strIP)
        {
            IPAddress result = null;
            return
                !string.IsNullOrEmpty(strIP) &&
                IPAddress.TryParse(strIP, out result);
        }

        private static bool CheckPort(string port)
        {
            int result;
            return
                !string.IsNullOrEmpty(port) &&
                int.TryParse(port, out result);
        }
    }
}