using P013EStore.Core.Entities;
using P013EStroe.Data.Abstract;

namespace P013EStroe.Service.Abstract
{
	public interface IService<T> : IRepository<T> where T : class, IEntity, new()
	{

	}
}
