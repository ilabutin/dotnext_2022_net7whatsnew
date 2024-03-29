﻿using System.Security.Claims;
using System.Threading.RateLimiting;

namespace TodoApi;

public static class RateLimitExtensions
{
    private static readonly string Policy = "PerUserRatelimit";

    public static IServiceCollection AddRateLimiting(this IServiceCollection services, bool log = false)
    {
        return services.AddRateLimiter(options =>
         {
             options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

             options.AddPolicy(Policy, context =>
             {
                 // We always have a user id
                 var id = context.User.FindFirstValue("id")!;

                 return RateLimitPartition.GetTokenBucketLimiter(id, key =>
                 {
                     return new()
                     {
                         ReplenishmentPeriod = TimeSpan.FromSeconds(10),
                         AutoReplenishment = true,
                         TokenLimit = 10,
                         TokensPerPeriod = 10,
                         QueueLimit = 10,
                     };
                 });
             });
         });
    }

    public static IEndpointConventionBuilder RequirePerUserRateLimit(this IEndpointConventionBuilder builder)
    {
        return builder.RequireRateLimiting(Policy);
    }
}
