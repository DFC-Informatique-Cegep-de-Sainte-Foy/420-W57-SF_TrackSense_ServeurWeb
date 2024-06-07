using Microsoft.AspNetCore.Identity;

namespace TrackSense.API.Entities
{
    public class UserPlannedRide
    {
        public string UserLogin { get; set; } = string.Empty!;
        public string PlannedRideId { get; set; } = string.Empty!;
        public string? PlannedRideName { get; set; }
        //public TimeSpan AvgDuration { get; set; }
        public double Distance { get; set; }
    }
}
