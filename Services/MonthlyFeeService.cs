using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace KNC.Services
{
    public class MonthlyFeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MonthlyFeeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<MonthlyFeeViewModel> GetAllMonthlyFees()
        {
            return _mapper.ProjectTo<MonthlyFeeViewModel>(_context.MonthlyFees.Where(a => a.IsDeleted != true)).ToList();
        }

        public MonthlyFeeViewModel GetMonthlyFeeById(int id)
        {
            return _mapper.Map<MonthlyFeeViewModel>(_context.MonthlyFees.SingleOrDefault(a => a.MonthlyFeeID == id && a.IsDeleted != true));
        }

        public void AddMonthlyFee(MonthlyFeeViewModel vm)
        {
            _context.MonthlyFees.Add(_mapper.Map<MonthlyFee>(vm));
            _context.SaveChanges();
        }

        public void DeleteMonthlyFee(int id)
        {
            var fee = _context.MonthlyFees.SingleOrDefault(a => a.MonthlyFeeID == id && a.IsDeleted != true);
            if (fee != null)
            {
                fee.IsDeleted = true;
                _context.Entry(fee).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateMonthlyFee(MonthlyFeeViewModel vm)
        {
            var existing = _context.MonthlyFees.Find(vm.MonthlyFeeID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}