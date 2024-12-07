using System;
using System.Collections.Generic;

namespace WorkoutTrackerAppGUI.Entities
{
    public class ProgressTracking
    {
        // Attributes
        public int ProgressId { get; set; }
        public int UserId { get; private set; } // Links progress tracking to a specific user
        public DateTime Date { get; private set; }
        public Dictionary<string, double> Metrics { get; private set; } // Metric name and value (e.g., "Weight", "BMI")

        // Constructor
        public ProgressTracking(int userId, DateTime date, Dictionary<string, double> metrics)
        {
            if (userId <= 0)
                throw new ArgumentException("UserId must be greater than zero.");
            if (metrics == null || metrics.Count == 0)
                throw new ArgumentException("Metrics cannot be null or empty.");

            UserId = userId;
            Date = date;
            Metrics = new Dictionary<string, double>(metrics);
        }

        // Methods

        // Add a new metric
        public void AddMetric(string metricName, double value)
        {
            if (string.IsNullOrWhiteSpace(metricName))
                throw new ArgumentException("Metric name cannot be empty.");
            if (value < 0)
                throw new ArgumentException("Metric value cannot be negative.");

            Metrics[metricName] = value; // Adds or updates the metric
        }

        // Remove an existing metric
        public void RemoveMetric(string metricName)
        {
            if (!Metrics.Remove(metricName))
                throw new KeyNotFoundException($"Metric '{metricName}' not found.");
        }

        // Update the date of the progress tracking entry
        public void UpdateDate(DateTime newDate)
        {
            Date = newDate;
        }

        // Get a specific metric value
        public double GetMetric(string metricName)
        {
            if (!Metrics.TryGetValue(metricName, out double value))
                throw new KeyNotFoundException($"Metric '{metricName}' not found.");
            return value;
        }

        // Retrieve all metrics
        public string GetAllMetrics()
        {
            var metricsInfo = string.Join(", ", Metrics.Select(m => $"{m.Key}: {m.Value}"));
            return $"Date: {Date.ToShortDateString()}, Metrics: {metricsInfo}";
        }

        // Static method to create a new progress tracking entry
        public static ProgressTracking CreateProgress(int userId, DateTime date, Dictionary<string, double> metrics)
        {
            return new ProgressTracking(userId, date, metrics);
        }
    }
}
