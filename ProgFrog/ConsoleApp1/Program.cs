using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = @"c:\Users\asasimovich\Documents\Visual Studio 2017\Projects\ProgFrog\ProgFrog\ProgFrog.Tests\TestData\output12345.exe";
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = fileName;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            var proc = Process.Start(startInfo);

            var res = proc.StandardOutput.ReadToEnd();
            Console.WriteLine(res);
        }

        private static string GetTestDataDirectoryPath()
        {
            return Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\..\TestData"));
        }
    }
}
