using LibraryManagermentCourse.Data.Interfaces;
using LibraryManagermentCourse.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagermentCourse.Data.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
