using System;
using System.Collections;
using System.Collections.Generic;
using BaseTypes = Gecko.BaseTypes;
using System.Text;

namespace Gecko.Collections
{
    /// <summary>
    /// Internal wrapper realization
    /// </summary>
    /// <typeparam name="TWrapper"></typeparam>
    /// <typeparam name="TGeckoObject"></typeparam>
    internal sealed class GeckoArray<TWrapper, TGeckoObject>
        : IGeckoArray<TWrapper>
    {
        private nsIArray _array;
        private BaseTypes.Func<TGeckoObject, TWrapper> _translator;
        internal GeckoArray(nsIArray array, BaseTypes.Func<TGeckoObject, TWrapper> translator)
        {
            _array = array;
            _translator = translator;
        }
        public int Length { get { return (int)_array.GetLengthAttribute(); } }

        public TWrapper this[int index]
        {
            get
            {
                var enumerator = _array.Enumerate();

                TGeckoObject wrapObj;

                int i = -1;
                while (enumerator.HasMoreElements())
                {
                    i++;
                    var one = enumerator.GetNext();
                    if (i == index)
                    {
                        wrapObj = (TGeckoObject)one;
                        var ret2 = _translator(wrapObj);
                        return ret2;
                    }
                }

                return default(TWrapper);

                //var obj = _array.GetElementAs<TGeckoObject>(index);
                //var ret = _translator(obj);
                //return ret;
            }
        }

        public IEnumerator<TWrapper> GetEnumerator()
        {
            return new GeckoEnumerator<TWrapper, TGeckoObject>(_array.Enumerate(), _translator);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
