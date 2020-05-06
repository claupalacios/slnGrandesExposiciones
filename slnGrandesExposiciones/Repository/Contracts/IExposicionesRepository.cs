using System.Collections.Generic;
using System.Threading.Tasks;
using slnGrandesExposiciones.Models;

namespace slnGrandesExposiciones.Contracts.Repository
{
    public interface IExposicionesRepository
    {

        IEnumerable<Exposiciones> GetAllExposiciones();
    }

}