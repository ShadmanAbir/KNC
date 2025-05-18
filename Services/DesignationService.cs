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
    public class DesignationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DesignationService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<DesignationViewModel> GetAllDesignations()
        {
            return _context.Designations
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .ProjectTo<DesignationViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public DesignationViewModel GetDesignationById(int id)
        {
            var designation = _context.Designations
                .AsNoTracking()
                .SingleOrDefault(a => a.DesignationID == id && !a.IsDeleted);
            return designation == null ? null : _mapper.Map<DesignationViewModel>(designation);
        }

        public void AddDesignation(DesignationViewModel vm)
        {
            var entity = _mapper.Map<Designation>(vm);
            
            // Set required properties for new entity
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = vm.CreatedBy;
            entity.IsDeleted = false; // Ensure new designations are not deleted
            
            _context.Designations.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteDesignation(int id)
        {
            var designation = _context.Designations
                .SingleOrDefault(a => a.DesignationID == id && !a.IsDeleted);
            
            if (designation != null)
            {
                designation.IsDeleted = true; // Soft delete
                designation.CreatedDate = DateTime.Now; // Update modification time
                _context.Entry(designation).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateDesignation(DesignationViewModel vm)
        {
            var existing = _context.Designations.Find(vm.DesignationID);
            if (existing != null)
            {
                // Keep the existing IsDeleted value
                bool wasDeleted = existing.IsDeleted;
                
                // Map all other properties
                _mapper.Map(vm, existing);
                
                // Restore IsDeleted and set CreatedDate
                existing.IsDeleted = wasDeleted; // Preserve deletion status
                existing.CreatedDate = DateTime.Now;
                
                _context.Entry(existing).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}