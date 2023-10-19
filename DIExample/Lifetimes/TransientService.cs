namespace DIExample.Lifetimes
{
    public class TransientService
    {
        private readonly Guid _guid;
        public TransientService()
        {
            _guid= Guid.NewGuid();
        }

        public string GetGuid() { 
            return _guid.ToString();
        }
    }
}
