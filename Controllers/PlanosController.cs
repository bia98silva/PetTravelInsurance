using Microsoft.AspNetCore.Mvc;
using PetTravelInsurance.DTO;
using PetTravelInsurance.Models;
using PetTravelInsurance.Services;

namespace PetTravelInsurance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanosController : ControllerBase
    {
        private readonly IPlanoPetService _planoService;

        public PlanosController(IPlanoPetService planoService)
        {
            _planoService = planoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanoPetDTO>>> GetPlanos()
        {
            try
            {
                var planos = await _planoService.GetAllPlanosAsync();
                var planosDto = planos.Select(p => new PlanoPetDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Cobertura = p.Cobertura,
                    Descricao = p.Descricao,
                    Ativo = p.Ativo
                });

                return Ok(planosDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanoPetDTO>> GetPlano(int id)
        {
            try
            {
                var plano = await _planoService.GetPlanoByIdAsync(id);
                if (plano == null)
                    return NotFound();

                var planoDto = new PlanoPetDTO
                {
                    Id = plano.Id,
                    Nome = plano.Nome,
                    Preco = plano.Preco,
                    Cobertura = plano.Cobertura,
                    Descricao = plano.Descricao,
                    Ativo = plano.Ativo
                };

                return Ok(planoDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PlanoPet>> CreatePlano([FromBody] PlanoPetDTO planoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var plano = new PlanoPet
                {
                    Nome = planoDto.Nome,
                    Preco = planoDto.Preco,
                    Cobertura = planoDto.Cobertura,
                    Descricao = planoDto.Descricao,
                    Ativo = planoDto.Ativo
                };

                var createdPlano = await _planoService.CreatePlanoAsync(plano);
                return CreatedAtAction(nameof(GetPlano), new { id = createdPlano.Id }, createdPlano);
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
        public async Task<IActionResult> UpdatePlano(int id, [FromBody] PlanoPetDTO planoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var plano = new PlanoPet
                {
                    Id = id,
                    Nome = planoDto.Nome,
                    Preco = planoDto.Preco,
                    Cobertura = planoDto.Cobertura,
                    Descricao = planoDto.Descricao,
                    Ativo = planoDto.Ativo
                };

                await _planoService.UpdatePlanoAsync(id, plano);
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
        public async Task<IActionResult> DeletePlano(int id)
        {
            try
            {
                await _planoService.DeletePlanoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("ativos")]
        public async Task<ActionResult<IEnumerable<PlanoPetDTO>>> GetPlanosAtivos()
        {
            try
            {
                var planos = await _planoService.GetAllPlanosAsync();
                var planosAtivos = planos.Where(p => p.Ativo).Select(p => new PlanoPetDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Cobertura = p.Cobertura,
                    Descricao = p.Descricao,
                    Ativo = p.Ativo
                });

                return Ok(planosAtivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
