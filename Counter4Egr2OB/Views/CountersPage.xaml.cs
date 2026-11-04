using System.Collections.ObjectModel;
using Counter4Egr2OB.Models;

namespace Counter4Egr2OB.Views;

public partial class CountersPage : ContentPage
{
    private readonly ObservableCollection<Counter> _counters = new();

    public CountersPage()
    {
        InitializeComponent();
        counterCollection.ItemsSource = _counters;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadCounters();
    }

    private void LoadCounters()
    {
        _counters.Clear();

        var files = Directory
            .EnumerateFiles(FileSystem.AppDataDirectory, "*.counter.txt")
            .OrderBy(f => f);

        foreach (string file in files)
            _counters.Add(Counter.Load(file));
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddCounter));
    }

    private void OnMinusClicked(object sender, EventArgs e)
    {
        if (((Button)sender).BindingContext is Counter counter)
        {
            counter.Value--;
            counter.Save();
        }
    }

    private void OnPlusClicked(object sender, EventArgs e)
    {
        if (((Button)sender).BindingContext is Counter counter)
        {
            counter.Value++;
            counter.Save();
        }
    }
}