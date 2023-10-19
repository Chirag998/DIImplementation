namespace DIExample.Lifetimes
{
    public class SingletonService
    {
        private readonly Guid _guid;
        public SingletonService()
        {
            _guid = Guid.NewGuid();
        }

        public string GetGuid()
        {
            return _guid.ToString();
        }
    }
}
