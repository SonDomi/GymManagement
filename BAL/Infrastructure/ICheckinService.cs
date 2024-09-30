using GYM_MANAGEMENT.BAL.DTOs.CheckSessionsDto;

namespace GYM_MANAGEMENT.BAL.Infrastructure
{
    public interface ICheckinService
    {
        // Task to retrieve information based on the CheckinDto
        Task<CheckinDto> GetInformation(CheckinDto checkinDto);
    }
}
