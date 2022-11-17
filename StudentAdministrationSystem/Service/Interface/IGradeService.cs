using StudentAdministrationSystem.Models;

namespace StudentAdministrationSystem.Service
{
    public interface IGradeService
    {
        GradeModel[] GetGrades();
        GradeModel GetGrade(int id);
        void AddGrade(GradeModel model);
        void UpdateGrade(GradeModel model);
        void RemoveGrade(int id); 
        GradeModel[] GetGradesByAssessment(int assessmentId);
        GradeModel[] GetGradesByStudent(string studentId);
        GradeModel[] GetGradesByStudentAssessmentModule(string studentId, string moduleId, int assessmentId);
        GradeModel[] GetGradesByStudentModule(string studentId, string moduleId);
    }
}