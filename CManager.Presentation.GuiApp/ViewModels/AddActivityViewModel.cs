
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class AddActivityViewModel : ObservableObject
{

    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private string _title = "Create new customer";


    [RelayCommand]

    private void GoToActivities()
    {
        var MainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        MainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ActivitiesViewModel>();
    }



    public AddActivityViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

    }
}
