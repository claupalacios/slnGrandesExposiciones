using System.Threading.Tasks;
using slnGrandesExposiciones.Models;
using System.Collections.Generic;

namespace slnGrandesExposiciones.Contracts.Business
{
    public interface IExposicionesBusiness
    {
         IEnumerable<Exposiciones> GetAllExposiciones();
    }

}