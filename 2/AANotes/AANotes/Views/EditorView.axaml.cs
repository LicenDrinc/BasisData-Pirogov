using AANotes.Windows;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AANotes.Views;

public partial class EditorView : UserControl
{
    private readonly MainWindow _mainWindow;
    private List<int> linksId = new();
    private List<Border> linkBorder = new();
    public EditorView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        LoadNote(); ViewLinks();
    }
    private void OnBack(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var iBDN = _mainWindow.indexListNotes; title.Text ??= ""; Editor.Text ??= "";
        if (iBDN == -1)
        {
            if (title.Text != "" || Editor.Text != "")
            {
                _mainWindow.NewNote(title.Text, Editor.Text); _mainWindow.UpdateNote();
                int maxI = 0; var MNL = _mainWindow.notesList; var links = _mainWindow.linksList;
                for (int i = 0; i < MNL.Count; i++) { if (MNL[i].Id > maxI) maxI = MNL[i].Id; }
                for (int i = 0; i < links.Count; i++) { if (links[i].IdNote == -1) _mainWindow.NewLinks(maxI, links[i].Link); }
            }
        }
        else
        {
            _mainWindow.notesList[iBDN].Title = title.Text; _mainWindow.notesList[iBDN].Text = Editor.Text;
            _mainWindow.SaveNote();
        }
        _mainWindow.UpdateNote(); _mainWindow.UpdateLinks();
        if (_mainWindow.notesJurnal.Count == 0) _mainWindow.OpenMain();
        else
        {
            var nj = _mainWindow.notesJurnal; var nl = _mainWindow.notesList;
            for (int i = 0; i < nl.Count; i++)
            {
                if (nl[i].Id == nj[^1]) { _mainWindow.indexListNotes = i; _mainWindow.indexBDNotes = nl[i].Id; break; }
            }
            nj.RemoveAt(nj.Count - 1); _mainWindow.OpenEditor();
        }
    }
    private void OnDelete(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_mainWindow.indexBDNotes != -1)
        {
            var links = _mainWindow.linksList;
            for (int i = 0; i < links.Count; i++)
            {
                if (links[i].IdNote == _mainWindow.indexBDNotes)
                {
                    _mainWindow.indexListLinks = i; _mainWindow.indexBDLinks = links[i].Id;
                    _mainWindow.DeleteLinks();
                }
            }
        }
        _mainWindow.DeleteNote(); _mainWindow.UpdateNote(); _mainWindow.UpdateLinks(); _mainWindow.OpenMain();
    }
    private void Editor_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        if ((e.KeyModifiers & KeyModifiers.Control) != 0)
        {
            Dispatcher.UIThread.Post(() =>
            {
                int caret = tb.CaretIndex; string word = GetWordAt(tb.Text ?? "", caret);

                if (!string.IsNullOrEmpty(word) && word.Length > 2 && word.StartsWith("{") && word.EndsWith("}"))
                {
                    if (int.TryParse(word[1..^1], out int idLink)) { if (!(idLink < 1 || idLink > linksId.Count)) OpenLink(linksId[idLink - 1]); }
                }
                else if (word.StartsWith("http://") || word.StartsWith("https://")) Process.Start(new ProcessStartInfo { FileName = word, UseShellExecute = true });
            });
        }
    }

    private void OnDeleteLink(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var iBDN = _mainWindow.indexBDNotes; var links = _mainWindow.linksList;
        if (iBDN != -1) { if (_mainWindow.indexListLinks >= 0) _mainWindow.DeleteLinks(); _mainWindow.UpdateLinks(); }
        else { if (_mainWindow.indexListLinks >= 0) links.RemoveAt(_mainWindow.indexListLinks); }
        linksId.Clear(); for (int i = 0; i < links.Count; i++) { if (links[i].IdNote == iBDN) linksId.Add(i); }
        ViewLinks();
    }
    private void OnNewLinkNote(object? sender, Avalonia.Interactivity.RoutedEventArgs e) { AddLinkNote(); }
    private void OnNewLinkHttp(object? sender, Avalonia.Interactivity.RoutedEventArgs e) { AddLinkHttp(); }


    private static string GetWordAt(string text, int index)
    {
        if (string.IsNullOrEmpty(text) || index >= text.Length) return "";

        int start = index; int end = index;

        while (start > 0 && !char.IsWhiteSpace(text[start - 1])) start--;
        while (end < text.Length && !char.IsWhiteSpace(text[end])) end++;

        return text[start..end];
    }
    private async void AddLinkNote()
    {
        var dialog = new AddLinkNoteWindows(_mainWindow); var result = await dialog.ShowDialog<int?>((Window)VisualRoot);
        if (result.HasValue && result.Value >= 0)
        {
            var iBDN = _mainWindow.indexBDNotes; var links = _mainWindow.linksList;
            if (iBDN == -1) links.Add(new(-1, -1, result.Value.ToString()));
            else { _mainWindow.NewLinks(_mainWindow.indexBDNotes, result.Value.ToString()); _mainWindow.UpdateLinks(); }
            linksId.Clear(); for (int i = 0; i < links.Count; i++) { if (links[i].IdNote == iBDN) linksId.Add(i); }
            ViewLinks(); Editor.Text += " {" + linksId.Count + "}";
        }
    }
    private async void AddLinkHttp()
    {
        var dialog = new AddLinkHttpWindows(); var result = await dialog.ShowDialog<string?>((Window)VisualRoot);
        if (!string.IsNullOrWhiteSpace(result))
        {
            var iBDN = _mainWindow.indexBDNotes; var links = _mainWindow.linksList;
            if (iBDN == -1) links.Add(new(-1, -1, result));
            else { _mainWindow.NewLinks(_mainWindow.indexBDNotes, result); _mainWindow.UpdateLinks(); }
            linksId.Clear(); for (int i = 0; i < links.Count; i++) { if (links[i].IdNote == iBDN) linksId.Add(i); }
            ViewLinks(); Editor.Text += " {" + linksId.Count + "}";
        }
    }
    private void OpenLink(int i)
    {
        if (i < 0 || i > _mainWindow.linksList.Count) return;
        var link = _mainWindow.linksList[i];
        if (int.TryParse(link.Link, out int idNote))
        {
            var note = _mainWindow.notesList;
            for (int j = 0; j < note.Count; j++) 
            { 
                if (note[j].Id == idNote)
                {
                    int maxI = 0; var iBDN = _mainWindow.indexListNotes; title.Text ??= ""; Editor.Text ??= "";
                    if (iBDN == -1)
                    {
                        _mainWindow.NewNote(title.Text, Editor.Text); _mainWindow.UpdateNote();
                        var MNL = _mainWindow.notesList; var links = _mainWindow.linksList;
                        for (int t = 0; t < MNL.Count; t++) { if (MNL[t].Id > maxI) maxI = MNL[t].Id; }
                        for (int t = 0; t < links.Count; t++) { if (links[t].IdNote == -1) _mainWindow.NewLinks(maxI, links[t].Link); }
                    }
                    else
                    {
                        _mainWindow.notesList[iBDN].Title = title.Text; _mainWindow.notesList[iBDN].Text = Editor.Text;
                        maxI = _mainWindow.notesList[iBDN].Id; _mainWindow.SaveNote();
                    }
                    _mainWindow.UpdateNote(); _mainWindow.UpdateLinks();
                    _mainWindow.notesJurnal.Add(maxI);
                    _mainWindow.indexListNotes = j; _mainWindow.indexBDNotes = note[j].Id;
                    _mainWindow.OpenEditor();
                }
            }
        }
        else Process.Start(new ProcessStartInfo { FileName = link.Link.ToString(), UseShellExecute = true });
    }
    private void LoadNote()
    {
        var iBDN = _mainWindow.indexListNotes;
        if (iBDN == -1) { title.Text = string.Empty; Editor.Text = string.Empty; }
        else
        {
            title.Text = _mainWindow.notesList[iBDN].Title; Editor.Text = _mainWindow.notesList[iBDN].Text;
            var links = _mainWindow.linksList;
            for (int i = 0; i < links.Count; i++) { if (links[i].IdNote == _mainWindow.indexBDNotes) linksId.Add(i); }
        }
    }
    private void ViewLinks()
    {
        LinksContainer.Children.Clear();
        linkBorder.Clear();

        if (linksId.Count == 0)
        {
            LinksContainer.ItemWidth = double.NaN; LinksContainer.ItemHeight = double.NaN;
            LinksContainer.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
            LinksContainer.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            return;
        }
        
        LinksContainer.ItemWidth = double.NaN; LinksContainer.ItemHeight = double.NaN;
        LinksContainer.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
        LinksContainer.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

        for (int i = 0; i < linksId.Count; i++)
        {
            var ind = i;

            var mainTextBlock = new TextBlock
            {
                Text = $"{i + 1}. {ShortenTextByLines(LinkText(linksId[i]), 1, 20)}", FontSize = 12,
                Foreground = new SolidColorBrush(Color.Parse("#ffffff")),
                TextWrapping = TextWrapping.Wrap
            };

            var linkPanel = new Border
            {
                Margin = new(5, 0, 5, 5), CornerRadius = new(5), Padding = new(5),
                Background = new SolidColorBrush(Color.Parse("#252525")),
                Child = mainTextBlock
            };

            linkPanel.PointerPressed += (s, e) =>
            {
                if (e.ClickCount == 1)
                {
                    LinkBorderDefault();
                    linkPanel.Background = Brushes.DarkSlateGray;
                    _mainWindow.indexListLinks = linksId[ind];
                    _mainWindow.indexBDLinks = _mainWindow.linksList[linksId[ind]].Id;
                }

                if (e.ClickCount == 2) OpenLink(linksId[ind]);
            };
            LinksContainer.Children.Add(linkPanel);
            linkBorder.Add(linkPanel);
        }
    }
    private void LinkBorderDefault() { for (int i = 0; i < linkBorder.Count; i++) linkBorder[i].Background = new SolidColorBrush(Color.Parse("#252525")); }
    private string LinkText(int i)
    {
        var link = _mainWindow.linksList[i]; var note = _mainWindow.notesList;
        if (int.TryParse(link.Link, out int idNote)) { for (int j = 0; j < note.Count; j++) { if (note[j].Id == idNote) return note[j].Title; } }
        else return link.Link;

        return "ERROR 404";
    }
    private static string ShortenTextByLines(string text, int maxLines, int charsPerLine, string plus = "")
    {
        if (string.IsNullOrWhiteSpace(text)) return "";

        text = text.Replace("\r\n", "\n");
        var lines = new List<string>(); var currentLine = "";

        foreach (var ch in text)
        {
            if (ch == '\n')
            {
                if (lines.Count > maxLines + 1) break;
                lines.Add(currentLine.TrimStart()); currentLine = ""; continue;
            }
            currentLine += ch;
            if (currentLine.Length >= charsPerLine)
            {
                if (lines.Count > maxLines + 1) break;
                lines.Add(currentLine.TrimStart()); currentLine = "";
            }
        }
        if (!string.IsNullOrEmpty(currentLine)) lines.Add(currentLine.TrimStart());

        var linesToShow = lines.Count > maxLines ? [.. lines.Take(maxLines)] : lines;
        int totalCharsInLines = linesToShow.Sum(l => l.Length);
        bool trimmed = lines.Count > maxLines && text.Length > totalCharsInLines;
        var result = string.Join("\n", linesToShow);

        return trimmed ? result + plus + "..." : result;
    }

}
