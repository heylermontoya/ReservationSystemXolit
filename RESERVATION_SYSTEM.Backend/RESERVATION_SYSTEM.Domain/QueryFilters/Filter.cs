using System.Diagnostics.CodeAnalysis;

namespace RESERVATION_SYSTEM.Domain.QueryFilters
{
    [ExcludeFromCodeCoverage]
    public class Filter
    {
        public string Search { get; set; } = string.Empty;
    }
}
