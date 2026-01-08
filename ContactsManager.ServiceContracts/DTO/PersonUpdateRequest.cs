using System.ComponentModel.DataAnnotations;
using ContactsManager.Entities;
using ContactsManager.ServiceContracts.Enums;

namespace ContactsManager.ServiceContracts.DTO;

/// <summary>
///     DTO class for updating an existing person
/// </summary>
public class PersonUpdateRequest
{
    [Required(ErrorMessage = "ID cannot be blank.")]
    public Guid PersonID { get; set; }

    [Required(ErrorMessage = "Person name cannot be blank.")]
    public string? PersonName { get; set; }

    [Required(ErrorMessage = "Email cannot be blank.")]
    [EmailAddress(ErrorMessage = "Email address should be a valid email address.")]
    public string? Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public GenderOptions? Gender { get; set; }

    public Guid? CountryID { get; set; }

    public string? Address { get; set; }

    public bool ReceiveNewsLetter { get; set; }

    public Person ToPerson()
    {
        return new Person
        {
            PersonID = PersonID,
            PersonName = PersonName,
            Email = Email,
            DateOfBirth = DateOfBirth,
            Gender = Gender.ToString(),
            CountryID = CountryID,
            Address = Address,
            ReceiveNewsLetter = ReceiveNewsLetter,
        };
    }
}
