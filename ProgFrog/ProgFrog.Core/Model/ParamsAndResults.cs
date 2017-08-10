namespace ProgFrog.Core.Model
{
    public class ParamsAndResults
    {
        public string Params { get; set; }
        public string Results { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as ParamsAndResults;
            if (other == null) return false;

            if (Params != other.Params) return false;
            if (Results != other.Results) return false;

            return true;
        }
    }
}
