using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace KNC.Services
{
    public class RoutineService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoutineService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<RoutineViewModel> GetAllRoutines()
        {
            return _mapper.ProjectTo<RoutineViewModel>(_context.Routines.Where(a => a.IsDeleted != true)).ToList();
        }

        public RoutineViewModel GetRoutineById(int id)
        {
            return _mapper.Map<RoutineViewModel>(_context.Routines.SingleOrDefault(a => a.RoutineID == id && a.IsDeleted != true));
        }

        public void AddRoutine(RoutineViewModel vm)
        {
            _context.Routines.Add(_mapper.Map<Routine>(vm));
            _context.SaveChanges();
        }

        public void DeleteRoutine(int id)
        {
            var routine = _context.Routines.SingleOrDefault(a => a.RoutineID == id && a.IsDeleted != true);
            if (routine != null)
            {
                routine.IsDeleted = true;
                _context.Entry(routine).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateRoutine(RoutineViewModel vm)
        {
            var existing = _context.Routines.Find(vm.RoutineID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}