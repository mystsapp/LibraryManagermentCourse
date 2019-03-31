using LibraryManagermentCourse.Data.Interfaces;
using LibraryManagermentCourse.Data.Model;
using LibraryManagermentCourse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagermentCourse.Controllers
{
    public class BookController: Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        [Route("Book")]
        public IActionResult List(int? authorId, int? borrowId)
        {
            if(authorId == null && borrowId == null)
            {
                //show all book
                var books = _bookRepository.GetAllBookWithAuthor();
                // check book
                return CheckBook(books);
            }
            else if(authorId != null)
            {
                //filter by authoId
                var author = _authorRepository.GetWithBooks((int)authorId);
                // check author books
                if (author.Books.Count() == 0)
                    return View("AuthorEmpty", author);
                else
                    return View(author.Books);
            }
            else if (borrowId != null)
            {
                //filter by borrowId
                var books = _bookRepository.FindWithAuthorAndBorrower(book => book.BorrowerId == borrowId);
                // check borrower book
                return CheckBook(books);
            }
            else
            {
                //throw exception
                throw new ArgumentException();
            }
        }

        public IActionResult CheckBook(IEnumerable<Book> books)
        {
            if (books.Count() == 0)
                return View("Empty");
            else
                return View(books);
        }

        public IActionResult Create()
        {
            var bookVM = new BookViewModel()
            {
                Authors = _authorRepository.GetAll()
            };
            return View(bookVM);
        }

        [HttpPost]
        public IActionResult Create(BookViewModel bookViewModel)
        {
            _bookRepository.Create(bookViewModel.Book);
            return RedirectToAction("List");
        }

        public IActionResult Update(int id)
        {
            var bookVM = new BookViewModel()
            {
                Book = _bookRepository.GetById(id),
                Authors = _authorRepository.GetAll()
            };
            return View(bookVM);
        }

        [HttpPost]
        public IActionResult Update(BookViewModel bookViewModel)
        {
            _bookRepository.Update(bookViewModel.Book);
            return View("List");
        }

        public IActionResult Delete(int id)
        {
            var book = _bookRepository.GetById(id);
            _bookRepository.Delete(book);
            return View("List");
        }
    }
}
