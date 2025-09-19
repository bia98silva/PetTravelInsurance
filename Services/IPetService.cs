using PetTravelInsurance.Models;

namespace PetTravelInsurance.Services
{
    public interface IPetService
    {
        Task<List<Pet>> GetAllPetsAsync();
        Task<Pet?> GetPetByIdAsync(int id);
        Task<Pet> CreatePetAsync(Pet pet);
        Task UpdatePetAsync(int id, Pet pet);
        Task DeletePetAsync(int id);
    }
}