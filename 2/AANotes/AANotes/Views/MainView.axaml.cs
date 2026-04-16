using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using static AANotes.MainWindow;

namespace AANotes.Views;

public partial class MainView : UserControl
{
    private readonly MainWindow _mainWindow;
    private string search;
    public List<BDNotes> notesListSearch = [];
    public MainView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        search = ""; LoadNotes(search);
    }
    private void OnOpenNote(object? sender, Avalonia.Interactivity.RoutedEventArgs e) { _mainWindow.OpenEditor(); }
    private void OnNew(object? sender, Avalonia.Interactivity.RoutedEventArgs e) { _mainWindow.indexBDNotes = -1; _mainWindow.indexListNotes = -1; _mainWindow.OpenEditor(); }
    private async void OnLoadAsync(object? sender, Avalonia.Interactivity.RoutedEventArgs e) { var Path = await _mainWindow.OpenFile(); if (Path != null) _mainWindow.LoadFromFile(Path); }
    private async void OnSaveAsync(object? sender, Avalonia.Interactivity.RoutedEventArgs e) { var Path = await _mainWindow.SaveFile(); if (Path != null) _mainWindow.SaveToFile(Path); }
    private void OnSearch(object? sender, TextChangedEventArgs e) { search = Search.Text ?? ""; LoadNotes(search); }

    private static bool Matches(string source, string search)
    {
        if (string.IsNullOrWhiteSpace(search)) return true;
        source = source.ToLower(); var parts = search.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.All(p => source.Contains(p));
    }
    private void LoadNotes(string s)
    {
        NotesContainer.Children.Clear();

        if (!string.IsNullOrWhiteSpace(s)) notesListSearch = [.. _mainWindow.notesList.Where(n => Matches($"{n.Title} {n.Text}", s))];
        else notesListSearch = _mainWindow.notesList;

        if (notesListSearch.Count == 0)
        {
            NotesContainer.ItemWidth = double.NaN; NotesContainer.ItemHeight = double.NaN;
            NotesContainer.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
            NotesContainer.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            NotesContainer.Children.Add(new TextBlock
            {
                Text = s != "" ? $"Ненайденный заметки по запросу \"{s}\"!" : "Нету заметок. Нажмите на 'Новая' для создания новой заметки!",
                FontSize = 16,
                Foreground = new SolidColorBrush(Color.Parse("#777777")),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            });
            return;
        }

        NotesContainer.ItemWidth = 185; NotesContainer.ItemHeight = 185;
        NotesContainer.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
        NotesContainer.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

        foreach (var note in notesListSearch)
        {
            var grid = new Grid { RowDefinitions = [ new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Star), new RowDefinition(GridLength.Auto) ] };

            var titleBlock = new TextBlock
            { Text = note.Title == "" ? "Нет заголовка" : note.Title, FontSize = 18, Foreground = new SolidColorBrush(Color.Parse("#ffffff")), TextWrapping = TextWrapping.Wrap, FontWeight = FontWeight.Bold };
            var mainTextBlock = new TextBlock
            { Text = note.Text == "" ? "Нет текста" : ShortenTextByLines(note.Text, 5, 15), FontSize = 16, Foreground = new SolidColorBrush(Color.Parse("#888888")), TextWrapping = TextWrapping.Wrap };
            var timeBlock = new TextBlock { Text = ToHumanTime(note.TimeEditor), FontSize = 12, Foreground = new SolidColorBrush(Color.Parse("#666666")), TextWrapping = TextWrapping.Wrap };

            Grid.SetRow(titleBlock, 0); Grid.SetRow(mainTextBlock, 1); Grid.SetRow(timeBlock, 2); grid.Children.Add(titleBlock); grid.Children.Add(mainTextBlock); grid.Children.Add(timeBlock);

            var tile = new Border { Width = 170, Height = 170, Margin = new(10), CornerRadius = new(10), Padding = new(10), Background = new SolidColorBrush(Color.Parse("#252525")), Child = grid };
            tile.PointerPressed += (_, _) => { _mainWindow.indexBDNotes = note.Id; _mainWindow.indexListNotes = _mainWindow.notesList.FindIndex(n => n.Id == note.Id); _mainWindow.OpenEditor(); };
            NotesContainer.Children.Add(tile);
        }
    }
    private static string ShortenTextByLines(string text, int maxLines, int charsPerLine)
    {
        if (string.IsNullOrWhiteSpace(text)) return "";
        text = text.Replace("\r\n", "\n"); var lines = new List<string>(); var currentLine = "";
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

        var linesToShow = lines.Count > maxLines ? [.. lines.Take(maxLines - 1)] : lines;
        int totalCharsInLines = linesToShow.Sum(l => l.Length);
        bool trimmed = lines.Count > maxLines && text.Length > totalCharsInLines;
        var result = string.Join("\n", linesToShow);

        return trimmed ? result + "\n..." : result;
    }

    private static string ToHumanTime(DateTime dt)
    {
        var span = DateTime.Now - dt;

        if (span.TotalMinutes < 1) return "только что";
        if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes} мин назад";
        if (span.TotalHours < 24) return $"{(int)span.TotalHours} ч назад";
        if (span.TotalDays < 7) return $"{(int)span.TotalDays} д назад";

        return dt.ToString("dd.MM.yyyy");
    }
}