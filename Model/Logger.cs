using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Logger
    {
        public delegate void Logg(string log);
        public event Logg OnLog;

        public void Log(string text)
        {
            OnLog?.Invoke(text);
        }
    }
}
