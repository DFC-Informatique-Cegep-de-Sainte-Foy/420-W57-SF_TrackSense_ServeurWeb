using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackSense.API.Services.DTOs
{
    [Keyless]
    [Table("UserPlannedRide")]
    public class UserPlannedRide
    {
        public string UserLogin { get; set; }
        public string PlannedRideId { get; set; }
        public string? PlannedRideName { get; set; }
        //public TimeSpan AvgDuration { get; set; }
        public double Distance { get; set; }
        public UserPlannedRide()
        {
            
        }
        public Entities.UserPlannedRide ToEntity()
        {
            return new Entities.UserPlannedRide()
            {
                UserLogin = this.UserLogin,
                PlannedRideId = this.PlannedRideId,
                PlannedRideName = this.PlannedRideName,
                //AvgDuration = this.AvgDuration,
                Distance = this.Distance
            };
        }
    }
    
}
