using PetTravelInsurance.Models;

namespace PetTravelInsurance.Services
{
    public interface IContratoService
    {
        Task<List<Contrato>> GetAllContratosAsync();
        Task<Contrato?> GetContratoByIdAsync(int id);
        Task<Contrato> CreateContratoAsync(Contrato contrato);
        Task UpdateContratoAsync(int id, Contrato contrato);
        Task DeleteContratoAsync(int id);
        Task<List<Contrato>> GetContratosByTutorIdAsync(int tutorId);
        Task<List<Contrato>> GetContratosAtivosByPetIdAsync(int petId);
    }
}
