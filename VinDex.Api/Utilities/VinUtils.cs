using System.Text.RegularExpressions;

namespace VinDex.Api.Utilities;

public static partial class VinUtils
{
    // Regex for 17-character VINs (excluding I, O, Q)
    [GeneratedRegex("^[A-HJ-NPR-Z0-9]{17}$")]
    private static partial Regex VinRegex();

    public static string Normalize(string vin)
    {
        return string.IsNullOrWhiteSpace(vin) ? string.Empty : vin.Trim().ToUpperInvariant();
    }

    public static bool IsValid(string vin)
    {
        if (string.IsNullOrWhiteSpace(vin))
            return false;

        var normalized = Normalize(vin);
        return VinRegex().IsMatch(normalized);
    }
}
