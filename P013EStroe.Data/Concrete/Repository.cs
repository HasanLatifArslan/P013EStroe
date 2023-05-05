using Microsoft.EntityFrameworkCore;
using P013EStore.Core.Entities;
using P013EStroe.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace P013EStroe.Data.Concrete
{
	public class Repository<T> : IRepository<T> where T : class, IEntity, new() // repository classına gönderilecek t nin şartları t bir class olmalı Ientity den implemente almalı ve new lenebilir olmalı
	{
		internal DatabaseContext _context; // boş bir database context oluşturduk 
		internal DbSet<T> _dbSet; // boş bir db set tanımladık repository e gönderilecek t classını parametre verdik
		public Repository(DatabaseContext context)
		{
			_context = context; // _contexti burada doldurduk aşağıda kullanabilmek için doldurmadan kullanmak istersek null reference hatası olurdu
			_dbSet = context.Set<T>(); // repositoryde gönderilecek t classı için context üzerindeki db sete göre kendini ayarla
		}

		
		
		public void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
		}

		public T Find(int id)
		{
			return _dbSet.Find(id);
			
		}

		public async Task<T> FindAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public T Get(Expression<Func<T, bool>> expression)
		{
			return _dbSet.FirstOrDefault(expression);
		}

		public List<T> GetAll()
		{
			return _dbSet.AsNoTracking().ToList(); // eğer sadece listeleme yapacaksak yani liste üzerinde kayıt güncelleme gibi bir işlem yapmayacaksak entity framewokdeki asnotracking yöntemi ile listeyi daha performanslı çekebiliriz
		}

		public List<T> GetAll(Expression<Func<T, bool>> expression)
		{
			return _dbSet.AsNoTracking().Where(expression).ToList();
		}

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
		{
			return await _dbSet.AsNoTracking().Where(expression).ToListAsync();
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _dbSet.AsNoTracking().ToListAsync();
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
		{
			return await _dbSet.FirstOrDefaultAsync(expression);
		}

		public int Save()
		{
			return _context.SaveChanges(); // entity framework de ekle güncelle sil vb işlemeleri db ye işleyen metot direk context üzerinden çalışır dbset in böyle bir metodu olmadığı için _contextsavechanges diyerek database context imiz üzerinden tüm işlemleri veritabanına yansıtmamızı sağlayan bu metodu bir kere çağırmamız gerekli yoksa işlemler db ye işlenmez
		} 

		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Update(T entity)
		{
			_context.Update(entity);
		}

		
	}
}
