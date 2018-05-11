using ServiceLibrary1C.Interfaces;
using ServiceReference1C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary1C.Repositories
{
    public class AttestationRepository : ICommonRepository<ФормаКонтроля>
    {
        ПФ_ПорталДПОPortTypeClient service;

        public AttestationRepository(ПФ_ПорталДПОPortTypeClient source)
        {
            service = source;
        }


        /// <summary>
        /// Получить аттестацию по Guid
        /// </summary>
        /// <param name="key">Принимает string или Guid</param>    
        public async Task<ФормаКонтроля> Get(string key)
        {
            return await GetFromServiceAsync(key);
        }

        /// <summary>
        /// Получить аттестацию по Guid
        /// </summary>
        /// <param name="key">Принимает string или Guid</param>    
        public async Task<ФормаКонтроля> Get(Guid key)
        {
            return await GetFromServiceAsync(key.ToString());
        }


        /// <summary>
        /// Получить все аттестации
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ФормаКонтроля>> GetList()
        {
            var array = await GetFromServiceAsync();
            return array as IEnumerable<ФормаКонтроля>;
        }



        protected async Task<ФормаКонтроля> GetFromServiceAsync(string guid)
        {
            var query = await GetFromServiceAsync();
            return query.FirstOrDefault(p => p.ГУИД == guid);
        }

        protected async Task<IEnumerable<ФормаКонтроля>> GetFromServiceAsync()
        {
            try
            {
                var query = await service.ПолучитьФормыКонтроляAsync();
                return query.@return as IEnumerable<ФормаКонтроля>;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
