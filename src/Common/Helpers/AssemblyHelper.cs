using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StatementIQ.Helpers
{
    public static class AssemblyHelper
    {
        public static void LoadAssembliesToCurrentDomain()
        {
            var binPath = AppDomain.CurrentDomain.BaseDirectory;

            foreach (var dll in Directory.GetFiles(binPath, "*.dll"))
            {
                var assemblyName = Assembly.LoadFile(dll).GetName();

                AppDomain.CurrentDomain.Load(assemblyName);
            }
        }
    }
}