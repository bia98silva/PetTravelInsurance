using PetTravelInsurance.Models;

namespace PetTravelInsurance.Services
{
    public interface ITutorService
    {
        Task<List<Tutor>> GetAllTutoresAsync();
        Task<Tutor?> GetTutorByIdAsync(int id);
        Task<Tutor> CreateTutorAsync(Tutor tutor);
        Task UpdateTutorAsync(int id, Tutor tutor);
        Task DeleteTutorAsync(int id);
    }
}

