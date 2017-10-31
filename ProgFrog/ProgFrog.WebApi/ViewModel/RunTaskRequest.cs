using ProgFrog.Interface.Model;
using System.ComponentModel.DataAnnotations;

namespace ProgFrog.WebApi.ViewModel
{
    public class RunTaskRequest
    {
        [Required]
        public ProgrammingTaskViewModel Task { get; set; }
        [Required]
        public string UserCode { get; set; }
        [Required]
        public ProgrammingLanguageViewModel ProgrammingLanguage { get; set; }
    }
}