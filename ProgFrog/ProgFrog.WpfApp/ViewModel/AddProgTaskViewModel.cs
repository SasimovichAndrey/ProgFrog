using ProgFrog.Interface.Data;
using ProgFrog.Interface.Model;
using System;
using System.Threading.Tasks;

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
