﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18051
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2 {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource1 {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource1() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApplication2.Resource1", typeof(Resource1).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找 System.Byte[] 类型的本地化资源。
        /// </summary>
        internal static byte[] mobilemarket {
            get {
                object obj = ResourceManager.GetObject("mobilemarket", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;resp&gt;
        ///  &lt;head&gt;
        ///    &lt;msgType&gt;UserLogon&lt;/msgType&gt;
        ///    &lt;msgPlace&gt;100&lt;/msgPlace&gt;
        ///  &lt;/head&gt;
        ///  &lt;body&gt;
        ///    &lt;hRet&gt;0&lt;/hRet&gt;
        ///    &lt;user_info&gt;
        ///      &lt;userId&gt;13500000000&lt;/userId&gt;
        ///      &lt;pUserId&gt;P0000074466&lt;/pUserId&gt;
        ///      &lt;userType&gt;1&lt;/userType&gt;
        ///      &lt;bindMsisdn/&gt;
        ///      &lt;errorTimes&gt;0&lt;/errorTimes&gt;
        ///      &lt;lockTime&gt;&lt;/lockTime&gt;
        ///      &lt;nickname&gt;&lt;/nickname&gt;
        ///      &lt;email&gt;&lt;/email&gt;
        ///      &lt;headImage&gt;&lt;/headImage&gt;
        ///      &lt;sex&gt;&lt;/sex&gt;
        ///      &lt;birthday&gt;&lt;/birthday&gt;
        ///      &lt;conste [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string UserLogonResp {
            get {
                return ResourceManager.GetString("UserLogonResp", resourceCulture);
            }
        }
    }
}