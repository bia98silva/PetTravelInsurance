using Microsoft.AspNetCore.Mvc;
using PetTravelInsurance.DTO;
using PetTravelInsurance.Models;
using PetTravelInsurance.Services;

namespace PetTravelInsurance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutoresController : ControllerBase
    {
        private readonly ITutorService _tutorService;

        public TutoresController(ITutorService tutorService)
        {
            _tutorService = tutorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TutorDTO>>> GetTutores()
        {
            try
            {
                var tutores = await _tutorService.GetAllTutoresAsync();

              
                var tutorDtos = tutores.Select(t => new TutorDTO
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    Email = t.Email,
                    Telefone = t.Telefone,
                    Contratos = t.Contratos?.Select(c => new ContratoDTO
                    {
                        Id = c.Id,
                        PetId = c.PetId,
                        PlanoPetId = c.PlanoPetId,
                        DataInicio = c.DataInicio,
                        DataFim = c.DataFim
                    }).ToList()
                });

                return Ok(tutorDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TutorDTO>> GetTutor(int id)
        {
            try
            {
                var tutor = await _tutorService.GetTutorByIdAsync(id);
                if (tutor == null)
                    return NotFound();

                var tutorDto = new TutorDTO
                {
                    Id = tutor.Id,
                    Nome = tutor.Nome,
                    Email = tutor.Email,
                    Telefone = tutor.Telefone,
                    Contratos = tutor.Contratos?.Select(c => new ContratoDTO
                    {
                        Id = c.Id,
                        PetId = c.PetId,
                        PlanoPetId = c.PlanoPetId,
                        DataInicio = c.DataInicio,
                        DataFim = c.DataFim
                    }).ToList()
                };

                return Ok(tutorDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TutorDTO>> CreateTutor(TutorDTO tutorDto)
        {
            try
            {
                var tutor = new Tutor
                {
                    Nome = tutorDto.Nome,
                    Email = tutorDto.Email,
                    Telefone = tutorDto.Telefone
                };

                var createdTutor = await _tutorService.CreateTutorAsync(tutor);

                var createdDto = new TutorDTO
                {
                    Id = createdTutor.Id,
                    Nome = createdTutor.Nome,
                    Email = createdTutor.Email,
                    Telefone = createdTutor.Telefone
                };

                return CreatedAtAction(nameof(GetTutor), new { id = createdDto.Id }, createdDto);
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
        public async Task<IActionResult> UpdateTutor(int id, TutorDTO tutorDto)
        {
            try
            {
                var tutor = new Tutor
                {
                    Id = tutorDto.Id,
                    Nome = tutorDto.Nome,
                    Email = tutorDto.Email,
                    Telefone = tutorDto.Telefone
                };

                await _tutorService.UpdateTutorAsync(id, tutor);
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
        public async Task<IActionResult> DeleteTutor(int id)
        {
            try
            {
                await _tutorService.DeleteTutorAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
