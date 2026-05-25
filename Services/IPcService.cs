using PJATK_APBD_Cw4_s29820.Models.DTOs;

namespace PJATK_APBD_Cw4_s29820.Services;

public interface IPcService
{
    Task<IReadOnlyCollection<PcResponseDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<PcWithComponentsResponseDto?> GetByIdWithComponentsAsync(int id, CancellationToken cancellationToken);
    Task<PcResponseDto> CreateAsync(PcUpsertRequestDto request, CancellationToken cancellationToken);
    Task<PcResponseDto?> UpdateAsync(int id, PcUpsertRequestDto request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
