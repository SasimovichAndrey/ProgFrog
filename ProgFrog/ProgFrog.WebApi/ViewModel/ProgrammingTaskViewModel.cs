using ProgFrog.Interface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgFrog.WebApi.ViewModel
{
    public class ProgrammingTaskViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }
        public ICollection<ParamsAndResults> ParamsAndResults { get; set; } = new List<ParamsAndResults>();
    }
}