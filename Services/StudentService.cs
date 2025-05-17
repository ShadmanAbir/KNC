using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KNC.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<StudentsViewModel> GetAllStudents()
        {
            return (from a in _context.Students
                        join p in _context.EducationPrograms on a.ProgramID equals p.EducationProgramID 
                        where a.IsDeleted != true && p.IsDeleted != true
                    select new StudentsViewModel
                        {
                            StudentID = a.StudentID,
                            StudentCode = a.StudentCode,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            Email = a.Email,
                            Phone = a.Phone,
                            PermanentAddress = a.PermanentAddress,
                            CurrentAddress = a.CurrentAddress,
                            AdmissionDate = a.AdmissionDate,
                            ProgramID = a.ProgramID,
                            LinkImage = a.LinkImage,
                            CreatedDate = a.CreatedDate,
                            CreatedBy = a.CreatedBy,
                            ProgramName = p.ProgramName
                        }).ToList();
        }

        public StudentsViewModel GetStudentById(int id)
        {
            return _mapper.Map<StudentsViewModel>(_context.Students.SingleOrDefault(a => a.StudentID == id && a.IsDeleted != true));
        }

        public void AddStudent(StudentsViewModel stVM)
        {
            _context.Students.Add(_mapper.Map<Student>(stVM));
            _context.SaveChanges();
        }

        public void DeleteStudent(int id)
        {
            var student = _context.Students.SingleOrDefault(a => a.StudentID == id && a.IsDeleted != true);
            student.IsDeleted = true;
            if (student != null)
            {
                _context.Entry(student).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateStudent(StudentsViewModel student)
        {
            var existingStudent = _context.Students.Find(student.StudentID);
            if (existingStudent != null)
            {
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.Email = student.Email;
                existingStudent.Phone = student.Phone;
                existingStudent.PermanentAddress = student.PermanentAddress;
                existingStudent.CurrentAddress = student.CurrentAddress;
                existingStudent.AdmissionDate = student.AdmissionDate;
                existingStudent.ProgramID = student.ProgramID;
                existingStudent.StudentCode = student.StudentCode;
                existingStudent.LinkImage = student.LinkImage;
                existingStudent.IsDeleted = student.IsDeleted;
                existingStudent.CreatedDate = student.CreatedDate;
                existingStudent.CreatedBy = student.CreatedBy;
                _context.Entry(existingStudent).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}