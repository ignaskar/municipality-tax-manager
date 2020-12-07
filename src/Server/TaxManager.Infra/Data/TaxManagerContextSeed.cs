using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaxManager.Core.Entities;

namespace TaxManager.Infra.Data
{
    public class TaxManagerContextSeed
    {
        public static async Task SeedAsync(TaxManagerContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Municipalities.Any())
                {
                    var municipalitiesData = File.ReadAllText("../TaxManager.Infra/Data/SeedData/municipalities.json");

                    var municipalities = JsonSerializer.Deserialize<List<Municipality>>(municipalitiesData);

                    foreach (var m in municipalities)
                    {
                        context.Municipalities.Add(m);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Schedules.Any())
                {
                    var schedulesData = File.ReadAllText("../TaxManager.Infra/Data/SeedData/schedules.json");

                    var schedules = JsonSerializer.Deserialize<List<TaxSchedule>>(schedulesData);

                    foreach (var s in schedules)
                    {
                        context.Schedules.Add(s);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<TaxManagerContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}