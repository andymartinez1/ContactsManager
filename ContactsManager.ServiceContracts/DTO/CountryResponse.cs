using ContactsManager.Entities;

namespace ContactsManager.ServiceContracts.DTO;

/// <summary>
///     DTO class used as return type for most CountriesService methods
/// </summary>
public class CountryResponse
{
    public Guid CountryId { get; set; }

    public string? CountryName { get; set; }

    // Returns true if the value(instead of reference) of the comparison are equal
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (obj.GetType() != typeof(CountryResponse))
            return false;

        var countryToCompare = (CountryResponse)obj;

        return CountryId == countryToCompare.CountryId
            && CountryName == countryToCompare.CountryName;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

/// <summary>
///     Extension method to convert an object of Country to CountryResponse
/// </summary>
public static class CountryExtensions
{
    public static CountryResponse ToCountryResponse(this Country country)
    {
        return new CountryResponse
        {
            CountryId = country.CountryId,
            CountryName = country.CountryName,
        };
    }
}
