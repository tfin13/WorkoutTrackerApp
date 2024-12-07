using System;
using System.Collections.Generic;
using WorkoutTrackerAppGUI.Entities;

namespace WorkoutTrackerAppGUI.BusinessLogic
{
    public static class ExerciseFactory
    {
        // Method to create a new Exercise
        public static Exercise CreateExercise(string name, string description, int repetitions, int sets, TimeSpan restPeriod)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
                throw new ArgumentException("Exercise name must be at least 3 characters.");
            if (repetitions <= 0)
                throw new ArgumentException("Repetitions must be greater than zero.");
            if (sets <= 0)
                throw new ArgumentException("Sets must be greater than zero.");
            if (restPeriod.TotalSeconds < 0)
                throw new ArgumentException("Rest period cannot be negative.");

            return new Exercise(name, description, repetitions, sets, restPeriod);
        }

        // Method to create a default exercise
        public static Exercise CreateDefaultExercise()
        {
            return new Exercise(
                name: "Default Exercise",
                description: "This is a placeholder exercise.",
                repetitions: 10,
                sets: 3,
                restPeriod: TimeSpan.FromSeconds(60)
            );
        }

        // Method to create multiple exercises from a predefined list
        public static List<Exercise> CreatePresetExercises()
        {
            return new List<Exercise>
            {
                new Exercise("Push-ups", "A basic upper body exercise", 15, 3, TimeSpan.FromSeconds(60)),
                new Exercise("Squats", "A lower body exercise", 20, 3, TimeSpan.FromSeconds(90)),
                new Exercise("Plank", "Core strengthening exercise", 1, 3, TimeSpan.FromSeconds(30)) // 1 repetition implies a timed hold
            };
        }
    }
}
