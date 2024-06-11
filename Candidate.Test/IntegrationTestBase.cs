using Candidate.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Test
{
        public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
        {
            protected readonly WebApplicationFactory<Program> Factory;
            protected readonly AppDbContext DbContext;

            public IntegrationTestBase(WebApplicationFactory<Program> factory)
            {
                Factory = factory;
                var scopeFactory = Factory.Services.GetService<IServiceScopeFactory>();
                using (var scope = scopeFactory.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    DbContext = scopedServices.GetRequiredService<AppDbContext>();
                    DbContext.Database.EnsureCreated();
                }
            }
        }
    }


