using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class PartyManager : IManager<Party, int>
    {
        private PartyContext context;
        public PartyManager(VotingDBContext contex)
        {
            this.context =new PartyContext( contex);
        }
        public async Task CreateAsync(Party item)
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
        public async Task DeleteAsync(int key)
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
        public async Task<List<Party>> ReadAllAsync(bool navProp = false)
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
        public async Task<Party> ReadAsync(int key, bool navProp = false)
        {
            try
            {
                return await context.ReadAsync(key , navProp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task UpdateAsync(Party item)
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
