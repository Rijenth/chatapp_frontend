using Avalonia.Controls;

namespace DCDesktop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        MinWidth = 600;
        MinHeight = 400;
        Width = 800;
        Height = 600;
    }
}