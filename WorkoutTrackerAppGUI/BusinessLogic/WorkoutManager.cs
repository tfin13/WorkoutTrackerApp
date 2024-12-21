using System;
using System.Collections.Generic;
using WorkoutTrackerAppGUI.DataAccess;
using WorkoutTrackerAppGUI.Entities;

namespace WorkoutTrackerAppGUI.BusinessLogic
{
    public class WorkoutManager
    {
        // Dependency on DatabaseManager
        private readonly DatabaseManager _databaseManager;

        // Constructor
        public WorkoutManager(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        // Add a new workout routine
        public void AddWorkoutRoutine(WorkoutRoutine routine)
        {
            if (routine == null)
                throw new ArgumentNullException(nameof(routine), "Workout routine cannot be null.");

            _databaseManager.AddWorkoutRoutine(routine);
        }

        // Get a workout routine by ID
        public WorkoutRoutine GetWorkoutRoutineById(int routineId)
        {
            var routine = _databaseManager.GetWorkoutRoutineById(routineId);
            if (routine == null)
                throw new KeyNotFoundException($"Workout routine with ID {routineId} not found.");

            return routine;
        }

        // Remove a workout routine by ID
        public void RemoveWorkoutRoutine(int routineId)
        {
            var routine = GetWorkoutRoutineById(routineId); // Ensures routine exists
            _databaseManager.DeleteWorkoutRoutine(routineId);
        }

        // Add an exercise to a specific workout routine
        public void AddExerciseToRoutine(int routineId, Exercise exercise)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise), "Exercise cannot be null.");

            var routine = GetWorkoutRoutineById(routineId); // Ensures routine exists
            _databaseManager.AddExerciseToRoutine(routineId, exercise.Name);
        }

        // Remove an exercise from a workout routine
        public void RemoveExerciseFromRoutine(int routineId, string exerciseName)
        {
            if (string.IsNullOrWhiteSpace(exerciseName))
                throw new ArgumentException("Exercise name cannot be null or empty.", nameof(exerciseName));

            var routine = GetWorkoutRoutineById(routineId); // Ensures routine exists
            _databaseManager.RemoveExerciseFromRoutine(routineId, exerciseName);
        }

        // Retrieve all workout routines
        public List<WorkoutRoutine> GetAllWorkoutRoutines()
        {
            return _databaseManager.GetAllWorkoutRoutines();
        }

        // Get all exercises for a workout routine
        public List<string> GetExercisesForRoutine(int routineId)
        {
            return _databaseManager.GetExercisesForRoutine(routineId);
        }
    }
}
