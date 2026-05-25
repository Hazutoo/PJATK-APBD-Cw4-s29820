using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PJATK_APBD_Cw4_s29820.Models.Entities;

[Table("PCComponents")]
[PrimaryKey(nameof(PcId), nameof(ComponentCode))]
public class PcComponent
{
    [Column("PCId")]
    public int PcId { get; set; }

    [Required]
    [MaxLength(10)]
    [Column(TypeName = "char(10)")]
    public string ComponentCode { get; set; } = null!;

    public int Amount { get; set; }

    [ForeignKey(nameof(PcId))]
    public Pc Pc { get; set; } = null!;

    [ForeignKey(nameof(ComponentCode))]
    public Component Component { get; set; } = null!;
}
