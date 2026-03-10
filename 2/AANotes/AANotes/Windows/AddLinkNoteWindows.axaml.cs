using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AANotes.Windows;

public partial class AddLinkNoteWindows : Window
{
    public int? Link { get; private set; }
    private readonly MainWindow _mainWindow;
    public AddLinkNoteWindows(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        LoadNote();
    }
    private void OnOk(object? sender, RoutedEventArgs e)
    {
        Link = (int)((ComboBoxItem)NotesCB.SelectedItem).Tag;
        Close(Link);
    }
    private void OnCancel(object? sender, RoutedEventArgs e)
    {
        Close(null);
    }

    private void LoadNote()
    {
        for (int i = 0; i < _mainWindow.notesList.Count; i++)
        {
            var note = new ComboBoxItem { Tag = i, Content = _mainWindow.notesList[i].Title };
            NotesCB.Items.Add(note);
        }
    }
}