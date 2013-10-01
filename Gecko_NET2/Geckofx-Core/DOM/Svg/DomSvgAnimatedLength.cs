using Gecko.Interop;

namespace Gecko.DOM.Svg
{
    public class DomSvgAnimatedLength
    {
        private ComPtr<nsIDOMSVGAnimatedLength> _domSvgAnimatedLength;


        private DomSvgAnimatedLength(nsIDOMSVGAnimatedLength domSvgAnimatedLength)
        {
            _domSvgAnimatedLength = new ComPtr<nsIDOMSVGAnimatedLength>(domSvgAnimatedLength);
        }

        public DomSvgLength AnimVal
        {
            get
            {
                return ExtensionMethods.Wrap(_domSvgAnimatedLength.Instance.GetAnimValAttribute(),
                    DomSvgLength.Create);
                //return _domSvgAnimatedLength.Instance.GetAnimValAttribute().Wrap(DomSvgLength.Create); 
            }
        }

        public DomSvgLength BaseVal
        {
            get
            {
                return ExtensionMethods.Wrap(_domSvgAnimatedLength.Instance.GetBaseValAttribute(),
                    DomSvgLength.Create);
                //return _domSvgAnimatedLength.Instance.GetBaseValAttribute().Wrap(DomSvgLength.Create);
            }
        }
    }
}