﻿#pragma checksum "..\..\..\UserInfo.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8C5FCF03D91084722D02555593C158E95001F540"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Project_PRN212_FALL24;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Project_PRN212_FALL24 {
    
    
    /// <summary>
    /// UserInfo
    /// </summary>
    public partial class UserInfo : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\..\UserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup ProfilePopup;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\UserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock EmailTextBlock;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\UserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock AdminAccountTextBlock;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\UserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEmail;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\UserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtOldPassword;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\UserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtNewPassword;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\UserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtCFNewPassword;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\UserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdate;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Project_PRN212_FALL24;component/userinfo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserInfo.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 18 "..\..\..\UserInfo.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Logo_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 23 "..\..\..\UserInfo.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 40 "..\..\..\UserInfo.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Popup_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ProfilePopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 5:
            this.EmailTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            
            #line 47 "..\..\..\UserInfo.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.AccountInfo_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 48 "..\..\..\UserInfo.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.BorrowHistory_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.AdminAccountTextBlock = ((System.Windows.Controls.TextBlock)(target));
            
            #line 49 "..\..\..\UserInfo.xaml"
            this.AdminAccountTextBlock.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.AdminAccount_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 50 "..\..\..\UserInfo.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Logout_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.txtEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.txtOldPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 12:
            this.txtNewPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 13:
            this.txtCFNewPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 14:
            this.btnUpdate = ((System.Windows.Controls.Button)(target));
            
            #line 80 "..\..\..\UserInfo.xaml"
            this.btnUpdate.Click += new System.Windows.RoutedEventHandler(this.btnUpdate_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

