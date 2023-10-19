namespace DIExample.Lifetimes
{
    public class ScopeService
    {
        private readonly Guid _guid;
        public ScopeService()
        {
            _guid= Guid.NewGuid();
        }

        public string GetGuid() { 
            return _guid.ToString();
        }
    }
}
