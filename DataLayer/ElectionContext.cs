using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ElectionContext : IDB<Election, int>
    {
        private VotingDBContext context;
        public ElectionContext(VotingDBContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Election item)
        {
            try
            {
                await context.Elections.AddAsync(item);
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
                Election electiondb = await context.Elections.FindAsync(key);
                if (electiondb == null)
                {
                    throw new ArgumentException("Election does not exsist");
                }
                context.Elections.Remove(electiondb);
                await context.SaveChangesAsync();
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
                if (navProp)
                {
                    return await context.Elections.Include(e => e.Question).ThenInclude(e => e.Answers).Include(e => e.Parties).ThenInclude(e => e.Candidates).ToListAsync();
                }
                return await context.Elections.ToListAsync();
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
                if (navProp)
                {
                    return await context.Elections.Include(e => e.Parties).Include(e => e.Question).ThenInclude(e => e.Answers).ThenInclude(e => e.Candidate).ThenInclude(c => c.FK_Party).FirstOrDefaultAsync(e => e.ID == key);
                }
                return await context.Elections.FindAsync(key);
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
                Election electiondb = await ReadAsync(item.ID);
                context.Entry(electiondb).CurrentValues.SetValues(item);
                //context.Update(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
