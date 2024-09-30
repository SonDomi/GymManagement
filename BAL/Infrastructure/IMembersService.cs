using GYM_MANAGEMENT.BAL.DTOs.MemberDtos;
using GYM_MANAGEMENT.DAL.Models;

namespace GYM_MANAGEMENT.BAL.Infrastructure
{
    public interface IMembersService
    {
        // Task to create a new member
        Task Create(AddMemberDto addMemberDto);
        // Task to retrieve a list of members for grid display
        Task<List<GridMemberDto>> GetForGrid();
        // Task to update an existing member
        Task<EditMemberDto> Edit(EditMemberDto editMemberDto);
        // Task to retrieve a member's details for editing by their ID
        Task<EditMemberDto> GetMemberForEditById(int id);
        // Task to retrieve a list of active members
        Task<List<GridMemberDto>> GetActiveMember();
    }
}
