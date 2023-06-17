using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CandidateContext : IDB<Candidate, int>
    {
        private VotingDBContext context;
        public CandidateContext(VotingDBContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Candidate item)
        {
            try
            {
                await context.Candidates.AddAsync(item);
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
                Candidate candidatedb = await context.Candidates.FindAsync(key);
                if (candidatedb == null)
                {
                    throw new ArgumentException("Candidate does not exsist");
                }
                context.Candidates.Remove(candidatedb);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Candidate>> ReadAllAsync( bool navProp = false)
        {
            try
            {
                if (navProp)
                {
                    return await context.Candidates.Include(c=>c.FK_Party).ToListAsync();
                }
                return await context.Candidates.ToListAsync();
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
                if (navProp)
                {
                    return await context.Candidates.Include(c => c.FK_Party).FirstOrDefaultAsync(c => c.ID == key);
                }
                return await context.Candidates.FirstOrDefaultAsync(c=>c.ID == key);
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
                 Candidate candidatedb = await ReadAsync(item.ID, true);
                context.Entry(candidatedb).CurrentValues.SetValues(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
