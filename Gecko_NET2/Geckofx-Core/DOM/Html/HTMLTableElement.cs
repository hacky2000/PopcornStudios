

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Gecko.Collections;
using Gecko.Interop;

namespace Gecko.DOM
{
    public class GeckoTableElement : GeckoHtmlElement
    {
        nsIDOMHTMLTableElement DOMHTMLElement;
        internal GeckoTableElement(nsIDOMHTMLTableElement element)
            : base(element)
        {
            this.DOMHTMLElement = element;
        }
        public GeckoTableElement(object element)
            : base(element as nsIDOMHTMLElement)
        {
            this.DOMHTMLElement = element as nsIDOMHTMLTableElement;
        }
        public GeckoTableCaptionElement Caption
        {
            get { return new GeckoTableCaptionElement(DOMHTMLElement.GetCaptionAttribute()); }
            set { DOMHTMLElement.SetCaptionAttribute(value.DomObject as nsIDOMHTMLTableCaptionElement); }
        }

        public GeckoTableSectionElement THead
        {
            get { return new GeckoTableSectionElement(DOMHTMLElement.GetTHeadAttribute()); }
            set { DOMHTMLElement.SetTHeadAttribute(value.DomObject as nsIDOMHTMLTableSectionElement); }
        }

        public GeckoTableSectionElement TFoot
        {
            get { return new GeckoTableSectionElement(DOMHTMLElement.GetTFootAttribute()); }
            set { DOMHTMLElement.SetTFootAttribute(value.DomObject as nsIDOMHTMLTableSectionElement); }
        }

        public IGeckoArray<GeckoElement> Rows
        {
            get
            {
                return ExtensionMethods.Wrap(DOMHTMLElement.GetRowsAttribute(),
                   x => new DomHtmlCollection<GeckoElement, nsIDOMElement>(
                                     x, CreateDomElementWrapper));
                //return DOMHTMLElement.GetRowsAttribute()
                //     .Wrap(x => new DomHtmlCollection<GeckoElement, nsIDOMElement>(
                //                     x, CreateDomElementWrapper));
            }
        }

        public IGeckoArray<GeckoElement> TBodies
        {
            get
            {
                return ExtensionMethods.Wrap(DOMHTMLElement.GetTBodiesAttribute(),
                   x => new DomHtmlCollection<GeckoElement, nsIDOMElement>(
                                                     x, CreateDomElementWrapper));
                //return DOMHTMLElement.GetTBodiesAttribute()
                //                     .Wrap(x => new DomHtmlCollection<GeckoElement, nsIDOMElement>(
                //                                     x, CreateDomElementWrapper));
            }
        }

        public string Align
        {
            get { return nsString.Get(DOMHTMLElement.GetAlignAttribute); }
            set { DOMHTMLElement.SetAlignAttribute(new nsAString(value)); }
        }

        public string BgColor
        {
            get { return nsString.Get(DOMHTMLElement.GetBgColorAttribute); }
            set { DOMHTMLElement.SetBgColorAttribute(new nsAString(value)); }
        }

        public string Border
        {
            get { return nsString.Get(DOMHTMLElement.GetBorderAttribute); }
            set { DOMHTMLElement.SetBorderAttribute(new nsAString(value)); }
        }

        public string CellPadding
        {
            get { return nsString.Get(DOMHTMLElement.GetCellPaddingAttribute); }
            set { DOMHTMLElement.SetCellPaddingAttribute(new nsAString(value)); }
        }

        public string CellSpacing
        {
            get { return nsString.Get(DOMHTMLElement.GetCellSpacingAttribute); }
            set { DOMHTMLElement.SetCellSpacingAttribute(new nsAString(value)); }
        }

        public string Frame
        {
            get { return nsString.Get(DOMHTMLElement.GetFrameAttribute); }
            set { DOMHTMLElement.SetFrameAttribute(new nsAString(value)); }
        }

        public string Rules
        {
            get { return nsString.Get(DOMHTMLElement.GetRulesAttribute); }
            set { DOMHTMLElement.SetRulesAttribute(new nsAString(value)); }
        }

        public string Summary
        {
            get { return nsString.Get(DOMHTMLElement.GetSummaryAttribute); }
            set { DOMHTMLElement.SetSummaryAttribute(new nsAString(value)); }
        }

        public string Width
        {
            get { return nsString.Get(DOMHTMLElement.GetWidthAttribute); }
            set { DOMHTMLElement.SetWidthAttribute(new nsAString(value)); }
        }

        public GeckoHtmlElement createTHead()
        {
            return new GeckoHtmlElement(DOMHTMLElement.CreateTHead());
        }

        public void deleteTHead()
        {
            DOMHTMLElement.DeleteTHead();
        }

        public GeckoHtmlElement createTFoot()
        {
            return new GeckoHtmlElement(DOMHTMLElement.CreateTFoot());
        }

        public void deleteTFoot()
        {
            DOMHTMLElement.DeleteTFoot();
        }

        public GeckoHtmlElement createCaption()
        {
            return new GeckoHtmlElement(DOMHTMLElement.CreateCaption());
        }

        public void deleteCaption()
        {
            DOMHTMLElement.DeleteCaption();
        }

        public GeckoHtmlElement insertRow(int index)
        {
            return new GeckoHtmlElement(DOMHTMLElement.InsertRow(index));
        }

        public void deleteRow(int index)
        {
            DOMHTMLElement.DeleteRow(index);
        }

    }
}
