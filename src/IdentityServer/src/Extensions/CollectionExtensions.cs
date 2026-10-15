using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#pragma warning disable 1591

// Root namespace so every Ourstudio.IdentityServer.* file sees it. Replaces the
// public CollectionUtilities.IsNullOrEmpty that Microsoft.IdentityModel.Tokens 7.x
// exposed and 8.x made internal.
namespace Ourstudio.IdentityServer
{
    internal static class CollectionExtensions
    {
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list) => list == null || !list.Any();
    }
}
