using Autofac;
using IndexMaker.Domain.Services;
using IndexMaker.Infrastructure.Services;
using System;
using System.Windows.Forms;

namespace IndexMaker
{
    static class Program
    {
        private static IContainer _container;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _container = ConfigureDependencies();
            Application.Run(_container.Resolve<MainForm>());
        }

        private static IContainer ConfigureDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainForm>();
            builder.RegisterType<DirectoryManagementService>().As<IDirectoryManagementService>().InstancePerDependency();
            var appContainer = builder.Build();
            return appContainer;
        }
    }
}
