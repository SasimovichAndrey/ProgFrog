using ProgFrog.Interface.Model;

namespace ProgFrog.WebApi.ViewModel
{
    public class RunTaskRequest
    {
        public ProgrammingTask Task { get; set; }
        public string UserCode { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
    }
}