using System.Threading.Tasks;
using slnGrandesExposiciones.Models;
using slnGrandesExposiciones.Contracts.Repository;
using System.Collections.Generic;
using slnGrandesExposiciones.Contracts.Business;

namespace slnGrandesExposiciones.Business

{
    public class ExposicionesBusiness : IExposicionesBusiness
    {
        private readonly IExposicionesRepository _ExposicionesRepository;

        public ExposicionesBusiness(IExposicionesRepository _ExposicionesRepository)
        {
            this._ExposicionesRepository = _ExposicionesRepository;
        }

        public  IEnumerable<Exposiciones> GetAllExposiciones()
        {
            return  _ExposicionesRepository.GetAllExposiciones();
        }
        
    }

}