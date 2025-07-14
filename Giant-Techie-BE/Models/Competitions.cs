namespace Giant_Techie_BE.Models
{
    public sealed class Competitions : EntityBase
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public string Status { get; private set; }


        private Competitions()
        {
            Title = string.Empty; 
            Description = string.Empty;
        }
        private Competitions(string title, string description, DateTimeOffset StartDate, DateTimeOffset EndDate, string status)
        {
            Title = title;
            Description = description;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            Status = status;
        }

        public static Competitions Create(string title, string description, DateTimeOffset startDate, DateTimeOffset endDate, string status)
        {
            ValidateInputs(title, description, startDate, endDate, status);
            return new Competitions(title, description, startDate, endDate, status);
        }
        public void Update(string title, string description, DateTimeOffset startDate, DateTimeOffset endDate, string status)
        {
            ValidateInputs(title, description, startDate, endDate, status);

            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;

            UpdateLastModified();
        }

        private static void ValidateInputs(string title, string description, DateTimeOffset startDate, DateTimeOffset endDate, string status)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));

            if (startDate > endDate)
                throw new ArgumentException("Start date cannot be greater than endDate date.", nameof(startDate));


            if (endDate < DateTimeOffset.UtcNow || endDate < startDate)
                throw new ArgumentException("End date cannot be less than current date.", nameof(endDate));

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status Of Competition is Required", nameof(status));
        }
    }
}
