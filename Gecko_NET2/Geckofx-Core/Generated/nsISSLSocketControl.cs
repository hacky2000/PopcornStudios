// --------------------------------------------------------------------------------------------
// Version: MPL 1.1/GPL 2.0/LGPL 2.1
// 
// The contents of this file are subject to the Mozilla Public License Version
// 1.1 (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at
// http://www.mozilla.org/MPL/
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
// for the specific language governing rights and limitations under the
// License.
// 
// <remarks>
// Generated by IDLImporter from file nsISSLSocketControl.idl
// 
// You should use these interfaces when you access the COM objects defined in the mentioned
// IDL/IDH file.
// </remarks>
// --------------------------------------------------------------------------------------------
namespace Gecko
{
	using System;
	using System.Runtime.InteropServices;
	using System.Runtime.InteropServices.ComTypes;
	using System.Runtime.CompilerServices;
	
	
	/// <summary>
    ///-*- Mode: C++; tab-width: 2; indent-tabs-mode: nil; c-basic-offset: 2 -*-
    ///
    /// This Source Code Form is subject to the terms of the Mozilla Public
    /// License, v. 2.0. If a copy of the MPL was not distributed with this
    /// file, You can obtain one at http://mozilla.org/MPL/2.0/. </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("753f0f13-681d-4de3-a6c6-11aa7e0b3afd")]
	public interface nsISSLSocketControl
	{
		
		/// <summary>
        ///-*- Mode: C++; tab-width: 2; indent-tabs-mode: nil; c-basic-offset: 2 -*-
        ///
        /// This Source Code Form is subject to the terms of the Mozilla Public
        /// License, v. 2.0. If a copy of the MPL was not distributed with this
        /// file, You can obtain one at http://mozilla.org/MPL/2.0/. </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIInterfaceRequestor GetNotificationCallbacksAttribute();
		
		/// <summary>
        ///-*- Mode: C++; tab-width: 2; indent-tabs-mode: nil; c-basic-offset: 2 -*-
        ///
        /// This Source Code Form is subject to the terms of the Mozilla Public
        /// License, v. 2.0. If a copy of the MPL was not distributed with this
        /// file, You can obtain one at http://mozilla.org/MPL/2.0/. </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetNotificationCallbacksAttribute([MarshalAs(UnmanagedType.Interface)] nsIInterfaceRequestor aNotificationCallbacks);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ProxyStartSSL();
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void StartTLS();
		
		/// <summary>
        ///NPN (Next Protocol Negotiation) is a mechanism for
        ///       negotiating the protocol to be spoken inside the SSL
        ///       tunnel during the SSL handshake. The NPNList is the list
        ///       of offered client side protocols. setNPNList() needs to
        ///       be called before any data is read or written (including the
        ///       handshake to be setup correctly. </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetNPNList(System.IntPtr aNPNList);
		
		/// <summary>
        ///negotiatedNPN is '' if no NPN list was provided by the client,
        /// or if the server did not select any protocol choice from that
        /// list. That also includes the case where the server does not
        /// implement NPN.
        ///
        /// If negotiatedNPN is read before NPN has progressed to the point
        /// where this information is available NS_ERROR_NOT_CONNECTED is
        /// raised.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetNegotiatedNPNAttribute([MarshalAs(UnmanagedType.LPStruct)] nsACStringBase aNegotiatedNPN);
		
		/// <summary>
        ///e.g. "spdy/2" </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool JoinConnection([MarshalAs(UnmanagedType.LPStruct)] nsACStringBase npnProtocol, [MarshalAs(UnmanagedType.LPStruct)] nsACStringBase hostname, int port);
	}
}
