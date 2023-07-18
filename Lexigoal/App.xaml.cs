﻿using Lexigoal.Core;
using Lexigoal.MVVM.ViewModel;
using Lexigoal.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lexigoal
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private ServiceProvider _servicesProvider;

		public App()
        {
            IServiceCollection services = new ServiceCollection();

			services.AddSingleton<MainWindow>(provider => new MainWindow
			{
				DataContext = provider.GetRequiredService<MainViewModel>()
			});

			services.AddSingleton<MainViewModel>();
			services.AddSingleton<HomeViewModel>();
			services.AddSingleton<SettingsViewModel>();
			services.AddSingleton<INavigationService, NavigationService>();

			services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

			_servicesProvider = services.BuildServiceProvider();
        }

		protected override void OnStartup(StartupEventArgs e)
		{
			_servicesProvider.GetRequiredService<MainWindow>().Show();
			base.OnStartup(e);
		}
    }
}