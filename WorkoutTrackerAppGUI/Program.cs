using WorkoutTrackerAppGUI.DataAccess;

namespace WorkoutTrackerAppGUI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Replace with your actual connection string
            string connectionString = "your_connection_string_here";

            // Create a single instance of DatabaseManager
            var databaseManager = new DatabaseManager(connectionString);

            // Pass DatabaseManager to LoginForm
            Application.Run(new LoginForm(databaseManager));
        }


    }
}