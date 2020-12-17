namespace CodeBase.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ILecturesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);
    }
}
