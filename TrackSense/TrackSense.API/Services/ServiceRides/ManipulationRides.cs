using TrackSense.API.Entities;
using TrackSense.API.Entities.Interfaces;

namespace TrackSense.API.Services.ServiceRides
{
    public class ManipulationRides
    {
        private IDepotRides m_depotRides;

        public ManipulationRides(IDepotRides p_depotRides)
        {
            if (p_depotRides == null)
            {
                throw new ArgumentNullException();
            }

            this.m_depotRides = p_depotRides;
        }

        public void AddCompletedRide(CompletedRide p_completedRide)
        {
            if (m_depotRides == null)
            {
                throw new ArgumentNullException("CompletedRide ne doit pas etre null - ManipulationRides.AddCompletedRide");
            }

            if (String.IsNullOrEmpty(p_completedRide.UserLogin))
            {
                throw new ArgumentNullException($"{nameof(p_completedRide.UserLogin)} ne doit pas etre null ni vide - ManipulationRides.AddCompletedRide");
            }

            this.m_depotRides.AddCompletedRide(p_completedRide);
        }

        public void AddPlannedRide(PlannedRide p_plannedRide)
        {
            if(m_depotRides != null)
            {
                throw new ArgumentNullException($"PlannedRide ne doit pas etre null - ManipulationRides.AddPlannedRide");
            }

            if (String.IsNullOrEmpty(p_plannedRide.UserLogin))
            {
                throw new ArgumentNullException($"{nameof(p_plannedRide.UserLogin)} ne doit pas etre null ni vide - ManipulationRides.AddPlannedRide");
            }

            this.m_depotRides.AddPlannedRide(p_plannedRide);
        }

        public CompletedRide? GetCompletedRideById(string p_completedRideId)
        {
            return this.m_depotRides.GetCompletedRideById(p_completedRideId);
        }

        public PlannedRide? GetPlannedRideById(string p_plannedRideId)
        {
            return this.m_depotRides.GetPlannedRideById(p_plannedRideId);
        }
    }
}
