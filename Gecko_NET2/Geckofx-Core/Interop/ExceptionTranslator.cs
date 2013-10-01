using System;
using System.Collections.Generic;
using BaseTypes = Gecko.BaseTypes;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.Interop
{
	internal static class ExceptionTranslator
	{
		internal static void AutotranslateComErrors(BaseTypes.Action action)
		{
			try
			{
				action();
			}
			catch ( COMException comException)
			{
				int code = comException.ErrorCode;
				throw Lookup( code );
			}
		}

		internal static GeckoException Lookup(int errorCode)
		{
			return Lookup( ( uint ) errorCode );
		}

		internal static GeckoException Lookup(uint errorCode)
		{
			return new GeckoException( errorCode.ToString() );
		}
	}
}
