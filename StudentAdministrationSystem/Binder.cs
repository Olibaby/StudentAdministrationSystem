using System.Data.Entity;
using Ninject.Modules;
using Ninject.Web.Common;
using StudentAdministrationSystem.data;
using StudentAdministrationSystem.data.Repository;
using StudentAdministrationSystem.data.Repository.Interface;
using StudentAdministrationSystem.Service;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem
{
    public class Binder: NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<DbContext>().To<DataEntityContext>().InRequestScope();
            Bind<IProgrammeRepository>().To<ProgrammeRepositoryImpl>().InRequestScope();
            Bind<IProgrammeService>().To<ProgrammeServiceImpl>().InRequestScope();
            Bind<IModuleRepository>().To<ModuleRepositoryImpl>().InRequestScope();
            Bind<IModuleService>().To<ModuleServiceImpl>().InRequestScope();
            Bind<IAssessmentRepository>().To<AssessmentRepositoryImpl>().InRequestScope();
            Bind<IAssessmentService>().To<AssessmentServiceImpl>().InRequestScope();
            Bind<IStudentRepository>().To<StudentRepositoryImpl>().InRequestScope();
            Bind<IStudentService>().To<StudentServiceImpl>().InRequestScope();
            Bind<IGradeRepository>().To<GradeRepositoryImpl>().InRequestScope();
            Bind<IGradeService>().To<GradeServiceImpl>().InRequestScope();
        }
    }
}