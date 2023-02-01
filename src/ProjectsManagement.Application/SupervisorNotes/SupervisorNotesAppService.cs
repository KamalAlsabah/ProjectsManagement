using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.SupervisorNotes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectsManagement.SupervisorNotes
{

    public class SupervisorNotesAppService: AsyncCrudAppService<ProjectsManagement.ProjectDatabase.SupervisorNotes.SupervisorNotes, SupervisorNotesDto, long, PagedSupervisorNotesResultRequestDto, CreateSupervisorNotesDto, UpDateInputSupervisorNotesDto>, ISupervisorNotesAppService
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.SupervisorNotes.SupervisorNotes, long> _supervisorNotesrepository;
        public SupervisorNotesAppService(
            IHttpContextAccessor accessor ,
            IRepository<ProjectsManagement.ProjectDatabase.SupervisorNotes.SupervisorNotes, long> repository) : base(repository)
        {
            this.accessor = accessor;
            _supervisorNotesrepository = repository;
        }

        public override async Task<PagedResultDto<SupervisorNotesDto>> GetAllAsync(PagedSupervisorNotesResultRequestDto input)
        {
            try
            {
                var listNotes = _supervisorNotesrepository.GetAll().Where(x => x.JobId == input.JobId)
                    .OrderByDescending(s => s.CreationTime)
                .Include(x => x.Job)
                .Include(x=>x.Supervisor)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Note.Contains(input.Keyword));
                return new PagedResultDto<SupervisorNotesDto>()
                {
                    Items = ObjectMapper.Map<List<SupervisorNotesDto>>(listNotes
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)),
                    TotalCount = listNotes.Count()
                };
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("error");
            }

        }


        public override async Task<SupervisorNotesDto> CreateAsync(CreateSupervisorNotesDto input)
        {
            var projectClosed = await _supervisorNotesrepository.GetAll().Where(x => x.JobId == input.JobId).Select(x => x.Job.Project.Status).FirstOrDefaultAsync();
            if (projectClosed==ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            input.SupervisorId = long.Parse(accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            
            return await base.CreateAsync(input);
        }

        public async Task<EditSupervisorNotesDto> GetSupervisorNotesForEdit(EntityDto<long> input)
        {
            var Note = _supervisorNotesrepository.Get(input.Id);
            var model = ObjectMapper.Map<EditSupervisorNotesDto>(Note);

            return model;
        }

        public override async Task<SupervisorNotesDto> UpdateAsync(UpDateInputSupervisorNotesDto input)
        {
            var projectClosed = await _supervisorNotesrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Job.Project.Status).FirstOrDefaultAsync();
            if (projectClosed==ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.UpdateAsync(input);
        }
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var projectClosed = await _supervisorNotesrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Job.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
             await base.DeleteAsync(input);
        }
    }
}
