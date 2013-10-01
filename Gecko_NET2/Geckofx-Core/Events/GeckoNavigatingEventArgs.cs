using System;
using System.Collections.Generic;
using BaseTypes = Gecko.BaseTypes;
using System.Text;
using System.ComponentModel;

namespace Gecko.Events
{
    /// <summary>
    /// Provides data for event.
    /// </summary>
    public class GeckoNavigatingEventArgs
        : CancelEventArgs
    {
        public readonly Uri Uri;
        public readonly GeckoWindow DomWindow;
        public readonly bool DomWindowTopLevel;

        /// <summary>Creates a new instance of a <see cref="GeckoNavigatingEventArgs"/> object.</summary>
        /// <param name="value"></param>
        public GeckoNavigatingEventArgs(Uri value, GeckoWindow domWind)
        {
            Uri = value;
            DomWindow = domWind;
            DomWindowTopLevel = GeckoWindowExtension.IsTopWindow(domWind);
            // domWind.IsTopWindow();
        }
    }
}
