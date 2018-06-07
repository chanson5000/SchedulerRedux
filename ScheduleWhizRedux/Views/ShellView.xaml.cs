using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ScheduleWhizRedux.Models;
using ScheduleWhizRedux.Repositories;
using ScheduleWhizRedux.Utilities;


namespace ScheduleWhizRedux.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void SelectedEmployee_AssignedJobs_GotFocus(object sender, RoutedEventArgs e)
        {
            SelectedEmployee_AvailableJobs.UnselectAll();
        }

        private void SelectedEmployee_AvailableJobs_GotFocus(object sender, RoutedEventArgs e)
        {
            SelectedEmployee_AssignedJobs.UnselectAll();
        }

        private async void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            // New scheduler object takes a list of employees and available shifts to schedule.
            Scheduler scheduler = new Scheduler(new EmployeeRepository().GetAllSorted(),
                new AssignedShiftRepository().GetAll().Where(x => x.NumAvailable > 0)
                    .ToList());

            // Some UI feedback while we wait for the awaited task to complete.
            BtnGenerate.IsEnabled = false;
            TxtGenerateSchedule.Text = "Generating Schedule...";

            Schedule schedule = await Task.Run(() => scheduler.GenerateSchedule());

            schedule.OpenSchedule();
            TxtGenerateSchedule.Text = "Generate Schedule";
            BtnGenerate.IsEnabled = true;
        }
    }
}
