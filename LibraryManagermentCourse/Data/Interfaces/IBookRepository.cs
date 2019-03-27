using LibraryManagermentCourse.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagermentCourse.Data.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetAllBookWithAuthor();
        IEnumerable<Book> FindWithAuthor(Func<Book, bool> predicate);
        IEnumerable<Book> FindWithAuthorAndBorrower(Func<Book, bool> predicate);
    }
}
