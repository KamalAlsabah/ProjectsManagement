﻿using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ProjectsManagement.Authorization
{
    public class ProjectsManagementAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            //For Project 

            context.CreatePermission(PermissionNames.Pages_Projects, L("Projects"));
            context.CreatePermission(PermissionNames.Pages_Projects_CreateProject, L("CreateProject"));
            context.CreatePermission(PermissionNames.Pages_Projects_DeleteProject, L("DeleteProject"));
            context.CreatePermission(PermissionNames.Pages_Projects_EditProject, L("EditProject"));

            //For Project worker
            context.CreatePermission(PermissionNames.Pages_ProjectsWorkers, L("ProjectsWorkers"));
            context.CreatePermission(PermissionNames.Pages_ProjectsWorkers_CreateProjectsWorkers, L("CreateProjectsWorkers"));
            context.CreatePermission(PermissionNames.Pages_ProjectsWorkers_EditProjectsWorkers, L("EditProjectsWorkers"));
            context.CreatePermission(PermissionNames.Pages_ProjectsWorkers_DeleteProjectsWorkers, L("DeleteProjectsWorkers"));

            //For Project supervisor
            context.CreatePermission(PermissionNames.Pages_ProjectsSupervisors, L("ProjectsSupervisors"));
            context.CreatePermission(PermissionNames.Pages_ProjectsSupervisors_CreateProjectSupervisors, L("CreateProjectSupervisors"));
            context.CreatePermission(PermissionNames.Pages_ProjectsSupervisors_EditProjectSupervisors, L("EditProjectSupervisors"));
            context.CreatePermission(PermissionNames.Pages_ProjectsSupervisors_DeleteProjectSupervisors, L("DeleteProjectSupervisors"));

            //For Jobs (Show & CUD)
            context.CreatePermission(PermissionNames.Pages_Jobs, L("Jobs"));
            context.CreatePermission(PermissionNames.Pages_Jobs_CreateJob, L("CreateJob"));
            context.CreatePermission(PermissionNames.Pages_Jobs_EditJob, L("EditJob"));
            context.CreatePermission(PermissionNames.Pages_Jobs_DeleteJob, L("DeleteJob"));

            //For Sprints (Show & CUD)
            context.CreatePermission(PermissionNames.Pages_Sprints, L("Sprint"));
            context.CreatePermission(PermissionNames.Pages_Sprints_CreateSprints, L("CreateSprint"));
            context.CreatePermission(PermissionNames.Pages_Sprints_EditSprints, L("EditSprint"));
            context.CreatePermission(PermissionNames.Pages_Sprints_DeleteSprints, L("DeleteSprint"));

            //JobTask (Show & CUD)
            context.CreatePermission(PermissionNames.Pages_JobTasks, L("JobTasks"));
            context.CreatePermission(PermissionNames.Pages_JobTasks_CreateJobTasks, L("CreateJobTasks"));
            context.CreatePermission(PermissionNames.Pages_JobTasks_EditJobTasks, L("EditJobTasks"));
            context.CreatePermission(PermissionNames.Pages_JobTasks_DeleteJobTasks, L("DeleteJobTasks"));

            //SupervisorNotes (Show & CUD)
            context.CreatePermission(PermissionNames.Pages_SupervisorNotes, L("SupervisorNotes"));
            context.CreatePermission(PermissionNames.Pages_SupervisorNotes_CreateSupervisorNotes, L("CreateSupervisorNotes"));
            context.CreatePermission(PermissionNames.Pages_SupervisorNotes_EditSupervisorNotes, L("EditSupervisorNotes"));
            context.CreatePermission(PermissionNames.Pages_SupervisorNotes_DeleteSupervisorNotes, L("DeleteSupervisorNotes"));

            //Suggestions (Show & CUD)
            context.CreatePermission(PermissionNames.Pages_Suggestions, L("Suggestions"));
            context.CreatePermission(PermissionNames.Pages_Suggestions_CreateSuggestions, L("CreateSuggestions"));
            context.CreatePermission(PermissionNames.Pages_Suggestions_EditSuggestions, L("EditSuggestions"));
            context.CreatePermission(PermissionNames.Pages_Suggestions_DeleteSuggestions, L("DeleteSuggestions"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ProjectsManagementConsts.LocalizationSourceName);
        }
    }
}
