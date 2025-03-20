namespace OneBeyondApi.DataAccess.Fine
{
    public class FineRepository : IFineRepository
    {
        private readonly LibraryContext _context;

        public FineRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task AddFineAsync(OneBeyondApi.Model.Fine fine)
        {
            _context.Fines.Add(fine);
            await _context.SaveChangesAsync();
        }
    }
}
