﻿using ExamCode.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCode.Data.DAL;

public class AppDbContext:IdentityDbContext
{

    public AppDbContext(DbContextOptions options): base(options)
    {
        
    }

    public DbSet<Explore> Explores { get; set; }

    public DbSet<AppUser> Users { get; set; }
}