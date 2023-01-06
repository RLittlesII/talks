using System.Reactive;
using ReactiveUI;

namespace LoginState
{
    public static class Interactions
    {
        public static readonly Interaction<(string, string, string), Unit> AlertInteraction = new Interaction<(string, string, string), Unit>(RxApp.MainThreadScheduler);
    }
}