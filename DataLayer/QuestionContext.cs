using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class QuestionContext : IDB<Question, int>
    {
        private VotingDBContext context;
        public QuestionContext(VotingDBContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Question item)
        {
            try
            {
                await context.Questions.AddAsync(item);
                await context.SaveChangesAsync();
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
                Question questiondb = await context.Questions.FindAsync(key);
                if (questiondb == null)
                {
                    throw new ArgumentException("Question does not exsist");
                }
                context.Questions.Remove(questiondb);
                await context.SaveChangesAsync();
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
                if (navProp)
                {
                    return await context.Questions.Include(q=> q.Answers).ThenInclude(a=>a.Candidate).ThenInclude(c=>c.FK_Party).ToListAsync();

                }
                return await context.Questions.ToListAsync();
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
                if (navProp)
                {
                    return await context.Questions.Include(q => q.Answers).ThenInclude(a => a.Candidate).ThenInclude(c => c.FK_Party).FirstOrDefaultAsync(q=>q.ID == key);

                }
                return await context.Questions.FindAsync(key);
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
                Question questiondb = await ReadAsync(item.ID, true);
                context.Entry(questiondb).CurrentValues.SetValues(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
