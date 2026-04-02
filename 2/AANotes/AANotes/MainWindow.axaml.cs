using AANotes.Views;
using Avalonia.Controls;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AANotes
{
    public partial class MainWindow : Window
    {
        private static readonly string targetDbName = "NotesLD";
        private static readonly string adminCs1 = "Host=localhost;Username=postgres;Password=123;Database=postgres";
        private static readonly string adminCs = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=postgres";
        private static readonly string cs = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=NotesLD";
        private static readonly string[] sqlCreate = [
            "note (id SERIAL PRIMARY KEY, title TEXT, text TEXT, time_editor TIMESTAMP NOT NULL DEFAULT now());",
            "links_in_note (id SERIAL PRIMARY KEY, id_note INT, link_out TEXT)"
            ];
        public NpgsqlConnection conn = new();
        public List<BDNotes> notesList = new(); public List<BDLinks> linksList = new();
        public List<int> notesJurnal = new();   public List<int> notesSort = new();
        public int indexBDNotes = 0; public int indexListNotes = 0;
        public int indexBDLinks = 0; public int indexListLinks = 0;

        public MainWindow()
        {
            InitializeComponent();
            OpenBD();
            notesJurnal.Clear();
            OpenMain();
        }
        public void OpenEditor() => MainContent.Content = new EditorView(this);
        public void OpenMain() => MainContent.Content = new MainView(this);
        public void OpenBD()
        {
            EnsureDatabaseExists();
            conn = OpenMainDatabase(cs);
            EnsureTables(conn);
            UpdateNote(); UpdateLinks();
        }
        private static void EnsureDatabaseExists()
        {
            using var conn = new NpgsqlConnection(adminCs); conn.Open();

            using var checkCmd = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @name", conn);
            checkCmd.Parameters.AddWithValue("name", targetDbName);

            var exists = checkCmd.ExecuteScalar() != null;

            if (!exists)
            {
                using var createCmd = new NpgsqlCommand($"CREATE DATABASE \"{targetDbName}\"", conn);
                createCmd.ExecuteNonQuery();
            }
        }
        private static NpgsqlConnection OpenMainDatabase(string cs) { var conn = new NpgsqlConnection(cs); conn.Open(); return conn; }
        private static void EnsureTables(NpgsqlConnection conn) { for (int i = 0; i < sqlCreate.Length; i++) EnsureTables(conn, i); }
        private static void EnsureTables(NpgsqlConnection conn, int i) { using var cmd = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS " + sqlCreate[i], conn); cmd.ExecuteNonQuery(); }
        private static void EnsureColumnExists(NpgsqlConnection conn, string table, string column, string typeAndOptions)
        {
            var sql = $"ALTER TABLE {table} ADD COLUMN IF NOT EXISTS {column} {typeAndOptions};";
            using var cmd = new NpgsqlCommand(sql, conn); cmd.ExecuteNonQuery();
        }
        public bool TableExists(string tableName)
        {
            string sql = "SELECT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = @name)";
            using var cmd = new NpgsqlCommand(sql, conn); cmd.Parameters.AddWithValue("@name", tableName);
            return (bool)cmd.ExecuteScalar();
        }
        public void SaveToFile(string path)
        {
            var json = JsonSerializer.Serialize(new BDBackup(notesList, linksList), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
        public void LoadFromFile(string path)
        {
            if (!File.Exists(path)) return;
            var json = File.ReadAllText(path); var db = JsonSerializer.Deserialize<BDBackup>(json) ?? new BDBackup();

            notesList = db.Notes; linksList = db.Links; DropDatabase(); OpenBD();
            for (int i = 0; i < notesList.Count; i++) NewNote(notesList[i].Id, notesList[i].Title, notesList[i].Text, notesList[i].TimeEditor);
            for (int i = 0; i < linksList.Count; i++) NewLinks(linksList[i].Id, linksList[i].IdNote, linksList[i].Link);
            UpdateNote(); UpdateLinks(); notesJurnal.Clear(); OpenMain();
        }
        public static void DropDatabase()
        {
            using var conn = new NpgsqlConnection(adminCs1); conn.Open();
            //var sql = $"SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '{targetDbName}' AND pid <> pg_backend_pid(); DROP DATABASE IF EXISTS {targetDbName};";
            var sql = $"DROP DATABASE IF EXISTS {targetDbName};";
            using var cmd = new NpgsqlCommand(sql, conn); cmd.ExecuteNonQuery();
        }
        public async Task<string?> OpenFile()
        {
            var dialog = new OpenFileDialog { Title = "Выбери файл", AllowMultiple = false, Filters = { new FileDialogFilter { Name = "JSON", Extensions = { "json" } } } };
            var result = await dialog.ShowAsync((Window)VisualRoot); return result?.FirstOrDefault();
        }
        public async Task<string?> SaveFile()
        {
            var dialog = new SaveFileDialog { Title = "Сохранить файл", DefaultExtension = "json", Filters = { new FileDialogFilter { Name = "JSON", Extensions = { "json" } } } };
            return await dialog.ShowAsync((Window)VisualRoot);
        }


        public void SaveNote()
        {
            var note = notesList[indexListNotes];
            const string sql = "UPDATE note SET title = @title, text = @text, time_editor = now() WHERE id = @id";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@title", note.Title);
            cmd.Parameters.AddWithValue("@text", note.Text);
            cmd.Parameters.AddWithValue("@id", note.Id);
            cmd.ExecuteNonQuery();
        }
        public void UpdateNote()
        {
            notesList.Clear(); var sql = "SELECT * FROM note"; List<BDNotes> notesListDemo = new();
            using var cmd = new NpgsqlCommand(sql, conn); using var reader = cmd.ExecuteReader();
            while (reader.Read()) notesListDemo.Add(new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3)));
            notesSort.Clear();
            while (notesSort.Count < notesListDemo.Count)
            {
                int l = 0; bool l1 = false;
                for (int i = 0; i < notesListDemo.Count; i++)
                {
                    int k = 0; for (int j = 0; j < notesSort.Count; j++) {if (i == notesSort[j]) break; k++; }
                    if (k == notesSort.Count) { if (notesListDemo[i].TimeEditor >= notesListDemo[l].TimeEditor) { l = i; l1 = true; } } else if (!l1) l++;
                }
                notesSort.Add(l);
            }
            for (int i = 0; i < notesSort.Count; i++) notesList.Add(notesListDemo[notesSort[i]]);
        }
        public void NewNote(string title, string text)
        {
            const string sql = "INSERT INTO note (title, text, time_editor) VALUES (@title, @text, now())";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@text", text);
            cmd.ExecuteScalar();
        }
        public void NewNote(string title, string text, DateTime time)
        {
            const string sql = "INSERT INTO note (title, text, time_editor) VALUES (@title, @text, @time)";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@text", text);
            cmd.Parameters.AddWithValue("@text", time);
            cmd.ExecuteScalar();
        }
        public void NewNote(int id, string title, string text, DateTime time)
        {
            const string sql = "INSERT INTO note (id, title, text, time_editor) VALUES (@id, @title, @text, @time)";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@text", text);
            cmd.Parameters.AddWithValue("@text", time);
            cmd.ExecuteScalar();
        }
        public void DeleteNote()
        {
            var sql = $"DELETE FROM note WHERE id = {indexBDNotes}";
            using var cmd = new NpgsqlCommand(sql, conn); cmd.ExecuteNonQuery();
        }

        public void SaveLinks()
        {
            var links = linksList[indexListLinks];
            const string sql = "UPDATE links_in_note SET id_note = @id_note, link_out = @link_out WHERE id = @id";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id_note", links.IdNote);
            cmd.Parameters.AddWithValue("@link_out", links.Link);
            cmd.Parameters.AddWithValue("@id", links.Id);
            cmd.ExecuteNonQuery();
        }
        public void UpdateLinks()
        {
            linksList.Clear(); var sql = "SELECT * FROM links_in_note";
            using var cmd = new NpgsqlCommand(sql, conn); using var reader = cmd.ExecuteReader();
            while (reader.Read()) linksList.Add(new(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2)));
        }
        public void NewLinks(int idNote, string link)
        {
            const string sql = "INSERT INTO links_in_note (id_note, link_out) VALUES (@id_note, @link_out)";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id_note", idNote);
            cmd.Parameters.AddWithValue("@link_out", link);
            cmd.ExecuteScalar();
        }
        public void NewLinks(int id, int idNote, string link)
        {
            const string sql = "INSERT INTO links_in_note (id, id_note, link_out) VALUES (@id, @id_note, @link_out)";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@id_note", idNote);
            cmd.Parameters.AddWithValue("@link_out", link);
            cmd.ExecuteScalar();
        }
        public void DeleteLinks()
        {
            var sql = $"DELETE FROM links_in_note WHERE id = {indexBDLinks}";
            using var cmd = new NpgsqlCommand(sql, conn); cmd.ExecuteNonQuery();
        }


        public class BDNotes
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
            public DateTime TimeEditor { get; set; }

            public BDNotes(int i, string t, string x, DateTime e) { Id = i; Title = t; Text = x; TimeEditor = e; }
            public BDNotes() { Id = 0; Title = ""; Text = ""; TimeEditor = new(); }
        }
        public class BDLinks
        {
            public int Id { get; set; }
            public int IdNote { get; set; }
            public string Link { get; set; }

            public BDLinks(int i, int n, string l) { Id = i; IdNote = n; Link = l; }
            public BDLinks() { Id = 0; IdNote = 0; Link = ""; }
        }

        public class BDBackup
        {
            public List<BDNotes> Notes { get; set; }
            public List<BDLinks> Links { get; set; }

            public BDBackup(List<BDNotes> n, List<BDLinks> l) { Notes = n; Links = l; }
            public BDBackup() { Notes = new(); Links = new(); }
        }
    }
}