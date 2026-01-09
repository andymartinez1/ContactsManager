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

    public PersonService(bool initialize = true)
    {
        _countriesService = new CountriesService();
        _persons = new List<Person>();
        if (initialize)
        {
            _persons.Add(
                new Person
                {
                    PersonID = Guid.Parse("8082ED0C-396D-4162-AD1D-29A13F929824"),
                    PersonName = "Aguste",
                    Email = "aleddy0@booking.com",
                    DateOfBirth = DateTime.Parse("1993-01-02"),
                    Gender = "Male",
                    Address = "0858 Novick Terrace",
                    ReceiveNewsLetter = false,
                    CountryID = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B"),
                }
            );

            _persons.Add(
                new Person
                {
                    PersonID = Guid.Parse("06D15BAD-52F4-498E-B478-ACAD847ABFAA"),
                    PersonName = "Jasmina",
                    Email = "jsyddie1@miibeian.gov.cn",
                    DateOfBirth = DateTime.Parse("1991-06-24"),
                    Gender = "Female",
                    Address = "0742 Fieldstone Lane",
                    ReceiveNewsLetter = true,
                    CountryID = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F"),
                }
            );

            _persons.Add(
                new Person
                {
                    PersonID = Guid.Parse("D3EA677A-0F5B-41EA-8FEF-EA2FC41900FD"),
                    PersonName = "Kendall",
                    Email = "khaquard2@arstechnica.com",
                    DateOfBirth = DateTime.Parse("1993-08-13"),
                    Gender = "Male",
                    Address = "7050 Pawling Alley",
                    ReceiveNewsLetter = false,
                    CountryID = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F"),
                }
            );

            _persons.Add(
                new Person
                {
                    PersonID = Guid.Parse("89452EDB-BF8C-4283-9BA4-8259FD4A7A76"),
                    PersonName = "Kilian",
                    Email = "kaizikowitz3@joomla.org",
                    DateOfBirth = DateTime.Parse("1991-06-17"),
                    Gender = "Male",
                    Address = "233 Buhler Junction",
                    ReceiveNewsLetter = true,
                    CountryID = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E"),
                }
            );

            _persons.Add(
                new Person
                {
                    PersonID = Guid.Parse("F5BD5979-1DC1-432C-B1F1-DB5BCCB0E56D"),
                    PersonName = "Dulcinea",
                    Email = "dbus4@pbs.org",
                    DateOfBirth = DateTime.Parse("1996-09-02"),
                    Gender = "Female",
                    Address = "56 Sundown Point",
                    ReceiveNewsLetter = false,
                    CountryID = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E"),
                }
            );

            _persons.Add(
                new Person
                {
                    PersonID = Guid.Parse("A795E22D-FAED-42F0-B134-F3B89B8683E5"),
                    PersonName = "Corabelle",
                    Email = "cadams5@t-online.de",
                    DateOfBirth = DateTime.Parse("1993-10-23"),
                    Gender = "Female",
                    Address = "4489 Hazelcrest Place",
                    ReceiveNewsLetter = false,
                    CountryID = Guid.Parse("15889048-AF93-412C-B8F3-22103E943A6D"),
                }
            );

            _persons.Add(
                new Person
                {
                    PersonID = Guid.Parse("3C12D8E8-3C1C-4F57-B6A4-C8CAAC893D7A"),
                    PersonName = "Faydra",
                    Email = "fbischof6@boston.com",
                    DateOfBirth = DateTime.Parse("1996-02-14"),
                    Gender = "Female",
                    Address = "2010 Farragut Pass",
                    ReceiveNewsLetter = true,
                    CountryID = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB"),
                }
            );

            _persons.Add(
                new Person
                {
                    PersonID = Guid.Parse("7B75097B-BFF2-459F-8EA8-63742BBD7AFB"),
                    PersonName = "Oby",
                    Email = "oclutheram7@foxnews.com",
                    DateOfBirth = DateTime.Parse("1992-05-31"),
                    Gender = "Male",
                    Address = "2 Fallview Plaza",
                    ReceiveNewsLetter = false,
                    CountryID = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB"),
                }
            );

            _persons.Add(
                new Person
                {
                    PersonID = Guid.Parse("6717C42D-16EC-4F15-80D8-4C7413E250CB"),
                    PersonName = "Seumas",
                    Email = "ssimonitto8@biglobe.ne.jp",
                    DateOfBirth = DateTime.Parse("1999-02-02"),
                    Gender = "Male",
                    Address = "76779 Norway Maple Crossing",
                    ReceiveNewsLetter = false,
                    CountryID = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB"),
                }
            );

            _persons.Add(
                new Person
                {
                    PersonID = Guid.Parse("6E789C86-C8A6-4F18-821C-2ABDB2E95982"),
                    PersonName = "Freemon",
                    Email = "faugustin9@vimeo.com",
                    DateOfBirth = DateTime.Parse("1996-04-27"),
                    Gender = "Male",
                    Address = "8754 Becker Street",
                    ReceiveNewsLetter = false,
                    CountryID = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB"),
                }
            );
        }
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
