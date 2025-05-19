using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class PartRepository : IPartRepository
    {
        #region Dependency Injection
        private readonly MatrixIncDbContext _context;

        public PartRepository(MatrixIncDbContext context) 
        {
            _context = context; 
        } 
        #endregion

        #region Add part
        public void AddPart(Part part)
        {
            _context.Parts.Add(part);
            _context.SaveChanges();
        }
        #endregion

        #region Delete part
        public void DeletePart(Part part)
        {
            _context.Parts.Remove(part);
            _context.SaveChanges();
        }
        #endregion
        
        #region Get all parts
        public IEnumerable<Part> GetAllParts()
        {
            return _context.Parts;            
        }
        #endregion
        
        #region Get part by id
        public Part? GetPartById(int id)
        {
            return _context.Parts.FirstOrDefault(p => p.Id == id);
        }
        #endregion
        
        #region Update part
        public void UpdatePart(Part part)
        {
            _context.Parts.Update(part);
            _context.SaveChanges();
        }
        #endregion
    }
}
