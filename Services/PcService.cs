using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw4_s29820.Data;
using PJATK_APBD_Cw4_s29820.Models.DTOs;
using PJATK_APBD_Cw4_s29820.Models.Entities;

namespace PJATK_APBD_Cw4_s29820.Services;

public class PcService(AppDbContext context) : IPcService
{
    public async Task<IReadOnlyCollection<PcResponseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.PCs
            .AsNoTracking()
            .OrderBy(pc => pc.Id)
            .Select(pc => new PcResponseDto(
                pc.Id,
                pc.Name,
                pc.Weight,
                pc.Warranty,
                pc.CreatedAt,
                pc.Stock
            ))
            .ToListAsync(cancellationToken);
    }

    public async Task<PcWithComponentsResponseDto?> GetByIdWithComponentsAsync(int id, CancellationToken cancellationToken)
    {
        return await context.PCs
            .AsNoTracking()
            .Where(pc => pc.Id == id)
            .Select(pc => new PcWithComponentsResponseDto(
                pc.Id,
                pc.Name,
                pc.Weight,
                pc.Warranty,
                pc.CreatedAt,
                pc.Stock,
                pc.PcComponents
                    .OrderBy(pcComponent => pcComponent.Component.ComponentTypesId)
                    .ThenBy(pcComponent => pcComponent.ComponentCode)
                    .Select(pcComponent => new PcComponentResponseDto(
                        pcComponent.Amount,
                        new ComponentResponseDto(
                            pcComponent.Component.Code,
                            pcComponent.Component.Name,
                            pcComponent.Component.Description,
                            new ComponentManufacturerResponseDto(
                                pcComponent.Component.ComponentManufacturer.Id,
                                pcComponent.Component.ComponentManufacturer.Abbreviation,
                                pcComponent.Component.ComponentManufacturer.FullName,
                                pcComponent.Component.ComponentManufacturer.FoundationDate
                            ),
                            new ComponentTypeResponseDto(
                                pcComponent.Component.ComponentType.Id,
                                pcComponent.Component.ComponentType.Abbreviation,
                                pcComponent.Component.ComponentType.Name
                            )
                        )
                    ))
                    .ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PcResponseDto> CreateAsync(PcUpsertRequestDto request, CancellationToken cancellationToken)
    {
        var pc = new Pc
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };

        context.PCs.Add(pc);
        await context.SaveChangesAsync(cancellationToken);

        return MapToResponse(pc);
    }

    public async Task<PcResponseDto?> UpdateAsync(int id, PcUpsertRequestDto request, CancellationToken cancellationToken)
    {
        var pc = await context.PCs.FirstOrDefaultAsync(pc => pc.Id == id, cancellationToken);

        if (pc is null)
        {
            return null;
        }

        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.CreatedAt = request.CreatedAt;
        pc.Stock = request.Stock;

        await context.SaveChangesAsync(cancellationToken);

        return MapToResponse(pc);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var pc = await context.PCs.FirstOrDefaultAsync(pc => pc.Id == id, cancellationToken);

        if (pc is null)
        {
            return false;
        }

        context.PCs.Remove(pc);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static PcResponseDto MapToResponse(Pc pc)
    {
        return new PcResponseDto(
            pc.Id,
            pc.Name,
            pc.Weight,
            pc.Warranty,
            pc.CreatedAt,
            pc.Stock
        );
    }
}
