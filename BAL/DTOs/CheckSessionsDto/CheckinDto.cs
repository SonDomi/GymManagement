namespace GYM_MANAGEMENT.BAL.DTOs.CheckSessionsDto
{
    public class CheckinDto
    {
        public string Code { get; set; }
        public string Info { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public int RemainingSessions { get; set; }
        public bool IsError { get; set; } = false;
    }
}
