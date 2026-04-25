using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Threading.Tasks;

namespace AANotes.Windows;

public partial class AddDateBaseWindows : Window
{
    private readonly MainWindow _mainWindow;
    private string path, name, host, port, username, password;

    public AddDateBaseWindows(MainWindow mainWindow)
    {
        InitializeComponent(); _mainWindow = mainWindow;
    }

    private void UpdateParametrs(object? sender, SelectionChangedEventArgs e)
    {
        if (BDType.SelectedItem is ComboBoxItem item)
        {
            var tag = item.Tag?.ToString(); Console.WriteLine($"Выбрано: {item.Content}, Tag: {tag}");
            switch (tag)
            {
                case "1": DBParametrs.Children.Clear(); break;
                case "2": DBParametrs.Children.Clear();
                    var titleBlock1 = new TextBlock() { Foreground = new SolidColorBrush(Color.Parse("#ffffff")), TextWrapping = TextWrapping.Wrap, Text = "Название" };
                    var titleBlock2 = new TextBlock() { Foreground = new SolidColorBrush(Color.Parse("#ffffff")), TextWrapping = TextWrapping.Wrap, Text = "Хост" };
                    var titleBlock3 = new TextBlock() { Foreground = new SolidColorBrush(Color.Parse("#ffffff")), TextWrapping = TextWrapping.Wrap, Text = "Прот" };
                    var titleBlock4 = new TextBlock() { Foreground = new SolidColorBrush(Color.Parse("#ffffff")), TextWrapping = TextWrapping.Wrap, Text = "Имя пользователя" };
                    var titleBlock5 = new TextBlock() { Foreground = new SolidColorBrush(Color.Parse("#ffffff")), TextWrapping = TextWrapping.Wrap, Text = "Пароль" };
                    var TextBox1 = new TextBox() { Watermark = "NotesLD" };   TextBox1.TextChanged += (_, _) => { name = TextBox1.Text; };
                    var TextBox2 = new TextBox() { Watermark = "localhost" }; TextBox2.TextChanged += (_, _) => { host = TextBox2.Text; };
                    var TextBox3 = new TextBox() { Watermark = "5432" };      TextBox3.TextChanged += (_, _) => { port = TextBox3.Text; };
                    var TextBox4 = new TextBox() { Watermark = "postgres" };  TextBox4.TextChanged += (_, _) => { username = TextBox4.Text; };
                    var TextBox5 = new TextBox() { Watermark = "123" };       TextBox5.TextChanged += (_, _) => { password = TextBox5.Text; };

                    DBParametrs.Children.Add(titleBlock1); DBParametrs.Children.Add(TextBox1); DBParametrs.Children.Add(titleBlock2); DBParametrs.Children.Add(TextBox2);
                    DBParametrs.Children.Add(titleBlock3); DBParametrs.Children.Add(TextBox3); DBParametrs.Children.Add(titleBlock4); DBParametrs.Children.Add(TextBox4);
                    DBParametrs.Children.Add(titleBlock5); DBParametrs.Children.Add(TextBox5);
                    break;
                case "3": DBParametrs.Children.Clear();
                    var titleBlock = new TextBlock() { Text = "Не выбран путь", Foreground = new SolidColorBrush(Color.Parse("#888888")), TextWrapping = TextWrapping.Wrap };
                    var button = new Button() { Content = "Открыть" }; button.Click += async (_, _) => { path = await _mainWindow.OpenFile(); titleBlock.Text = path; };
                    DBParametrs.Children.Add(titleBlock); DBParametrs.Children.Add(button);
                    break;
            }
        }
    }
    private async Task OnOkAsync(object? sender, RoutedEventArgs e)
    {
        if (BDType.SelectedItem is ComboBoxItem item)
        {
            var tag = item.Tag?.ToString(); Console.WriteLine($"Выбрано: {item.Content}, Tag: {tag}");
            switch (tag)
            {
                case "1": break;
                case "2": 
                    break;
                case "3": while (path == null) { path = await _mainWindow.OpenFile(); } if (path != null) _mainWindow.LoadFromFile(path); break;
            }
        }
        Close(null);
    }

}
