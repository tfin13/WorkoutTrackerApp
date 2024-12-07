using System;

namespace WorkoutTrackerAppGUI.Entities
{
    public class Exercise
    {
        // Attributes
        public int ExerciseId { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Repetitions { get; private set; } // Number of repetitions per set
        public int Sets { get; private set; }        // Number of sets
        public TimeSpan RestPeriod { get; private set; } // Rest period between sets

        // Constructor
        public Exercise(string name, string description, int repetitions, int sets, TimeSpan restPeriod)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
                throw new ArgumentException("Exercise name must be at least 3 characters.");
            if (repetitions <= 0)
                throw new ArgumentException("Repetitions must be greater than zero.");
            if (sets <= 0)
                throw new ArgumentException("Sets must be greater than zero.");
            if (restPeriod.TotalSeconds < 0)
                throw new ArgumentException("Rest period cannot be negative.");

            Name = name;
            Description = description;
            Repetitions = repetitions;
            Sets = sets;
            RestPeriod = restPeriod;
        }

        // Methods

        // Update the name of the exercise
        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName) || newName.Length < 3)
                throw new ArgumentException("Exercise name must be at least 3 characters.");
            Name = newName;
        }

        // Update the description of the exercise
        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
        }

        // Update the number of repetitions
        public void UpdateRepetitions(int newRepetitions)
        {
            if (newRepetitions <= 0)
                throw new ArgumentException("Repetitions must be greater than zero.");
            Repetitions = newRepetitions;
        }

        // Update the number of sets
        public void UpdateSets(int newSets)
        {
            if (newSets <= 0)
                throw new ArgumentException("Sets must be greater than zero.");
            Sets = newSets;
        }

        // Update the rest period
        public void UpdateRestPeriod(TimeSpan newRestPeriod)
        {
            if (newRestPeriod.TotalSeconds < 0)
                throw new ArgumentException("Rest period cannot be negative.");
            RestPeriod = newRestPeriod;
        }

        // Retrieve the full exercise details
        public string GetExerciseDetails()
        {
            return $"Exercise Name: {Name}" +
                $"\nDescription: {Description}" +
                $"\nRepetitions: {Repetitions}" +
                $"\nSets: {Sets}" +
                $"\nRest Period: {RestPeriod.TotalSeconds} seconds";
        }

        // Static method to create a new exercise
        public static Exercise CreateExercise(string name, string description, int repetitions, int sets, TimeSpan restPeriod)
        {
            return new Exercise(name, description, repetitions, sets, restPeriod);
        }
    }
}
