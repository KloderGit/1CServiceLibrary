using ServiceLibrary1C.Interfaces;
using ServiceReference1C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary1C.Repositories
{
    class LessonRepository : ICommonRepository<Дисциплина>
    {
        ПФ_ПорталДПОPortTypeClient service;

        public LessonRepository(ПФ_ПорталДПОPortTypeClient source)
        {
            service = source;
        }

        /// <summary>
        /// Получить занятие по Guid
        /// </summary>
        /// <param name="key">Принимает string или Guid</param>    
        public async Task<Дисциплина> Get(string key)
        {
            return await GetLessonAsync(key);
        }

        /// <summary>
        /// Получить занятие по Guid
        /// </summary>
        /// <param name="key">Принимает string или Guid</param>
        public async Task<Дисциплина> Get(Guid key)
        {
            return await GetLessonAsync(key.ToString());
        }

        /// <summary>
        /// Получить все занятия
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Дисциплина>> GetList()
        {
            var array = await GetFromServiceAsync();
            return array as IEnumerable<Дисциплина>;
        }


        protected async Task<Дисциплина> GetLessonAsync(string guid)
        {
            var query = await GetFromServiceAsync();
            return query.FirstOrDefault(p => p.ГУИД == guid);
        }

        protected async Task<IEnumerable<Дисциплина>> GetFromServiceAsync()
        {
            try
            {
                var query = await service.ПолучитьДисциплиныAsync();
                return query.@return as IEnumerable<Дисциплина>;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }





    }
}
