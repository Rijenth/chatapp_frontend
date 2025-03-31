using Avalonia.Controls;

namespace DCDesktop;

public partial class MainWindow : Window
{
    
    public string Greeting => "Welcome to Avalonia! This is my added text.";
    
    public MainWindow()
    {
        InitializeComponent();
    }
}