using Avalonia.Controls;
namespace AANotes.Views;

public partial class EditorView : UserControl
{
    private readonly MainWindow _mainWindow;
    public EditorView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        LoadNote();
    }
    private void OnBack(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        title.Text ??= "";
        text.Text ??= "";

        if (_mainWindow.indexBDNotes == -1)
        {
            if (title.Text != "" || text.Text != "") 
                _mainWindow.NewNote(title.Text, text.Text);
        }
        else
        {
            _mainWindow.notesList[_mainWindow.indexListNotes].Title = title.Text;
            _mainWindow.notesList[_mainWindow.indexListNotes].Text = text.Text;
            _mainWindow.SaveBD();
        }
        _mainWindow.ObdateBD();
        _mainWindow.OpenMain();
    }
    private void OnDelete(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _mainWindow.DeleteNote();
        _mainWindow.ObdateBD();
        _mainWindow.OpenMain();
    }

    private void LoadNote()
    {
        if (_mainWindow.indexBDNotes == -1)
        {
            title.Text = string.Empty;
            text.Text = string.Empty;
        }
        else
        {
            title.Text = _mainWindow.notesList[_mainWindow.indexListNotes].Title;
            text.Text = _mainWindow.notesList[_mainWindow.indexListNotes].Text;
        }
    }
}
