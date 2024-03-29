﻿using System;
using ReactiveUI;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginState
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Composition.Compose();

            MainPage = ((Screen)Locator.Current.GetService<IScreen>()).PresentDefaultView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
