using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoreniApp.Model
{
    public interface IArmadorRepository
    {
        /**
         * Agrega un registro de armador a la db;
         */
        Task<bool> AddArmador(Armador armador);

        /**
         * Actualiza los datos de un armando.
         */
        Task<bool> UpdateArmador(int id, Armador armador);
        
        /**
         * Recupera un armador por su id.
         */
        Task<Armador> GetById(int id);

        /**
         * Recupera un armador por su razon_social.
         */
        Task<Armador> GetByRazonSocial(string rs);

        /**
         * Recupera todos los armadores.
         */
        Task<IEnumerable> GetAll();

        string GetError();
    }
}
