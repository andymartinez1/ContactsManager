using ContactsManager.ServiceContracts.DTO;
using ContactsManager.ServiceContracts.Enums;

namespace ContactsManager.ServiceContracts;

/// <summary>
///     Represents the business logic for manipulating Person entity
/// </summary>
public interface IPersonsService
{
    /// <summary>
    ///     Adds a new person into the list of persons
    /// </summary>
    /// <param name="personAddRequest"></param>
    /// <returns>Returns the same person details along with a newly generated PersonID</returns>
    public PersonResponse AddPerson(PersonAddRequest? personAddRequest);

    /// <summary>
    ///     Returns all persons
    /// </summary>
    /// <returns>List of objects of PersonResponse type</returns>
    public List<PersonResponse> GetAllPersons();

    /// <summary>
    ///     Returns the person object based on the given personID
    /// </summary>
    /// <param name="personID"></param>
    /// <returns>Matching person object</returns>
    public PersonResponse? GetPersonByPersonID(Guid? personID);

    /// <summary>
    ///     Returns all objects that match the given search field and search string
    /// </summary>
    /// <param name="searchBy"></param>
    /// <param name="searchString"></param>
    /// <returns>Returns all matching persons based on the given search parameters</returns>
    public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);

    /// <summary>
    ///     Returns a list of person responses in ascending or descending order given the sortBy parameter
    /// </summary>
    /// <param name="allPersons"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortOrder"></param>
    /// <returns>list of person responses in ascending or descending order</returns>
    public List<PersonResponse> GetSortedPersons(
        List<PersonResponse> allPersons,
        string sortBy,
        SortOrderOptions sortOrder
    );

    /// <summary>
    ///     Updates the specified person details based on the given personID
    /// </summary>
    /// <param name="personUpdateRequest"></param>
    /// <returns>Returns the person response object after updating</returns>
    public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);

    /// <summary>
    ///     Deletes the person given the personID
    /// </summary>
    /// <param name="personID"></param>
    /// <returns>Returns true if delete was successful</returns>
    public bool DeletePerson(Guid? personID);
}
