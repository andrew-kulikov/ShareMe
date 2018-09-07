﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShareMe.Core.Models;

namespace ShareMe.Core
{
	public class IdentityDbContext : IdentityDbContext<AspNetUsers>
	{
		public DbSet<AspNetUsers> ApplicationUsers { get; set; }

		public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
		}

	}
}