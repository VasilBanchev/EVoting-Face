using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class IDCardContext : IDB<IDCard, string>
    {
        private VotingDBContext context;
        public IDCardContext(VotingDBContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(IDCard item)
        {
            try
            {
               await context.IDCards.AddAsync(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task DeleteAsync(string key)
        {
            try
            {
                IDCard iDcarddb = await context.IDCards.FindAsync(key);
                if (iDcarddb == null)
                {
                    throw new ArgumentException("IDCard does not exsist");
                }
                context.IDCards.Remove(iDcarddb);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<IDCard>> ReadAllAsync(bool navProp = false)
        {
            try
            {
                if (navProp)
                {
                    return await context.IDCards.Include(id=> id.CardPhotos).ToListAsync();
                }
                return await context.IDCards.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IDCard> ReadAsync(string key, bool navProp = false)
        {
            try
            {
                if (navProp)
                {
                    return await context.IDCards.Include(id => id.CardPhotos).FirstOrDefaultAsync(id => id.IDCardNumber == key);
                }
                return await context.IDCards.FindAsync(key);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IDCard> ReadEGNAsync(string key, bool navProp = false)
        {
            try
            {
                if (navProp)
                {
                    return await context.IDCards.Include(id => id.CardPhotos).FirstOrDefaultAsync(id => id.EGN == key);
                }
                return await context.IDCards.FindAsync(key);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task UpdateAsync(IDCard item)
        {
            try
            {
                IDCard iDcarddb = await ReadAsync(item.IDCardNumber, true);
                context.Entry(iDcarddb).CurrentValues.SetValues(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
