using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("Kluby")]
    public class Klub
    {
       [Key]
        public int KlubId { get; set; } // klucz glowny
       [Column(TypeName = "TINYINT")]
       public int rokZalozenia { get; set; }
        //to trza zaznaczyć
       public virtual ICollection<Zawodnik> Zawodnicy { get; set; }
        

        [MaxLength(30)]
            public string Nazwa { get; set; }

        public Klub()
        {
            Zawodnicy = new List<Zawodnik>();
        }


    }
}
