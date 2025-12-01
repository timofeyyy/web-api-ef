using app.Db.ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Globalization;

namespace app.Db.Context
{
    public class DataBase : DbContext
    {
        public DbSet<Foreignness> Foreignnesses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Capacitors> Capacitors { get; set; }
        public DbSet<Resistors> Resistors { get; set; }
        public DbSet<Diods> Diods { get; set; }
        public DbSet<Microchips> Microchips { get; set; }
        public DbSet<Transistors> Transistors { get; set; }
        public DbSet<Technologies> Technologies { get; set; }
        public DbSet<Manufacturers> Manufacturers { get; set; }
        public DbSet<ComponentKinds> ComponentKinds { get; set; }
        public DbSet<ComponentTypes> ComponentTypes { get; set; }
        public DataBase(DbContextOptions<DataBase> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Capacitors>()
            .Property(p => p.DocID)
            .HasColumnType("bigint");

            modelBuilder.Entity<Capacitors>()
            .Property(p => p.MinVoltage)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );

            modelBuilder.Entity<Capacitors>()
            .Property(p => p.MaxVoltage)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );

            modelBuilder.Entity<Capacitors>()
            .Property(p => p.MaxCapacity)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );

            modelBuilder.Entity<Capacitors>()
            .Property(p => p.MinCapacity)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );

            modelBuilder.Entity<Capacitors>()
            .Property(p => p.MinOperatingTemperature)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );

            modelBuilder.Entity<Capacitors>()
            .Property(p => p.MaxOperatingTemperature)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );

            modelBuilder.Entity<Capacitors>()
            .Property(p => p.AcceptableCapacityIncrease)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );

            modelBuilder.Entity<Capacitors>()
            .Property(p => p.AcceptableСapacityReduction)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );




            modelBuilder.Entity<Diods>()
               .Property(p => p.DocID)
               .HasColumnType("bigint");


            modelBuilder.Entity<Diods>()
            .Property(p => p.MaxPermissibleDCVoltage)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );

            modelBuilder.Entity<Diods>()
            .Property(p => p.MinOperatingTemperature)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );

            modelBuilder.Entity<Diods>()
            .Property(p => p.MaxOperatingTemperature)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );
           
            modelBuilder.Entity<Diods>()
            .Property(p => p.MaxPermissibleAverageDirectCurrent)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );
                   
            modelBuilder.Entity<Diods>()
            .Property(p => p.MaxiPermissibleDirectCurrent)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );

            modelBuilder.Entity<Diods>()
            .Property(p => p.RadiationResistance)
            .HasColumnType("float")
            .HasConversion(
                 v => v,
                 v => v != null ? v : null
            );



            modelBuilder.Entity<Transistors>()
           .Property(p => p.DocID)
           .HasColumnType("bigint");

            modelBuilder.Entity<Transistors>()
           .Property(p => p.MaxPermissibleDCVoltage)
           .HasColumnType("float")
           .HasConversion(
                v => v, 
                v => v != null ? v : null 
           );
            
            modelBuilder.Entity<Transistors>()
           .Property(p => p.MinOperatingTemperature)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );
            
            modelBuilder.Entity<Transistors>()
           .Property(p => p.RadiationResistance)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );
            
            modelBuilder.Entity<Transistors>()
           .Property(p => p.MaxOperatingTemperature)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
            );
            
            modelBuilder.Entity<Transistors>()
           .Property(p => p.MaxPermissibleDCCollectorCurrent)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
            );

			modelBuilder.Entity<Resistors>()
           .Property(p => p.DocID)
           .HasColumnType("bigint");

            modelBuilder.Entity<Resistors>()
           .Property(p => p.PowerRating)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );
            
            modelBuilder.Entity<Resistors>()
           .Property(p => p.MinVoltage)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );


           modelBuilder.Entity<Resistors>()
           .Property(p => p.MaxVoltage)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
            );

           modelBuilder.Entity<Resistors>()
           .Property(p => p.MinRatedResistance)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );

           modelBuilder.Entity<Resistors>()
           .Property(p => p.MaxRatedResistance)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );

           modelBuilder.Entity<Resistors>()
           .Property(p => p.ResistanceTolerance)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );

           modelBuilder.Entity<Resistors>()
           .Property(p => p.MinOperatingTemperature)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );

           modelBuilder.Entity<Resistors>()
           .Property(p => p.MaxOperatingTemperature)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );

           modelBuilder.Entity<Resistors>()
           .Property(p => p.CurrentLimit)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );


            modelBuilder.Entity<Microchips>()
           .Property(p => p.DocID)
           .HasColumnType("bigint");

            modelBuilder.Entity<Microchips>()
           .Property(p => p.MinVoltage)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => (double)v
           );

           modelBuilder.Entity<Microchips>()
           .Property(p => p.MaxVoltage)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => (double)v
           );

           modelBuilder.Entity<Microchips>()
           .Property(p => p.Frequency)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );

           modelBuilder.Entity<Microchips>()
           .Property(p => p.ConsumptionCurrent)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );

           modelBuilder.Entity<Microchips>()
           .Property(p => p.MinOperatingTemperature)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => (double)v
           );
            
           modelBuilder.Entity<Microchips>()
           .Property(p => p.MaxOperatingTemperature)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => (double)v
           );

           modelBuilder.Entity<Microchips>()
           .Property(p => p.RadiationResistance)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );

           modelBuilder.Entity<Microchips>()
           .Property(p => p.SamplingTime)
           .HasColumnType("float")
           .HasConversion(
                v => v,
                v => v != null ? v : null
           );

			modelBuilder.Entity<Transistors>()
	       .HasOne(t => t.Kind)
	       .WithMany(k => k.Transistors)
	       .HasForeignKey(t => t.Kind_ID);


			modelBuilder.Entity<Transistors>()
		   .HasOne(t => t.Type)
		   .WithMany(t => t.Transistors)
		   .HasForeignKey(t => t.Type_ID);

			modelBuilder.Entity<Transistors>()
		   .HasOne(t => t.Manufacturer)
		   .WithMany(m => m.Transistors)
		   .HasForeignKey(t => t.ManufacturerName_ID);

			modelBuilder.Entity<Resistors>()
		   .HasOne(t => t.Kind)
		   .WithMany(k => k.Resistors)
		   .HasForeignKey(t => t.Kind_ID);

			modelBuilder.Entity<Resistors>()
		   .HasOne(t => t.Type)
		   .WithMany(t => t.Resistors)
		   .HasForeignKey(t => t.Type_ID);

			modelBuilder.Entity<Resistors>()
		   .HasOne(t => t.Manufacturer)
		   .WithMany(m => m.Resistors)
		   .HasForeignKey(t => t.ManufacturerName_ID);

			modelBuilder.Entity<Diods>()
		   .HasOne(t => t.Kind)
		   .WithMany(k => k.Diods)
		   .HasForeignKey(t => t.Kind_ID);

			modelBuilder.Entity<Diods>()
		   .HasOne(t => t.Type)
		   .WithMany(t => t.Diods)
		   .HasForeignKey(t => t.Type_ID);

			modelBuilder.Entity<Diods>()
		   .HasOne(t => t.Manufacturer)
		   .WithMany(m => m.Diods)
		   .HasForeignKey(t => t.ManufacturerName_ID);

			modelBuilder.Entity<Microchips>()
		   .HasOne(t => t.Kind)
		   .WithMany(k => k.Microchips)
		   .HasForeignKey(t => t.Kind_ID);

			modelBuilder.Entity<Microchips>()
		   .HasOne(t => t.Type)
		   .WithMany(t => t.Microchips)
		   .HasForeignKey(t => t.Type_ID);

			modelBuilder.Entity<Microchips>()
		   .HasOne(t => t.Manufacturer)
		   .WithMany(m => m.Microchips)
		   .HasForeignKey(t => t.ManufacturerName_ID);

			modelBuilder.Entity<Microchips>()
		   .HasOne(t => t.Technology)
		   .WithMany(m => m.Microchips)
		   .HasForeignKey(t => t.TechnologyName_ID);

			modelBuilder.Entity<Capacitors>()
		   .HasOne(t => t.Kind)
		   .WithMany(k => k.Capacitors)
		   .HasForeignKey(t => t.Kind_ID);

			modelBuilder.Entity<Capacitors>()
		   .HasOne(t => t.Type)
		   .WithMany(t => t.Capacitors)
		   .HasForeignKey(t => t.Type_ID);

			modelBuilder.Entity<Capacitors>()
		   .HasOne(t => t.Manufacturer)
		   .WithMany(m => m.Capacitors)
		   .HasForeignKey(t => t.ManufacturerName_ID);
			modelBuilder.Entity<Foreignness>()
	        .ToTable("Foreignness");

			

			modelBuilder.Entity<Foreignness>()
		   .Property(p => p.ID)
		   .HasColumnType("tinyint");

			modelBuilder.Entity<Country>()
		   .Property(c => c.ForeignNative)
		   .HasColumnType("tinyint");

			modelBuilder.Entity<Country>()
		   .HasOne(c => c.Foreignness)
		   .WithMany(f => f.Countries)
		   .HasForeignKey(c => c.ForeignNative);

			modelBuilder.Entity<Manufacturers>()
		   .HasOne(m => m.Country)
		   .WithMany(c => c.Manufacturers)
		   .HasForeignKey(m => m.CountryID);

		}
	}
}
