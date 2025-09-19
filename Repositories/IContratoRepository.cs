using PetTravelInsurance.Models;

namespace PetTravelInsurance.Repositories
{
    public interface IContratoRepository : IRepository<Contrato>
    {
        Task<List<Contrato>> GetAllWithIncludesAsync();
        Task<Contrato?> GetByIdWithIncludesAsync(int id);
        Task<List<Contrato>> GetByTutorIdAsync(int tutorId);
        Task<List<Contrato>> GetActiveByPetIdAsync(int petId);
    }
}
