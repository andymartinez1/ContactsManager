using ContactsManager.Entities;
using ContactsManager.ServiceContracts;
using ContactsManager.ServiceContracts.DTO;
using ContactsManager.ServiceContracts.Enums;
using ContactsManager.Services;
using Xunit.Abstractions;

namespace ContactsManager.Tests;

public class PersonsServiceTests
{
    private readonly ICountriesService _countriesService;
    private readonly IPersonsService _personsService;
    private readonly ITestOutputHelper _testOutputHelper;

    public PersonsServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _personsService = new PersonService(false);
        _countriesService = new CountriesService(false);
    }

    #region GetSortedPersons

    [Fact]
    public void GetSortedPersons_SortByPersonNameInDescOrder_ShouldReturnPersonsInDescOrder()
    {
        // Assert
        var countryAddRequest1 = new CountryAddRequest { CountryName = "USA" };
        var countryAddRequest2 = new CountryAddRequest { CountryName = "China" };
        var countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
        var countryResponse2 = _countriesService.AddCountry(countryAddRequest2);

        var personAddRequest1 = new PersonAddRequest
        {
            PersonName = "Test1",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Male,
            DateOfBirth = DateTime.Parse("2000-12-31"),
            ReceiveNewsLetter = true,
            CountryID = countryResponse1.CountryId,
        };
        var personAddRequest2 = new PersonAddRequest
        {
            PersonName = "Test2",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Female,
            DateOfBirth = DateTime.Parse("1991-01-12"),
            ReceiveNewsLetter = false,
            CountryID = countryResponse2.CountryId,
        };
        var requests = new List<PersonAddRequest> { personAddRequest1, personAddRequest2 };

        var responsesFromAdd = new List<PersonResponse>();
        foreach (var request in requests)
        {
            var personResponse = _personsService.AddPerson(request);
            responsesFromAdd.Add(personResponse);
        }

        var allPersons = _personsService.GetAllPersons();

        // Act
        _testOutputHelper.WriteLine("Actual: ");
        var personsResponsesFromSort = _personsService.GetSortedPersons(
            allPersons,
            nameof(Person.PersonName),
            SortOrderOptions.DESC
        );
        foreach (var response in personsResponsesFromSort)
            _testOutputHelper.WriteLine(response.ToString());

        responsesFromAdd = responsesFromAdd.OrderByDescending(pr => pr.PersonName).ToList();

        // Print each object in expected response list
        _testOutputHelper.WriteLine("Expected: ");
        foreach (var response in responsesFromAdd)
            _testOutputHelper.WriteLine(response.ToString());

        // Assert
        for (var i = 0; i < responsesFromAdd.Count; i++)
            Assert.Equal(responsesFromAdd[i], personsResponsesFromSort[i]);
    }

    #endregion

    #region DeletePerson

    [Fact]
    public void DeletePerson_ValidPersonID_ReturnsTrue()
    {
        // Arrange
        var countryAddRequest = new CountryAddRequest { CountryName = "USA" };
        var countryResponse = _countriesService.AddCountry(countryAddRequest);
        var personAddRequest = new PersonAddRequest
        {
            PersonName = "Test1",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Male,
            DateOfBirth = DateTime.Parse("2000-12-31"),
            ReceiveNewsLetter = true,
            CountryID = countryResponse.CountryId,
        };
        var personResponse = _personsService.AddPerson(personAddRequest);

        // Act
        var isDeleted = _personsService.DeletePerson(personResponse.PersonID);

        // Assert
        Assert.True(isDeleted);
    }

    [Fact]
    public void DeletePerson_InvalidPersonID_ReturnsFalse()
    {
        // Act
        var isDeleted = _personsService.DeletePerson(Guid.NewGuid());

        // Assert
        Assert.False(isDeleted);
    }

    #endregion

    #region UpdatePerson

    [Fact]
    public void UpdatePerson_NullPerson_ThrowsArgumentNullException()
    {
        // Arrange
        PersonUpdateRequest? request = null;

        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            _personsService.UpdatePerson(request);
        });
    }

    [Fact]
    public void UpdatePerson_NullPersonID_ThrowsArgumentException()
    {
        // Arrange
        var request = new PersonUpdateRequest { PersonID = Guid.NewGuid() };

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _personsService.UpdatePerson(request);
        });
    }

    [Fact]
    public void UpdatePerson_NullPersonName_ThrowsArgumentException()
    {
        // Arrange
        var countryAddRequest = new CountryAddRequest { CountryName = "USA" };
        var countryResponse = _countriesService.AddCountry(countryAddRequest);
        var personAddRequest = new PersonAddRequest
        {
            PersonName = "Test1",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Male,
            DateOfBirth = DateTime.Parse("2000-12-31"),
            ReceiveNewsLetter = true,
            CountryID = countryResponse.CountryId,
        };
        var personResponse = _personsService.AddPerson(personAddRequest);

        var updateRequest = personResponse.ToPersonUpdateRequest();
        updateRequest.PersonName = null;

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _personsService.UpdatePerson(updateRequest);
        });
    }

    [Fact]
    public void UpdatePerson_ValidPersonDetails_UpdatesPerson()
    {
        // Arrange
        var countryAddRequest = new CountryAddRequest { CountryName = "USA" };
        var countryResponse = _countriesService.AddCountry(countryAddRequest);
        var personAddRequest = new PersonAddRequest
        {
            PersonName = "Test1",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Male,
            DateOfBirth = DateTime.Parse("2000-12-31"),
            ReceiveNewsLetter = true,
            CountryID = countryResponse.CountryId,
        };
        var personResponse = _personsService.AddPerson(personAddRequest);

        var updateRequest = personResponse.ToPersonUpdateRequest();
        updateRequest.PersonName = "Updated Name";

        // Act
        var updatedPerson = _personsService.UpdatePerson(updateRequest);
        var getPerson = _personsService.GetPersonByPersonID(updatedPerson.PersonID);

        // Assert
        Assert.Equal(getPerson, updatedPerson);
    }

    #endregion

    #region GetAllPersons

    [Fact]
    public void GetAllPersons_ByDefault_ShouldBeEmpty()
    {
        // Act
        var personResponse = _personsService.GetAllPersons();

        // Assert
        Assert.Empty(personResponse);
    }

    [Fact]
    public void GetAllPersons_AddPersons_ShouldReturnAddedPersons()
    {
        // Assert
        var countryAddRequest1 = new CountryAddRequest { CountryName = "USA" };
        var countryAddRequest2 = new CountryAddRequest { CountryName = "China" };
        var countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
        var countryResponse2 = _countriesService.AddCountry(countryAddRequest2);

        var personAddRequest1 = new PersonAddRequest
        {
            PersonName = "Test1",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Male,
            DateOfBirth = DateTime.Parse("2000-12-31"),
            ReceiveNewsLetter = true,
            CountryID = countryResponse1.CountryId,
        };
        var personAddRequest2 = new PersonAddRequest
        {
            PersonName = "Test2",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Female,
            DateOfBirth = DateTime.Parse("1991-01-12"),
            ReceiveNewsLetter = false,
            CountryID = countryResponse2.CountryId,
        };
        var requests = new List<PersonAddRequest> { personAddRequest1, personAddRequest2 };

        var responsesFromAdd = new List<PersonResponse>();
        foreach (var request in requests)
        {
            var personResponse = _personsService.AddPerson(request);
            responsesFromAdd.Add(personResponse);
        }

        // Print each object in expected response list
        _testOutputHelper.WriteLine("Expected: ");
        foreach (var response in responsesFromAdd)
            _testOutputHelper.WriteLine(response.ToString());

        // Act
        _testOutputHelper.WriteLine("Actual: ");
        var personsResponsesFromGetAll = _personsService.GetAllPersons();
        foreach (var response in personsResponsesFromGetAll)
            _testOutputHelper.WriteLine(response.ToString());

        // Assert
        foreach (var response in responsesFromAdd)
            Assert.Contains(response, personsResponsesFromGetAll);
    }

    #endregion

    #region GetPersonByPersonID

    [Fact]
    public void GetPersonByPersonID_NullPersonID_ReturnsNullValue()
    {
        // Arrange
        Guid? personID = null;

        // Act
        var response = _personsService.GetPersonByPersonID(personID);

        // Assert
        Assert.Null(response);
    }

    [Fact]
    public void GetPersonByPersonID_ValidPersonID_ReturnsPersonResponse()
    {
        // Arrange
        var newCountry = new CountryAddRequest { CountryName = "India" };
        var countryResponse = _countriesService.AddCountry(newCountry);

        // Act
        var newPerson = new PersonAddRequest
        {
            PersonName = "Test",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Male,
            DateOfBirth = DateTime.Parse("2000-12-31"),
            ReceiveNewsLetter = true,
            CountryID = countryResponse.CountryId,
        };
        var personResponse = _personsService.AddPerson(newPerson);

        var response = _personsService.GetPersonByPersonID(personResponse.PersonID);

        // Assert
        Assert.Equal(personResponse, response);
    }

    #endregion

    #region AddPerson

    [Fact]
    public void AddPerson_NullPerson_ThrowsArgumentNullException()
    {
        // Arrange
        PersonAddRequest? request = null;

        // Act
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            return _personsService.AddPerson(request);
        });
    }

    [Fact]
    public void AddPerson_NullPersonName_ThrowsArgumentException()
    {
        // Arrange
        var request = new PersonAddRequest { PersonName = null };

        // Act
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            return _personsService.AddPerson(request);
        });
    }

    [Fact]
    public void AddPerson_ProperPersonDetails_AddsPersonToList()
    {
        // Arrange
        var request = new PersonAddRequest
        {
            PersonName = "Test",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Male,
            DateOfBirth = DateTime.Parse("2000-12-31"),
            ReceiveNewsLetter = true,
            CountryID = Guid.NewGuid(),
        };

        // Act
        var response = _personsService.AddPerson(request);
        var allPersons = _personsService.GetAllPersons();

        // Assert
        Assert.True(response.PersonID != Guid.Empty);
        Assert.Contains(response, allPersons);
    }

    #endregion

    #region GetFilteredPersons

    [Fact]
    public void GetFilteredPersons_SearchByPersonName_ShouldReturnPersonsContainingMatchingPersonName()
    {
        // Assert
        var countryAddRequest1 = new CountryAddRequest { CountryName = "USA" };
        var countryAddRequest2 = new CountryAddRequest { CountryName = "China" };
        var countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
        var countryResponse2 = _countriesService.AddCountry(countryAddRequest2);

        var personAddRequest1 = new PersonAddRequest
        {
            PersonName = "Test1",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Male,
            DateOfBirth = DateTime.Parse("2000-12-31"),
            ReceiveNewsLetter = true,
            CountryID = countryResponse1.CountryId,
        };
        var personAddRequest2 = new PersonAddRequest
        {
            PersonName = "Test2",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Female,
            DateOfBirth = DateTime.Parse("1991-01-12"),
            ReceiveNewsLetter = false,
            CountryID = countryResponse2.CountryId,
        };
        var requests = new List<PersonAddRequest> { personAddRequest1, personAddRequest2 };

        var responsesFromAdd = new List<PersonResponse>();
        foreach (var request in requests)
        {
            var personResponse = _personsService.AddPerson(request);
            responsesFromAdd.Add(personResponse);
        }

        // Print each object in expected response list
        _testOutputHelper.WriteLine("Expected: ");
        foreach (var response in responsesFromAdd)
            _testOutputHelper.WriteLine(response.ToString());

        // Act
        _testOutputHelper.WriteLine("Actual: ");
        var personsResponsesFromSearch = _personsService.GetFilteredPersons(
            nameof(Person.PersonName),
            "1"
        );
        foreach (var response in personsResponsesFromSearch)
            _testOutputHelper.WriteLine(response.ToString());

        // Assert
        foreach (var response in responsesFromAdd)
            if (response.PersonName.Contains("1"))
                Assert.Contains(response, personsResponsesFromSearch);
    }

    [Fact]
    public void GetFilteredPersons_SearchTextEmpty_ShouldReturnAllPersons()
    {
        // Assert
        var countryAddRequest1 = new CountryAddRequest { CountryName = "USA" };
        var countryAddRequest2 = new CountryAddRequest { CountryName = "China" };
        var countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
        var countryResponse2 = _countriesService.AddCountry(countryAddRequest2);

        var personAddRequest1 = new PersonAddRequest
        {
            PersonName = "Test1",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Male,
            DateOfBirth = DateTime.Parse("2000-12-31"),
            ReceiveNewsLetter = true,
            CountryID = countryResponse1.CountryId,
        };
        var personAddRequest2 = new PersonAddRequest
        {
            PersonName = "Test2",
            Email = "test@test.com",
            Address = "123 Test Lane",
            Gender = GenderOptions.Female,
            DateOfBirth = DateTime.Parse("1991-01-12"),
            ReceiveNewsLetter = false,
            CountryID = countryResponse2.CountryId,
        };
        var requests = new List<PersonAddRequest> { personAddRequest1, personAddRequest2 };

        var responsesFromAdd = new List<PersonResponse>();
        foreach (var request in requests)
        {
            var personResponse = _personsService.AddPerson(request);
            responsesFromAdd.Add(personResponse);
        }

        // Print each object in expected response list
        _testOutputHelper.WriteLine("Expected: ");
        foreach (var response in responsesFromAdd)
            _testOutputHelper.WriteLine(response.ToString());

        // Act
        _testOutputHelper.WriteLine("Actual: ");
        var personsResponsesFromSearch = _personsService.GetFilteredPersons(
            nameof(Person.PersonName),
            ""
        );
        foreach (var response in personsResponsesFromSearch)
            _testOutputHelper.WriteLine(response.ToString());

        // Assert
        foreach (var response in responsesFromAdd)
            Assert.Contains(response, personsResponsesFromSearch);
    }

    #endregion
}
