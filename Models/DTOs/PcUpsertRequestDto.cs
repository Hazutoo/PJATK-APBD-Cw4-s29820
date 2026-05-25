using System.ComponentModel.DataAnnotations;

namespace PJATK_APBD_Cw4_s29820.Models.DTOs;

public sealed record PcUpsertRequestDto(
    [Required]
    [MaxLength(50)]
    string Name,

    [Range(0.01, 9999.99)]
    float Weight,

    [Range(0, 600)]
    int Warranty,

    DateTime CreatedAt,

    [Range(0, 1_000_000)]
    int Stock
);
