using AANotes.Views;
using Avalonia.Controls;
using Npgsql;
using System.Collections.Generic;

namespace AANotes
{
    public partial class MainWindow : Window
    {
        private readonly string cs = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=NotesLD";
        public NpgsqlConnection conn = new();
        public List<BDNotes> notesList = new();
        public int indexBDNotes = 0;
        public int indexListNotes = 0;

        public MainWindow()
        {
            InitializeComponent();
            OpenBD();

            OpenMain();
        }
        public void OpenEditor() => MainContent.Content = new EditorView(this);
        public void OpenMain() => MainContent.Content = new MainView(this);
        public void OpenBD()
        {
            EnsureDatabaseExists();
            conn = OpenMainDatabase(cs);
            EnsureTables(conn);
            // conn = new(cs); conn.Open();
            ObdateBD();
        }
        private static void EnsureDatabaseExists()
        {
            var adminCs = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=postgres";
            var targetDbName = "NotesLD";

            using var conn = new NpgsqlConnection(adminCs);
            conn.Open();

            // проверяем, существует ли БД
            using var checkCmd = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @name", conn);
            checkCmd.Parameters.AddWithValue("name", targetDbName);

            var exists = checkCmd.ExecuteScalar() != null;

            if (!exists)
            {
                using var createCmd = new NpgsqlCommand($"CREATE DATABASE \"{targetDbName}\"", conn);
                createCmd.ExecuteNonQuery();
            }
        }
        private static NpgsqlConnection OpenMainDatabase(string cs)
        {
            var conn = new NpgsqlConnection(cs);
            conn.Open();
            return conn;
        }
        private static void EnsureTables(NpgsqlConnection conn)
        {
            var sql = "CREATE TABLE IF NOT EXISTS note (id SERIAL PRIMARY KEY, title TEXT, text TEXT);";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
        void EnsureColumnExists(NpgsqlConnection conn, string table, string column, string typeAndOptions)
        {
            var sql = $"ALTER TABLE {table} ADD COLUMN IF NOT EXISTS {column} {typeAndOptions};";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }


        public void SaveBD()
        {
            var sql = $"UPDATE note SET title = '{notesList[indexListNotes].Title}', text = '{notesList[indexListNotes].Text}' WHERE id = {notesList[indexListNotes].Id}";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
        public void ObdateBD()
        {
            notesList.Clear();

            var sql = "SELECT * FROM note";

            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read()) notesList.Add(new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
        }
        public void NewNote(string title, string text)
        {
            var sql = $"INSERT INTO note (title, text) VALUES ('{title}', '{text}')";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
        public void DeleteNote()
        {
            var sql = $"DELETE FROM note WHERE id = {indexBDNotes}";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        public class BDNotes
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }

            public BDNotes(int i, string t, string x) { Id = i; Title = t; Text = x; }
            public BDNotes() { Id = 0; Title = ""; Text = ""; }
        }
    }
}