﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sdl.Community.GoogleApiValidator {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AppResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Sdl.Community.GoogleApiValidator.AppResources", typeof(AppResources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All fields are required.
        /// </summary>
        public static string AllFieldsRequired {
            get {
                return ResourceManager.GetString("AllFieldsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter yout Google Translate API key into the field bellow and press Validate button.
        /// </summary>
        public static string ApiKeyDescription {
            get {
                return ResourceManager.GetString("ApiKeyDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select against what version of Google Translate API you want to validate the API Key.
        /// </summary>
        public static string ApiVersionDescription {
            get {
                return ResourceManager.GetString("ApiVersionDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Close.
        /// </summary>
        public static string CloseBtn {
            get {
                return ResourceManager.GetString("CloseBtn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to API Key field is required.
        /// </summary>
        public static string EmptyKey {
            get {
                return ResourceManager.GetString("EmptyKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        public static System.Drawing.Icon GAV {
            get {
                object obj = ResourceManager.GetObject("GAV", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GOOGLE_APPLICATION_CREDENTIALS.
        /// </summary>
        public static string GoogleApiEnvironmentVariableName {
            get {
                return ResourceManager.GetString("GoogleApiEnvironmentVariableName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Google Response.
        /// </summary>
        public static string GoogleResponseDescription {
            get {
                return ResourceManager.GetString("GoogleResponseDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid project name..
        /// </summary>
        public static string InvalidProjectName {
            get {
                return ResourceManager.GetString("InvalidProjectName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Json file could not be found at the specified path..
        /// </summary>
        public static string JsonFileMessage {
            get {
                return ResourceManager.GetString("JsonFileMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to API Key is valid..
        /// </summary>
        public static string SuccessMsg {
            get {
                return ResourceManager.GetString("SuccessMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter the path where the json file asociated to your API Key  is saved locally.
        /// </summary>
        public static string V3JsonPathDescription {
            get {
                return ResourceManager.GetString("V3JsonPathDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter project name.
        /// </summary>
        public static string V3ProjectNameDescription {
            get {
                return ResourceManager.GetString("V3ProjectNameDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Validate.
        /// </summary>
        public static string ValidateBtn {
            get {
                return ResourceManager.GetString("ValidateBtn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Close.
        /// </summary>
        public static string WindowsControl_Close {
            get {
                return ResourceManager.GetString("WindowsControl_Close", resourceCulture);
            }
        }
    }
}
