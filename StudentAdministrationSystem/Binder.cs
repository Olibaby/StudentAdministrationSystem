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
        }
    }
}