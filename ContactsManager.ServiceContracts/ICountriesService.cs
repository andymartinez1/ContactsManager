using ContactsManager.ServiceContracts.DTO;

namespace ContactsManager.ServiceContracts;

/// <summary>
///     Represents business logic for manipulating the Country entity
/// </summary>
public interface ICountriesService
{
    /// <summary>
    ///     Adds a country object to the list of countries
    /// </summary>
    /// <param name="countryAddRequest"></param>
    /// <returns>Returns the Country object after adding it</returns>
    public CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

    /// <summary>
    ///     Returns all countries
    /// </summary>
    /// <returns>All countries</returns>
    public List<CountryResponse> GetAllCountries();

    /// <summary>
    ///     Returns a country object given the country id
    /// </summary>
    /// <param name="countryID"></param>
    /// <returns>Matching country object as CountryResponse</returns>
    public CountryResponse? GetCountryByCountryID(Guid? countryID);
}
