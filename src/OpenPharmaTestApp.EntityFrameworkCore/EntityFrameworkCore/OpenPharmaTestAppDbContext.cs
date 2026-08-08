using Microsoft.EntityFrameworkCore;
using OpenPharmaTestApp.TasksList;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace OpenPharmaTestApp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class OpenPharmaTestAppDbContext :
    AbpDbContext<OpenPharmaTestAppDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
   
    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public DbSet<Customer> Customers { get; set; }
    public DbSet<TaskList> TaskLists { get; set; }
    public DbSet<CustomerTaskList> CustomerTaskLists { get; set; }


    public OpenPharmaTestAppDbContext(DbContextOptions<OpenPharmaTestAppDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        var dBTablePrefix = "OpenPharma";

        builder.Entity<Customer>(b =>
        {
            b.ToTable($"{dBTablePrefix}Customers");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(50);
        });

        builder.Entity<TaskList>(b =>
        {
            b.ToTable($"{dBTablePrefix}TaskLists");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(256);
        });

        builder.Entity<CustomerTaskList>(b =>
        {
            b.ToTable($"{dBTablePrefix}CustomerTaskLists");
            b.ConfigureByConvention();
            b.HasKey(x => new { x.CustomerId, x.TaskListId });

            b.HasOne(x => x.Customer)
             .WithMany(x => x.CustomerTaskLists)
             .HasForeignKey(x => x.CustomerId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.TaskList)
             .WithMany(x => x.CustomerTaskLists)
             .HasForeignKey(x => x.TaskListId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
        });

    }
}
