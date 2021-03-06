

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Gecko.Collections;
using Gecko.Interop;

namespace Gecko.DOM
{
    public class GeckoTableRowElement : GeckoHtmlElement
    {
        nsIDOMHTMLTableRowElement DOMHTMLElement;
        internal GeckoTableRowElement(nsIDOMHTMLTableRowElement element)
            : base(element)
        {
            this.DOMHTMLElement = element;
        }
        public GeckoTableRowElement(object element)
            : base(element as nsIDOMHTMLElement)
        {
            this.DOMHTMLElement = element as nsIDOMHTMLTableRowElement;
        }
        public int RowIndex
        {
            get { return DOMHTMLElement.GetRowIndexAttribute(); }
        }

        public int SectionRowIndex
        {
            get { return DOMHTMLElement.GetSectionRowIndexAttribute(); }
        }

        public IGeckoArray<GeckoElement> Cells
        {
            get
            {
                return ExtensionMethods.Wrap(DOMHTMLElement.GetCellsAttribute(),
                    x => new DomHtmlCollection<GeckoElement, nsIDOMElement>(
                                                     x, CreateDomElementWrapper));
                //return DOMHTMLElement.GetCellsAttribute()
                //                     .Wrap( x => new DomHtmlCollection<GeckoElement, nsIDOMElement>(
                //                                     x, CreateDomElementWrapper ) );
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

        public string Ch
        {
            get { return nsString.Get(DOMHTMLElement.GetChAttribute); }
            set { DOMHTMLElement.SetChAttribute(new nsAString(value)); }
        }

        public string ChOff
        {
            get { return nsString.Get(DOMHTMLElement.GetChOffAttribute); }
            set { DOMHTMLElement.SetChOffAttribute(new nsAString(value)); }
        }

        public string VAlign
        {
            get { return nsString.Get(DOMHTMLElement.GetVAlignAttribute); }
            set { DOMHTMLElement.SetVAlignAttribute(new nsAString(value)); }
        }

        public GeckoHtmlElement insertCell(int index)
        {
            return new GeckoHtmlElement(DOMHTMLElement.InsertCell(index));
        }

        public void deleteCell(int index)
        {
            DOMHTMLElement.DeleteCell(index);
        }

    }
}
