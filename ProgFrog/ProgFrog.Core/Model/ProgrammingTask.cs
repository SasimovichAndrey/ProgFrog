using Newtonsoft.Json;
using ProgFrog.Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace ProgFrog.Core.Model
{
    public class ProgrammingTask
    {
        [JsonIgnore]
        public IIdentifier Identifier { get; set; }

        public string Description { get; set; }
        public ICollection<ParamsAndResults> ParamsAndResults { get; set; } = new List<ParamsAndResults>();

        public override bool Equals(object obj)
        {
            var other = obj as ProgrammingTask;

            if (other == null) return false;

            if (Description != other.Description) return false;

            if (ParamsAndResults == null ^ other.ParamsAndResults == null) return false;
            if(ParamsAndResults != null)
            {
                if (ParamsAndResults.Count != other.ParamsAndResults.Count) return false;
                for(int index = 0; index < ParamsAndResults.Count; index++)
                {
                    var thisPrm = ParamsAndResults.ElementAt(index);
                    var otherPrm = other.ParamsAndResults.ElementAt(index);

                    if (!thisPrm.Equals(otherPrm))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
