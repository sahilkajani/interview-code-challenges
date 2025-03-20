namespace OneBeyondApi.DataAccess.Fine
{
    public interface IFineRepository
    {
        Task AddFineAsync(OneBeyondApi.Model.Fine fine);
    }
}
