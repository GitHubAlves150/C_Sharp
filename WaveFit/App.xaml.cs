using HandyControl.Properties.Langs;
using HandyControl.Tools;
using System;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WaveFit2.Migrations;

namespace WaveFit2
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Logs();

            base.OnStartup(e);

            DebugDirectory();
            Translate();
            //Migrations();
            BackupDatabase();
        }

        public void Logs()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception);
            e.Handled = true;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            HandleException(e.Exception);
            e.SetObserved();
        }

        private void HandleException(Exception ex)
        {
            if (ex != null)
            {
                // Exibir mensagem de erro
                MessageBox.Show($"O aplicativo encontrou um erro inesperado:\n\n{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

                // Registrar a exceção em um arquivo de log
                LogException(ex);
            }
        }

        private void LogException(Exception ex)
        {
            try
            {
                string logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
                string logFileName = $"error_log.txt";
                string logFilePath = Path.Combine(logDirectory, logFileName);
                File.AppendAllText(logFilePath, $"{DateTime.Now}: {ex}\n\n");
            }
            catch
            {
                // Se a gravação no log falhar, não faça nada para evitar um loop infinito de exceções.
            }
        }

        public void DebugDirectory()
        {
            Directory.SetCurrentDirectory(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")));
        }

        public void Translate()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            ConfigHelper.Instance.SetLang("pt-br");
            Lang.Culture = new System.Globalization.CultureInfo("pt-BR");
        }

        public void Migrations()
        {
            var migrator = new DbMigrator(new Configuration());
            migrator.Update();
        }

        private void BackupDatabase()
        {
            string databaseName = "LocalDb";
            string username = "postgres";
            string date = DateTime.Now.ToString("ddMMyyyy");
            string backupDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Database/Backup");
            string backupFileName = $"backup_{date}.sql";
            string backupFilePath = Path.Combine(backupDirectory, backupFileName);
            string pgDumpPath = @"C:\Program Files\PostgreSQL\15\bin\pg_dump.exe";

            if (!Directory.Exists(backupDirectory))
            {
                Directory.CreateDirectory(backupDirectory);
            }

            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.EnvironmentVariables["PGPASSWORD"] = "admin";
                    process.StartInfo.FileName = pgDumpPath; // Certifique-se de que está no PATH ou especifique o caminho completo
                    process.StartInfo.Arguments = $"-U {username} -d {databaseName} -f \"{backupFilePath}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string errors = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        Console.WriteLine("Backup completed successfully.");
                        Console.WriteLine(output); // Log da saída em caso de sucesso
                    }
                    else
                    {
                        Console.WriteLine("Error during backup process:");
                        Console.WriteLine(errors);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to execute backup:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}