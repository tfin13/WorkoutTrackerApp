using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutTrackerAppGUI.Entities;

namespace WorkoutTrackerAppGUI.BusinessLogic
{
    public class WorkoutManager
    {
        // Attributes
        private readonly List<WorkoutRoutine> _workoutRoutines;
        private readonly List<Exercise> _exercises;

        // Constructor
        public WorkoutManager()
        {
            _workoutRoutines = new List<WorkoutRoutine>();
            _exercises = new List<Exercise>();
        }

        // Methods

        // Add a new workout routine
        public void AddWorkoutRoutine(WorkoutRoutine routine)
        {
            if (routine == null)
                throw new ArgumentNullException(nameof(routine), "Workout routine cannot be null.");

            _workoutRoutines.Add(routine);
        }

        // Get a workout routine by ID
        public WorkoutRoutine GetWorkoutRoutineById(int routineId)
        {
            var routine = _workoutRoutines.FirstOrDefault(r => r.RoutineId == routineId);
            if (routine == null)
                throw new KeyNotFoundException($"Workout routine with ID {routineId} not found.");

            return routine;
        }

        // Remove a workout routine by ID
        public void RemoveWorkoutRoutine(int routineId)
        {
            var routine = GetWorkoutRoutineById(routineId);
            _workoutRoutines.Remove(routine);
        }

        // Add an exercise to a specific workout routine
        public void AddExerciseToRoutine(int routineId, Exercise exercise)
        {
            var routine = GetWorkoutRoutineById(routineId);
            routine.AddExercise(exercise.Name);
        }

        // Remove an exercise from a workout routine
        public void RemoveExerciseFromRoutine(int routineId, string exerciseName)
        {
            var routine = GetWorkoutRoutineById(routineId);
            routine.RemoveExercise(exerciseName);
        }

        // Add a new exercise
        public void AddExercise(Exercise exercise)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise), "Exercise cannot be null.");

            _exercises.Add(exercise);
        }

        // Get all exercises
        public List<Exercise> GetAllExercises()
        {
            return _exercises;
        }

        // Get exercise by name
        public Exercise GetExerciseByName(string name)
        {
            var exercise = _exercises.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (exercise == null)
                throw new KeyNotFoundException($"Exercise '{name}' not found.");

            return exercise;
        }

        // Retrieve all workout routines
        public List<WorkoutRoutine> GetAllWorkoutRoutines()
        {
            return _workoutRoutines;
        }
    }
}
