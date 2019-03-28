using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagermentCourse.Data.Interfaces;
using LibraryManagermentCourse.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagermentCourse.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookRepository _bookRepository;

        public CustomerController(ICustomerRepository customerRepository, IBookRepository bookRepository)
        {
            _customerRepository = customerRepository;
            _bookRepository = bookRepository;
        }
       
        [Route("Customer")]
        public IActionResult List()
        {
            var customVM = new List<CustomerViewModel>();
            var customers = _customerRepository.GetAll();

            if(customers.Count() == 0)
            {
                return View("Empty");
            }

            foreach(var customer in customers)
            {
                customVM.Add(new CustomerViewModel
                {
                    Customer = customer,
                    BootCount = _bookRepository.Count(a => a.BorrowId == customer.CustomerId)
                });
            }

            return View(customVM);
        }
    }
}