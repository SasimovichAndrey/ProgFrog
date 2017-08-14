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
using System.IO;
using System.Reflection;

namespace ProgFrog.IoC
{
    public class Unity
    {
        public static IUnityContainer Configure()
        {
            var container = new UnityContainer();

            container.RegisterType<IModelSerializer<ProgrammingTask>, JsonSerializer<ProgrammingTask>>();
            container.RegisterType<IProgrammingTaskRepository, FileProgramminTaskRepository>(new InjectionConstructor(new ResolvedParameter<IModelSerializer<ProgrammingTask>>(), new InjectionParameter<string>(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "App_Data"))));
            container.RegisterType<IResultsChecker, ResultsChecker>();

            container.RegisterType<IInputWriter, StandardInputStreamWriter>(new InjectionConstructor());
            container.RegisterType<IOutputReader, StandardOutputStreamReader>(new InjectionConstructor());
            container.RegisterType<IFileWriter, FileWriter>();
            container.RegisterType<IProcessFactory, ProcessFactory>();
            container.RegisterType<ITempFileProvider, TempFileProvider>();
            container.RegisterType<ICompiler, CSharpCompiler>("CSharp", new InjectionConstructor(new InjectionParameter<string>(@"c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe")));
            container.RegisterType<PythonTaskRunner>(new InjectionConstructor(new InjectionParameter<string>(@"c:\Python27\python.exe"), new ResolvedParameter<IInputWriter>(),
                new ResolvedParameter<IOutputReader>(), new ResolvedParameter<IFileWriter>(), new ResolvedParameter<IProcessFactory>(), new ResolvedParameter<ITempFileProvider>()));
            container.RegisterType<CSharpTaskRunner>("CSharp", new InjectionConstructor(new ResolvedParameter<ICompiler>("CSharp"), new ResolvedParameter<IInputWriter>(),
                new ResolvedParameter<IOutputReader>(), new ResolvedParameter<IFileWriter>(), new ResolvedParameter<IProcessFactory>(), new ResolvedParameter<ITempFileProvider>()));

            var taskRunnerProvider = new TaskRunnerProvider();
            taskRunnerProvider.RegisterRunner(container.Resolve<PythonTaskRunner>(), ProgrammingLanguage.Python);
            taskRunnerProvider.RegisterRunner(container.Resolve<CSharpTaskRunner>("CSharp"), ProgrammingLanguage.CSharp);
            container.RegisterInstance<ITaskRunnerProvider>(taskRunnerProvider);

            return container;
        }
    }
}
