namespace TajikKEA
{
    public class TajikKEALogger
    {
        public delegate void Logg(string log);
        public event Logg OnLog;
        public void Log(string text)
        {
            OnLog?.Invoke(text);
        }
    }
}
