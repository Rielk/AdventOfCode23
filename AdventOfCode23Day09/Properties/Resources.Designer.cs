﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdventOfCode23Day09.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AdventOfCode23Day09.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
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
        ///   Looks up a localized string similar to 13 23 46 97 206 436 919 1924 3971 8016 15765 30243 56875 105604 195153 361805 677725 1289089 2494076 4898137 9713336
        ///-7 6 44 130 309 672 1404 2864 5712 11113 21082 39112 71386 129156 233312 422773 769075 1400300 2538072 4551386 8029995
        ///-4 10 38 80 136 206 290 388 500 626 766 920 1088 1270 1466 1676 1900 2138 2390 2656 2936
        ///6 22 45 75 118 191 327 580 1030 1788 3001 4857 7590 11485 16883 24186 33862 46450 62565 82903 108246
        ///6 9 3 -13 -36 -44 46 466 1757 5034 12396 27528 56549 109167 200209 351601 594880 9 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Input1 {
            get {
                return ResourceManager.GetString("Input1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0 3 6 9 12 15
        ///1 3 6 10 15 21
        ///10 13 16 21 30 45.
        /// </summary>
        internal static string InputTest1 {
            get {
                return ResourceManager.GetString("InputTest1", resourceCulture);
            }
        }
    }
}