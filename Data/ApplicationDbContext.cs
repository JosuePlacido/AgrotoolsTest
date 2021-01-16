using System;
using System.Collections.Generic;
using System.Text;
using Agrotools.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agrotools.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
			this.ChangeTracker.LazyLoadingEnabled = false;
		}
		public DbSet<Quiz> Quizs { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Quiz>().HasOne(q => q.Owner).WithMany()
				.OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Quiz>().HasMany(q => q.Questions)
				.WithOne()
				.HasForeignKey(q => q.QuizId)
				.OnDelete(DeleteBehavior.Cascade);
			builder.Entity<Question>().HasMany(q => q.Answers)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
