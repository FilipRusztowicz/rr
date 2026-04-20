using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace Domain
{
    [Table("Zawodnicy")]

    public class Zawodnik
    {
        [Key]
        public int ZawodnikId { get; set; } // klucz glowny
        [Column(TypeName ="TINYINT")]
        public int nrKoszulki { get; set; }
        [Column(TypeName ="NUMERIC(2,1)")]
        public double kondycja { get; set; }
        //
        public bool czyKontuzja { get; set; }
        [MaxLength(30)]
        public string? imie { get; set; }
        
        public override string ToString()
        {
            string kontuzja;
            if (czyKontuzja)
            {
                kontuzja = "TAK";
            }
            else
            {
                kontuzja = "NIE";

            }
            return $"{ZawodnikId}. {imie} nr koszulki: {nrKoszulki} kondycja: {kondycja} czy kontuzjowany?:{kontuzja}";
        }


    }
}

