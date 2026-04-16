using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AANotes.Windows;

public partial class AddLinkHttpWindows : Window
{
    public string? Link { get; private set; }

    public AddLinkHttpWindows() { InitializeComponent(); }
    private void OnOk(object? sender, RoutedEventArgs e) { Link = LinkBox.Text; Close(Link); }
    private void OnCancel(object? sender, RoutedEventArgs e) { Close(null); }
}