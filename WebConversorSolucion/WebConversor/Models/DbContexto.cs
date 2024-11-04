using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace WebConversor.Models;

public class DbContexto : DbContext
{

    public DbContexto(DbContextOptions<DbContexto> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Coin>().HasData(
            new Coin { Id = 1, Name = "Dólar Estadounidense", ShortName = "USD", Symbol = "USD" },
            new Coin { Id = 2, Name = "Euro", ShortName = "EUR", Symbol = "EUR" },
            new Coin { Id = 3, Name = "Yen Japonés", ShortName = "YEN", Symbol = "JPY" }
        );

        modelBuilder.Entity<User>().HasData(
           new User { Id = 1, Name = "Dólar Estadounidense", Email = "asda@gmail.com", LastName = "aaa", Password = "ddd", Img = "dd" },
           new User { Id = 2, Name = "Euro", Email = "ggrg2@gmail.com", LastName = "aaa", Password = "fff", Img = "ff" }

       );
        modelBuilder.Entity<History>().HasData(
          new History { Id = 1, UserId = 2, FromCoin = "EUR", ToCoin = "USD", Result = 20.0, Date = new DateTime(2004, 12, 20) }
        );

    }



    /*Declaramos las clases*/
    public DbSet<Coin> Coins { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<History> Histories { get; set; }
}