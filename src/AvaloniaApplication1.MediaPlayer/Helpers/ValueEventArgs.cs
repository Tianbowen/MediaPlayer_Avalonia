using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaApplication1.MediaPlayer.Helpers
{
    public class ValueEventArgs<T> : RoutedEventArgs
    {
        /// <summary>
        /// The event value.
        /// </summary>
        public T Value { get; set; }

        //public ValueEventArgs()
        //{ }

        /// <summary>
        /// Initializes a new instance of ValueEventArgs with specified value.
        /// </summary>
        /// <param name="value">The event value.</param>
        public ValueEventArgs(RoutedEvent routedEvent, T value) : base(routedEvent)
        {
            Value = value;
        }
    }
}
