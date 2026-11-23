using System.Collections.Generic;
using System.Globalization;

public sealed class AseanCountry
{
    public string Name { get; }
    public int Code { get; }

    private AseanCountry(string name, int code)
    {
        Name = name;
        Code = code;
    }

    public static readonly AseanCountry Philippines = new("Philippines", 63);
    public static readonly AseanCountry Thailand = new("Thailand", 66);
    public static readonly AseanCountry Singapore = new("Singapore", 65);
    public static readonly AseanCountry Indonesia = new("Indonesia", 62);
    public static readonly AseanCountry Malaysia = new("Malaysia", 60);

    public static IReadOnlyList<AseanCountry> All { get; } =
        new[] { Philippines, Thailand, Singapore, Indonesia, Malaysia };

    public static bool TryFromCode(int code, out AseanCountry country)
    {
        foreach (AseanCountry candidate in All)
        {
            if (candidate.Code == code)
            {
                country = candidate;
                return true;
            }
        }

        country = null!;
        return false;
    }

    public static bool TryParseCode(string text, out AseanCountry country)
    {
        if (int.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out int code))
        {
            return TryFromCode(code, out country);
        }

        country = null!;
        return false;
    }

    public override string ToString() => Name;
}
