﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdventOfCode23Day05.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AdventOfCode23Day05.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to seeds: 2494933545 159314859 4045092792 172620202 928898138 554061882 2740120981 81327018 2031777983 63513119 2871914181 270575980 2200250633 216481794 3289604059 25147787 3472625834 10030240 260990830 232636388
        ///
        ///seed-to-soil map:
        ///3272284283 2724782980 1022683013
        ///138187491 4195038636 99928660
        ///2359623759 797621236 127984779
        ///662451929 2224466386 266466256
        ///928918185 714355413 83265823
        ///1012184008 3891516474 303522162
        ///3063776460 1098322140 208507823
        ///2194238166 1306829963 50525692
        ///357106588 2091837170 1 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Input1 {
            get {
                return ResourceManager.GetString("Input1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to seeds: 79 14 55 13
        ///
        ///seed-to-soil map:
        ///50 98 2
        ///52 50 48
        ///
        ///soil-to-fertilizer map:
        ///0 15 37
        ///37 52 2
        ///39 0 15
        ///
        ///fertilizer-to-water map:
        ///49 53 8
        ///0 11 42
        ///42 0 7
        ///57 7 4
        ///
        ///water-to-light map:
        ///88 18 7
        ///18 25 70
        ///
        ///light-to-temperature map:
        ///45 77 23
        ///81 45 19
        ///68 64 13
        ///
        ///temperature-to-humidity map:
        ///0 69 1
        ///1 0 69
        ///
        ///humidity-to-location map:
        ///60 56 37
        ///56 93 4.
        /// </summary>
        internal static string InputTest {
            get {
                return ResourceManager.GetString("InputTest", resourceCulture);
            }
        }
    }
}
