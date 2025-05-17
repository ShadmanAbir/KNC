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
            throw new KeyNotFoundException("The key was not found in the dictionary.");
            //return _mapper.ProjectTo<DropDownViewModel>(_context.DropDowns.Where(a => a.IsDeleted != true)).ToList();
        }

        public DropDownViewModel GetDropDownById(int id)
        {
            throw new KeyNotFoundException("The key was not found in the dictionary.");
            //return _mapper.Map<DropDownViewModel>(_context.DropDowns.SingleOrDefault(a => a.DropDownID == id && a.IsDeleted != true));
        }

        public void AddDropDown(DropDownViewModel vm)
        {
            throw new KeyNotFoundException("The key was not found in the dictionary.");
            /*_context.DropDowns.Add(_mapper.Map<DropDown>(vm));
            _context.SaveChanges();*/
        }

        public void DeleteDropDown(int id)
        {
            throw new KeyNotFoundException("The key was not found in the dictionary.");
            /*            var dropdown = _context.DropDowns.SingleOrDefault(a => a.DropDownID == id && a.IsDeleted != true);
                        if (dropdown != null)
                        {
                            dropdown.IsDeleted = true;
                            _context.Entry(dropdown).State = System.Data.Entity.EntityState.Modified;
                            _context.SaveChanges();
                        }*/
        }

        public void UpdateDropDown(DropDownViewModel vm)
        {
            throw new KeyNotFoundException("The key was not found in the dictionary.");
            /*            var existing = _context.DropDowns.Find(vm.DropDownID);
                        if (existing != null)
                        {
                            _mapper.Map(vm, existing);
                            _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                            _context.SaveChanges();
                        }*/
        }
    }
}