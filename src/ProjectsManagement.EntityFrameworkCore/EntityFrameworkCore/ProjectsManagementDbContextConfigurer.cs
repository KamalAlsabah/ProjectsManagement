using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ProjectsManagement.EntityFrameworkCore
{
    public static class ProjectsManagementDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ProjectsManagementDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ProjectsManagementDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
