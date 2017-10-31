using ProgFrog.Interface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgFrog.WebApi.ViewModel.Mappings
{
    public static class MappingsHelper
    {
        private static Dictionary<int, string> _progLangIdToEnumMappings = new Dictionary<int, string>
        {
            {0, "CSharp" },
            { 1, "Python" }
        };

        public static ProgrammingLanguage MapProgrammingLanguage(ProgrammingLanguageViewModel model)
        {
            return (ProgrammingLanguage)model.Id;   
        }

        public static ProgrammingLanguageViewModel MapFromProgrammingLanguage(ProgrammingLanguage model)
        {
            return new ProgrammingLanguageViewModel
            {
                Id = (int)model,
                Name = _progLangIdToEnumMappings[(int)model]
            };
        }
    }
}