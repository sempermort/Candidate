using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Application.Services
{
        public interface ICacheService
        {
            T Get<T>(string key);
            Task<T> GetAsync<T>(string key);
            void Set<T>(string key, T value, TimeSpan expiration);
            Task SetAsync<T>(string key, T value, TimeSpan expiration);
            void Remove(string key);
            Task RemoveAsync(string key);
        }
}


