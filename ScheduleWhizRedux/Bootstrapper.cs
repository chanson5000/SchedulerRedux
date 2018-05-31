using System.Windows;
using Caliburn.Micro;
using ScheduleWhizRedux.ViewModels;
using ScheduleWhizRedux.Repositories;

namespace ScheduleWhizRedux
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
               Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            Repository.CheckIfDatabaseExists();
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
