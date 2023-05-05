using Microsoft.EntityFrameworkCore;
using P013EStore.Core.Entities;
using P013EStroe.Data.Configurations;
using System.Reflection;

namespace P013EStroe.Data
{
	public class DatabaseContext : DbContext
	{
		// katmanlı mimaride bir proje katmanından başka bir katmana erişebilmek için bulunduğumuz data projesinin dependencies kısmına sağ tıklayıp add project refences diyerek açılan pencereden core projesine tik atıp ok diyerek pencereyi kapatmamız gerekiyor.
		public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// OnConfiguring metodu entity frameworkcore ile gelir ve veritabanı bağlantı ayarlarını yapmamızı sağlar.
			optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=P013EStroe; Trusted_Connection=True");
			//optionsBuilder.UseSqlServer(@"Server=CanlıServrAdı; Database=CAnlıdakiDatabase; Username=CanlıdakiVeritabanıUsername; Password=canlıdakiveritabanışifre");
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//fluentapı ile veritabanı tablolarımız oluşurken veri tiplerini db kurallarını burada tanılmayabiliriz
			modelBuilder.Entity<AppUser>().Property(a=>a.Name).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50); // fluentapi ile appuser classının name propertysi için oluşacak veritabanı kolonu ayarlarını bu şekilde belirleyebilirz
			modelBuilder.Entity<AppUser>().Property(a => a.Surname).HasColumnType("varchar(50)").HasMaxLength(50);
			modelBuilder.Entity<AppUser>().Property(a => a.UserName).HasColumnType("varchar(50)").HasMaxLength(50);
			modelBuilder.Entity<AppUser>().Property(a => a.Password).IsRequired().HasColumnType("nvarchar(100)").HasMaxLength(100);
			modelBuilder.Entity<AppUser>().Property(a => a.Email).IsRequired().HasMaxLength(50);
			modelBuilder.Entity<AppUser>().Property(a => a.Phone).HasMaxLength(20);


			//Fluentapi has data ile db oluştduktan sonra başlangıç kayıtları ekleme

			modelBuilder.Entity<AppUser>().HasData(new AppUser
			{
				Id = 1,
				Email = "info@P013EStore.com",
				Password= "123",
				IsActive= true,
				IsAdmin = true,
				Name="Admin",
				UserGuid=Guid.NewGuid(), // kullancıya benzersiz bir id no oluşturur
			});
			//modelBuilder.ApplyConfiguration(new BrandConfigurations()); // marka için yaptığımız konfigürasyon ayarlarını çağırdık
			//modelBuilder.ApplyConfiguration(new CategoryConfigurations()); 
			//modelBuilder.ApplyConfiguration(new ContactConfigurations()); 
			//modelBuilder.ApplyConfiguration(new ProductConfigurations()); 
			//modelBuilder.ApplyConfiguration(new SliderConfigurations()); 

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			// ugulamadaki tüm configurations class larını burada çalıştır

			// fuletn validation : data annotationdaki hata mesajları vb işlemlerini yönetebileceğimiz parti paket.

			// Katmanlı mimaride MvcWebUI katmanından direk data katmanına erişilmesi istenmez arada bir iş katmanının tüm db süreçlerini yönetmesi istenir bu yüzden solutiona service katmanı ekleyip mvc katmanından service katmanına erişim vermemiz gerekiz service katmanı da data katmanına erişir data katmanı da core katmanına erişir böylece mvcuı >service>data>core ile en üstten en alt katmana kadar ulaşılabilmiş olur
			base.OnModelCreating(modelBuilder);
		}

	}
}