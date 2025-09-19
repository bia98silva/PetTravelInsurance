using PetTravelInsurance.Models;
using PetTravelInsurance.Repositories;

namespace PetTravelInsurance.Services
{
    public class ContratoService : IContratoService
    {
        private readonly IContratoRepository _contratoRepository;
        private readonly IRepository<Tutor> _tutorRepository;
        private readonly IRepository<Pet> _petRepository;
        private readonly IRepository<PlanoPet> _planoRepository;

        public ContratoService(
            IContratoRepository contratoRepository,
            IRepository<Tutor> tutorRepository,
            IRepository<Pet> petRepository,
            IRepository<PlanoPet> planoRepository)
        {
            _contratoRepository = contratoRepository;
            _tutorRepository = tutorRepository;
            _petRepository = petRepository;
            _planoRepository = planoRepository;
        }

        public async Task<List<Contrato>> GetAllContratosAsync()
        {
            return await _contratoRepository.GetAllWithIncludesAsync();
        }

        public async Task<Contrato?> GetContratoByIdAsync(int id)
        {
            return await _contratoRepository.GetByIdWithIncludesAsync(id);
        }

        public async Task<Contrato> CreateContratoAsync(Contrato contrato)
        {
            await ValidateContratoAsync(contrato);
            return await _contratoRepository.CreateAsync(contrato);
        }

        public async Task UpdateContratoAsync(int id, Contrato contrato)
        {
            if (id != contrato.Id)
                throw new ArgumentException("ID mismatch");

            await ValidateContratoAsync(contrato);
            await _contratoRepository.UpdateAsync(contrato);
        }

        public async Task DeleteContratoAsync(int id)
        {
            await _contratoRepository.DeleteAsync(id);
        }

        public async Task<List<Contrato>> GetContratosByTutorIdAsync(int tutorId)
        {
            return await _contratoRepository.GetByTutorIdAsync(tutorId);
        }

        public async Task<List<Contrato>> GetContratosAtivosByPetIdAsync(int petId)
        {
            return await _contratoRepository.GetActiveByPetIdAsync(petId);
        }

        private async Task ValidateContratoAsync(Contrato contrato)
        {
           
            var tutor = await _tutorRepository.GetByIdAsync(contrato.TutorId);
            if (tutor == null)
                throw new ArgumentException("Tutor não encontrado");

            var pet = await _petRepository.GetByIdAsync(contrato.PetId);
            if (pet == null)
                throw new ArgumentException("Pet não encontrado");

            
            var plano = await _planoRepository.GetByIdAsync(contrato.PlanoPetId);
            if (plano == null)
                throw new ArgumentException("Plano não encontrado");

           
            if (contrato.DataFim <= contrato.DataInicio)
                throw new ArgumentException("Data fim deve ser posterior à data início");

            if (contrato.DataInicio < DateTime.Now.Date)
                throw new ArgumentException("Data início não pode ser no passado");
        }
    }
}