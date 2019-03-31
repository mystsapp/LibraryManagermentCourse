using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagermentCourse.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagermentCourse.Controllers
{
    public class ReturnController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReturnController(IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
        }
        public IActionResult List()
        {
            // load all borrowed books
            var borrowedBooks = _bookRepository.FindWithAuthorAndBorrower(x => x.BorrowerId == 0);

            // check the books collection
            if (borrowedBooks == null || borrowedBooks.Count() == 0)
                return View("Empty");

            return View(borrowedBooks);
        }

        public IActionResult ReturnBook(int id)
        {
            // load the current book
            var book = _bookRepository.GetById(id);

            // remove borrower
            book.Borrower = null;
            book.BorrowerId = 0;

            // update database
            _bookRepository.Update(book);

            // redirect to lis method
            return RedirectToAction("List");
        }
    }
}