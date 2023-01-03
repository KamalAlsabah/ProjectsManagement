using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}