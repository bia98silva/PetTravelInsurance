using PetTravelInsurance.Models;

namespace PetTravelInsurance.Services
{
    public interface IPlanoPetService
    {
        Task<List<PlanoPet>> GetAllPlanosAsync();
        Task<PlanoPet?> GetPlanoByIdAsync(int id);
        Task<PlanoPet> CreatePlanoAsync(PlanoPet plano);
        Task UpdatePlanoAsync(int id, PlanoPet plano);
        Task DeletePlanoAsync(int id);
    }
}
