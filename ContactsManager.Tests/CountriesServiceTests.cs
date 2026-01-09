using ContactsManager.ServiceContracts;
using ContactsManager.ServiceContracts.DTO;
using ContactsManager.Services;

namespace ContactsManager.Tests;

public class CountriesServiceTests
{
    private readonly ICountriesService _countriesService;

    public CountriesServiceTests()
    {
        _countriesService = new CountriesService(false);
    }

    #region GetAllCountries

    [Fact]
    public void GetAllCountries_ByDefault_ShouldBeEmpty()
    {
        // Act
        var countryResponse = _countriesService.GetAllCountries();

        // Assert
        Assert.Empty(countryResponse);
    }

    [Fact]
    public void GetAllCountries_AddCountries_ShouldReturnAddedCountries()
    {
        // Arrange
        var countryAddRequests = new List<CountryAddRequest>
        {
            new() { CountryName = "USA" },
            new() { CountryName = "Japan" },
            new() { CountryName = "India" },
        };

        // Act
        var countryResponses = new List<CountryResponse>();
        foreach (var country in countryAddRequests)
            countryResponses.Add(_countriesService.AddCountry(country));

        var actualCountryResponses = _countriesService.GetAllCountries();

        foreach (var expectedCountry in countryResponses)
            // Assert
            Assert.Contains(expectedCountry, actualCountryResponses);
    }

    #endregion

    #region AddCountry

    [Fact]
    public void AddCountry_NullCountry_ThrowsArgumentNullException()
    {
        // Arrange
        CountryAddRequest? request = null;

        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            _countriesService.AddCountry(request);
        });
    }

    [Fact]
    public void AddCountry_CountryNameIsNull_ThrowsArgumentException()
    {
        // Arrange
        var request = new CountryAddRequest { CountryName = null };

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _countriesService.AddCountry(request);
        });
    }

    [Fact]
    public void AddCountry_DuplicateCountryName_ThrowsArgumentException()
    {
        // Arrange
        var request1 = new CountryAddRequest { CountryName = "USA" };
        var request2 = new CountryAddRequest { CountryName = "USA" };

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _countriesService.AddCountry(request1);
            _countriesService.AddCountry(request2);
        });
    }

    [Fact]
    public void AddCountry_ProperCountryDetails_AddsCountryToList()
    {
        // Arrange
        var request = new CountryAddRequest { CountryName = "USA" };

        // Act
        var response = _countriesService.AddCountry(request);
        var countryResponses = _countriesService.GetAllCountries();

        // Assert
        Assert.True(response.CountryId != Guid.Empty);
        Assert.Contains(response, countryResponses);
    }

    #endregion

    #region GetCountryByCountryID

    [Fact]
    public void GetCountryByCountryID_NullCountryID_ReturnsNullValue()
    {
        // Arrange
        Guid? countryID = null;

        // Act
        var countryResponseFromGet = _countriesService.GetCountryByCountryID(countryID);

        // Assert
        Assert.Null(countryResponseFromGet);
    }

    [Fact]
    public void GetCountryByCountryID_ValidCountryID_ReturnsCountryResponse()
    {
        // Arrange
        var request = new CountryAddRequest { CountryName = "China" };
        var countryResponseFromAdd = _countriesService.AddCountry(request);

        // Act
        var countryResponseFromGet = _countriesService.GetCountryByCountryID(
            countryResponseFromAdd.CountryId
        );

        // Assert
        Assert.Equal(countryResponseFromAdd, countryResponseFromGet);
    }

    #endregion
}
