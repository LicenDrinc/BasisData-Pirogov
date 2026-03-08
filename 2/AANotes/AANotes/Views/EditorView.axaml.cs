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
        title.Text ??= ""; Editor.Text ??= "";

        if (_mainWindow.indexBDNotes == -1)
        {
            if (title.Text != "" || Editor.Text != "") _mainWindow.NewNote(title.Text, Editor.Text);
        }
        else
        {
            _mainWindow.notesList[_mainWindow.indexListNotes].Title = title.Text;
            _mainWindow.notesList[_mainWindow.indexListNotes].Text = Editor.Text;
            _mainWindow.SaveNote();
        }
        _mainWindow.ObdateNote();
        _mainWindow.OpenMain();
    }
    private void OnDelete(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _mainWindow.DeleteNote();
        _mainWindow.ObdateNote();
        _mainWindow.OpenMain();
    }

    private void LoadNote()
    {
        if (_mainWindow.indexBDNotes == -1) { title.Text = string.Empty; Editor.Text = string.Empty; }
        else
        {
            title.Text = _mainWindow.notesList[_mainWindow.indexListNotes].Title;
            Editor.Text = _mainWindow.notesList[_mainWindow.indexListNotes].Text;
        }
    }



}
