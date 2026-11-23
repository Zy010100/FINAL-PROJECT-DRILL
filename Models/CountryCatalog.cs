using System.Collections.Generic;
using System.Linq;

public static class CountryCatalog
{
    public static readonly List<AseanCountry> Countries =
        new List<AseanCountry>
        {
            new AseanCountry("Malaysia", "60"),
            new AseanCountry("Indonesia", "62"),
            new AseanCountry("Philippines", "63"),
            new AseanCountry("Singapore", "65"),
            new AseanCountry("Thailand", "66")
        };

    // Order used in the country search menu
    public static readonly List<AseanCountry> SearchOrder =
        new List<AseanCountry>
        {
            Countries[2], // Philippines
            Countries[4], // Thailand
            Countries[3], // Singapore
            Countries[1], // Indonesia
            Countries[0]  // Malaysia
        };

    public static AseanCountry FindByCode(string code)
    {
        return Countries.FirstOrDefault(
            country => country.CountryCode == code);
    }
}
