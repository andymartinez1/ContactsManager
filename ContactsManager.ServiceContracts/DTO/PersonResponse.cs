using System.ComponentModel.DataAnnotations;
using ContactsManager.Entities;
using ContactsManager.ServiceContracts.Enums;

namespace ContactsManager.ServiceContracts.DTO;

/// <summary>
///     DTO class used as return type for most PersonsService methods
/// </summary>
public class PersonResponse
{
    public Guid PersonID { get; set; }

    [Display(Name = "Person Name")]
    public string? PersonName { get; set; }

    public string? Email { get; set; }

    [Display(Name = "Date of Birth")]
    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public Guid? CountryID { get; set; }

    public string? Country { get; set; }

    public string? Address { get; set; }

    [Display(Name = "Receive News Letter?")]
    public bool ReceiveNewsLetter { get; set; }

    public double? Age { get; set; }

    // Returns true if the value(instead of reference) of the comparison are equal
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (obj.GetType() != typeof(PersonResponse))
            return false;

        var personToCompare = (PersonResponse)obj;

        return PersonID == personToCompare.PersonID
            && PersonName == personToCompare.PersonName
            && Email == personToCompare.Email
            && DateOfBirth == personToCompare.DateOfBirth
            && Gender == personToCompare.Gender
            && CountryID == personToCompare.CountryID
            && Country == personToCompare.Country
            && Address == personToCompare.Address
            && ReceiveNewsLetter == personToCompare.ReceiveNewsLetter;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    // Override to get more information for testing
    public override string ToString()
    {
        return $"Person ID: {PersonID}, Person Name: {PersonName}, Email: {Email}, Date of Birth: {DateOfBirth?.ToString("dd MMM yyyy")}, Gender: {Gender}, Country ID: {CountryID}, Country: {Country}, Address: {Address}, Receive News Letter: {ReceiveNewsLetter} ";
    }

    public PersonUpdateRequest ToPersonUpdateRequest()
    {
        return new PersonUpdateRequest
        {
            PersonID = PersonID,
            PersonName = PersonName,
            Email = Email,
            DateOfBirth = DateOfBirth,
            Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),
            CountryID = CountryID,
            Address = Address,
            ReceiveNewsLetter = ReceiveNewsLetter,
        };
    }
}

/// <summary>
///     Extension method to convert an object of Person to PersonResponse
/// </summary>
public static class PersonExtensions
{
    public static PersonResponse ToPersonResponse(this Person person)
    {
        return new PersonResponse
        {
            PersonID = person.PersonID,
            PersonName = person.PersonName,
            Email = person.Email,
            DateOfBirth = person.DateOfBirth,
            Gender = person.Gender,
            CountryID = person.CountryID,
            Address = person.Address,
            ReceiveNewsLetter = person.ReceiveNewsLetter,
            Age =
                person.DateOfBirth != null
                    ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25)
                    : null,
        };
    }
}
