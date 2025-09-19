using PetTravelInsurance.Models;
using PetTravelInsurance.Repositories;

namespace PetTravelInsurance.Services
{
    public class PlanoPetService : IPlanoPetService
    {
        private readonly IRepository<PlanoPet> _planoRepository;

        public PlanoPetService(IRepository<PlanoPet> planoRepository)
        {
            _planoRepository = planoRepository;
        }

        public async Task<List<PlanoPet>> GetAllPlanosAsync()
        {
            return await _planoRepository.GetAllAsync();
        }

        public async Task<PlanoPet?> GetPlanoByIdAsync(int id)
        {
            return await _planoRepository.GetByIdAsync(id);
        }

        public async Task<PlanoPet> CreatePlanoAsync(PlanoPet plano)
        {
            ValidatePlano(plano);
            return await _planoRepository.CreateAsync(plano);
        }

        public async Task UpdatePlanoAsync(int id, PlanoPet plano)
        {
            if (id != plano.Id)
                throw new ArgumentException("ID mismatch");

            ValidatePlano(plano);
            await _planoRepository.UpdateAsync(plano);
        }

        public async Task DeletePlanoAsync(int id)
        {
            await _planoRepository.DeleteAsync(id);
        }

        private void ValidatePlano(PlanoPet plano)
        {
            if (string.IsNullOrWhiteSpace(plano.Nome))
                throw new ArgumentException("Nome do plano é obrigatório");

            if (plano.Preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero");

            if (string.IsNullOrWhiteSpace(plano.Cobertura))
                throw new ArgumentException("Cobertura é obrigatória");
        }
    }
}
