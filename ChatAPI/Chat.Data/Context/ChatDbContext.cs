    using System.Reflection;
    using Chat.Data.Models;
    using Microsoft.EntityFrameworkCore;



    namespace Chat.Data.Context
    {
        public class ChatDbContext : DbContext
        {
            public ChatDbContext(DbContextOptions<ChatDbContext> options)
                : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=Chat.db");
        //}

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ApplicationUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                   .ToTable(new ApplicationUser().GetEntityName())
                   .HasKey(x => x.Id);

            builder.Entity<ApplicationUser>().Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Entity<Room>().HasKey(x => x.Id);
            builder.Entity<Room>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<Room>().Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Room>().HasOne(x => x.Admin).WithMany(u => u.Rooms)
                    .IsRequired();
            builder.Entity<Room>().ToTable(new Room().GetEntityName());


                builder.Entity<Message>().HasKey(x => x.Id);
            builder.Entity<Message>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<Message>().ToTable(new Message().GetEntityName());

                builder.Entity<Message>().Property(s => s.Content).IsRequired().HasMaxLength(500);

                builder.Entity<Message>().HasOne(s => s.ToRoom)
                    .WithMany(m => m.Messages)
                    .HasForeignKey(s => s.ToRoomId)
                    .OnDelete(DeleteBehavior.Cascade);


                builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
    }
