﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdventOfCode23Day20.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AdventOfCode23Day20.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to &amp;ls -&gt; fg, ck, fv, sk, fl, hz
        ///%px -&gt; ls, dv
        ///%jk -&gt; xt, tx
        ///%hd -&gt; fs, vn
        ///%pk -&gt; ql
        ///%bj -&gt; tx, pr
        ///%vg -&gt; xl, sf
        ///%cj -&gt; bj
        ///%sk -&gt; bz
        ///%fl -&gt; fx
        ///%th -&gt; fl, ls
        ///%pr -&gt; gm
        ///%xv -&gt; sf, hp
        ///%mh -&gt; jb
        ///%jh -&gt; kx, tx
        ///%jz -&gt; pm, fs
        ///%hr -&gt; tx, gk
        ///%kx -&gt; cj
        ///%ql -&gt; jx
        ///%gm -&gt; tx, jt
        ///%hz -&gt; ls, fv
        ///%dt -&gt; fs
        ///%gg -&gt; sf, vg
        ///%xl -&gt; fh
        ///&amp;pq -&gt; vr
        ///%jx -&gt; mv
        ///%kr -&gt; gg
        ///%bn -&gt; px, ls
        ///&amp;fs -&gt; pm, rg, pq, pj, nk, mh, jb
        ///%vn -&gt; rt, fs
        ///%jt -&gt; tx, zb
        ///broadcaster -&gt; hz, hr, nk, nv
        ///%fx -&gt; sk, ls
        ///%rt -&gt; fs, kz
        ///%g [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Input1 {
            get {
                return ResourceManager.GetString("Input1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to broadcaster -&gt; a, b, c
        ///%a -&gt; b
        ///%b -&gt; c
        ///%c -&gt; inv
        ///&amp;inv -&gt; a.
        /// </summary>
        internal static string InputTest1 {
            get {
                return ResourceManager.GetString("InputTest1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to broadcaster -&gt; a
        ///%a -&gt; inv, con
        ///&amp;inv -&gt; b
        ///%b -&gt; con
        ///&amp;con -&gt; output.
        /// </summary>
        internal static string InputTest2 {
            get {
                return ResourceManager.GetString("InputTest2", resourceCulture);
            }
        }
    }
}