namespace AdventForCode
{
    abstract public class BaseChallenge
    {
        protected string filePath = "";

        protected BaseChallenge(string filePath)
        {
            this.filePath = filePath;
        }
    }
}
