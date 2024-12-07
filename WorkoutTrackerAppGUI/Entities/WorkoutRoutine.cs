using System;
using System.Collections.Generic;

namespace WorkoutTrackerAppGUI.Entities
{
    public class WorkoutRoutine
    {
        // Attributes
        public int RoutineId { get; set; }
        public string Name { get; set; } // Changed from private to public setter
        public string Description { get; private set; }
        public List<string> Exercises { get; private set; }
        public TimeSpan Duration { get; private set; } // Total duration of the workout routine

        // Constructor
        public WorkoutRoutine(string name, string description, List<string> exercises, TimeSpan duration)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
                throw new ArgumentException("Workout name must be at least 3 characters.");
            if (exercises == null || exercises.Count == 0)
                throw new ArgumentException("Workout routine must have at least one exercise.");
            if (duration.TotalMinutes <= 0)
                throw new ArgumentException("Duration must be greater than zero.");

            Name = name;
            Description = description;
            Exercises = new List<string>(exercises);
            Duration = duration;
        }

        // Methods

        // Add an exercise to the routine
        public void AddExercise(string exercise)
        {
            if (string.IsNullOrWhiteSpace(exercise))
                throw new ArgumentException("Exercise cannot be empty.");
            Exercises.Add(exercise);
        }

        // Remove an exercise from the routine
        public void RemoveExercise(string exercise)
        {
            if (!Exercises.Remove(exercise))
                throw new KeyNotFoundException("Exercise not found in the routine.");
        }

        // Update the name of the routine
        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName) || newName.Length < 3)
                throw new ArgumentException("Workout name must be at least 3 characters.");
            Name = newName;
        }

        // Update the description of the routine
        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
        }

        // Update the duration of the routine
        public void UpdateDuration(TimeSpan newDuration)
        {
            if (newDuration.TotalMinutes <= 0)
                throw new ArgumentException("Duration must be greater than zero.");
            Duration = newDuration;
        }

        // Retrieve the full workout routine details
        public string GetRoutineDetails()
        {
            return $"Routine Name: {Name}" +
                $"\nDescription: {Description}" +
                $"\nDuration: {Duration}" +
                $"\nExercises: {string.Join(", ", Exercises)}";
        }

        // Static method to create a new routine
        public static WorkoutRoutine CreateRoutine(string name, string description, List<string> exercises, TimeSpan duration)
        {
            return new WorkoutRoutine(name, description, exercises, duration);
        }
    }
}
