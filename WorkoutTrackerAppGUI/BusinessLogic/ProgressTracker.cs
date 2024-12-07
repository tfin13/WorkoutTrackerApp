using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutTrackerAppGUI.Entities;

namespace WorkoutTrackerAppGUI.BusinessLogic
{
    public class ProgressTracker
    {
        // Attributes
        private readonly List<ProgressTracking> _progressEntries;

        // Constructor
        public ProgressTracker()
        {
            _progressEntries = new List<ProgressTracking>();
        }

        // Methods

        // Add a progress entry
        public void AddProgressEntry(ProgressTracking progress)
        {
            if (progress == null)
                throw new ArgumentNullException(nameof(progress), "Progress entry cannot be null.");

            _progressEntries.Add(progress);
        }

        // Get progress entry by ID
        public ProgressTracking GetProgressEntryById(int progressId)
        {
            var entry = _progressEntries.FirstOrDefault(p => p.ProgressId == progressId);
            if (entry == null)
                throw new KeyNotFoundException($"Progress entry with ID {progressId} not found.");
            return entry;
        }

        // Get all progress entries for a specific user
        public List<ProgressTracking> GetProgressEntriesByUser(int userId)
        {
            return _progressEntries.Where(p => p.UserId == userId).ToList();
        }

        // Update progress metrics
        public void UpdateProgressMetrics(int progressId, Dictionary<string, double> newMetrics)
        {
            var entry = GetProgressEntryById(progressId);

            foreach (var metric in newMetrics)
            {
                entry.AddMetric(metric.Key, metric.Value); // Uses the AddMetric method from ProgressTracking
            }
        }

        // Remove a progress entry by ID
        public void RemoveProgressEntry(int progressId)
        {
            var entry = GetProgressEntryById(progressId);
            _progressEntries.Remove(entry);
        }

        // Calculate average of a specific metric for a user
        public double CalculateAverageMetric(int userId, string metricName)
        {
            var userEntries = GetProgressEntriesByUser(userId);
            var values = userEntries
                .Where(entry => entry.Metrics.ContainsKey(metricName))
                .Select(entry => entry.Metrics[metricName])
                .ToList();

            if (values.Count == 0)
                throw new InvalidOperationException($"No data available for metric '{metricName}'.");

            return values.Average();
        }

        // Get all progress details for a user
        public string GetUserProgressSummary(int userId)
        {
            var userEntries = GetProgressEntriesByUser(userId);

            if (!userEntries.Any())
                return "No progress entries found for this user.";

            var details = userEntries.Select(entry => entry.GetAllMetrics());
            return string.Join("\n\n", details);
        }
    }
}
