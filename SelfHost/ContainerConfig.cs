using Autofac;
using BusinessLogic.Business.Account;
using BusinessLogic.Business.Admin;
using BusinessLogic.Business.Credentials;
using BusinessLogic.Interfaces.Account;
using BusinessLogic.Interfaces.Admin;
using BusinessLogic.Interfaces.Credentials;
using RepozytoriumDB;
using RepozytoriumDB.IRepository;
using RepozytoriumDB.Repository;
using SelfHost.Services;
using SelfHost.Services.Interfaces;

namespace SelfHost
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            //db
            builder.RegisterType<BankDbContext>().As<IBankDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<ClientRepository>().As<IClientRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TokenRepository>().As<ITokenRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OperationRepository>().As<IOperationRepository>().InstancePerLifetimeScope();


            //token           
            builder.RegisterType<TokenValidator>().As<ITokenValidator>().InstancePerLifetimeScope();
            builder.RegisterType<CredentialsValidator>().As<ICredentialsValidator>().InstancePerLifetimeScope();
            builder.RegisterType<TokenManager>().As<ITokenManager>().InstancePerLifetimeScope();

            //client
            builder.RegisterType<AdminManager>().As<IAdminManager>().InstancePerLifetimeScope();

            //account
            builder.RegisterType<AccountManager>().As<IAccountManager>().InstancePerLifetimeScope();

            //services


            builder.RegisterType<ServiceBank>().As<IServiceBank>();
            builder.RegisterType<ServiceTransfer>().As<IServiceTransfer>();
            builder.RegisterType<ServiceAdmin>().As<IServiceAdmin>();

            return builder.Build();
        }
    }
}