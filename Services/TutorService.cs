using PetTravelInsurance.Models;
using PetTravelInsurance.Repositories;

namespace PetTravelInsurance.Services
{
    public class TutorService : ITutorService
    {
        private readonly IRepository<Tutor> _tutorRepository;

        public TutorService(IRepository<Tutor> tutorRepository)
        {
            _tutorRepository = tutorRepository;
        }

        public async Task<List<Tutor>> GetAllTutoresAsync()
        {
            return await _tutorRepository.GetAllAsync();
        }

        public async Task<Tutor?> GetTutorByIdAsync(int id)
        {
            return await _tutorRepository.GetByIdAsync(id);
        }

        public async Task<Tutor> CreateTutorAsync(Tutor tutor)
        {
        
            ValidateTutor(tutor);
            return await _tutorRepository.CreateAsync(tutor);
        }

        public async Task UpdateTutorAsync(int id, Tutor tutor)
        {
            if (id != tutor.Id)
                throw new ArgumentException("ID mismatch");

            ValidateTutor(tutor);
            await _tutorRepository.UpdateAsync(tutor);
        }

        public async Task DeleteTutorAsync(int id)
        {
            await _tutorRepository.DeleteAsync(id);
        }

        private void ValidateTutor(Tutor tutor)
        {
            if (string.IsNullOrWhiteSpace(tutor.Nome))
                throw new ArgumentException("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(tutor.Email))
                throw new ArgumentException("Email é obrigatório");
        }
    }
}