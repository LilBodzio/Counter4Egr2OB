using Counter4Egr2OB.Models;

namespace Counter4Egr2OB.Views;

public partial class AddCounter : ContentPage
{
    public AddCounter()
    {
        InitializeComponent();
    }

    private async void CreateCounter(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CounterName.Text))
        {
            await DisplayAlert("Invalid counter", "Enter a counter name.", "OK");
            return;
        }

        int startValue = 0;
        if (!string.IsNullOrWhiteSpace(StartValue.Text) &&
            !int.TryParse(StartValue.Text, out startValue))
        {
            await DisplayAlert("Invalid value", "Starting value must be a whole number.", "OK");
            return;
        }

        string fileName = Path.Combine(
            FileSystem.AppDataDirectory,
            $"{DateTime.UtcNow.Ticks}.counter.txt");

        new Counter { FileName = fileName, Name = CounterName.Text.Trim(), Value = startValue }.Save();

        await Shell.Current.GoToAsync("..");
    }
}