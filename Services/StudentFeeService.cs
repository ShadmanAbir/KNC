using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace KNC.Services
{
    public class StudentFeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentFeeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<StudentFeeViewModel> GetAllStudentFees()
        {
            return _mapper.ProjectTo<StudentFeeViewModel>(_context.StudentFees.Where(a => a.IsDeleted != true)).ToList();
        }

        public StudentFeeViewModel GetStudentFeeById(int id)
        {
            return _mapper.Map<StudentFeeViewModel>(_context.StudentFees.SingleOrDefault(a => a.FeeID == id && a.IsDeleted != true));
        }

        public void AddStudentFee(StudentFeeViewModel vm)
        {
            _context.StudentFees.Add(_mapper.Map<StudentFee>(vm));
            _context.SaveChanges();
        }

        public void DeleteStudentFee(int id)
        {
            var fee = _context.StudentFees.SingleOrDefault(a => a.FeeID == id && a.IsDeleted != true);
            if (fee != null)
            {
                fee.IsDeleted = true;
                _context.Entry(fee).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateStudentFee(StudentFeeViewModel vm)
        {
            var existing = _context.StudentFees.Find(vm.StudentFeeID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}