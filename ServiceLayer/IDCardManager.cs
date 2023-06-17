using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class IDCardManager : IManager<IDCard, string>
    {
        private IDCardContext context;
        public IDCardManager(VotingDBContext contex)
        {
            this.context = new IDCardContext(contex);
        }

        public async Task CreateAsync(IDCard item)
        {
            try
            {
                await context.CreateAsync(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(string key)
        {
            try
            {
                await context.DeleteAsync(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<IDCard>> ReadAllAsync(bool navProp = false)
        {
            try
            {
                return await context.ReadAllAsync(navProp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IDCard> ReadAsync(string key, bool navProp = false)
        {
            try
            {
                return await context.ReadAsync(key, navProp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IDCard> ReadEGNAsync(string key, bool navProp = false)
        {
            try
            {
                return await context.ReadEGNAsync(key, navProp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task UpdateAsync(IDCard item)
        {
            try
            {
                await context.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
