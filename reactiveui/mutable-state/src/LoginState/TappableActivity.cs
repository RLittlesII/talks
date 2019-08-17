using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LoginState
{
    public class TappableActivity : ActivityIndicator
    {
        private readonly TapGestureRecognizer _tapGestureRecognizer;

        /// <summary>
        /// Tap gesture recognizer for the entry.
        /// </summary>
        public TapGestureRecognizer TapGesture => _tapGestureRecognizer;

        public TappableActivity()
        {
            _tapGestureRecognizer = new TapGestureRecognizer();
            GestureRecognizers.Add(_tapGestureRecognizer);
        }
    }
}
