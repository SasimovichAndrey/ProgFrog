using ProgFrog.Core.Data;
using ProgFrog.Core.Model;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ProgFrog.WpfApp.ViewModel
{
    public class AddProgTaskViewModel : ViewModelBase
    {
        private IProgrammingTaskRepository _taskRepo;

        public AddProgTaskViewModel(IProgrammingTaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public ProgrammingTask NewTask { get; private set; } = new ProgrammingTask();

        public async Task SaveNewTask()
        {
            throw new NotImplementedException();
        }
    }
}
