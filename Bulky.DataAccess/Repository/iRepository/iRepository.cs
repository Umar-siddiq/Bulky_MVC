using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository.IRepository
{
	internal interface IRepository<T> where T : class
	{
		//T  = Category
		///
		IEnumerable<T> GetAll(); 

		T Get(Expression<Func<T, bool>> filter);
	}
}
