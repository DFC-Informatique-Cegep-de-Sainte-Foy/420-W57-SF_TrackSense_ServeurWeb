using Microsoft.EntityFrameworkCore;
using TrackSense.API.Services.DTOs;

namespace TrackSense.API.Data;

public partial class TracksenseContext : DbContext
{
    public TracksenseContext()
    {
    }

    public TracksenseContext(DbContextOptions<TracksenseContext> options)
        : base(options)
    {
    }
    public virtual DbSet<UserCompletedRide> UserCompletedRides { get; set; }

    public virtual DbSet<UserPlannedRide> UserPlannedRides { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<ApplicationToken> ApplicationTokens { get; set; }

    public virtual DbSet<CompletedRide> CompletedRides { get; set; }

    public virtual DbSet<CompletedRidePoint> CompletedRidePoints { get; set; }

    public virtual DbSet<CompletedRideStatistic> CompletedRideStatistics { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Credential> Credentials { get; set; }

    public virtual DbSet<InterestPoint> InterestPoints { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<PlannedRide> PlannedRides { get; set; }

    public virtual DbSet<PlannedRidePoint> PlannedRidePoints { get; set; }

    public virtual DbSet<PlannedRideStatistic> PlannedRideStatistics { get; set; }

    public virtual DbSet<Tracksense> Tracksenses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserStatistic> UserStatistics { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CompletedRideStatistic>()
            .Property(e => e.Duration)
            .HasColumnType("TIME");

        modelBuilder.Entity<UserStatistic>()
            .Property(e => e.Duration)
            .HasColumnType("TIME");
    }

}


