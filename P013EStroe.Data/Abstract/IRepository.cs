using System;
using System.Collections.Generic;
using System.Linq.Expressions; // kendi lambda expression kullabileceğimiz metotları yazmamızı sağlayan kütüphane
using System.Text;
using System.Threading.Tasks;

namespace P013EStroe.Data.Abstract
{
	public interface IRepository<T> where T : class // Irepository i dışarıdan alacağı t tipinde bir parametreyle çalışacak ve where şartı ile  bu t nin veri tipi bir class olmalıdır
	{
		// senkron metotlar

		List<T> GetAll(); // db deki tüm kayıtları çekmemizi sağlayacak metot imzası
		List<T> GetAll(Expression<Func<T,bool>> expression); // uygulmada verileri listelerken p=> p.IsActtive vb gibi sorgulama ve filtreleme kodları kullanabilmemizi sağlar
		T Get(Expression<Func<T, bool>> expression);
		T Find(int id);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
		int Save();

		//asenkron metotlar
		Task<T> FindAsync(int id);
		Task<T> GetAsync(Expression<Func<T, bool>> expression); // lambda expression kullanarak db de filtreleme yazıp geriye 1 tane kayıt döner
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
		Task<List<T>> GetAllAsync();
		Task AddAsync(T entity);
		Task<int> SaveAsync(); // asenktron kaydetme

	}
}
