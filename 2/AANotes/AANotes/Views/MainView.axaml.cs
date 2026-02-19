using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Generic;
using System.Linq;
namespace AANotes.Views;

public partial class MainView : UserControl
{
    private readonly MainWindow _mainWindow;
    public MainView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        LoadNotes();
    }
    private void OnOpenNote(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _mainWindow.OpenEditor();
    }
    private void OnNew(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _mainWindow.indexBDNotes = -1;
        _mainWindow.indexListNotes = -1;
        _mainWindow.OpenEditor();
    }

    private void LoadNotes()
    {
        NotesContainer.Children.Clear();

        if (_mainWindow.notesList.Count == 0)
        {
            NotesContainer.ItemWidth = double.NaN; NotesContainer.ItemHeight = double.NaN;
            NotesContainer.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
            NotesContainer.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            NotesContainer.Children.Add(new TextBlock
            {
                Text = "Нету заметок. Нажмите на 'Новая' для создания новой заметки!",
                FontSize = 16, Foreground = new SolidColorBrush(Color.Parse("#777777")),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            });
            return;
        }

        foreach (var note in _mainWindow.notesList)
        {
            var tile = new Border
            {
                Width = 170, Height = 170,
                Margin = new(10), CornerRadius = new(10), Padding = new(10),
                Background = new SolidColorBrush(Color.Parse("#252525")),
                Child = new StackPanel
                {
                    Spacing = 5,
                    Children =
                    {
                        new TextBlock
                        {
                            Text = note.Title, FontSize = 18,
                            Foreground = new SolidColorBrush(Color.Parse("#ffffff")),
                            TextWrapping = TextWrapping.Wrap,
                            FontWeight = FontWeight.Bold
                        },
                        new TextBlock
                        {
                            Text = ShortenTextByLines(note.Text, 5, 15), FontSize = 16,
                            Foreground = new SolidColorBrush(Color.Parse("#888888")),
                            TextWrapping = TextWrapping.Wrap
                        }
                    }
                }
            };
            tile.PointerPressed += (_, _) =>
            {
                _mainWindow.indexBDNotes = note.Id;
                _mainWindow.indexListNotes = _mainWindow.notesList.FindIndex(n => n.Id == note.Id);
                _mainWindow.OpenEditor();
            };
            NotesContainer.Children.Add(tile);
        }
    }
    private static string ShortenTextByLines(string text, int maxLines, int charsPerLine)
    {
        if (string.IsNullOrWhiteSpace(text)) return "";

        text = text.Replace("\r\n", "\n");
        var lines = new List<string>(); var currentLine = "";

        foreach (var ch in text)
        {
            if (ch == '\n')
            {
                lines.Add(currentLine.TrimStart()); currentLine = "";
                if (lines.Count >= maxLines) break;
                continue;
            }

            currentLine += ch;

            if (currentLine.Length >= charsPerLine)
            {
                lines.Add(currentLine.TrimStart()); currentLine = "";
                if (lines.Count >= maxLines) break;
            }
        }

        if (!string.IsNullOrEmpty(currentLine) && lines.Count < maxLines) lines.Add(currentLine.TrimStart());

        int totalCharsInLines = lines.Sum(l => l.Length);
        bool trimmed = lines.Count >= maxLines && text.Length > totalCharsInLines;
        var result = string.Join("\n", lines);

        return trimmed ? result + "\n..." : result;
    }
}