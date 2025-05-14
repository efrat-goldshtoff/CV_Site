using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CVSite.SERVICE
{
    public static class Extensions
    {
        public static void AddGitHubIntegration(this IServiceCollection services, Action<GitHubOptions> configurOptions)
        {
            services.Configure(configurOptions);
            services.AddScoped<IGitHubService, GitHubService>();
        }
    }
}
