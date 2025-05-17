using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace KNC.Services
{
    public class ProgramsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProgramsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<EducationProgramsViewModel> GetAllPrograms()
        {
            return _mapper.ProjectTo<EducationProgramsViewModel>(_context.EducationPrograms.Where(a => a.IsDeleted != true)).ToList();
        }

        public EducationProgramsViewModel GetProgramById(int id)
        {
            return _mapper.Map<EducationProgramsViewModel>(_context.EducationPrograms.SingleOrDefault(a => a.ProgramID == id && a.IsDeleted != true));
        }

        public void AddProgram(EducationProgramsViewModel vm)
        {
            _context.EducationPrograms.Add(_mapper.Map<EducationPrograms>(vm));
            _context.SaveChanges();
        }

        public void DeleteProgram(int id)
        {
            var program = _context.EducationPrograms.SingleOrDefault(a => a.ProgramID == id && a.IsDeleted != true);
            if (program != null)
            {
                program.IsDeleted = true;
                _context.Entry(program).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateProgram(EducationProgramsViewModel vm)
        {
            var existing = _context.EducationPrograms.Find(vm.ProgramID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}