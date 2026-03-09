using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Generic;
using System.Linq;

namespace AANotes.Views;

public partial class EditorView : UserControl
{
    private readonly MainWindow _mainWindow;
    private List<int> linksId = new();
    public EditorView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        LoadNote(); ViewLinks();
    }
    private void OnBack(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var iBDN = _mainWindow.indexBDNotes; title.Text ??= ""; Editor.Text ??= "";
        if (iBDN == -1) { if (title.Text != "" || Editor.Text != "") _mainWindow.NewNote(title.Text, Editor.Text); }
        else
        {
            _mainWindow.notesList[iBDN].Title = title.Text; _mainWindow.notesList[iBDN].Text = Editor.Text;
            _mainWindow.SaveNote();
        }
        _mainWindow.ObdateNote(); _mainWindow.OpenMain();
    }
    private void OnDelete(object? sender, Avalonia.Interactivity.RoutedEventArgs e) { _mainWindow.DeleteNote(); _mainWindow.ObdateNote(); _mainWindow.OpenMain(); }
    private void OnDeleteLink(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }
    private void OnNewLinkNote(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }
    private void OnNewLinkHttp(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }


    private void OpenLink(int i)
    {
        var link = _mainWindow.linksList[i];
        if (int.TryParse(link.Link, out int idNote))
        {
            var note = _mainWindow.notesList;
            for (int j = 0; j < note.Count; j++) 
            { 
                if (note[j].Id == idNote) 
                {
                    
                }
            }
        }
        else
        {

        }
    }
    private void LoadNote()
    {
        var iBDN = _mainWindow.indexBDNotes;
        if (iBDN == -1) { title.Text = string.Empty; Editor.Text = string.Empty; }
        else
        {
            title.Text = _mainWindow.notesList[iBDN].Title; Editor.Text = _mainWindow.notesList[iBDN].Text;
            var links = _mainWindow.linksList;
            for (int i = 0; i < links.Count; i++) { if (links[i].IdNote == iBDN) linksId.Add(i); }
        }
    }
    private void ViewLinks()
    {
        LinksContainer.Children.Clear();
        
        if (linksId.Count == 0)
        {
            LinksContainer.ItemWidth = double.NaN; LinksContainer.ItemHeight = double.NaN;
            LinksContainer.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
            LinksContainer.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            LinksContainer.Children.Add(new TextBlock
            {
                Text = "нет ссылок", FontSize = 12,
                Foreground = new SolidColorBrush(Color.Parse("#777777")),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            });
            return;
        }
        
        LinksContainer.ItemWidth = double.NaN; LinksContainer.ItemHeight = double.NaN;
        LinksContainer.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
        LinksContainer.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

        for (int i = 0; i < linksId.Count; i++)
        {
            var mainTextBlock = new TextBlock
            {
                Text = $"{i + 1}.{ShortenTextByLines(LinkText(linksId[i]), 1, 20)}", FontSize = 12,
                Foreground = new SolidColorBrush(Color.Parse("#ffffff")),
                TextWrapping = TextWrapping.Wrap
            };

            var linkPanel = new Border
            {
                //Width = 170, Height = 170,
                Margin = new(5, 0, 5, 5), CornerRadius = new(5), Padding = new(5),
                Background = new SolidColorBrush(Color.Parse("#252525")),
                Child = mainTextBlock
            };

            linkPanel.PointerPressed += (s, e) =>
            {
                if (e.ClickCount == 1)
                {
                    linkPanel.Background = Brushes.DarkSlateGray;
                    _mainWindow.indexListLinks = linksId[i];
                    _mainWindow.indexBDLinks = _mainWindow.linksList[linksId[i]].Id;
                }

                if (e.ClickCount == 2) OpenLink(linksId[i]);
            };
            LinksContainer.Children.Add(linkPanel);
        }
    }
    private string LinkText(int i)
    {
        var link = _mainWindow.linksList[i];
        if (int.TryParse(link.Link, out int idNote))
        {
            var note = _mainWindow.notesList;
            for (int j = 0; j < note.Count; j++) { if (note[j].Id == idNote) return note[j].Title; }
        }
        else return link.Link;

        return "ERROR 404";
    }
    private static string ShortenTextByLines(string text, int maxLines, int charsPerLine, string pusl = "")
    {
        if (string.IsNullOrWhiteSpace(text)) return "";

        text = text.Replace("\r\n", "\n");
        var lines = new List<string>(); var currentLine = "";

        foreach (var ch in text)
        {
            if (ch == '\n')
            {
                if (lines.Count > maxLines + 1) break;
                lines.Add(currentLine.TrimStart()); currentLine = "";
                continue;
            }
            currentLine += ch;
            if (currentLine.Length >= charsPerLine)
            {
                if (lines.Count > maxLines + 1) break;
                lines.Add(currentLine.TrimStart()); currentLine = "";
            }
        }
        if (!string.IsNullOrEmpty(currentLine)) lines.Add(currentLine.TrimStart());

        var linesToShow = lines.Count > maxLines ? [.. lines.Take(maxLines - 1)] : lines;
        int totalCharsInLines = linesToShow.Sum(l => l.Length);
        bool trimmed = lines.Count > maxLines && text.Length > totalCharsInLines;
        var result = string.Join("\n", linesToShow);

        return trimmed ? result + pusl + "..." : result;
    }

}
