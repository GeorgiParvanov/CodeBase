namespace CodeBase.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ICoursesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAll<T>(int pageNumber, int itemsPerPage);

        IEnumerable<T> GetAllByTagName<T>(string tagName);

        T GetById<T>(int id);
    }
}
