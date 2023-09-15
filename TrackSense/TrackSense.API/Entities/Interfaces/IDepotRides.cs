﻿using System.Globalization;
using TrackSense.API.Services.DTOs;

namespace TrackSense.API.Entities.Interfaces
{
    public interface IDepotRides
    {
        // CompletedRide
        IEnumerable<CompletedRide> GetAllCompletedRidesByUser(string p_userLogin);
        CompletedRide? GetCompletedRideById(string p_completedRideId);
        void UpdateCompletedRide(CompletedRide p_completedRide);
        void DeleteCompletedRideById(string p_completedRideId);
        void AddCompletedRide(CompletedRide p_comletedRide);

        //CompletedRideStatisitics
        CompletedRideStatistics? GetCompletedRideStatistics(string p_completedRideId);
        void AddCompletedRideStatistics(CompletedRideStatistics p_completedRideStatistic);

        //CompletedRidePoints
        void AddCompletedRidePoint(CompletedRidePoint p_comletedRidePoint);
        void UpdateCompletedRidePoint(CompletedRidePoint p_completedRidePoint);
        void DeleteCompletedRidePoint(string p_completedRidePointId);

        // PlannedRide
        IEnumerable<PlannedRide> GetAllPlannedRidesByUser(string p_userLogin);
        PlannedRide? GetPlannedRideById(string p_completedRideId);
        void UpdatePlannedRide(PlannedRide p_plannedRide);
        void DeletePlannedRideById(string p_plannedRideId);
        void AddPlannedRide(PlannedRide p_plannedRide);
        PlannedRideStatistics? GetAllPlannedRideStatistics(string p_plannedRideId);
    }
}
