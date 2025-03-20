using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class CatalogueRepository : ICatalogueRepository
    {
        private readonly LibraryContext _context;

        public CatalogueRepository(LibraryContext context)
        {
            _context = context;
        }

        public List<BookStock> GetCatalogue()
        {
            return _context.Catalogue
                .Include(x => x.Book)
                .ThenInclude(x => x.Author)
                .Include(x => x.OnLoanTo)
                .Include(x => x.Reservations)
                .ToList();
        }

        public List<BookStock> SearchCatalogue(CatalogueSearch search)
        {
            return _context.Catalogue
                .Include(x => x.Book)
                .ThenInclude(x => x.Author)
                .Include(x => x.OnLoanTo)
                .Include(x => x.Reservations)
                .ToList();
        }
    }
}
