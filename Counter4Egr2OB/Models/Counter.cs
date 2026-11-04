using System.ComponentModel;

namespace Counter4Egr2OB.Models;

public class Counter : INotifyPropertyChanged
{
    public string FileName { get; set; }
    public string Name { get; set; }

    private int _value;
    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void Save() =>
        File.WriteAllLines(FileName, new[] { Name, Value.ToString() });

    public static Counter Load(string fileName)
    {
        string[] lines = File.ReadAllLines(fileName);
        return new Counter
        {
            FileName = fileName,
            Name = lines.Length > 0 ? lines[0] : "",
            Value = lines.Length > 1 && int.TryParse(lines[1], out int v) ? v : 0
        };
    }
}