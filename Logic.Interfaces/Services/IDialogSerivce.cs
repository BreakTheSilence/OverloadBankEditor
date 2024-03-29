﻿namespace Logic.Interfaces.Services;

public interface IDialogService
{
    string ShowTextInputDialog(string dialogTitle);
    void ShowOkDialog(string dialogTitle, string dialogContent);
    bool ShowYesNoDialog(string dialogQuestion, string dialogDescription);
    string ShowFilePickDialog();
    string ShowFolderSelectDialog();
}