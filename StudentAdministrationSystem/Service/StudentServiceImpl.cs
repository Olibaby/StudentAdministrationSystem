using System;
using System.Collections.Generic;
using System.Linq;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.data.Repository.Interface;
using StudentAdministrationSystem.Models;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem.Service
{
    public class StudentServiceImpl: IStudentService
    {
        private IStudentRepository _studentRepository;
        public StudentServiceImpl(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public StudentModel[] GetStudents()
        {
            var entities = _studentRepository.GetStudent(s => s.Programme ).ToList();
            var models = entities.Select(s => new StudentModel(s)
            {
                Programme = new ProgrammeModel(s.Programme)
            }).ToArray();
            return models;
        }

        public StudentModel GetStudent(string id)
        {
            var entities = _studentRepository
                .GetStudent(s => s.Programme)
                .Where(s => s.StudentId == id)
                .ToList();
            var models = entities.Select(s => new StudentModel(s)
            {
                Programme = new ProgrammeModel(s.Programme)
            }).FirstOrDefault();
            return models;
        }

        public void AddStudent(StudentModel model)
        {
            var entity = model.Create(model);
            _studentRepository.AddStudent(entity);
        }

        public void UpdateStudent(StudentModel model)
        {
            var programme = _studentRepository.GetStudentById(model.StudentId);
            var entity = model.Edit(programme, model);
            _studentRepository.UpdateStudent(entity);
        }

        public void RemoveStudent(string id)
        {
            _studentRepository.RemoveStudent(id);
        }

        public StudentModel[] GetStudentByProgramme(string programmeId)
        {
            var entities = _studentRepository
                .GetStudent(s => s.Programme)
                .Where(s => s.ProgrammeId == programmeId)
                .ToList();
            var models = entities.Select(s => new StudentModel(s)
            {
                Programme = new ProgrammeModel(s.Programme)
            }).ToArray();
            return models;
        }

        public void AddModuleToStudent(string moduleId, string studentId, string studentModuleId)
        {
            _studentRepository.InsertStudentWithModule(moduleId, studentId, studentModuleId);
        }

        public IEnumerable<ModuleModel> GetModuleByStudentId(string studentId)
        {
            // var entities = _studentRepository.GetModuleByStudentId(studentId);
            var entities = _studentRepository.GetModuleByStudentIdStatement(studentId);
            var models = entities.Select(c => new ModuleModel(c)
            {
                Programme = new ProgrammeModel(c.Programme)
            }).ToArray();
            return models;
        }

        public IEnumerable<GradeModel> GetStudentModuleGrade(string studentId)
        {
            var entities = _studentRepository.GetStudentModulesScore(studentId);
            var models = entities.Select(c => new GradeModel(c)
            {
                Assessment = new AssessmentModel(c.Assessment),
                Student = new StudentModel(c.Student),
                Module = new ModuleModel(c.Module)
            }).ToArray();
            return models;
        }
    }
}