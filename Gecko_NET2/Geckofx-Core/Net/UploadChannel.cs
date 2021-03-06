using Gecko.IO;
using Gecko.Interop;

namespace Gecko.Net
{
    public sealed class UploadChannel
    {
        private ComPtr<nsIUploadChannel> _uploadChannel;

        private nsIUploadChannel2 _uploadChannel2;

        internal UploadChannel(nsIUploadChannel uploadChannel)
        {
            _uploadChannel = new ComPtr<nsIUploadChannel>(uploadChannel);
            _uploadChannel2 = (nsIUploadChannel2)uploadChannel;
        }



        public InputStream UploadStream
        {
            get
            {
                return ExtensionMethods.Wrap(_uploadChannel.Instance.GetUploadStreamAttribute(), InputStream.Create);
                // return _uploadChannel.Instance.GetUploadStreamAttribute().Wrap(InputStream.Create);
            }
        }

        public void SetUploadStream(InputStream stream, string contentType, long len)
        {
            nsString.Set(_uploadChannel.Instance.SetUploadStream, stream._inputStream.Instance, contentType, len);
        }

        public bool UploadStreamHasHeaders
        {
            get { return _uploadChannel2.GetUploadStreamHasHeadersAttribute(); }
        }


        public void ExplicitSetUploadStream(
            InputStream stream,
            string contentType, int len, string method, bool streamHasHeaders)
        {
            using (nsACString nct = new nsACString(contentType), nmethod = new nsACString(method))
            {
                _uploadChannel2.ExplicitSetUploadStream(
                    stream._inputStream.Instance,
                    nct,
                    len,
                    nmethod,
                    streamHasHeaders
                    );
            }
        }

    }
}