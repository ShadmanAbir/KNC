using KNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KNC.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}