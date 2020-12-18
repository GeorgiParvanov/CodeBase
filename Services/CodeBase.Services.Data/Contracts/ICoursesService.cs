namespace CodeBase.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ICoursesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);
    }
}
