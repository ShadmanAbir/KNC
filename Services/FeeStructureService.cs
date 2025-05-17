using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace KNC.Services
{
    public class FeeStructureService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FeeStructureService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<FeeStructureViewModel> GetAllFeeStructures()
        {
            return _mapper.ProjectTo<FeeStructureViewModel>(_context.FeeStructures.Where(a => a.IsDeleted != true)).ToList();
        }

        public FeeStructureViewModel GetFeeStructureById(int id)
        {
            return _mapper.Map<FeeStructureViewModel>(_context.FeeStructures.SingleOrDefault(a => a.ID == id && a.IsDeleted != true));
        }

        public void AddFeeStructure(FeeStructureViewModel vm)
        {
            _context.FeeStructures.Add(_mapper.Map<FeeStructure>(vm));
            _context.SaveChanges();
        }

        public void DeleteFeeStructure(int id)
        {
            var feeStructure = _context.FeeStructures.SingleOrDefault(a => a.ID == id && a.IsDeleted != true);
            if (feeStructure != null)
            {
                feeStructure.IsDeleted = true;
                _context.Entry(feeStructure).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateFeeStructure(FeeStructureViewModel vm)
        {
            var existing = _context.FeeStructures.Find(vm.FeeStructureID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}