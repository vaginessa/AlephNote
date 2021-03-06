﻿using AlephNote.PluginInterface;
using AlephNote.WPF.Controls;
using AlephNote.WPF.Util;
using Ookii.Dialogs.Wpf;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace AlephNote.WPF.Converter
{
	class CreatePluginSettingsGrid : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length < 1 || values.Any(p => p == DependencyProperty.UnsetValue)) return DependencyProperty.UnsetValue;

			var provider = values[0] as RemoteStorageAccount;
			//var settings = values[1] as AppSettings;
			var listener = values.Length >= 3 ? (values[2] as IChangeListener) : null;

			if (provider == null) return DependencyProperty.UnsetValue;

			var cfg = provider.Config;
			var plugin = provider.Plugin;

			var grid = new Grid();

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto)   }); // label
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)   }); // component
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(35, GridUnitType.Pixel) }); // additionalComponent
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto)   }); // Help

			int row = 0;
			foreach (var prop in cfg.ListProperties())
			{
				var xprop = prop;

				switch (prop.Type)
				{
					case DynamicSettingValue.SettingType.Text:
						var tb = new TextBox {Text = prop.CurrentValue};
						tb.TextChanged += (s, a) => { cfg.SetProperty(xprop.ID, tb.Text); listener?.OnChanged("pluginconfig", xprop.ID, tb.Text); };
						AddComponent(prop, plugin, ref row, grid, tb);
						break;

					case DynamicSettingValue.SettingType.Password:
						var pb = new BindablePasswordBox {Password = prop.CurrentValue};
						pb.PasswordChanged += (s, a) => { cfg.SetProperty(xprop.ID, pb.Password); listener?.OnChanged("pluginconfig", xprop.ID, pb.Password); };
						AddComponent(prop, plugin, ref row, grid, pb);
						break;

					case DynamicSettingValue.SettingType.Checkbox:
						var cb = new CheckBox { IsChecked = (bool)prop.Arguments[0] };
						cb.Checked += (s, a) => { cfg.SetProperty(xprop.ID, cb.IsChecked ?? false); };
						cb.Unchecked += (s, a) => { cfg.SetProperty(xprop.ID, cb.IsChecked ?? false); };
						AddComponent(prop, plugin, ref row, grid, cb);
						break;

					case DynamicSettingValue.SettingType.ComboBox:
						var ob = new ComboBox();
						foreach (var arg in xprop.Arguments.Cast<string>()) ob.Items.Add(arg);
						ob.SelectedItem = xprop.CurrentValue;
						ob.SelectionChanged += (s, a) => { cfg.SetProperty(xprop.ID, (string)ob.SelectedValue); listener?.OnChanged("pluginconfig", xprop.ID, ob.SelectedValue); };
						AddComponent(prop, plugin, ref row, grid, ob);
						break;

					case DynamicSettingValue.SettingType.Folder:
						var fb = new TextBox { Text = prop.CurrentValue };
						fb.TextChanged += (s, a) => { cfg.SetProperty(xprop.ID, fb.Text); listener?.OnChanged("pluginconfig", xprop.ID, fb.Text); };
						fb.IsReadOnly = true;
						fb.IsReadOnlyCaretVisible = true;
						var btn = new Button();
						btn.Content = "...";
						btn.Click += (s, a) =>
						{
							VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
							if (Directory.Exists(fb.Text)) dialog.SelectedPath = fb.Text;
							if (dialog.ShowDialog() ?? false) fb.Text = dialog.SelectedPath;
						};
						AddComponent(prop, plugin, ref row, grid, fb, true, btn);
						break;

					case DynamicSettingValue.SettingType.Hyperlink:
						var hl = new TextBlock
						{
							Text = prop.Description,
							TextDecorations = TextDecorations.Underline,
							Foreground = Brushes.Blue,
							Cursor = Cursors.Hand,
							ToolTip = prop.Arguments[0],
						};
						hl.MouseDown += (o, e) => Process.Start((string)xprop.Arguments[0]);
						AddComponent(prop, plugin, ref row, grid, hl, false);
						break;

					case DynamicSettingValue.SettingType.Spinner:
						var iud = new IntegerUpDown();
						iud.Minimum = (int)xprop.Arguments[1];
						iud.Maximum = (int)xprop.Arguments[2];
						iud.Value   = (int)xprop.Arguments[0];

						iud.ValueChanged += (s, a) => { cfg.SetProperty(xprop.ID, iud.Value.Value); listener?.OnChanged("pluginconfig", xprop.ID, iud.Value); };

						AddComponent(prop, plugin, ref row, grid, iud);
						break;

					default:
						return DependencyProperty.UnsetValue;
				}
			}

			return grid;
		}

		private void AddComponent(DynamicSettingValue prop, IRemotePlugin plug, ref int row, Grid grid, FrameworkElement comp, bool addLabel = true, FrameworkElement secondElem = null)
		{
			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

			if (addLabel)
			{
				var label = new TextBlock { Text = prop.Description + ":" };
				label.Margin = new Thickness(2);
				label.VerticalAlignment = VerticalAlignment.Center;
				Grid.SetRow(label, row);
				Grid.SetColumn(label, 0);
				grid.Children.Add(label);
			}

			comp.Margin = new Thickness(2);
			comp.VerticalAlignment = VerticalAlignment.Center;

			Grid.SetRow(comp, row);
			Grid.SetColumn(comp, 1);

			if (secondElem == null)
			{
				Grid.SetColumnSpan(comp, 2);
			}
			else
			{
				secondElem.Margin = new Thickness(2);
				Grid.SetRow(secondElem, row);
				Grid.SetColumn(secondElem, 2);
				grid.Children.Add(secondElem);
			}
			grid.Children.Add(comp);

			if (prop.HelpID != null)
			{
				var hlp = new PHelpBtn();
				hlp.Margin = new Thickness(2, 0, 2, 0);
				hlp.VerticalAlignment = VerticalAlignment.Center;
				hlp.HelpProperty = plug.GetUniqueID().ToString("B") + "::" + prop.HelpID;
				Grid.SetRow(hlp, row);
				Grid.SetColumn(hlp, 3);
				grid.Children.Add(hlp);
			}

			row++;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
