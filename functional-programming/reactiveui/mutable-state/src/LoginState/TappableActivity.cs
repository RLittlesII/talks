using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LoginState
{
    public class TappableActivity : ActivityIndicator
    {
        private readonly TapGestureRecognizer _tapGestureRecognizer;
        private readonly ClickGestureRecognizer _clickGestureRecognizer;

        /// <summary>
        /// Tap gesture recognizer for the entry.
        /// </summary>
        public TapGestureRecognizer TapGesture => _tapGestureRecognizer;

        /// <summary>
        /// Click gesture recognizer for the entry.
        /// </summary>
        public ClickGestureRecognizer ClickGesture => _clickGestureRecognizer;

        public TappableActivity()
        {
            _tapGestureRecognizer = new TapGestureRecognizer();
            _clickGestureRecognizer = new ClickGestureRecognizer();
            GestureRecognizers.Add(_tapGestureRecognizer);
            GestureRecognizers.Add(_clickGestureRecognizer);
        }
    }
}
