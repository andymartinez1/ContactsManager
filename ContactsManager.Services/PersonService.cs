using ContactsManager.Entities;
using ContactsManager.ServiceContracts;
using ContactsManager.ServiceContracts.DTO;
using ContactsManager.ServiceContracts.Enums;
using ContactsManager.Services.Helpers;

namespace ContactsManager.Services;

public class PersonService : IPersonsService
{
    private readonly ICountriesService _countriesService;
    private readonly List<Person> _persons;

    public PersonService()
    {
        _countriesService = new CountriesService();
        _persons = new List<Person>();
    }

    public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
    {
        if (personAddRequest == null)
            throw new ArgumentNullException(nameof(personAddRequest));

        // Model validation
        ValidationHelper.ModelValidation(personAddRequest);

        var person = personAddRequest.ToPerson();
        person.PersonID = Guid.NewGuid();

        _persons.Add(person);

        return ConvertPersonToPersonResponse(person);
    }

    public List<PersonResponse> GetAllPersons()
    {
        return _persons.Select(p => p.ToPersonResponse()).ToList();
    }

    public PersonResponse? GetPersonByPersonID(Guid? personID)
    {
        if (personID is null)
            return null;

        var person = _persons.FirstOrDefault(p => p.PersonID == personID);

        if (person is null)
            return null;

        return person.ToPersonResponse();
    }

    public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
    {
        var allPersons = GetAllPersons();
        var matchingPersons = allPersons;

        if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
            return matchingPersons;

        switch (searchBy)
        {
            case nameof(Person.PersonName):
                matchingPersons = allPersons
                    .Where(pr =>
                        string.IsNullOrEmpty(pr.PersonName)
                        || pr.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    )
                    .ToList();
                break;

            case nameof(Person.Email):
                matchingPersons = allPersons
                    .Where(pr =>
                        string.IsNullOrEmpty(pr.Email)
                        || pr.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    )
                    .ToList();
                break;

            case nameof(Person.DateOfBirth):
                matchingPersons = allPersons
                    .Where(pr =>
                        pr.DateOfBirth == null
                        || pr.DateOfBirth.Value.ToString("dd MMMM yyyy")
                            .Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    )
                    .ToList();
                break;

            case nameof(Person.Gender):
                matchingPersons = allPersons
                    .Where(pr =>
                        string.IsNullOrEmpty(pr.Gender)
                        || pr.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    )
                    .ToList();
                break;

            case nameof(Person.CountryID):
                matchingPersons = allPersons
                    .Where(pr =>
                        string.IsNullOrEmpty(pr.Country)
                        || pr.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    )
                    .ToList();
                break;

            case nameof(Person.Address):
                matchingPersons = allPersons
                    .Where(pr =>
                        string.IsNullOrEmpty(pr.Address)
                        || pr.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    )
                    .ToList();
                break;

            default:
                matchingPersons = allPersons;
                break;
        }

        return matchingPersons;
    }

    public List<PersonResponse> GetSortedPersons(
        List<PersonResponse> allPersons,
        string sortBy,
        SortOrderOptions sortOrder
    )
    {
        if (string.IsNullOrEmpty(sortBy))
            return allPersons;

        var sortedPersons = (sortBy, sortOrder) switch
        {
            (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) => allPersons
                .OrderBy(pr => pr.PersonName, StringComparer.OrdinalIgnoreCase)
                .ToList(),

            (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) => allPersons
                .OrderByDescending(pr => pr.PersonName, StringComparer.OrdinalIgnoreCase)
                .ToList(),

            (nameof(PersonResponse.Email), SortOrderOptions.ASC) => allPersons
                .OrderBy(pr => pr.Email, StringComparer.OrdinalIgnoreCase)
                .ToList(),

            (nameof(PersonResponse.Email), SortOrderOptions.DESC) => allPersons
                .OrderByDescending(pr => pr.Email, StringComparer.OrdinalIgnoreCase)
                .ToList(),

            (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) => allPersons
                .OrderBy(pr => pr.DateOfBirth)
                .ToList(),

            (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) => allPersons
                .OrderByDescending(pr => pr.DateOfBirth)
                .ToList(),

            (nameof(PersonResponse.Age), SortOrderOptions.ASC) => allPersons
                .OrderBy(pr => pr.Age)
                .ToList(),

            (nameof(PersonResponse.Age), SortOrderOptions.DESC) => allPersons
                .OrderByDescending(pr => pr.Age)
                .ToList(),

            (nameof(PersonResponse.Gender), SortOrderOptions.ASC) => allPersons
                .OrderBy(pr => pr.Gender, StringComparer.OrdinalIgnoreCase)
                .ToList(),

            (nameof(PersonResponse.Gender), SortOrderOptions.DESC) => allPersons
                .OrderByDescending(pr => pr.Gender, StringComparer.OrdinalIgnoreCase)
                .ToList(),

            (nameof(PersonResponse.Country), SortOrderOptions.ASC) => allPersons
                .OrderBy(pr => pr.Country, StringComparer.OrdinalIgnoreCase)
                .ToList(),

            (nameof(PersonResponse.Country), SortOrderOptions.DESC) => allPersons
                .OrderByDescending(pr => pr.Country, StringComparer.OrdinalIgnoreCase)
                .ToList(),

            (nameof(PersonResponse.Address), SortOrderOptions.ASC) => allPersons
                .OrderBy(pr => pr.Address, StringComparer.OrdinalIgnoreCase)
                .ToList(),

            (nameof(PersonResponse.Address), SortOrderOptions.DESC) => allPersons
                .OrderByDescending(pr => pr.Address, StringComparer.OrdinalIgnoreCase)
                .ToList(),

            (nameof(PersonResponse.ReceiveNewsLetter), SortOrderOptions.ASC) => allPersons
                .OrderBy(pr => pr.ReceiveNewsLetter)
                .ToList(),

            (nameof(PersonResponse.ReceiveNewsLetter), SortOrderOptions.DESC) => allPersons
                .OrderByDescending(pr => pr.ReceiveNewsLetter)
                .ToList(),

            _ => allPersons,
        };

        return sortedPersons;
    }

    public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
    {
        if (personUpdateRequest is null)
            throw new ArgumentNullException(nameof(personUpdateRequest));

        ValidationHelper.ModelValidation(personUpdateRequest);

        var matchingPerson = _persons.FirstOrDefault(p =>
            p.PersonID == personUpdateRequest.PersonID
        );
        if (matchingPerson is null)
            throw new ArgumentException("ID does not exist.");

        matchingPerson.PersonName = personUpdateRequest.PersonName;
        matchingPerson.Email = personUpdateRequest.Email;
        matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
        matchingPerson.Gender = personUpdateRequest.Gender.ToString();
        matchingPerson.CountryID = personUpdateRequest.CountryID;
        matchingPerson.Address = personUpdateRequest.Address;
        matchingPerson.ReceiveNewsLetter = personUpdateRequest.ReceiveNewsLetter;

        return matchingPerson.ToPersonResponse();
    }

    public bool DeletePerson(Guid? personID)
    {
        if (personID is null)
            throw new ArgumentNullException(nameof(personID));

        var person = _persons.FirstOrDefault(p => p.PersonID == personID);

        if (person is null)
            return false;

        _persons.Remove(person);

        return true;
    }

    private PersonResponse ConvertPersonToPersonResponse(Person person)
    {
        // Reduces boilerplate code adding the country given the country ID here only
        var personResponse = person.ToPersonResponse();
        personResponse.Country = _countriesService
            .GetCountryByCountryID(person.CountryID)
            ?.CountryName;
        return personResponse;
    }
}
