using PetTravelInsurance.Models;
using PetTravelInsurance.Repositories;

namespace PetTravelInsurance.Services
{
    public class PetService : IPetService
    {
        private readonly IRepository<Pet> _petRepository;

        public PetService(IRepository<Pet> petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<List<Pet>> GetAllPetsAsync()
        {
            return await _petRepository.GetAllAsync();
        }

        public async Task<Pet?> GetPetByIdAsync(int id)
        {
            return await _petRepository.GetByIdAsync(id);
        }

        public async Task<Pet> CreatePetAsync(Pet pet)
        {
            ValidatePet(pet);
            return await _petRepository.CreateAsync(pet);
        }

        public async Task UpdatePetAsync(int id, Pet pet)
        {
            if (id != pet.Id)
                throw new ArgumentException("ID mismatch");

            ValidatePet(pet);
            await _petRepository.UpdateAsync(pet);
        }

        public async Task DeletePetAsync(int id)
        {
            await _petRepository.DeleteAsync(id);
        }

        private void ValidatePet(Pet pet)
        {
            if (string.IsNullOrWhiteSpace(pet.Nome))
                throw new ArgumentException("Nome do pet é obrigatório");

            if (string.IsNullOrWhiteSpace(pet.Raca))
                throw new ArgumentException("Espécie é obrigatória");
        }
    }
}
