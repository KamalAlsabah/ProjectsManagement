using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper;
using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsManagement.Home.HomeStatistics.Dto;
using Abp.Collections.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProjectsManagement.Authorization.Roles;

namespace ProjectsManagement.Home.HomeStatistics
{
    public class HomeStatisticsAppService : AsyncCrudAppService<ProjectDatabase.Home.HomeStatistics, HomeStatisticsDto, long, PagedHomeStatisticsResultRequestDto, CreateHomeStatisticsDto, UpdateInputDto>, IHomeStatisticsAppService
    {
        private readonly IRepository<ProjectDatabase.Home.HomeStatistics, long> _HomeStatisticsrepository;
        private readonly IRepository<ProjectDatabase.Home.HomeStatisticsUserTypes, long> _HomeStatisticsUserTypesrepository;
        private readonly RoleManager _roleManager;

        public HomeStatisticsAppService(IRepository<ProjectDatabase.Home.HomeStatistics, long> repository, 
            RoleManager roleManager, IRepository<ProjectDatabase.Home.HomeStatisticsUserTypes, long> homeStatisticsUserTypesrepository) : base(repository)
        {
            _HomeStatisticsrepository = repository;
            _roleManager = roleManager;
            _HomeStatisticsUserTypesrepository = homeStatisticsUserTypesrepository;
        }

        public override async Task<PagedResultDto<HomeStatisticsDto>> GetAllAsync(PagedHomeStatisticsResultRequestDto input)
        {
            var listHomeStatistics = _HomeStatisticsrepository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));
            return new PagedResultDto<HomeStatisticsDto>()
            {
                Items = ObjectMapper.Map<List<HomeStatisticsDto>>(listHomeStatistics
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)),
                TotalCount = listHomeStatistics.Count()
            };
        }
        public override async Task<HomeStatisticsDto> CreateAsync(CreateHomeStatisticsDto input)
        {
            var model = await base.CreateAsync(input);

            foreach (var item in input.UserTypesId)
            {
                await _HomeStatisticsUserTypesrepository.InsertAsync(new ProjectDatabase.Home.HomeStatisticsUserTypes()
                {
                 UserTypeId=item,
                 HomeStatisticsId=model.Id
                });
            }
            return model;
        }
        public override async Task<HomeStatisticsDto> UpdateAsync(UpdateInputDto input)
        {
            var model = await base.UpdateAsync(input);
            await _HomeStatisticsUserTypesrepository
                .DeleteAsync(x => x.HomeStatisticsId == model.Id);
            foreach (var item in input.UserTypes)
            {
                await _HomeStatisticsUserTypesrepository.InsertAsync(new ProjectDatabase.Home.HomeStatisticsUserTypes()
                {
                    UserTypeId = item,
                    HomeStatisticsId = model.Id
                });
            }
            return model;
        }
        public async Task<List<NameValueDto<int>>> GetUserTypes()
        {
            return await _roleManager.Roles.Select(x => new NameValueDto<int> { Name = x.Name, Value = x.Id }).ToListAsync();
        }
        public async Task<EditHomeStatisticsDto> GetHomeStatisticsForEdit(EntityDto input)
        {
            var HomeStatistics = await _HomeStatisticsrepository.GetAll().Where(x => x.Id == input.Id).Include(x=>x.UserTypes).FirstOrDefaultAsync();
            var model = ObjectMapper.Map<EditHomeStatisticsDto>(HomeStatistics);
            model.UserTypes =await _roleManager.Roles.Select(x=>new NameValueDto<int> {Name=x.Name,Value=x.Id }).ToListAsync();
            model.GrantedUserTypes =HomeStatistics.UserTypes.Select(x=>x.UserTypeId).ToList();
            return model;
        }
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            await base.DeleteAsync(input);
        }
    }
}
