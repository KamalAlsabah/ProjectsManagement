using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Users;
using ProjectsManagement.WorkersHistory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersHistory
{
    public class WorkersHistoryAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.WorkersHistory.WorkersHistory, WorkersHistoryDto, long, WorkersHistoryPagedDto, WorkersHistoryCreateDto, UpdateInputDto>, IWorkersHistoryAppService
    {
        private readonly IRepository<ProjectsManagement.ProjectDatabase.WorkersHistory.WorkersHistory, long> _WorkersHistoryrepository;
        private readonly IRepository<ProjectsManagement.Authorization.Users.User, long> _Usersrepository;
        private readonly IHttpContextAccessor _accessor;

        public WorkersHistoryAppService(
            IRepository<ProjectsManagement.ProjectDatabase.WorkersHistory.WorkersHistory, long> repository,
            IRepository<ProjectsManagement.Authorization.Users.User, long> Usersrepository,
           IHttpContextAccessor accessor) : base(repository)
        {
            _WorkersHistoryrepository = repository;
            _Usersrepository = Usersrepository;
            _accessor = accessor;
        }

        public override async Task<PagedResultDto<WorkersHistoryDto>> GetAllAsync(WorkersHistoryPagedDto input)
        {

            var listWorkersHistory = _WorkersHistoryrepository.GetAll()
                .Include(x => x.Worker)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Worker.Name.Contains(input.Keyword));
            var items = ObjectMapper.Map<List<WorkersHistoryDto>>(listWorkersHistory
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount));
            return new PagedResultDto<WorkersHistoryDto>()
            {
                Items = items,
                TotalCount = listWorkersHistory.Count()
            };
        }
        public override async Task<WorkersHistoryDto> CreateAsync(WorkersHistoryCreateDto input)
        {
            return await base.CreateAsync(input);
        }
        public async Task<WorkersHistoryEditDto> GetWorkersHistoryForEdit(EntityDto input)
        {
            var WorkersHistory = await _WorkersHistoryrepository.GetAll().Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            var model = ObjectMapper.Map<WorkersHistoryEditDto>(WorkersHistory);

            return model;
        }

        public override async Task<WorkersHistoryDto> UpdateAsync(UpdateInputDto input)
        {
            return await base.UpdateAsync(input);
        }
        public override async Task DeleteAsync(EntityDto<long> input)
        {

            await base.DeleteAsync(input);
        }

        public async Task<WorkersHistoryDto> GetHistoryByUserId(long UserId)
        {
             var History=await _WorkersHistoryrepository.GetAll().Where(x=>x.WorkerId==UserId).OrderBy(x=>x.Id).LastOrDefaultAsync();
            return ObjectMapper.Map<WorkersHistoryDto>(History);
        }
        WorkersHistoryCreateDto workersHistoryCreate = new WorkersHistoryCreateDto();

        public async Task CreateHistory(bool input)
        {
            var userId = long.Parse(_accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _Usersrepository.GetAll().Where(x => x.Id == userId).FirstOrDefault();


            if (input== true)
            {
                workersHistoryCreate.WorkerId = userId;
                workersHistoryCreate.LogInTime = DateTime.Now;
                workersHistoryCreate.TotalHours = 0;
                await CreateAsync(workersHistoryCreate) ;
                user.IsOnine = true;
                _Usersrepository.Update(user);

            }
            else
            {
                var exsitedUserHistroy = await GetHistoryByUserId(userId);
                var model = ObjectMapper.Map<UpdateInputDto>(exsitedUserHistroy);
                model.LogOutTime = DateTime.Now;
                await UpdateAsync(model);
                user.IsOnine = false;
                _Usersrepository.Update(user);
            }
        }

        public async Task<bool> IsUserOnline()
        {
            var userId = long.Parse(_accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _Usersrepository.GetAll().Where(x => x.Id == userId).FirstOrDefault();
            return user.IsOnine;
        }
    }
}