using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Data
{
    public class DbPZPN:DbContext
    {
        public DbSet<Zawodnik> Zawodnicy { get; set; }
        public DbSet<Klub> Kluby { get; set; }
        public DbSet<Statystyka> Statystyki { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);

            string jsonString = File.ReadAllText("appsettings.json");
            JsonNode konfiguracja = JsonNode.Parse(jsonString);
            string connectionString = (string)konfiguracja["ConnectionString"]["Baza"];
            optionsBuilder.UseSqlServer(connectionString); 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            
            modelBuilder.Entity<Zawodnik>()
                .HasOne(z => z.Klub)
                .WithMany(k => k.Zawodnicy)
                .HasForeignKey(z => z.KlubId)
                .OnDelete(DeleteBehavior.Cascade); 

           
            modelBuilder.Entity<Statystyka>()
                .HasOne(s => s.Zawodnik)
                .WithOne(z => z.Statystyka)
                .HasForeignKey<Statystyka>(s => s.ZawodnikId)
                .OnDelete(DeleteBehavior.Cascade); 
  
            modelBuilder.Entity<Klub>().HasData(
                new Klub { KlubId = 1, Nazwa = "Legia Warszawa", rokZalozenia = 2019 },
                new Klub { KlubId = 2, Nazwa = "Lech Poznań", rokZalozenia = 1945 }
            );

            
            modelBuilder.Entity<Zawodnik>().HasData(
                new Zawodnik { ZawodnikId = 1, imie = "Robert", nrKoszulki = 9, kondycja = 9.5, czyKontuzja = false, KlubId = 1 },
                new Zawodnik { ZawodnikId = 2, imie = "Kamil", nrKoszulki = 15, kondycja = 7.0, czyKontuzja = true, KlubId = 2 },
                new Zawodnik { ZawodnikId = 3, imie = "Wojciech", nrKoszulki = 1, kondycja = 8.0, czyKontuzja = false, KlubId = 2 }
            );

            
            modelBuilder.Entity<Statystyka>().HasData(
                new Statystyka { StatystykaId = 1, ZawodnikId = 1, rozegraneMecze = 120, zdobyteGole = 85 },
                new Statystyka { StatystykaId = 2, ZawodnikId = 2, rozegraneMecze = 45, zdobyteGole = 5 }
            );
        }
    }
}


/*
Zeby utworzyć migracje, należy otworzyć narzedzia/Menadżer pakietów nuget/konsola menadżera a potem:
 Add-Migration dodanieZawodnikow
  -> enter
update-database
  -> enter
 */

