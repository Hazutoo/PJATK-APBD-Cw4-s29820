namespace PJATK_APBD_Cw4_s29820.Models.DTOs;

public sealed record PcWithComponentsResponseDto(
    int Id,
    string Name,
    float Weight,
    int Warranty,
    DateTime CreatedAt,
    int Stock,
    IReadOnlyCollection<PcComponentResponseDto> Components
);

public sealed record PcComponentResponseDto(
    int Amount,
    ComponentResponseDto Component
);

public sealed record ComponentResponseDto(
    string Code,
    string Name,
    string Description,
    ComponentManufacturerResponseDto Manufacturer,
    ComponentTypeResponseDto Type
);

public sealed record ComponentManufacturerResponseDto(
    int Id,
    string Abbreviation,
    string FullName,
    DateOnly FoundationDate
);

public sealed record ComponentTypeResponseDto(
    int Id,
    string Abbreviation,
    string Name
);
