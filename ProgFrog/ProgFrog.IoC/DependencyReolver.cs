using Microsoft.Practices.Unity;
using ProgFrog.Core.Data;
using ProgFrog.Core.Data.Serialization;
using ProgFrog.Core.TaskRunning;
using ProgFrog.Core.TaskRunning.Compilers;
using ProgFrog.Core.TaskRunning.ResultsChecking;
using ProgFrog.Core.TaskRunning.Runners;
using ProgFrog.Interface.Data;
using ProgFrog.Interface.Data.Serialization;
using ProgFrog.Interface.Model;
using ProgFrog.Interface.TaskRunning;
using ProgFrog.Interface.TaskRunning.Compilers;
using ProgFrog.Interface.TaskRunning.ResultsChecking;
using ProgFrog.Interface.TaskRunning.Runners;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web.Http.Dependencies;
using Unity.WebApi;

namespace ProgFrog.IoC
{
    public class DependencyReolver
    {
        private static IUnityContainer _container;

        public static void Configure()
        {
            _container = new UnityContainer();

            _container.RegisterType<IModelSerializer<ProgrammingTask>, JsonSerializer<ProgrammingTask>>();

            var progTasksLocation = ConfigurationManager.AppSettings["progTasksLocation"];
            _container.RegisterType<IProgrammingTaskRepository, FileProgramminTaskRepository>(new InjectionConstructor(new ResolvedParameter<IModelSerializer<ProgrammingTask>>(), new InjectionParameter<string>(progTasksLocation)));
            _container.RegisterType<IResultsChecker, ResultsChecker>();

            _container.RegisterType<IInputWriter, StandardInputStreamWriter>(new InjectionConstructor());
            _container.RegisterType<IOutputReader, StandardOutputStreamReader>(new InjectionConstructor());
            _container.RegisterType<IFileWriter, FileWriter>();
            _container.RegisterType<IProcessFactory, ProcessFactory>();
            _container.RegisterType<ITempFileProvider, TempFileProvider>();
            _container.RegisterType<ICompiler, CSharpCompiler>("CSharp", new InjectionConstructor(new InjectionParameter<string>(@"c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe")));
            _container.RegisterType<PythonTaskRunner>(new InjectionConstructor(new InjectionParameter<string>(@"c:\Python27\python.exe"), new ResolvedParameter<IInputWriter>(),
                new ResolvedParameter<IOutputReader>(), new ResolvedParameter<IFileWriter>(), new ResolvedParameter<IProcessFactory>(), new ResolvedParameter<ITempFileProvider>()));
            _container.RegisterType<CSharpTaskRunner>("CSharp", new InjectionConstructor(new ResolvedParameter<ICompiler>("CSharp"), new ResolvedParameter<IInputWriter>(),
                new ResolvedParameter<IOutputReader>(), new ResolvedParameter<IFileWriter>(), new ResolvedParameter<IProcessFactory>(), new ResolvedParameter<ITempFileProvider>()));

            var taskRunnerProvider = new TaskRunnerProvider();
            taskRunnerProvider.RegisterRunner(_container.Resolve<PythonTaskRunner>(), ProgrammingLanguage.Python);
            taskRunnerProvider.RegisterRunner(_container.Resolve<CSharpTaskRunner>("CSharp"), ProgrammingLanguage.CSharp);
            _container.RegisterInstance<ITaskRunnerProvider>(taskRunnerProvider);
        }

        public static IDependencyResolver GetWebDependencyResolver()
        {
            return new UnityDependencyResolver(_container);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
