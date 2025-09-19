using Microsoft.AspNetCore.Mvc;
using PetTravelInsurance.DTO;
using PetTravelInsurance.Models;
using PetTravelInsurance.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetTravelInsurance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratosController : ControllerBase
    {
        private readonly IContratoService _contratoService;

        public ContratosController(IContratoService contratoService)
        {
            _contratoService = contratoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratos()
        {
            try
            {
                var contratos = await _contratoService.GetAllContratosAsync();
                return Ok(contratos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contrato>> GetContrato(int id)
        {
            try
            {
                var contrato = await _contratoService.GetContratoByIdAsync(id);
                if (contrato == null)
                    return NotFound();
                return Ok(contrato);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Contrato>> CreateContrato(ContratoDTO contratoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (contratoDto == null)
                    return BadRequest("Dados do contrato são obrigatórios");

                var contrato = new Contrato
                {
                    TutorId = contratoDto.TutorId,
                    PetId = contratoDto.PetId,
                    PlanoPetId = contratoDto.PlanoPetId,
                    DataInicio = contratoDto.DataInicio,
                    DataFim = contratoDto.DataFim,
                    PlanoNome = contratoDto.PlanoNome,
                    PlanoPreco = contratoDto.PlanoPreco,
                    PlanoCobertura = contratoDto.PlanoCobertura
                };

                var createdContrato = await _contratoService.CreateContratoAsync(contrato);

                var response = new
                {
                    id = createdContrato.Id,
                    tutorId = createdContrato.TutorId,
                    petId = createdContrato.PetId,
                    planoPetId = createdContrato.PlanoPetId,
                    dataInicio = createdContrato.DataInicio,
                    dataFim = createdContrato.DataFim,
                    planoNome = createdContrato.PlanoNome,
                    planoPreco = createdContrato.PlanoPreco,
                    planoCobertura = createdContrato.PlanoCobertura
                };

                return CreatedAtAction(nameof(GetContrato), new { id = createdContrato.Id }, response);
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
        public async Task<IActionResult> UpdateContrato(int id, [FromBody] ContratoDTO contratoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (contratoDto == null)
                    return BadRequest("Dados do contrato são obrigatórios");

                var contrato = new Contrato
                {
                    Id = id,
                    TutorId = contratoDto.TutorId,
                    PetId = contratoDto.PetId,
                    PlanoPetId = contratoDto.PlanoPetId,
                    DataInicio = contratoDto.DataInicio,
                    DataFim = contratoDto.DataFim,
                    PlanoNome = contratoDto.PlanoNome,
                    PlanoPreco = contratoDto.PlanoPreco,
                    PlanoCobertura = contratoDto.PlanoCobertura
                };

                await _contratoService.UpdateContratoAsync(id, contrato);
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
        public async Task<IActionResult> DeleteContrato(int id)
        {
            try
            {
                await _contratoService.DeleteContratoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("tutor/{tutorId}")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratosByTutor(int tutorId)
        {
            try
            {
                var contratos = await _contratoService.GetContratosByTutorIdAsync(tutorId);
                return Ok(contratos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }


    }
}