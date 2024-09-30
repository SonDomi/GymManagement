using GYM_MANAGEMENT.BAL.Infrastructure;

namespace GYM_MANAGEMENT.BAL.DTOs.TimeDto
{
    public class SystemClock : IClock
    {
        DateTime IClock.Now { get { return DateTime.Now; } }
    }
}
