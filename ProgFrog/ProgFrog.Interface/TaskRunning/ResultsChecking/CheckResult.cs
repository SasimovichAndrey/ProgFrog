namespace ProgFrog.Interface.TaskRunning.ResultsChecking
{
    public class CheckResult
    {
        public bool IsSuccessfull { get; set; }
        public ResultFailureType? ErrorType { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as CheckResult;

            if(other == null)
            {
                return false;
            }

            if (this.ErrorType == null ? (other.ErrorType!= null) : !this.ErrorType.Equals(other.ErrorType)) return false;
            if (this.IsSuccessfull != other.IsSuccessfull) return false;

            return true;
        }
    }
}