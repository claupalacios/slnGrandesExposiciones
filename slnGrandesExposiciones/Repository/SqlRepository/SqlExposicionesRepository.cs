using System.Collections.Generic;
using slnGrandesExposiciones.Models;
using System.Threading.Tasks;
using System.Linq;
using slnGrandesExposiciones.Contracts.Repository;

namespace slnGrandesExposiciones.SqlRepository
{

    public class SqlExposicionesRepository : IExposicionesRepository
    {
        private readonly GrandesExposicionesEntities _Context;

        public SqlExposicionesRepository(GrandesExposicionesEntities _context)
        {
            _Context = _context;
        }

        public  IEnumerable<Exposiciones> GetAllExposiciones()
        {
            return  _Context.Exposiciones.ToList();
        }
    }
}