using ContactsManager.Entities;
using ContactsManager.ServiceContracts;
using ContactsManager.ServiceContracts.DTO;

namespace ContactsManager.Services;

public class CountriesService : ICountriesService
{
    private readonly List<Country> _countries;

    public CountriesService()
    {
        _countries = new List<Country>();
    }

    public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
    {
        if (countryAddRequest == null)
            throw new ArgumentNullException(nameof(countryAddRequest));

        if (countryAddRequest.CountryName == null)
            throw new ArgumentException(nameof(countryAddRequest.CountryName));

        if (_countries.Where(c => c.CountryName == countryAddRequest.CountryName).Count() > 0)
            throw new ArgumentException("Country name already exists.");

        // Convert object from CountryAddRequest to Country type
        var country = countryAddRequest.ToCountry();

        country.CountryId = Guid.NewGuid();
        _countries.Add(country);

        // Convert to CountryResponse and return
        return country.ToCountryResponse();
    }

    public List<CountryResponse> GetAllCountries()
    {
        return _countries.Select(c => c.ToCountryResponse()).ToList();
    }

    public CountryResponse? GetCountryByCountryID(Guid? countryID)
    {
        if (countryID == null)
            return null;

        var countryResponse = _countries.FirstOrDefault(c => c.CountryId == countryID);

        if (countryResponse == null)
            return null;

        return countryResponse.ToCountryResponse();
    }
}
