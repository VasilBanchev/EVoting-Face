using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class CandidateManager : IManager<Candidate, int>
    {
        private CandidateContext context;
        public CandidateManager(VotingDBContext contex)
        {
            this.context = new CandidateContext(contex);
        }

        public async Task CreateAsync(Candidate item)
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

        public async Task<List<Candidate>> ReadAllAsync(bool navProp = false)
        {
            try
            {
                return await context.ReadAllAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Candidate> ReadAsync(int key, bool navProp = false)
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

        public async Task UpdateAsync(Candidate item)
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

