using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ElectionManager : IManager<Election, int>
    {
        private ElectionContext context;
        public ElectionManager(VotingDBContext contex)
        {
            this.context = new ElectionContext(contex);
        }

        public async Task CreateAsync(Election item)
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

        public async Task<List<Election>> ReadAllAsync(bool navProp = false)
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

        public async Task<Election> ReadAsync(int key, bool navProp = false)
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

        public async Task UpdateAsync(Election item)
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

