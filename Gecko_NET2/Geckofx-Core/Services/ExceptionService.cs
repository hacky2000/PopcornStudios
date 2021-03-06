﻿using System;
using System.Collections.Generic;
using BaseTypes = Gecko.BaseTypes;
using System.Text;
using Gecko.Interop;

namespace Gecko
{
	public static class ExceptionService
	{
		private static ComPtr<nsIExceptionService> _exceptionService;

		static ExceptionService()
		{
			_exceptionService = Xpcom.GetService2<nsIExceptionService>(Contracts.ExceptionService);
			
		}

		/// <summary>
		/// Gets ExceptionManager for current thread
		/// </summary>
		public static ExceptionManager ExceptionManager
		{
			get { return new ExceptionManager(_exceptionService.Instance.GetCurrentExceptionManagerAttribute()); }
		}

		public static GeckoNativeException GetCurrentException()
		{
			return GeckoNativeException.Create( _exceptionService.Instance.GetCurrentException() );
		}
	}

	public sealed class ExceptionManager
	{
		private nsIExceptionManager _exceptionManager;

		internal ExceptionManager(nsIExceptionManager exceptionManager)
		{
			_exceptionManager = exceptionManager;
		}

		public GeckoNativeException CurrentException
		{
			get { return GeckoNativeException.Create(_exceptionManager.GetCurrentException()); }
		}
	}

}
