using ArrowInventory.Models;
using ArrowInventory.Data;

namespace ArrowInventory.Services
{
    public class SiteService
    {

        private readonly AppDbContext _context;

        public SiteService(AppDbContext context)
        {
            _context = context;
        }

        public void AddSite(Sites site)
        {
            _context.Sites.Add(site);
            _context.SaveChanges();
        }

        public List<Sites> GetSites()
        {
            return _context.Sites.ToList();
        }

        public void DeleteSite(string siteCode)
        {
            var sites = _context.Sites.Find(siteCode);
            if (sites != null)
            {
                _context.Sites.Remove(sites);
                _context.SaveChanges();
            }
        }

    }
}
