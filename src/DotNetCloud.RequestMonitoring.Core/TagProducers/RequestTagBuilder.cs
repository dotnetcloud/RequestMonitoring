using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DotNetCloud.RequestMonitoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace DotNetCloud.RequestMonitoring.Core.TagProducers
{
    internal class RequestTagBuilder : IRequestTagBuilder
    {
        private readonly IEnumerable<ITagProducer> _tagProducers;

        private readonly ConcurrentDictionary<int, string[]> _tagArrays = new ConcurrentDictionary<int, string[]>();

        public RequestTagBuilder(IEnumerable<ITagProducer> tagProducers) => _tagProducers = tagProducers;

        public string[] BuildTags(HttpContext context)
        {
            var hash = new HashCode();
            
            foreach (var value in _tagProducers.Select(t => t.GetValue(context)).Where(tag => !string.IsNullOrEmpty(tag)))
            {
                hash.Add(value);
            }

            var hashCode = hash.ToHashCode();

            if (_tagArrays.TryGetValue(hashCode, out var tags))
            {
                return tags;
            }
            
            var tagArray = _tagProducers
                .Select(tagProducer => tagProducer.ProduceTag(context))
                .Where(tag => !string.IsNullOrEmpty(tag)).ToArray();

            _tagArrays.TryAdd(hashCode, tagArray);

            return tagArray;
        }
    }
}