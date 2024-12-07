using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // Use this for SQL Server
using WorkoutTrackerAppGUI.Entities;

namespace WorkoutTrackerAppGUI.DataAccess
{
    public class DatabaseManager
    {
        // Connection String
        private readonly string _connectionString;

        // Constructor
        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // Execute a non-query command
        public void ExecuteQuery(string query)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
                throw;
            }
        }

        // Add a new user
        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Users (Username, PasswordHash, Email) VALUES (@Username, @PasswordHash, @Email)", connection);

                command.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar) { Value = user.Username });
                command.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.NVarChar) { Value = user.GetPasswordHash() }); // Ensure GetPasswordHash is implemented
                command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = user.Email });

                connection.Open();
                command.ExecuteNonQuery();
            }
        }



        // Get a user by ID
        public User GetUserById(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT * FROM Users WHERE UserId = @UserId", connection);
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User(
                                username: reader["Username"].ToString(),
                                passwordHash: reader["PasswordHash"].ToString(),
                                email: reader["Email"].ToString()
                            )
                            {
                                UserId = Convert.ToInt32(reader["UserId"])
                            };
                        }
                        else
                        {
                            return null; // User not found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user: {ex.Message}");
                throw;
            }
        }

        // Get a user by username
        public User GetUserByUsername(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Users WHERE Username = @Username", connection);
                command.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username });

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            username: reader["Username"].ToString(),
                            passwordHash: reader["PasswordHash"].ToString(),
                            email: reader["Email"].ToString()
                        )
                        {
                            UserId = Convert.ToInt32(reader["UserId"])
                        };
                    }
                }
            }
            return null; // User not found
        }


        // Other methods remain unchanged...

        // Add progress tracking entry
        public void AddProgressEntry(ProgressTracking progress)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("INSERT INTO Progress (UserId, Date) VALUES (@UserId, @Date); SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@UserId", progress.UserId);
                    command.Parameters.AddWithValue("@Date", progress.Date);

                    connection.Open();
                    progress.ProgressId = Convert.ToInt32(command.ExecuteScalar());

                    foreach (var metric in progress.Metrics)
                    {
                        AddProgressMetric(progress.ProgressId, metric.Key, metric.Value, connection);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding progress entry: {ex.Message}");
                throw;
            }
        }

        // Add a progress metric
        private void AddProgressMetric(int progressId, string metricName, double value, SqlConnection connection)
        {
            var command = new SqlCommand("INSERT INTO ProgressMetrics (ProgressId, MetricName, Value) VALUES (@ProgressId, @MetricName, @Value)", connection);
            command.Parameters.AddWithValue("@ProgressId", progressId);
            command.Parameters.AddWithValue("@MetricName", metricName);
            command.Parameters.AddWithValue("@Value", value);

            command.ExecuteNonQuery();
        }

        // Get all progress for a user
        public List<ProgressTracking> GetProgressForUser(int userId)
        {
            var progressList = new List<ProgressTracking>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Progress WHERE UserId = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var progressId = Convert.ToInt32(reader["ProgressId"]);
                        var date = Convert.ToDateTime(reader["Date"]);

                        var metrics = GetProgressMetrics(progressId, connection);
                        progressList.Add(new ProgressTracking(userId, date, metrics) { ProgressId = progressId });
                    }
                }
            }

            return progressList;
        }

        // Get progress metrics
        private Dictionary<string, double> GetProgressMetrics(int progressId, SqlConnection connection)
        {
            var metrics = new Dictionary<string, double>();

            var command = new SqlCommand("SELECT * FROM ProgressMetrics WHERE ProgressId = @ProgressId", connection);
            command.Parameters.AddWithValue("@ProgressId", progressId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var metricName = reader["MetricName"].ToString();
                    var value = Convert.ToDouble(reader["Value"]);
                    metrics[metricName] = value;
                }
            }

            return metrics;
        }

        // WORKOUT ROUTINE METHODS**

        // Add a new workout routine
        public void AddWorkoutRoutine(WorkoutRoutine routine)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO WorkoutRoutines (Name, Description, Duration) OUTPUT INSERTED.RoutineId VALUES (@Name, @Description, @Duration)",
                    connection
                );

                command.Parameters.AddWithValue("@Name", routine.Name);
                command.Parameters.AddWithValue("@Description", (object)routine.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@Duration", routine.Duration.TotalMinutes);

                connection.Open();
                routine.RoutineId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        // Fetch all workout routines
        public List<WorkoutRoutine> GetAllWorkoutRoutines()
        {
            var routines = new List<WorkoutRoutine>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM WorkoutRoutines", connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int routineId = Convert.ToInt32(reader["RoutineId"]);
                        var routine = new WorkoutRoutine(
                            name: reader["Name"].ToString(),
                            description: reader["Description"]?.ToString(),
                            exercises: GetExercisesForRoutine(routineId), // Fetch associated exercises
                            duration: TimeSpan.FromMinutes(Convert.ToDouble(reader["Duration"]))
                        )
                        {
                            RoutineId = routineId
                        };

                        routines.Add(routine);
                    }
                }
            }

            return routines;
        }

        // Get a workout routine by ID
        public WorkoutRoutine GetWorkoutRoutineById(int routineId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM WorkoutRoutines WHERE RoutineId = @RoutineId", connection);
                command.Parameters.AddWithValue("@RoutineId", routineId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new WorkoutRoutine(
                            name: reader["Name"].ToString(),
                            description: reader["Description"]?.ToString(),
                            exercises: GetExercisesForRoutine(routineId),
                            duration: TimeSpan.FromMinutes(Convert.ToDouble(reader["Duration"]))
                        )
                        {
                            RoutineId = routineId
                        };
                    }
                }
            }

            return null; // Routine not found
        }

        // Update a workout routine
        public void UpdateWorkoutRoutine(WorkoutRoutine routine)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE WorkoutRoutines SET Name = @Name, Description = @Description, Duration = @Duration WHERE RoutineId = @RoutineId",
                    connection
                );

                command.Parameters.AddWithValue("@Name", routine.Name);
                command.Parameters.AddWithValue("@Description", (object)routine.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@Duration", routine.Duration.TotalMinutes);
                command.Parameters.AddWithValue("@RoutineId", routine.RoutineId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Delete a workout routine
        public void DeleteWorkoutRoutine(int routineId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM WorkoutRoutines WHERE RoutineId = @RoutineId", connection);
                command.Parameters.AddWithValue("@RoutineId", routineId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Add an exercise to a routine
        public void AddExerciseToRoutine(int routineId, string exerciseName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO WorkoutRoutineExercises (RoutineId, ExerciseName) VALUES (@RoutineId, @ExerciseName)",
                    connection
                );

                command.Parameters.AddWithValue("@RoutineId", routineId);
                command.Parameters.AddWithValue("@ExerciseName", exerciseName);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Remove an exercise from a routine
        public void RemoveExerciseFromRoutine(int routineId, string exerciseName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "DELETE FROM WorkoutRoutineExercises WHERE RoutineId = @RoutineId AND ExerciseName = @ExerciseName",
                    connection
                );

                command.Parameters.AddWithValue("@RoutineId", routineId);
                command.Parameters.AddWithValue("@ExerciseName", exerciseName);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Fetch all exercises for a specific routine
        public List<string> GetExercisesForRoutine(int routineId)
        {
            var exercises = new List<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT ExerciseName FROM WorkoutRoutineExercises WHERE RoutineId = @RoutineId", connection);
                command.Parameters.AddWithValue("@RoutineId", routineId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        exercises.Add(reader["ExerciseName"].ToString());
                    }
                }
            }

            return exercises;
        }

    }
}
