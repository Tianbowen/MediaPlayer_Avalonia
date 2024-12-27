using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvaloniaApplication2.LibMpv.Client.Helpers.Resolvers
{
    public abstract class FunctionResolverBase : IFunctionResolver
    {
        public static readonly Dictionary<string, string[]> LibraryDependenciesMap = new Dictionary<string, string[]>()
        {
            { "libmpv", new string[] { } },
            { "libavcodec", new string[] { } }
        };

        private readonly Dictionary<string, IntPtr> _loadedLibraries = new Dictionary<string, IntPtr>();

        private readonly object _syncRoot = new object();

        public T GetFunctionDelegate<T>(string libraryName, string functionName, bool throwOnError = true)
        {
            var nativeLibraryHandle = GetOrLoadLibrary(libraryName, throwOnError);
        }

        public IntPtr GetOrLoadLibrary(string libraryName, bool throwOnError)
        {
            if (_loadedLibraries.TryGetValue(libraryName, out var ptr)) return ptr;

            lock (_syncRoot)
            {
                if (_loadedLibraries.TryGetValue(libraryName, out ptr)) return ptr;

                var dependencies = LibraryDependenciesMap[libraryName];
                dependencies.Where(n => !_loadedLibraries.ContainsKey(n) && !n.Equals(libraryName))
                    .ToList()
                    .ForEach(n => GetOrLoadLibrary(n, false));

                var version = LibMpv.Lib;

            }
        }

        protected abstract string GetNativeLibraryName(string libraryName, int version);

        protected abstract IntPtr LoadNativeLibrary(string libraryName);

        protected abstract IntPtr FindFunctionPointer(IntPtr nativeLibraryHandle, string functionName);
    }
}
