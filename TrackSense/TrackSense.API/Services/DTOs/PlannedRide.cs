using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TrackSense.API.Services.DTOs;
[Table("PlannedRide")]
public class PlannedRide
{
    [Key]
    public string PlannedRideId { get; set; }

    public string UserLogin { get; set; } = null!;

    public string? Name { get; set; }

    public bool? IsFavorite { get; set; }

    public virtual ICollection<CompletedRide> CompletedRides { get; set; }

    public virtual ICollection <PlannedRidePoint> PlannedRidePoints { get; set; }

    public virtual PlannedRideStatistic PlannedRideStatistic { get; set; }

    [ForeignKey(nameof(UserLogin))]	
    public virtual User User { get; set; }

    public PlannedRide()
    {
        
    }
    public PlannedRide(Entities.PlannedRide p_plannedRide)
    {
        if (string.IsNullOrEmpty(p_plannedRide.UserLogin))
        {
            throw new ArgumentNullException(nameof(p_plannedRide.UserLogin));
        }

        if (p_plannedRide.PlannedRideId == "")
        {
            throw new ArgumentNullException(nameof(p_plannedRide.PlannedRideId));
        }

        this.UserLogin = p_plannedRide.UserLogin;
        this.PlannedRideId = p_plannedRide.PlannedRideId;
        this.Name = p_plannedRide.Name;
        this.IsFavorite = p_plannedRide.IsFavorite;
        this.PlannedRidePoints = p_plannedRide.PlannedRidePoints
                                                .Select(p => new DTOs.PlannedRidePoint(p))
                                                .ToList();
    }

    public Entities.PlannedRide ToEntity()
    {
        return new Entities.PlannedRide()
        {

            PlannedRideId = this.PlannedRideId,
            PlannedRidePoints = this.PlannedRidePoints.Select(p => p.ToEntity()).ToList(),
            UserLogin = this.UserLogin,
            Name = this.Name,
            IsFavorite = this.IsFavorite,
        };
    }
}
