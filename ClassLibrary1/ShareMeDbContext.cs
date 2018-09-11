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

			builder.Entity<Photo>()
				.HasMany(u => u.Comments)
				.WithOne(r => r.Photo)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Photo>()
				.HasMany(p => p.Ratings)
				.WithOne(r => r.Photo);

			builder.Entity<AspNetUsers>()
				.HasMany(u => u.Comments)
				.WithOne(r => r.User);

			builder.Entity<AspNetUsers>()
				.HasMany(u => u.Photos)
				.WithOne(r => r.User);

			builder.Entity<AspNetUsers>()
				.HasMany(u => u.Ratings)
				.WithOne(r => r.User);

			base.OnModelCreating(builder);
		}

		public virtual DbSet<Photo> Photos { get; set; }
		public virtual DbSet<Following> Followings { get; set; }
		public virtual DbSet<Tag> Tags { get; set; }
		public virtual DbSet<PhotoTag> PhotoTags { get; set; }
		public virtual DbSet<Comment> Comments { get; set; }
		public virtual DbSet<Rating> Ratings { get; set; }
		public virtual DbSet<RatingType> RatingTypes { get; set; }
		public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
	}
}
