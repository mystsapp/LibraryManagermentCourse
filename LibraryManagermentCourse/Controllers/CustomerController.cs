using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagermentCourse.Data.Interfaces;
using LibraryManagermentCourse.Data.Model;
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
                    BootCount = _bookRepository.Count(a => a.BorrowerId == customer.CustomerId)
                });
            }

            return View(customVM);
        }

        public IActionResult Delete(int id)
        {
            var customer = _customerRepository.GetById(id);

            _customerRepository.Delete(customer);

            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            _customerRepository.Create(customer);

            return RedirectToAction("List");
        }

        public IActionResult Update(int id)
        {
            var customer = _customerRepository.GetById(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            _customerRepository.Update(customer);

            return RedirectToAction("List");
        }
    }
}