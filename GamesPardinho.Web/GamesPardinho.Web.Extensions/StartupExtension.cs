using GamesPardinho.Web.Models.Repository.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;

namespace GamesPardinho.Web.Extensions
{
    public static class StartupExtension
    {
        public static IHostBuilder LoadAllDllsIBinFolder(this IHostBuilder builder)
        {
            LoadAllDllsIBinFolder();
            return builder;
        }

        public static IWebHostBuilder LoadAllDllsIBinFolder(this IWebHostBuilder builder)
        {
            LoadAllDllsIBinFolder();
            return builder;
        }

        public static void LoadAllDllsIBinFolder()
        {
            List<string> stringList = new List<string>();
            try
            {
                stringList = ((IEnumerable<CompilationLibrary>)DependencyContext.Default.CompileLibraries).SelectMany(x => GetReferencePaths(x)).Distinct().Where(x => x.Contains(Directory.GetCurrentDirectory())).ToList();
            }
            catch (Exception ex) { }

            foreach (string assemblyPath in stringList)
            {
                try
                {
                    AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
                }
                catch (FileLoadException ex)
                {
                }
                catch (BadImageFormatException ex)
                {
                }
                catch (Exception ex)
                {
                }
            }
        }

        private static IEnumerable<string> GetReferencePaths(CompilationLibrary x)
        {
            try
            {
                return x.ResolveReferencePaths();
            }
            catch
            {
                return new List<string>();
            }
        }

        public static void AddUserContextLoader(this IServiceCollection services)
        {
            ServiceCollectionServiceExtensions.AddScoped(services, x => x.GetUserContext(new UserContext()));
            ServiceCollectionServiceExtensions.AddScoped<IUserContextLoader, UserContextLoader>(services);
        }

        public static IUserContext GetUserContext( this IServiceProvider container, IUserContext userContext)
        {
            IUserContext userContext1 = userContext;
            try
            {
                if (ServiceProviderServiceExtensions.GetService<IUserContextLoader>(container) != null)
                    ServiceProviderServiceExtensions.GetService<IUserContextLoader>(container).Load(userContext1);
            }
            catch
            {
            }
            return userContext1;
        }
    }
}
