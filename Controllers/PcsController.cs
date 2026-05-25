using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw4_s29820.Models.DTOs;
using PJATK_APBD_Cw4_s29820.Services;

namespace PJATK_APBD_Cw4_s29820.Controllers;

[ApiController]
[Route("api/pcs")]
public class PcsController(IPcService pcService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<PcResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<PcResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        var pcs = await pcService.GetAllAsync(cancellationToken);
        return Ok(pcs);
    }

    [HttpGet("{id:int}/components")]
    [ProducesResponseType(typeof(PcWithComponentsResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PcWithComponentsResponseDto>> GetByIdWithComponents(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0.");
        }

        var pc = await pcService.GetByIdWithComponentsAsync(id, cancellationToken);

        if (pc is null)
        {
            return NotFound($"PC with id {id} was not found.");
        }

        return Ok(pc);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PcResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PcResponseDto>> Create(
        [FromBody] PcUpsertRequestDto request,
        CancellationToken cancellationToken)
    {
        var createdPc = await pcService.CreateAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetByIdWithComponents),
            new { id = createdPc.Id },
            createdPc
        );
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PcResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PcResponseDto>> Update(
        [FromRoute] int id,
        [FromBody] PcUpsertRequestDto request,
        CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0.");
        }

        var updatedPc = await pcService.UpdateAsync(id, request, cancellationToken);

        if (updatedPc is null)
        {
            return NotFound($"PC with id {id} was not found.");
        }

        return Ok(updatedPc);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0.");
        }

        var deleted = await pcService.DeleteAsync(id, cancellationToken);

        if (!deleted)
        {
            return NotFound($"PC with id {id} was not found.");
        }

        return NoContent();
    }
}
