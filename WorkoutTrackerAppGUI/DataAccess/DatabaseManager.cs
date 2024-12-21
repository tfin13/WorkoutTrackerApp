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
            // Use the provided connection string
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null.");
        }

        // Test the database connection
        public void TestConnection()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connection to the database was successful!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection test failed: {ex.Message}");
                throw;
            }
        }

        // Execute a non-query command
        public void ExecuteNonQuery(string query, List<SqlParameter> parameters = null)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error [{sqlEx.Number}]: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw;
            }
        }

        // Execute a scalar query
        public object ExecuteScalar(string query, List<SqlParameter> parameters = null)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }

                        connection.Open();
                        return command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error [{sqlEx.Number}]: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw;
            }
        }

        // Execute a reader and process the results with a callback
        public void ExecuteReader(string query, List<SqlParameter> parameters, Action<SqlDataReader> processRow)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                processRow(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error [{sqlEx.Number}]: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw;
            }
        }

        // Add a new user
        public void AddUser(User user)
        {
            var query = "INSERT INTO Users (Username, PasswordHash, Email) VALUES (@Username, @PasswordHash, @Email)";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Username", SqlDbType.NVarChar) { Value = user.Username },
                new SqlParameter("@PasswordHash", SqlDbType.NVarChar) { Value = user.GetPasswordHash() },
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = user.Email }
            };

            ExecuteNonQuery(query, parameters);
        }

        public User GetUserByUsername(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT Username, PasswordHash, Email FROM Users WHERE Username = @Username";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User(
                                reader["Username"].ToString(),
                                reader["PasswordHash"].ToString(),
                                reader["Email"].ToString()
                            );
                        }
                    }
                }
            }
            return null; // Return null if user not found
        }

        // Get a user by email
        public User GetUserByEmail(string email)
        {
            email = email.Trim(); // Ensure email is sanitized

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Username, PasswordHash, Email FROM Users WHERE LOWER(Email) = LOWER(@Email)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"User found with email: {email}");
                            return new User
                            {
                                Username = reader["Username"].ToString(),
                                Email = reader["Email"].ToString(),
                            };
                        }
                        else
                        {
                            Console.WriteLine($"No user found with email: {email}");
                            return null;
                        }
                    }
                }
            }
        }



        // Get a user by ID
        public User GetUserById(int userId)
        {
            User user = null;
            var query = "SELECT * FROM Users WHERE UserId = @UserId";
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@UserId", userId)
    };

            ExecuteReader(query, parameters, reader =>
            {
                user = new User(
                    username: reader["Username"].ToString(),
                    passwordHash: reader["PasswordHash"].ToString(),
                    email: reader["Email"].ToString())
                {
                    UserId = Convert.ToInt32(reader["UserId"])
                };
            });

            return user; // Returns the user or null if not found
        }


        // Add progress tracking entry
        public void AddProgressEntry(ProgressTracking progress)
        {
            var query = "INSERT INTO Progress (UserId, Date) VALUES (@UserId, @Date); SELECT SCOPE_IDENTITY();";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserId", progress.UserId),
                new SqlParameter("@Date", progress.Date)
            };

            progress.ProgressId = Convert.ToInt32(ExecuteScalar(query, parameters));

            foreach (var metric in progress.Metrics)
            {
                AddProgressMetric(progress.ProgressId, metric.Key, metric.Value);
            }
        }

        // Add a progress metric
        private void AddProgressMetric(int progressId, string metricName, double value)
        {
            var query = "INSERT INTO ProgressMetrics (ProgressId, MetricName, Value) VALUES (@ProgressId, @MetricName, @Value)";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProgressId", progressId),
                new SqlParameter("@MetricName", metricName),
                new SqlParameter("@Value", value)
            };

            ExecuteNonQuery(query, parameters);
        }

        // Get all progress entries for a specific user
        public List<ProgressTracking> GetProgressForUser(int userId)
        {
            var progressList = new List<ProgressTracking>();
            var query = "SELECT * FROM Progress WHERE UserId = @UserId";
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@UserId", userId)
    };

            ExecuteReader(query, parameters, reader =>
            {
                var progressId = Convert.ToInt32(reader["ProgressId"]);
                var date = Convert.ToDateTime(reader["Date"]);
                var metrics = GetProgressMetrics(progressId);

                progressList.Add(new ProgressTracking(userId, date, metrics) { ProgressId = progressId });
            });

            return progressList;
        }

        // Get all metrics associated with a specific progress entry
        private Dictionary<string, double> GetProgressMetrics(int progressId)
        {
            var metrics = new Dictionary<string, double>();
            var query = "SELECT * FROM ProgressMetrics WHERE ProgressId = @ProgressId";
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@ProgressId", progressId)
    };

            ExecuteReader(query, parameters, reader =>
            {
                var metricName = reader["MetricName"].ToString();
                var value = Convert.ToDouble(reader["Value"]);
                metrics[metricName] = value;
            });

            return metrics;
        }

        // Add a new workout routine
        public void AddWorkoutRoutine(WorkoutRoutine routine)
        {
            var query = "INSERT INTO WorkoutRoutines (Name, Description, Duration) OUTPUT INSERTED.RoutineId VALUES (@Name, @Description, @Duration)";
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@Name", routine.Name),
        new SqlParameter("@Description", (object)routine.Description ?? DBNull.Value),
        new SqlParameter("@Duration", routine.Duration.TotalMinutes)
    };

            routine.RoutineId = Convert.ToInt32(ExecuteScalar(query, parameters));
        }


        // Get a workout routine by ID
        public WorkoutRoutine GetWorkoutRoutineById(int routineId)
        {
            WorkoutRoutine routine = null;
            var query = "SELECT * FROM WorkoutRoutines WHERE RoutineId = @RoutineId";
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@RoutineId", routineId)
    };

            ExecuteReader(query, parameters, reader =>
            {
                routine = new WorkoutRoutine(
                    name: reader["Name"].ToString(),
                    description: reader["Description"]?.ToString(),
                    exercises: GetExercisesForRoutine(routineId),
                    duration: TimeSpan.FromMinutes(Convert.ToDouble(reader["Duration"]))
                )
                {
                    RoutineId = routineId
                };
            });

            return routine;
        }

        // Update a workout routine
        public void UpdateWorkoutRoutine(WorkoutRoutine routine)
        {
            var query = "UPDATE WorkoutRoutines SET Name = @Name, Description = @Description, Duration = @Duration WHERE RoutineId = @RoutineId";
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@Name", routine.Name),
        new SqlParameter("@Description", (object)routine.Description ?? DBNull.Value),
        new SqlParameter("@Duration", routine.Duration.TotalMinutes),
        new SqlParameter("@RoutineId", routine.RoutineId)
    };

            ExecuteNonQuery(query, parameters);
        }

        // Delete a workout routine
        public void DeleteWorkoutRoutine(int routineId)
        {
            var query = "DELETE FROM WorkoutRoutines WHERE RoutineId = @RoutineId";
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@RoutineId", routineId)
    };

            ExecuteNonQuery(query, parameters);
        }

        // Add an exercise to a routine
        public void AddExerciseToRoutine(int routineId, string exerciseName)
        {
            var query = "INSERT INTO WorkoutRoutineExercises (RoutineId, ExerciseName) VALUES (@RoutineId, @ExerciseName)";
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@RoutineId", routineId),
        new SqlParameter("@ExerciseName", exerciseName)
    };

            ExecuteNonQuery(query, parameters);
        }

        // Remove an exercise from a routine
        public void RemoveExerciseFromRoutine(int routineId, string exerciseName)
        {
            var query = "DELETE FROM WorkoutRoutineExercises WHERE RoutineId = @RoutineId AND ExerciseName = @ExerciseName";
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@RoutineId", routineId),
        new SqlParameter("@ExerciseName", exerciseName)
    };

            ExecuteNonQuery(query, parameters);
        }

        // Fetch all workout routines
        public List<WorkoutRoutine> GetAllWorkoutRoutines()
        {
            var routines = new List<WorkoutRoutine>();
            var query = "SELECT * FROM WorkoutRoutines";

            ExecuteReader(query, null, reader =>
            {
                int routineId = Convert.ToInt32(reader["RoutineId"]);
                var routine = new WorkoutRoutine(
                    name: reader["Name"].ToString(),
                    description: reader["Description"]?.ToString(),
                    exercises: GetExercisesForRoutine(routineId),
                    duration: TimeSpan.FromMinutes(Convert.ToDouble(reader["Duration"]))
                )
                {
                    RoutineId = routineId
                };

                routines.Add(routine);
            });

            return routines;
        }


        // Fetch all exercises for a specific routine
        public List<string> GetExercisesForRoutine(int routineId)
        {
            var exercises = new List<string>();
            var query = "SELECT ExerciseName FROM WorkoutRoutineExercises WHERE RoutineId = @RoutineId";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@RoutineId", routineId)
            };

            ExecuteReader(query, parameters, reader =>
            {
                exercises.Add(reader["ExerciseName"].ToString());
            });

            return exercises;
        }
    }
}

