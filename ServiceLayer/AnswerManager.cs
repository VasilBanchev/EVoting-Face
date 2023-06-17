using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class AnswerManager : IManager<Answer, int>
    {
        private AnswerContext context;
        public AnswerManager(VotingDBContext contex)
        {
            this.context = new AnswerContext(contex);
        }

        public async Task CreateAsync(Answer item)
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

        public async Task<List<Answer>> ReadAllAsync(bool navProp = false)
        {
            try
            {
                return await context.ReadAllAsync(navProp );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Answer> ReadAsync(int key, bool navProp = false)
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

        public async Task UpdateAsync(Answer item)
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
        public async Task VoteIncrement(int key)
        {
            try
            {
                await context.IncrementVoteValue(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
