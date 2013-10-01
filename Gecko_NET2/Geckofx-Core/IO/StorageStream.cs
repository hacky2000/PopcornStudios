using System;
using System.Collections.Generic;
using BaseTypes = Gecko.BaseTypes;
using System.Text;
using Gecko.Interop;

namespace Gecko.IO
{
    public sealed class StorageStream
    {
        private ComPtr<nsIStorageStream> _storageStream;
        public StorageStream()
        {
            _storageStream = Xpcom.CreateInstance2<nsIStorageStream>(Contracts.StorageStream);

            _storageStream.Instance.Init(1024 * 32, 1024 * 1024 * 16, null);
        }

        public OutputStream GetOutputStream(int position)
        {
            return ExtensionMethods.Wrap(_storageStream.Instance.GetOutputStream(position),//_searchSubmission.Instance.GetPostDataAttribute(), 
                 x => new OutputStream(x));
            //return _storageStream.Instance.GetOutputStream(position)
            //                     .Wrap(x => new OutputStream(x));
        }


        public InputStream NewInputStream(int position)
        {
            return InputStream.Create(_storageStream.Instance.NewInputStream(position));
        }

        public uint Length
        {
            get { return _storageStream.Instance.GetLengthAttribute(); }
            set { _storageStream.Instance.SetLengthAttribute(value); }
        }
    }
}
