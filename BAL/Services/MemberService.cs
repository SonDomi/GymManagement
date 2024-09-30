using GYM_MANAGEMENT.BAL.DTOs.MemberDtos;
using GYM_MANAGEMENT.BAL.Infrastructure;
using GYM_MANAGEMENT.DAL.Models;
using GYM_MANAGEMENT.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using System.Text;

namespace GYM_MANAGEMENT.BAL.Services
{
    public class MemberService : IMembersService
    {
        private readonly ApplicationDbContext db;
        public MemberService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task Create(AddMemberDto memberDto)
        {
            try
            {
                // Create a new member from the provided DTO
                var member = new Members
                {
                    FirstName = memberDto.FirstName,
                    LastName = memberDto.LastName,
                    Birthday = memberDto.Birthday,
                    IdCardNumber = memberDto.IdCardNumber,
                    Email = memberDto.Email,
                    RegistrationDate = memberDto.RegistrationDate.Date,
                    RegistrationCard = GenerateRandomCardCode()
                };
                // Check if member already exists
                var checkMember = db.Members.FirstOrDefault(x => x.IdCardNumber == memberDto.IdCardNumber);
                if (checkMember == null)
                {
                    db.Members.Add(member);
                    await db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("User with this ID already exists in the system.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<GridMemberDto>> GetForGrid()
        {
            try
            {
                var gridMember = new List<GridMemberDto>();
                // Retrieve all members from the database

                var membersFromDb = await db.Members.ToListAsync();

                // Map members to GridMemberDto
                foreach (var member in membersFromDb)
                {
                    var memberDto = new GridMemberDto
                    {
                        Id = member.Id,
                        FirstName = member.FirstName,
                        LastName = member.LastName,
                        Birthday = member.Birthday,
                        IdCardNumber = member.IdCardNumber,
                        Email = member.Email,
                        RegistrationDate = member.RegistrationDate.Date,
                        IsDeleted = member.IsDeleted,
                        RegistrationCard=member.RegistrationCard
                    };
                    gridMember.Add(memberDto);
                }
                // Return the list of members for grid display
                return gridMember;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<EditMemberDto> GetMemberForEditById(int id)
        {
            try
            {
                var editMemberDto = new EditMemberDto();
                // Retrieve member details for editing
                var members = await db.Members.SingleAsync(x => x.Id == id);

                // Map member details to EditMemberDto
                editMemberDto.Id = members.Id;
                editMemberDto.FirstName = members.FirstName;
                editMemberDto.LastName = members.LastName;
                editMemberDto.Email = members.Email;
                editMemberDto.IsDeleted = members.IsDeleted;

                return editMemberDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<EditMemberDto> Edit(EditMemberDto editMemberDto)
        {
            try
            {
                // Retrieve the member to edit
                var member = await db.Members.SingleAsync(x => x.Id == editMemberDto.Id);
                // Update member properties
                member.FirstName = editMemberDto.FirstName;
                member.LastName = editMemberDto.LastName;
                member.Email = editMemberDto.Email;
                member.IsDeleted = editMemberDto.IsDeleted;

                db.Members.Update(member);

                // Handle membership deletion if the member is marked as deleted
                if (editMemberDto.IsDeleted)
                {
                    var validMemberships = await db.MemberSubscriptions.
                        Where(x => x.MemberId == editMemberDto.Id && !x.IsDeleted && x.EndDate > DateTime.Now)
                       .ToListAsync();

                    foreach (var membership in validMemberships) 
                    {
                        membership.IsDeleted = true;
                    }
                    db.MemberSubscriptions.UpdateRange(validMemberships);
                }
                else
                {
                    // Restore previously deleted memberships if the member is not deleted
                    var validMemberships = await db.MemberSubscriptions.
                       Where(x => x.MemberId == editMemberDto.Id && x.IsDeleted && x.EndDate > DateTime.Now)
                      .ToListAsync();

                    foreach (var membership in validMemberships)
                    {
                        membership.IsDeleted = false;
                    }
                    db.MemberSubscriptions.UpdateRange(validMemberships);
                }

                await db.SaveChangesAsync();
                return editMemberDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<GridMemberDto>> GetActiveMember()
        {
            try
            {
                var gridMember = new List<GridMemberDto>();
                // Retrieve active members from the database
                var membersFromDb = await db.Members.Where(x => x.IsDeleted == false).ToListAsync();

                // Map active members to GridMemberDto
                foreach (var member in membersFromDb)
                {
                    var memberDto = new GridMemberDto
                    {
                        Id = member.Id,
                        FirstName = member.FirstName,
                        LastName = member.LastName,
                        Birthday = member.Birthday,
                        IdCardNumber = member.IdCardNumber,
                        Email = member.Email,
                        RegistrationDate = member.RegistrationDate.Date,
                        IsDeleted = member.IsDeleted
                    };
                    gridMember.Add(memberDto);
                }
                return gridMember;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string GenerateRandomCardCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "1234567890";
            StringBuilder result = new StringBuilder(7);
            Random random = new Random();
            for (int i = 0; i < 3; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            result.Append("-");
            for (int i = 0; i < 3; i++)
            {
                result.Append(numbers[random.Next(numbers.Length)]);
            }
            return result.ToString();
        }
    }
}
