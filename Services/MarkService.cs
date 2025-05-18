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
    public class MarkService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MarkService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<MarkViewModel> GetAllMarks()
        {
            return _context.Marks
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .ProjectTo<MarkViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public MarkViewModel GetMarkById(int id)
        {
            var mark = _context.Marks
                .AsNoTracking()
                .SingleOrDefault(a => a.MarkID == id && !a.IsDeleted);
            
            return mark == null ? null : _mapper.Map<MarkViewModel>(mark);
        }

        public void AddMark(MarkViewModel vm)
        {
            var entity = _mapper.Map<Mark>(vm);
            
            // Set initial values for new mark
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = vm.CreatedBy;
            entity.IsDeleted = false;
            
            // Set default values if needed
            entity.ExamName = entity.ExamName ?? "Regular";
            
            _context.Marks.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteMark(int id)
        {
            var mark = _context.Marks
                .SingleOrDefault(a => a.MarkID == id && !a.IsDeleted);
            
            if (mark != null)
            {
                mark.IsDeleted = true;
                mark.CreatedDate = DateTime.Now; // Track modification time
                _context.Entry(mark).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateMark(MarkViewModel vm)
        {
            var existing = _context.Marks.Find(vm.MarkID);
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