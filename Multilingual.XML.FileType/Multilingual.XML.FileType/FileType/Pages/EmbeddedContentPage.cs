﻿using System;
using System.Windows;
using System.Windows.Threading;
using Multilingual.XML.FileType.FileType.Settings;
using Multilingual.XML.FileType.FileType.ViewModels;
using Multilingual.XML.FileType.FileType.Views;
using Sdl.FileTypeSupport.Framework.Core.Settings;

namespace Multilingual.XML.FileType.FileType.Pages
{
	[FileTypeSettingsPage(
		Id = Constants.EmbeddedContentId,
		Name = "MultilingualXMLFileType_EmbeddedContentPage_Name",
		Description = "MultilingualXMLFileType_EmbeddedContentPage_Description",
		HelpTopic = "")]
	public class EmbeddedContentPage : AbstractFileTypeSettingsPage<EmbeddedContentView, EmbeddedContentSettings>
	{
		private EmbeddedContentView _embeddedContentView;
		private EmbeddedContentViewModel _embeddedContentViewModel;
		
		public override object GetControl()
		{
			if (_embeddedContentView != null)
			{
				return _embeddedContentView;
			}

			Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
			{
				_embeddedContentView = base.GetControl() as EmbeddedContentView;
				if (_embeddedContentView != null)
				{
					_embeddedContentViewModel = new EmbeddedContentViewModel(Settings, SubContentFileTypeConfigurationIds);
					_embeddedContentView.Loaded += EmbeddedContentViewOnLoaded;
				}
			}));

			return _embeddedContentView;
		}

		private void EmbeddedContentViewOnLoaded(object sender, RoutedEventArgs e)
		{
			_embeddedContentView.DataContext = _embeddedContentViewModel;
		}

		public override void Save()
		{
			Settings = _embeddedContentViewModel.Settings;
			base.Save();
		}

		public override void ResetToDefaults()
		{
			Settings = _embeddedContentViewModel.ResetToDefaults();
		}

		public override void Dispose()
		{
			if (_embeddedContentView != null)
			{
				_embeddedContentView.Loaded -= EmbeddedContentViewOnLoaded;
			}

			base.Dispose();
		}
	}
}
