using Microsoft.EntityFrameworkCore;
using TrackSense.API.Data;
using TrackSense.API.Entities;
using TrackSense.API.Entities.Interfaces;

namespace TrackSense.API.Services.ServiceRides
{
    public class DepotRides_MySQL : IDepotRides
    {
        private readonly TracksenseContext m_context;

        public DepotRides_MySQL(TracksenseContext tracksenseContext)
        {
            if(tracksenseContext == null)
            {
                throw new ArgumentNullException(nameof(m_context));
            }
            this.m_context = tracksenseContext;
        }


        public void AddCompletedRide(CompletedRide p_comletedRide)
        {
            if(p_comletedRide == null)
            {
                throw new ArgumentNullException(nameof(p_comletedRide));
            }

            // Add completedRide
            DTOs.CompletedRide completedRide = new DTOs.CompletedRide(p_comletedRide);

            m_context.Add(completedRide);
            m_context.SaveChanges();
            p_comletedRide.CompletedRidePoints = completedRide.ToEntity().CompletedRidePoints;
            m_context.ChangeTracker.Clear();


        }

        public void AddPlannedRide(PlannedRide p_plannedRide)
        {
            m_context.PlannedRides.Add(new DTOs.PlannedRide(p_plannedRide));

            m_context.SaveChanges();
            m_context.ChangeTracker.Clear();
        }

        public void AddCompletedRidePoint(CompletedRidePoint p_comletedRidePoint)
        {
            m_context.CompletedRidePoints.Add(new DTOs.CompletedRidePoint(p_comletedRidePoint));

            m_context.SaveChanges();
            m_context.ChangeTracker.Clear();
        }

        public void DeleteCompletedRideById(string p_completedRideId)
        {
            if (this.CompletedRideExist(p_completedRideId))
            {
                var RideToRemove = m_context.CompletedRides.Find(p_completedRideId);

                m_context.Remove(RideToRemove);
                m_context.SaveChanges();
                m_context.ChangeTracker.Clear();
            }
        }

        public void DeleteCompletedRidePoint(string p_completedRidePointId)
        {
            if (this.CompletedRideExist(p_completedRidePointId))
            {
                var PointsToRemove = m_context.CompletedRidePoints.Where( p => p.CompletedRideId== p_completedRidePointId);
                m_context.CompletedRidePoints.RemoveRange(PointsToRemove);
                m_context.SaveChanges();
                m_context.ChangeTracker.Clear();
            }
        }

        public void DeletePlannedRideById(string p_plannedRideId)
        {
            if (PlannedRideExist(p_plannedRideId))
            {
                m_context.Remove(m_context.PlannedRides.Find(p_plannedRideId));
                m_context.SaveChanges();
                m_context.ChangeTracker.Clear();
            }
        }

        public IEnumerable<CompletedRide> GetAllCompletedRidesByUser(string p_userLogin)
        {
            return m_context.CompletedRides
                    .Where(r => r.UserLogin == p_userLogin)
                    .Select(r => r.ToEntity());
        }

        public IEnumerable<PlannedRide> GetAllPlannedRidesByUser(string p_userLogin)
        {
            return m_context.PlannedRides
                    .Where(r => r.UserLogin == p_userLogin)
                    .Select(r => r.ToEntity());
        }

        public PlannedRideStatistics? GetPlannedRideStatisticsById(string p_plannedRideId)
        {
            return m_context.PlannedRideStatistics.Find(p_plannedRideId)?.ToEntity();
                    
        }

        public Entities.CompletedRide? GetCompletedRideById(string p_completedRideId)
        {
            DTOs.CompletedRide? completedRideDTO = m_context.CompletedRides.Where(r => r.CompletedRideId == p_completedRideId)

                                                                            .Include(r => r.CompletedRidePoints)
                                                                                    .ThenInclude(r => r.Location)

                                                                            //.Include(r => r.CompletedRideStatistic )

                                                                             .Include(r => r.PlannedRide)
                                                                                   .ThenInclude(r=> r.PlannedRidePoints)
                                                                                   .ThenInclude(r => r.Location)

                                                                             .SingleOrDefault();
            if(completedRideDTO != null)
            {

                DTOs.CompletedRideStatistic? completedRideStatistic = m_context.CompletedRideStatistics.SingleOrDefault(s => s.CompletedRideId == p_completedRideId
                                                                                                                         && s.UserLogin == completedRideDTO.UserLogin);

                completedRideDTO.CompletedRideStatistic = completedRideStatistic;

                return completedRideDTO.ToEntity();
            }
            return null;
            

        }

        public IEnumerable<CompletedRidePoint>? GetCompletedRidePointById(string p_completedRidePointId)
        {
            List<CompletedRidePoint> completedRidePoint = m_context.CompletedRidePoints.Where(p => p.CompletedRideId == p_completedRidePointId)
                                                                                       .Select(p => p.ToEntity())
                                                                                       .ToList();

            completedRidePoint.ForEach(p => p.Location = this.GetLocationById(p.LocationId)!);

            return completedRidePoint;

        }

        public CompletedRideStatistics? GetCompletedRideStatistics(string p_completedRideId)
        {
            return m_context.CompletedRideStatistics?.Find(p_completedRideId)?.ToEntity();
        }

        public Location? GetLocationById(int p_locationId)
        {
            return m_context.Locations.Find(p_locationId)?.ToEntity();
        }

        public PlannedRide? GetPlannedRideById(string p_plannedRideId)
        {
            DTOs.PlannedRide? plannedRideDTO = m_context.PlannedRides.Where(r => r.PlannedRideId == p_plannedRideId)
                                                                      .Include(r => r.PlannedRidePoints)
                                                                      .ThenInclude(r => r.Location)
                                                                      .SingleOrDefault();

            return plannedRideDTO?.ToEntity();
        }

  
        public IEnumerable<DTOs.PlannedRidePoint> ? GetPlannedRidePointsDTOById(string p_plannedRideId)
        {
            return m_context.PlannedRidePoints.Where(p => p.PlannedRideId == p_plannedRideId);
        }
        public DTOs.Location ?GetLocationDTOById(int p_locationId)
        {
            return m_context.Locations.Find(p_locationId);
        }

        public void UpdateCompletedRide(CompletedRide p_completedRide)
        {
            if (this.CompletedRideExist(p_completedRide.CompletedRideId))
            {

            }
        }

        public void UpdateCompletedRidePoint(CompletedRidePoint p_completedRidePoint)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlannedRide(PlannedRide p_plannedRide)
        {
            throw new NotImplementedException();
        }

        public void AddLocation(Location p_location)
        {
            m_context.Locations.Add(new DTOs.Location(p_location));
            m_context.SaveChanges();
            m_context.ChangeTracker.Clear();
        }
        private bool PlannedRideExist(string p_plannedRideId)
        {
            return m_context.PlannedRides.Any(r => r.PlannedRideId == p_plannedRideId);
        }

       
        private bool CompletedRideExist(string p_CompletedRideId)
        {
            return m_context.CompletedRides.Any(r => r.CompletedRideId == p_CompletedRideId);
        }

        public void AddPlannedRidePoint(PlannedRidePoint p_plannedRidePoint)
        {
            m_context.PlannedRidePoints.Add(new DTOs.PlannedRidePoint(p_plannedRidePoint));
            m_context.SaveChanges();
            m_context.ChangeTracker.Clear();
        }
    }
}
