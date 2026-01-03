using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class ActivitiesViewModel : ObservableObject
{


    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private string _title = "RUBRIK 1";

    [RelayCommand]
    
    private void GoToAddActivity()
    {
        var MainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        MainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddActivityViewModel>();
    }


    
    public ActivitiesViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

    }
}
