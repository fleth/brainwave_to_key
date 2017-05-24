﻿#pragma once
//------------------------------------------------------------------------------
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//------------------------------------------------------------------------------


namespace Windows {
    namespace UI {
        namespace Xaml {
            namespace Controls {
                ref class ComboBox;
                ref class Button;
                ref class TextBlock;
            }
        }
    }
}

namespace ReaderWriter
{
    [::Windows::Foundation::Metadata::WebHostHidden]
    partial ref class MainPage : public ::Windows::UI::Xaml::Controls::Page, 
        public ::Windows::UI::Xaml::Markup::IComponentConnector,
        public ::Windows::UI::Xaml::Markup::IComponentConnector2
    {
    public:
        void InitializeComponent();
        virtual void Connect(int connectionId, ::Platform::Object^ target);
        virtual ::Windows::UI::Xaml::Markup::IComponentConnector^ GetBindingConnector(int connectionId, ::Platform::Object^ target);
    
    private:
        bool _contentLoaded;
    
        private: ::Windows::UI::Xaml::Controls::ComboBox^ muse_list_combobox;
        private: ::Windows::UI::Xaml::Controls::Button^ refresh_button;
        private: ::Windows::UI::Xaml::Controls::Button^ connect_button;
        private: ::Windows::UI::Xaml::Controls::Button^ disconnect_button;
        private: ::Windows::UI::Xaml::Controls::TextBlock^ connection_status_label;
        private: ::Windows::UI::Xaml::Controls::TextBlock^ connection_status;
        private: ::Windows::UI::Xaml::Controls::TextBlock^ test_status_label;
        private: ::Windows::UI::Xaml::Controls::TextBlock^ test_status;
    };
}

