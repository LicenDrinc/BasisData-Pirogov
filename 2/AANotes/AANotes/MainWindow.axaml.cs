using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System.ComponentModel;
using System.IO;

namespace AANotes
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /*
        private async void OpenFile(object? sender, RoutedEventArgs e)
        {
            var files = await this.StorageProvider.OpenFilePickerAsync(
                new FilePickerOpenOptions
                {
                    Title = "Открыть файл",
                    AllowMultiple = false
                });

            if (files.Count > 0)
            {
                await using var stream = await files[0].OpenReadAsync();
                using var reader = new StreamReader(stream);
                Editor.Text = await reader.ReadToEndAsync();
            }
        }

        private async void SaveFile(object? sender, RoutedEventArgs e)
        {
            var file = await this.StorageProvider.SaveFilePickerAsync(
                new FilePickerSaveOptions
                {
                    Title = "Сохранить файл"
                });

            if (file != null)
            {
                await using var stream = await file.OpenWriteAsync();
                using var writer = new StreamWriter(stream);
                await writer.WriteAsync(Editor.Text);
            }
        }

        public void OnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Title = "дададададада";
        }
        */
    }
}