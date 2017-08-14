using System.Collections.Generic;

namespace ProgFrog.Interface.Model
{
    public class ParamsAndResults
    {
        public List<string> Params { get; set; }
        public string Results { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as ParamsAndResults;
            if (other == null) return false;

            if (Results != other.Results) return false;

            if (Params == null ^ other.Params == null) return false;
            if (Params != null && Params.Count != other.Params.Count) return false;
            if (Params != null)
            {
                for(var i = 0; i < Params.Count; i++)
                {
                    if (Params[i] != other.Params[i]) return false;
                }
            }

            return true;
        }
    }
}
