using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PartyContext : IDB<Party, int>
    {
        private VotingDBContext context;
        public PartyContext(VotingDBContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Party item)
        {
            try
            {
                await context.Parties.AddAsync(item);
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
                Party partydb = await ReadAsync(key, true);
                if (partydb == null)
                {
                    throw new ArgumentException("Party does not exsist");
                }
                context.Candidates.RemoveRange(partydb.Candidates);

                context.Parties.Remove(partydb);
                await context.SaveChangesAsync();
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
                if (navProp)
                {
                    return await context.Parties.Include(p => p.Candidates).ToListAsync();
                }
                return await context.Parties.ToListAsync();
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
                if (navProp)
                {
                    return await context.Parties.Include(p => p.Candidates).FirstOrDefaultAsync(p => p.ID == key);
                }
                return await context.Parties.FindAsync(key);
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
                Party partydb = await ReadAsync(item.ID, true);
                context.Entry(partydb).CurrentValues.SetValues(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
