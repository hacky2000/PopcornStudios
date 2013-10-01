using System;
using System.Collections.Generic;
using BaseTypes = Gecko.BaseTypes;
using System.Text;

namespace Gecko.DOM.Events
{
	public sealed class DomSvgEvent
		:DomEventArgs
	{
		private nsIDOMSVGEvent _domSvgEvent;

		private DomSvgEvent( nsIDOMSVGEvent domSvgEvent )
			:base(domSvgEvent)
		{
			_domSvgEvent = domSvgEvent;
		}

		public static DomSvgEvent Create( nsIDOMSVGEvent domSvgEvent )
		{
			return new DomSvgEvent( domSvgEvent );
		}
	}
}
