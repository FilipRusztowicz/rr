using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("Statystyki")]
    public class Statystyka
    {
        [Key]
        public int StatystykaId { get; set; }
        [Column(TypeName ="TINYINT")]
        public int rozegraneMecze { get; set;}
        [Column(TypeName = "TINYINT")]
        public int zdobyteGole { get; set; }
        [Required]
        public int ZawodnikId { get; set; }
        public virtual Zawodnik Zawodnik { get; set; }
    }
}
