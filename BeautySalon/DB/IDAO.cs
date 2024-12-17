using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautySalon.DB.DAO
{
    internal interface IDAO<TIndex, TModel>
    {
        Task<List<TModel>> GetAll();
        Task<TModel> GetById(TIndex id);
        Task<TIndex> GetNewId();
        Task Insert(TModel m);
        Task Update(TModel m);
        Task Update(TModel m, TIndex oldId);
        Task Delete(TIndex id);

    }
}
