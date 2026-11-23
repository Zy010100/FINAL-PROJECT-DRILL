using System;

namespace AseanPhonebook.Models;

public sealed class PhoneNumber
{
    private string _areaCode;
    private string _number;

    public PhoneNumber(AseanCountry country, string areaCode, string number)
    {
        Country = country ?? throw new ArgumentNullException(nameof(country));
        _areaCode = RequireDigits(areaCode, nameof(areaCode));
        _number = RequireDigits(number, nameof(number));
    }

    public AseanCountry Country { get; set; }

    public string AreaCode
    {
        get => _areaCode;
        set => _areaCode = RequireDigits(value, nameof(value));
    }

    public string Number
    {
        get => _number;
        set => _number = RequireDigits(value, nameof(value));
    }

    public static bool IsDigits(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }

        foreach (char c in text)
        {
            if (c < '0' || c > '9')
            {
                return false;
            }
        }

        return true;
    }

    private static string RequireDigits(string? text, string paramName)
    {
        if (!IsDigits(text))
        {
            throw new ArgumentException("Value must contain digits only.", paramName);
        }

        return text!;
    }

    public override string ToString() => $"{Country.Code}-{AreaCode}-{Number}";
}
