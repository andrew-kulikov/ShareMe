using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShareMe.Core.Models;

namespace ShareMe.Core
{
	public class ShareMeDbContext : IdentityDbContext<AspNetUsers>
	{
		public ShareMeDbContext(DbContextOptions<ShareMeDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Following>()
				.HasKey(f => new { f.FolloweeId, f.FollowerId });

			builder.Entity<AspNetUsers>()
				.HasMany(u => u.Followers)
				.WithOne(f => f.Followee)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<AspNetUsers>()
				.HasMany(u => u.Followings)
				.WithOne(f => f.Follower)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Photo>()
				.HasMany(u => u.Ratings)
				.WithOne(r => r.Photo)
				.OnDelete(DeleteBehavior.Restrict);


			base.OnModelCreating(builder);
		}

		public DbSet<Photo> Photos { get; set; }
		public DbSet<Following> Followings { get; set; }
	}
}
