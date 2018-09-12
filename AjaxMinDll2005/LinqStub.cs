// LinqStub.cs
//
// Copyright 2012 Microsoft Corporation
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace System.Linq
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Ajax.Utilities;

    /// <summary>
    /// define some Linq-style extensions that we take for granted in our .NET 3.5+ world.
    /// </summary>
    internal static class LinqStubs
    {
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            foreach (var item in source)
            {
                yield return selector(item);
            }
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            var newDictionary = new Dictionary<TKey, TElement>();
            foreach (var element in source)
            {
                newDictionary.Add(keySelector(element), elementSelector(element));
            }

            return newDictionary;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            // for each item in the source collection...
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    // as soon as ONE evaluates to true, we return true
                    return true;
                }
            }

            // if we get here, nothing evaluated to true
            return false;
        }
    }
}
