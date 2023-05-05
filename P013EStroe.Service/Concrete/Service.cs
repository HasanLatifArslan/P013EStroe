using P013EStore.Core.Entities;
using P013EStroe.Data;
using P013EStroe.Data.Concrete;
using P013EStroe.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P013EStroe.Service.Concrete
{
	public class Service<T> : Repository<T>, IService<T> where T : class, IEntity, new()
	{
		public Service(DatabaseContext context) : base(context)
		{
		}
	}
}
