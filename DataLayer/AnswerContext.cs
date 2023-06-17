using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AnswerContext : IDB<Answer, int>
    {
        private VotingDBContext context;
        public AnswerContext(VotingDBContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Answer item)
        {
            try
            {
                await context.Answers.AddAsync(item);
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
                Answer answerdb = await context.Answers.FindAsync(key);
                if (answerdb == null)
                {
                    throw new ArgumentException("Answer does not exsist");
                }
                context.Answers.Remove(answerdb);
                await context.SaveChangesAsync();
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
                if (navProp)
                {
                    return await context.Answers.Include(a=> a.Candidate).ThenInclude(c=> c.FK_Party).ToListAsync();
                }
                return await context.Answers.ToListAsync();
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
                if (navProp)
                {
                    return await context.Answers.Include(a => a.Candidate).ThenInclude(c => c.FK_Party).FirstOrDefaultAsync(a=> a.ID == key);
                }
                return await context.Answers.FindAsync(key);
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
                Answer answerdb = await ReadAsync(item.ID, true);
                context.Entry(answerdb).CurrentValues.SetValues(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task IncrementVoteValue(int itemId)
        {
            using (var _context = new VotingDBContext())
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    var item = await ReadAsync(itemId);
                    if (item == null)
                    {
                        return;
                    }

                    item.Votes++;

                    await UpdateAsync(item);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // handle exception
                    await transaction.RollbackAsync();
                    return;
                }
            }
               
            
        }

    }
}
