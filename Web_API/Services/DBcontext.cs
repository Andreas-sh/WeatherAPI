using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web_API.models.weatherapi;


namespace Web_API.Services
{
    public class DBcontext : DbContext
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options) { }

        public DbSet<WeatherItem> WeatherItems { get; set; }
        public DbSet<AstronomyItem> AstronomyItems { get; set; }
    }
}