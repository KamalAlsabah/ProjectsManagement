using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ProjectsManagement.Authorization.Roles;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.MultiTenancy;
using ProjectsManagement.ProjectDatabase.Job;
using ProjectsManagement.ProjectDatabase.JobTask;
using ProjectsManagement.ProjectDatabase.Project;
using ProjectsManagement.ProjectDatabase.ProjectSupervisor;
using ProjectsManagement.ProjectDatabase.ProjectWorker;
using ProjectsManagement.ProjectDatabase.Sprint;
using ProjectsManagement.ProjectDatabase.Suggestion;
using ProjectsManagement.ProjectDatabase.SupervisorNotes;
using ProjectsManagement.ProjectDatabase.Home;
using ProjectsManagement.ProjectDatabase.ProjectHistory;
using ProjectsManagement.ProjectDatabase.WorkersHistory;

namespace ProjectsManagement.EntityFrameworkCore
{
    public class ProjectsManagementDbContext : AbpZeroDbContext<Tenant, Role, User, ProjectsManagementDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public ProjectsManagementDbContext(DbContextOptions<ProjectsManagementDbContext> options)
            : base(options)
        {
        }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<JobTasks> JobTasks { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<ProjectSupervisors> ProjectSupervisors { get; set; }
        public DbSet<ProjectWorkers> ProjectWorkers { get; set; }
        public DbSet<Sprints> Sprints { get; set; }
        public DbSet<Suggestions> Suggestions { get; set; }

        public DbSet<SupervisorNotes> SupervisorNotes { get; set; }
        public DbSet<HomeStatistics> HomeStatistics { get; set; }
        public DbSet<HomeStatisticsUserTypes> HomeStatisticsUserTypes { get; set; }
        public DbSet<ProjectHistory> ProjectHistory { get; set; }
        public DbSet<WorkersHistory> WorkersHistory { get; set; }
    }
}
