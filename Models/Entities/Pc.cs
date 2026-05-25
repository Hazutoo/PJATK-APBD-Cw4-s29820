using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJATK_APBD_Cw4_s29820.Models.Entities;

[Table("PCs")]
public class Pc
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "float(5)")]
    public float Weight { get; set; }

    public int Warranty { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    public int Stock { get; set; }

    public ICollection<PcComponent> PcComponents { get; set; } = new List<PcComponent>();
}
