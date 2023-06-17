using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class IDCardPhotosContext : IDB<IDCardPhotos, int>
    {
        private VotingDBContext context;
        public IDCardPhotosContext(VotingDBContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(IDCardPhotos item)
        {
            try
            {
                context.IDCardsPhotos.Add(item);
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
                IDCardPhotos iDCardPhotosdb = await context.IDCardsPhotos.FindAsync(key);
                if (iDCardPhotosdb == null)
                {
                    throw new ArgumentException("IDCardPhotos does not exsist");
                }
                context.IDCardsPhotos.Remove(iDCardPhotosdb);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task DeletePhotoesAsync(string key)
        {
            try
            {
                
                IDCardPhotos iDCardPhotoes = (await context.IDCards.Include(id => id.CardPhotos).FirstOrDefaultAsync(card => card.EGN == key)).CardPhotos;

                if (iDCardPhotoes == null)
                {
                    throw new ArgumentException("IDCardPhotos does not exsist");
                }
                iDCardPhotoes.IDCardImage1 = new byte[0];
                iDCardPhotoes.IDCardImage2 = new byte[0];

                UpdateAsync(iDCardPhotoes);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<IDCardPhotos>> ReadAllAsync(bool navProp = false)
        {
            try
            {
                return await context.IDCardsPhotos.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IDCardPhotos> ReadAsync(int key, bool navProp = false)
        {
            try
            {
                return await context.IDCardsPhotos.FindAsync(key);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task UpdateAsync(IDCardPhotos item)
        {
            try
            {
                IDCardPhotos iDCardPhotosdb = await ReadAsync(item.IDCardNumber, true);
                context.Entry(iDCardPhotosdb).CurrentValues.SetValues(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
