using ContactsManager.Entities;
using ContactsManager.ServiceContracts;
using ContactsManager.ServiceContracts.DTO;

namespace ContactsManager.Services;

public class CountriesService : ICountriesService
{
    private readonly List<Country> _countries;

    public CountriesService(bool initialize = true)
    {
        _countries = new List<Country>();
        if (initialize)
            _countries.AddRange(
                new List<Country>
                {
                    new()
                    {
                        CountryId = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B"),
                        CountryName = "USA",
                    },
                    new()
                    {
                        CountryId = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F"),
                        CountryName = "Canada",
                    },
                    new()
                    {
                        CountryId = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E"),
                        CountryName = "UK",
                    },
                    new()
                    {
                        CountryId = Guid.Parse("15889048-AF93-412C-B8F3-22103E943A6D"),
                        CountryName = "India",
                    },
                    new()
                    {
                        CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB"),
                        CountryName = "Australia",
                    },
                }
            );
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
