using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
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
            return _mapper.ProjectTo<DesignationViewModel>(_context.Designations.Where(a => a.IsDeleted != true)).ToList();
        }

        public DesignationViewModel GetDesignationById(int id)
        {
            return _mapper.Map<DesignationViewModel>(_context.Designations.SingleOrDefault(a => a.DesignationID == id && a.IsDeleted != true));
        }

        public void AddDesignation(DesignationViewModel vm)
        {
            _context.Designations.Add(_mapper.Map<Designation>(vm));
            _context.SaveChanges();
        }

        public void DeleteDesignation(int id)
        {
            var designation = _context.Designations.SingleOrDefault(a => a.DesignationID == id && a.IsDeleted != true);
            if (designation != null)
            {
                designation.IsDeleted = true;
                _context.Entry(designation).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateDesignation(DesignationViewModel vm)
        {
            var existing = _context.Designations.Find(vm.DesignationID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}