﻿using System;
using System.Collections.Generic;
using System.Drawing;
using BaseTypes = Gecko.BaseTypes;
using System.Runtime.InteropServices;
using System.Text;
using Gecko;

namespace Gecko.DOM
{
	public static class GeckoElementExtensionMethods
	{

		/// <summary>
		/// UI specific implementation extension method GetBoundingClientRect()
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static System.Drawing.Rectangle GetBoundingClientRect( GeckoElement element )
		{
			nsIDOMClientRect domRect = element.DOMElement.GetBoundingClientRect();
			if ( domRect == null ) return Rectangle.Empty;
			var r = WinFormsConverter.WrapDomClientRect(domRect);
			Marshal.ReleaseComObject( domRect );
			return r;

		}


		public static Rectangle[] GetClientRects( GeckoElement element )
		{
			nsIDOMClientRectList domRects = element.DOMElement.GetClientRects();
			var ret = WinFormsConverter.WrapDomClientRectList( domRects );
			// TODO - check code for memory leaks
			//Marshal.ReleaseComObject( domRects );
			return ret;
		}
	}
}
