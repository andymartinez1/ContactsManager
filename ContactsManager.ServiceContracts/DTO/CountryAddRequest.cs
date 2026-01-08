using System.ComponentModel.DataAnnotations;
using ContactsManager.Entities;

namespace ContactsManager.ServiceContracts.DTO;

/// <summary>
///     DTO class for adding a new country
/// </summary>
public class CountryAddRequest
{
    [Required(ErrorMessage = "Person name cannot be blank.")]
    public string? CountryName { get; set; }

    public Country ToCountry()
    {
        return new Country { CountryName = CountryName };
    }
}
