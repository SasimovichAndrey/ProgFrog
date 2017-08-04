using System.Collections.Generic;

namespace ProgFrog.Core.Model
{
    public class ProgrammingTask
    {
        public string Description { get; set; }
        public ICollection<ParamsAndResults> ParamsAndResults { get; set; }
    }
}
