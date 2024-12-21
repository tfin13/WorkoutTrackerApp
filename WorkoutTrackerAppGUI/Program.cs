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
            string connectionString = "Server=TREVOR4070;Database=WorkoutTrackerDB;Trusted_Connection=True;";

            // Create a single instance of DatabaseManager
            var databaseManager = new DatabaseManager(connectionString);

            // Pass DatabaseManager to LoginForm
            Application.Run(new LoginForm(databaseManager));
        }


    }
}