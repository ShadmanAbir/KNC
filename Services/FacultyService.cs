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
    public class FacultyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FacultyService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<FacultyViewModel> GetAllFaculties()
        {
            return _context.Faculties
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .ProjectTo<FacultyViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public FacultyViewModel GetFacultyById(int id)
        {
            var faculty = _context.Faculties
                .AsNoTracking()
                .SingleOrDefault(a => a.FacultyID == id && !a.IsDeleted);
            
            return faculty == null ? null : _mapper.Map<FacultyViewModel>(faculty);
        }

        public void AddFaculty(FacultyViewModel vm)
        {
            var entity = _mapper.Map<Faculty>(vm);
            
            // Set required properties for new faculty
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = vm.CreatedBy;
            entity.IsDeleted = false;
            
            _context.Faculties.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteFaculty(int id)
        {
            var faculty = _context.Faculties
                .SingleOrDefault(a => a.FacultyID == id && !a.IsDeleted);
            
            if (faculty != null)
            {
                faculty.IsDeleted = true;
                faculty.CreatedDate = DateTime.Now; // Track modification time
                _context.Entry(faculty).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateFaculty(FacultyViewModel vm)
        {
            var existing = _context.Faculties.Find(vm.FacultyID);
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