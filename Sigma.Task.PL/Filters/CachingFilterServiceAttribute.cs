using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Sigma.Task.PL.Filters;

public class CachingFilterResourceAttribute : Attribute, IResourceFilter
{
    private readonly string _key;
    private readonly IMemoryCache _memoryCache;

    public CachingFilterResourceAttribute(IMemoryCache memoryCache, string key)
    {
        _key = key;
        _memoryCache = memoryCache;
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        if (_memoryCache.TryGetValue(_key, out var cacheValue))
        {
            context.Result = new OkObjectResult(cacheValue);
        }
    }
}
