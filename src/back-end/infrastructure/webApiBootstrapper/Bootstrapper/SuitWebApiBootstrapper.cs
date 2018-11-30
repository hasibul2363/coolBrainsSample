using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using SuitSupply.Infrastructure.Bootstrapper.Extensions;

namespace SuitSupply.Infrastructure.Bootstrapper
{
    public static class SuitWebApiBootstrapper
    {
        public static void Use(IApplicationBuilder builder, string route ="")
        {
            builder
                .UseCorsToAllowAll()
                .UseSuitRouting(route);
        }
    }
}
