using Microsoft.AspNetCore.Mvc;
using PetTravelInsurance.DTO;
using PetTravelInsurance.Models;
using PetTravelInsurance.Services;

namespace PetTravelInsurance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetDTO>>> GetPets()
        {
            try
            {
                var pets = await _petService.GetAllPetsAsync();
                var petsDto = pets.Select(p => new PetDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Raca = p.Raca,
                    Idade = p.Idade,
                    TutorId = p.TutorId
                });

                return Ok(petsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetDTO>> GetPet(int id)
        {
            try
            {
                var pet = await _petService.GetPetByIdAsync(id);
                if (pet == null)
                    return NotFound();

                var petDto = new PetDTO
                {
                    Id = pet.Id,
                    Nome = pet.Nome,
                    Raca = pet.Raca,
                    Idade = pet.Idade,
                    TutorId = pet.TutorId
                };

                return Ok(petDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> CreatePet([FromBody] PetDTO petDto)
        {
            try
            {
                var pet = new Pet
                {
                    Nome = petDto.Nome,
                    Raca = petDto.Raca,
                    Idade = petDto.Idade,
                    TutorId = petDto.TutorId
                };

                var createdPet = await _petService.CreatePetAsync(pet);
                return CreatedAtAction(nameof(GetPet), new { id = createdPet.Id }, createdPet);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet(int id, [FromBody] PetDTO petDto)
        {
            try
            {
                var pet = new Pet
                {
                    Id = id,
                    Nome = petDto.Nome,
                    Raca = petDto.Raca,
                    Idade = petDto.Idade,
                    TutorId = petDto.TutorId
                };

                await _petService.UpdatePetAsync(id, pet);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            try
            {
                await _petService.DeletePetAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
