using ProjectsManagement.WorkersHistory.Dto;

namespace ProjectsManagement.Web.Models.Home
{
    public class IndexHomeModalViewModel
    {
        public int UserTypeId { get; set; }
        public  WorkersHistoryDto WorkersHistoryDto { get; set; }
    }
}
