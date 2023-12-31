﻿using System.Windows;
using System.Windows.Controls;
using Logic.ViewModels.Pages;

namespace OverloadBankEditor.Views;

public partial class SettingsView : Page
{
    public SettingsView()
    {
        InitializeComponent();
    }

    private string SelectWorkingDirectory()
    {
        using var dialog = new FolderBrowserDialog();
        var result = dialog.ShowDialog();
        return result != DialogResult.OK ? string.Empty : dialog.SelectedPath;
    }

    private void SelectFolderView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is not SettingsPageViewModel viewModel) return;
        viewModel.SetupFolderSelection(SelectWorkingDirectory);
    }
}