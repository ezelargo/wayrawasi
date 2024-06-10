﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using WayraWasi.Models;

namespace WayraWasi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cabania> Cabanias { get; set; }
        public DbSet<Reserva> Reservaciones { get; set; }

    }
}
