using System;
using System.Diagnostics;

namespace GoReactive
{
    public class GoReactiveExceptions : IObserver<Exception>
    {
        public void OnCompleted()
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        public void OnError(Exception error)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        public void OnNext(Exception value)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }
    }
}