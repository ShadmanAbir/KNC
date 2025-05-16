using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace KNC.Services
{
    public class DropDownService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DropDownService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<DropDownViewModel> GetAllDropDowns()
        {
            return _mapper.ProjectTo<DropDownViewModel>(_context.DropDowns.Where(a => a.IsDeleted != true)).ToList();
        }

        public DropDownViewModel GetDropDownById(int id)
        {
            return _mapper.Map<DropDownViewModel>(_context.DropDowns.SingleOrDefault(a => a.DropDownID == id && a.IsDeleted != true));
        }

        public void AddDropDown(DropDownViewModel vm)
        {
            _context.DropDowns.Add(_mapper.Map<DropDown>(vm));
            _context.SaveChanges();
        }

        public void DeleteDropDown(int id)
        {
            var dropdown = _context.DropDowns.SingleOrDefault(a => a.DropDownID == id && a.IsDeleted != true);
            if (dropdown != null)
            {
                dropdown.IsDeleted = true;
                _context.Entry(dropdown).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateDropDown(DropDownViewModel vm)
        {
            var existing = _context.DropDowns.Find(vm.DropDownID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}