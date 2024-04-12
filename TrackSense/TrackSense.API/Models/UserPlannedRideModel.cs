using TrackSense.API.Entities;

namespace TrackSense.API.Models
{
    public class UserPlannedRideModel
    {
        public string PlannedRideId { get; set; }
        public string? PlannedRideName { get; set; }
        //public TimeSpan AvgDuration { get; set; }
        public double Distance { get; set; }

        public UserPlannedRideModel()
        {
            ;
        }

        public UserPlannedRideModel(UserPlannedRide p_userPlannedRides)
        {
            this.PlannedRideId = p_userPlannedRides.PlannedRideId;
            this.PlannedRideName = p_userPlannedRides.PlannedRideName;
            //this.AvgDuration = p_userPlannedRides.AvgDuration;
            this.Distance = p_userPlannedRides.Distance;
        }

        public UserPlannedRide ToEntity()
        {
            return new UserPlannedRide()
            {
                PlannedRideId = this.PlannedRideId,
                PlannedRideName = this.PlannedRideName,
                //AvgDuration = this.AvgDuration,
                Distance = this.Distance
            };
        }
    }
}
