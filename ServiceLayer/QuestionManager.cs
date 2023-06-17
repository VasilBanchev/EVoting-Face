using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class QuestionManager : IManager<Question, int>
    {
        private QuestionContext context;
        public QuestionManager(VotingDBContext contex)
        {
            this.context = new QuestionContext(contex);
        }

        public async Task CreateAsync(Question item)
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

        public async Task<List<Question>> ReadAllAsync(bool navProp = false)
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

        public async Task<Question> ReadAsync(int key, bool navProp = false)
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

        public async Task UpdateAsync(Question item)
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
