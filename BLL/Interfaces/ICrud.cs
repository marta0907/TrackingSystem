using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface ICrud<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Add(T model);

        void Update(T model);

        void DeleteById(int modelId);


    }
}
