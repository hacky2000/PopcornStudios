using System;
using System.Collections.Generic;
using BaseTypes = Gecko.BaseTypes;
using System.Text;

namespace Gecko.Interop
{
	public static class ExtensionMethods
	{
		/// <summary>
		/// Function that check if object is null -> then call wrapper creator
		/// </summary>
		/// <typeparam name="TGeckoObject"></typeparam>
		/// <typeparam name="TWrapper"></typeparam>
		/// <param name="obj"></param>
		/// <param name="wrapper"></param>
		/// <returns></returns>
		public static TWrapper Wrap<TGeckoObject, TWrapper>( TGeckoObject obj, BaseTypes.Func<TGeckoObject, TWrapper> wrapper )
			where TGeckoObject : class
			where TWrapper : class
		{
			return obj == null ? null : wrapper( obj );
		}

		/// <summary>
		/// Function that check if object is null -> then call property getter,check returned object, and if it not null -> calls wrapper creator
		/// </summary>
		/// <typeparam name="TWrapper"></typeparam>
		/// <typeparam name="TGeckoObject2"></typeparam>
		/// <typeparam name="TGeckoObject1"></typeparam>
		/// <param name="obj"></param>
		/// <param name="wrapper"></param>
		/// <returns></returns>
        public static TWrapper Wrap<TGeckoObject1, TGeckoObject2, TWrapper>(TGeckoObject1 obj, BaseTypes.Func<TGeckoObject1, TGeckoObject2> getter, BaseTypes.Func<TGeckoObject2, TWrapper> wrapper)
			where TGeckoObject1 : class
			where TGeckoObject2 : class
			where TWrapper : class
		{
			if ( obj == null ) return null;
			var obj1 = getter( obj );
			return obj1 == null ? null : wrapper( obj1 );
		}
	}
}
