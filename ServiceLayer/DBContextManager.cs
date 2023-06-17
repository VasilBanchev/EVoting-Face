using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class DBContextManager
    {
        private static VotingDBContext _context;

        public static VotingDBContext CreateContext()
        {
            _context = new VotingDBContext();
            return _context;
        }

        public static VotingDBContext GetContext()
        {
            return _context;
        }

        public static void SetChangeTracking(bool value)
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = value;
        }
    }
}
