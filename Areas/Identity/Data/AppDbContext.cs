using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnRodeassisment.Models;

namespace OnRodeassisment.Areas.Identity.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
    public DbSet<Customer_CareModel> CustomerCare { get; set; }
    public DbSet<CustomerModel> Customer { get; set; }
    public DbSet<ComplaintModel> Complaint { get; set; }
    public DbSet<NotificationsModel> Notifications { get; set; }
    public DbSet<Services_JobModel> Services_Job { get; set; }
    public DbSet<Technician_JobModel> Technician_Job { get; set; }
    public DbSet<Technician_InfoModel> Technician { get; set; }
    public DbSet<Complaint_ClosedModel> Complaint_Closed { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Configure relationships using Fluent API
        builder.Entity<ComplaintModel>()
            .HasOne(c => c.Customer)
            .WithMany(cu => cu.Complaints)
            .HasForeignKey(c => c.CustomerId);

        builder.Entity<NotificationsModel>()
            .HasOne(n => n.Customer)
            .WithMany(c => c.Notifications)
            .HasForeignKey(n => n.CustomerId);

        builder.Entity<NotificationsModel>()
            .HasOne(n => n.Technician)
            .WithMany(t => t.Notifications)
            .HasForeignKey(n => n.TechnicianId);

        builder.Entity<Services_JobModel>()
            .HasOne(s => s.Complaint)
            .WithMany(c => c.Services_Jobs)
            .HasForeignKey(s => s.ComplaintId);

        builder.Entity<Services_JobModel>()
            .HasOne(s => s.Technician)
            .WithMany(t => t.Services_Jobs)
            .HasForeignKey(s => s.TechnicianId);

        builder.Entity<Technician_JobModel>()
            .HasOne(tj => tj.Complaint)
            .WithMany(c => c.Technician_Jobs)
            .HasForeignKey(tj => tj.ComplaintId);

        builder.Entity<Technician_JobModel>()
            .HasOne(tj => tj.Technician)
            .WithMany(t => t.Technician_Jobs)
            .HasForeignKey(tj => tj.TechnicianId);

        builder.Entity<Complaint_ClosedModel>()
            .HasOne(cc => cc.Services_Job)
            .WithMany(sj => sj.Complaint_Closed)
            .HasForeignKey(cc => cc.ServicesId);        
    }
}
