namespace GYM_MANAGEMENT.BAL.Infrastructure
{
    public interface IClock
    {
        //provide the current date and time
        public DateTime Now { get; }
    }
}
