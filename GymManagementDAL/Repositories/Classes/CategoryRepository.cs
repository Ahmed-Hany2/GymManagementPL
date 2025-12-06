using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GymDbContext _context;

        public CategoryRepository(GymDbContext context)
        {
            _context = context;
        }
        public int Add(Category category)
        {
            _context.Categories.Add(category);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Remove(category);
                return _context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<Category> GetAll() => _context.Categories.ToList();

        public Category? GetById(int id) => _context.Categories.Find(id);
        public int Update(Category category)
        {
            _context.Update(category);
            return _context.SaveChanges();
        }
    }
}
