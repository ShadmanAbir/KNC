using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
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
            return _mapper.ProjectTo<MarkViewModel>(_context.Marks.Where(a => a.IsDeleted != true)).ToList();
        }

        public MarkViewModel GetMarkById(int id)
        {
            return _mapper.Map<MarkViewModel>(_context.Marks.SingleOrDefault(a => a.MarkID == id && a.IsDeleted != true));
        }

        public void AddMark(MarkViewModel vm)
        {
            _context.Marks.Add(_mapper.Map<Mark>(vm));
            _context.SaveChanges();
        }

        public void DeleteMark(int id)
        {
            var mark = _context.Marks.SingleOrDefault(a => a.MarkID == id && a.IsDeleted != true);
            if (mark != null)
            {
                mark.IsDeleted = true;
                _context.Entry(mark).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateMark(MarkViewModel vm)
        {
            var existing = _context.Marks.Find(vm.MarkID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}