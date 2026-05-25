namespace PJATK_APBD_Cw4_s29820.Models.DTOs;

public sealed record PcResponseDto(
    int Id,
    string Name,
    float Weight,
    int Warranty,
    DateTime CreatedAt,
    int Stock
);
