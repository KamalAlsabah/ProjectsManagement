using AutoMapper;

namespace ProjectsManagement.Suggestions.Dto
{
    public class SuggestionsMapProfile : Profile
    {
        public SuggestionsMapProfile()
        {


            CreateMap<ProjectsManagement.Suggestions.Dto.CreateSuggestionsDto, ProjectsManagement.ProjectDatabase.Suggestion.Suggestions>().ReverseMap();

            CreateMap<ProjectsManagement.Suggestions.Dto.SuggestionsDto, ProjectsManagement.ProjectDatabase.Suggestion.Suggestions>().ReverseMap();


            CreateMap<ProjectsManagement.Suggestions.Dto.EditSuggestionsDto, ProjectsManagement.ProjectDatabase.Suggestion.Suggestions>().ReverseMap();

            CreateMap<ProjectsManagement.Suggestions.Dto.ListSuggestionsDto, ProjectsManagement.ProjectDatabase.Suggestion.Suggestions>().ReverseMap();



            CreateMap<ProjectsManagement.Suggestions.Dto.UpDateInputSuggestionsDto, ProjectsManagement.ProjectDatabase.Suggestion.Suggestions>().ReverseMap();

        }
    }
}
