﻿using Logic.Interfaces;

namespace Logic.ViewModels;

public class CreateNewBankViewModel : ContentPageViewModelAbstract
{
    public CreateNewBankViewModel()
    {
        PageTitle = "Create New Bank";
    }

    public override string PageTitle { get; }
}