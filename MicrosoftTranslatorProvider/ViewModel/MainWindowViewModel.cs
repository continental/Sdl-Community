﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using MicrosoftTranslatorProvider.Commands;
using MicrosoftTranslatorProvider.Interfaces;
using MicrosoftTranslatorProvider.Model;
using MicrosoftTranslatorProvider.Service;
using MicrosoftTranslatorProvider.Studio.TranslationProvider;
using Sdl.LanguagePlatform.Core;
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace MicrosoftTranslatorProvider.ViewModel
{
	public class MainWindowViewModel : BaseModel, IMainWindow
	{
		private readonly ISettingsControlViewModel _settingsControlViewModel;
		private readonly IProviderControlViewModel _providerControlViewModel;
		private readonly ITranslationProviderCredentialStore _credentialStore;
		private readonly LanguagePair[] _languagePairs;
		private readonly HtmlUtil _htmlUtil;
		private readonly bool _isTellMeAction;

		private string _translatorErrorResponse;
		private ViewDetails _selectedView;
		private string _errorMessage;
		private bool _dialogResult;

		public event CloseWindowEventRaiser CloseEventRaised;
		public delegate void CloseWindowEventRaiser();

		public MainWindowViewModel(ITranslationOptions options, IProviderControlViewModel providerControlViewModel,
			ISettingsControlViewModel settingsControlViewModel,
			ITranslationProviderCredentialStore credentialStore, LanguagePair[] languagePairs, HtmlUtil htmlUtil)
		{
			Options = options;
			_providerControlViewModel = providerControlViewModel;
			_settingsControlViewModel = settingsControlViewModel;
			_credentialStore = credentialStore;
			_languagePairs = languagePairs;
			_htmlUtil = htmlUtil;

			SaveCommand = new RelayCommand(Save);
			ShowSettingsViewCommand = new CommandHandler(ShowSettingsPage, true);
			ShowMainViewCommand = new CommandHandler(ShowProvidersPage, true);

			providerControlViewModel.ShowSettingsCommand = ShowSettingsViewCommand;
			providerControlViewModel.ClearMessageRaised += ClearMessageRaised;
			settingsControlViewModel.ShowMainWindowCommand = ShowMainViewCommand;

			AvailableViews = new List<ViewDetails>
			{
				new ViewDetails
				{
					Name = PluginResources.PluginsView,
					ViewModel = providerControlViewModel.ViewModel
				},
				new ViewDetails
				{
					Name = PluginResources.SettingsView,
					ViewModel = settingsControlViewModel.ViewModel
				}
			};

			ShowProvidersPage();
		}

		public MainWindowViewModel(ITranslationOptions options, ISettingsControlViewModel settingsControlViewModel, bool isTellMeAction)
		{
			Options = options;
			_isTellMeAction = isTellMeAction;
			_settingsControlViewModel = settingsControlViewModel;
			SaveCommand = new RelayCommand(Save);

			AvailableViews = new List<ViewDetails>
			{
				new ViewDetails
				{
					Name = PluginResources.SettingsView,
					ViewModel = settingsControlViewModel.ViewModel
				}
			};

			if (_isTellMeAction)
			{
				SelectedView = AvailableViews[0];
			}
		}

		public ViewDetails SelectedView
		{
			get => _selectedView;
			set
			{
				_selectedView = value;
				ErrorMessage = string.Empty;
				OnPropertyChanged(nameof(SelectedView));
			}
		}

		public List<ViewDetails> AvailableViews { get; set; }
		public ICommand ShowSettingsViewCommand { get; set; }
		public ICommand ShowMainViewCommand { get; set; }
		public ICommand SaveCommand { get; set; }
		public ITranslationOptions Options { get; set; }

		public bool DialogResult
		{
			get => _dialogResult;
			set
			{
				if (_dialogResult == value) return;
				_dialogResult = value;
				OnPropertyChanged(nameof(DialogResult));
			}
		}

		public string ErrorMessage
		{
			get => _errorMessage;
			set
			{
				if (_errorMessage == value) return;
				_errorMessage = value;
				OnPropertyChanged(nameof(ErrorMessage));
			}
		}

		public string TranslatorErrorResponse
		{
			get => _translatorErrorResponse;
			set
			{
				if (_translatorErrorResponse == value) return;
				_translatorErrorResponse = value;
				OnPropertyChanged(nameof(TranslatorErrorResponse));
			}
		}

		public void AddEncriptionMetaToResponse(string errorMessage)
		{
			const string htmlStart = @"<html>
<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>
<body style=""font-family:Segoe Ui!important;color:red!important;font-size:13px!important"">";
			TranslatorErrorResponse = $"{errorMessage.Insert(0, htmlStart)}\n</body></html>";
		}

		public bool IsWindowValid()
		{
			ErrorMessage = string.Empty;
			var microsoftOptions = ValidMicrosoftOptions();
			return microsoftOptions ? ValidSettingsPageOptions() : microsoftOptions;
		}

		private bool ValidSettingsPageOptions()
		{
			if (_settingsControlViewModel.DoPreLookup)
			{
				if (string.IsNullOrEmpty(_settingsControlViewModel.PreLookupFileName))
				{
					ErrorMessage = PluginResources.PreLookupEmptyMessage;
					return false;
				}

				if (!File.Exists(_settingsControlViewModel.PreLookupFileName))
				{
					ErrorMessage = PluginResources.PreLookupWrongPathMessage;
					return false;
				}
			}

			if (!_settingsControlViewModel.DoPostLookup)
			{
				return true;
			}

			if (string.IsNullOrEmpty(_settingsControlViewModel.PostLookupFileName))
			{
				ErrorMessage = PluginResources.PostLookupEmptyMessage;
				return false;
			}

			if (!File.Exists(_settingsControlViewModel.PostLookupFileName))
			{
				ErrorMessage = PluginResources.PostLookupWrongPathMessage;
				return false;
			}

			return true;
		}

		private bool ValidMicrosoftOptions()
		{
			if (string.IsNullOrEmpty(_providerControlViewModel.ClientID))
			{
				ErrorMessage = PluginResources.ApiKeyError;
				return false;
			}
			
			if (_providerControlViewModel.UseCategoryID && string.IsNullOrEmpty(_providerControlViewModel.CategoryID))
			{
				ErrorMessage = PluginResources.CatIdError;
				return false;
			}

			return AreMicrosoftCredentialsValid();
		}

		private void ShowSettingsPage()
		{
			SelectedView = AvailableViews[1];
		}

		private void ShowProvidersPage()
		{
			SelectedView = AvailableViews[0];
		}

		private void ClearMessageRaised()
		{
			ErrorMessage = string.Empty;
			TranslatorErrorResponse = "<html><body></html></body>";
		}

		private void Save(object window)
		{
			if (_isTellMeAction)
			{
				SetGeneralProviderOptions();
				DialogResult = true;
				CloseEventRaised?.Invoke();
				return;
			}

			if (!IsWindowValid())
			{
				return;
			}

			SetMicrosoftProviderOptions();
			SetGeneralProviderOptions();
			DeleteCredentialsIfNecessary();
			DialogResult = true;
			CloseEventRaised?.Invoke();
		}

		private void DeleteCredentialsIfNecessary()
		{
			var isMicrosoftProvider = _providerControlViewModel.SelectedTranslationOption.ProviderType == MTETranslationOptions.ProviderType.MicrosoftTranslator;
			if (isMicrosoftProvider && !Options.PersistMicrosoftCredentials)
			{
				RemoveCredentialsFromStore(new Uri(Constants.MicrosoftProviderUriScheme));
			}
		}

		private void RemoveCredentialsFromStore(Uri providerUri)
		{
			if (_credentialStore.GetCredential(providerUri) is null)
			{
				return;
			}

			_credentialStore.RemoveCredential(providerUri);
		}

		private bool AreMicrosoftCredentialsValid()
		{
			try
			{
				var apiConnecter = new ProviderConnecter(_providerControlViewModel.ClientID, _providerControlViewModel.Region?.Key, _htmlUtil);
				apiConnecter.RefreshAuthToken();

				return true;
			}
			catch (Exception e)
			{
				AddEncriptionMetaToResponse(e.Message);
			}

			return false;
		}

		private void SetGeneralProviderOptions()
		{
			if (_settingsControlViewModel != null)
			{
				Options.SendPlainTextOnly = _settingsControlViewModel.SendPlainText;
				Options.ResendDrafts = _settingsControlViewModel.ReSendDraft;
				Options.UsePreEdit = _settingsControlViewModel.DoPreLookup;
				Options.PreLookupFilename = _settingsControlViewModel.PreLookupFileName;
				Options.UsePostEdit = _settingsControlViewModel.DoPostLookup;
				Options.PostLookupFilename = _settingsControlViewModel.PostLookupFileName;
			}

			if (Options != null && Options.LanguagesSupported == null)
			{
				Options.LanguagesSupported = new Dictionary<string, string>();
			}

			if (_languagePairs == null)
			{
				return;
			}

			foreach (var languagePair in _languagePairs)
			{
				Options?.LanguagesSupported.Add(languagePair.TargetCultureName, _providerControlViewModel.SelectedTranslationOption.Name);
			}
		}

		private void SetMicrosoftProviderOptions()
		{
			Options.ClientID = _providerControlViewModel.ClientID;
			Options.Region = _providerControlViewModel.Region.Key;
			Options.UseCategoryID = _providerControlViewModel.UseCategoryID;
			Options.CategoryID = _providerControlViewModel.CategoryID;
			Options.PersistMicrosoftCredentials = _providerControlViewModel.PersistMicrosoftKey;
		}
	}
}