using Microsoft.EntityFrameworkCore;
using PetTravelInsurance.Data;
using PetTravelInsurance.Models;

namespace PetTravelInsurance.Repositories
{
    public class ContratoRepository : Repository<Contrato>, IContratoRepository
    {
        public ContratoRepository(AppDbContext context) : base(context) { }

        public async Task<List<Contrato>> GetAllWithIncludesAsync()
        {
            return await _dbSet
                .Include(c => c.Tutor)
                .Include(c => c.Pet)
                .Include(c => c.PlanoPet)
                .ToListAsync();
        }

        public async Task<Contrato?> GetByIdWithIncludesAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Tutor)
                .Include(c => c.Pet)
                .Include(c => c.PlanoPet)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Contrato>> GetByTutorIdAsync(int tutorId)
        {
            return await _dbSet
                .Where(c => c.TutorId == tutorId)
                .Include(c => c.Pet)
                .Include(c => c.PlanoPet)
                .ToListAsync();
        }

        public async Task<List<Contrato>> GetActiveByPetIdAsync(int petId)
        {
            var hoje = DateTime.Now;
            return await _dbSet
                .Where(c => c.PetId == petId &&
                           c.DataInicio <= hoje &&
                           c.DataFim >= hoje)
                .Include(c => c.Tutor)
                .Include(c => c.PlanoPet)
                .ToListAsync();
        }
    }
}
