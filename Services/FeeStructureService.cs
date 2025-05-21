using AutoMapper;
using AutoMapper.QueryableExtensions;
using KNC.Models;
using KNC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return _context.FeeStructures
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .ProjectTo<FeeStructureViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public FeeStructureViewModel GetFeeStructureById(int id)
        {
            var feeStructure = _context.FeeStructures
                .AsNoTracking()
                .SingleOrDefault(a => a.ID == id && !a.IsDeleted);
            
            return feeStructure == null ? null : _mapper.Map<FeeStructureViewModel>(feeStructure);
        }

        public void AddFeeStructure(FeeStructureViewModel vm)
        {
            var entity = _mapper.Map<FeeStructure>(vm);
            
            // Set initial values for new fee structure
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = vm.CreatedBy;
            entity.IsDeleted = false;
            
            _context.FeeStructures.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteFeeStructure(int id)
        {
            var feeStructure = _context.FeeStructures
                .SingleOrDefault(a => a.ID == id && !a.IsDeleted);
            
            if (feeStructure != null)
            {
                feeStructure.IsDeleted = true;
                feeStructure.CreatedDate = DateTime.Now; // Track modification time
                _context.Entry(feeStructure).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateFeeStructure(FeeStructureViewModel vm)
        {
            var existing = _context.FeeStructures.Find(vm.ID);
            if (existing != null)
            {
                // Keep track of deletion status
                bool wasDeleted = existing.IsDeleted;
                
                // Map all properties
                _mapper.Map(vm, existing);
                
                // Preserve/update tracking fields
                existing.IsDeleted = wasDeleted; // Keep original deletion status
                existing.CreatedDate = DateTime.Now;
                existing.CreatedBy = vm.CreatedBy;
                
                _context.Entry(existing).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}