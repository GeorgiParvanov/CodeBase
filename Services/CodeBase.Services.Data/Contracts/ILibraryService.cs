namespace CodeBase.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ILibraryService
    {
        IEnumerable<T> GetUserCourses<T>(string userId, int pageNumber, int itemsPerPage);

        int GetCount(string userId);
    }
}
