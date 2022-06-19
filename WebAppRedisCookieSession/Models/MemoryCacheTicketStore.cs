using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
///https://github.com/aspnet/Security/blob/master/samples/CookieSessionSample/MemoryCacheTicketStore.cs
namespace WebAppRedisCookieSession.Models
{
    public class MemoryCacheTicketStore : ITicketStore
    {
        private readonly IMemoryCache m_memoryCache;        

        public MemoryCacheTicketStore(IMemoryCache memoryCache)
        {
            m_memoryCache = memoryCache;
        }

        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var cacheKey = GenerateEntryKey() + "_" + ticket.Principal.Identity.Name;
            await RenewAsync(cacheKey, ticket);
            return cacheKey;
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            MemoryCacheEntryOptions options = new();
            DateTimeOffset? expiresUtc = ticket.Properties.ExpiresUtc;
            if (expiresUtc.HasValue)
            {
                options.SetAbsoluteExpiration(expiresUtc.Value);
            }
            options.SetSlidingExpiration(TimeSpan.FromHours(1));
            m_memoryCache.Set(key, ticket, options);
            return Task.CompletedTask;
        }

        public Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            var ticketWasPresent = m_memoryCache.TryGetValue(key, out AuthenticationTicket ticket);

            return ticketWasPresent ? Task.FromResult(ticket) : Task.FromResult<AuthenticationTicket>(null);
        }

        public Task RemoveAsync(string key)
        {
            m_memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        private string GenerateEntryKey()
        {
            var entryKey = Guid.NewGuid();
            return entryKey.ToString();
        }
    }
}
