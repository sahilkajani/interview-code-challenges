using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _context;

        public AuthorRepository(LibraryContext context)
        {
            _context = context;
        }

        public List<Author> GetAuthors()
        {
            return _context.Authors.ToList();
        }

        public Guid AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return author.Id;
        }
    }
}
