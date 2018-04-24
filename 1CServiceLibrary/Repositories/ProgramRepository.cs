using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ServiceReference1C;
using ServiceLibrary1C.Interfaces;

namespace ServiceLibrary1C.Repositories
{
    class ProgramRepository : ICommonRepository<ProgramEdu>
    {
        ПФ_ПорталДПОPortTypeClient service;

        public ProgramRepository(ПФ_ПорталДПОPortTypeClient source)
        {
            service = source;
        }

        /// <summary>
        /// Получить программу по Guid
        /// </summary>
        /// <param name="key">Принимает string или Guid</param>    
        public async Task<ProgramEdu> Get(string key)
        {
            return await GetProgramAsync(key);
        }

        /// <summary>
        /// Получить программу по Guid
        /// </summary>
        /// <param name="key">Принимает string или Guid</param>
        public async Task<ProgramEdu> Get(Guid key)
        {
            return await GetProgramAsync(key.ToString());
        }

        /// <summary>
        /// Получить все программы
        /// </summary>
        public async Task<IEnumerable<ProgramEdu>> GetList()
        {
            var query = await GetFromServiceAsync(new DateTime(2006, 1, 1), DateTime.Today);
            return query;
        }


        /// <summary>
        /// Получить список измененных программ за период
        /// </summary>
        public async Task<IEnumerable<ProgramEdu>> GetList(DateTime startDate, DateTime endDate)
        {
            var query = await GetFromServiceAsync(startDate, endDate);
            return query;
        }

        protected async Task<ProgramEdu> GetProgramAsync(string guid)
        {
            var query = await GetFromServiceAsync(new DateTime(2006, 1, 1), DateTime.Today);
            return query.FirstOrDefault(p => p.XML_ID == guid);
        }

        protected async Task<IEnumerable<ProgramEdu>> GetFromServiceAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var query = await service.ПолучитьИзмененныеДанныеОПрограммахДПОЗаПериодAsync(startDate.Date, endDate.Date);
                return query.@return as IEnumerable<ProgramEdu>;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

    }
}
