using System;
using System.Collections.Generic;

namespace LetsBank.Core
{
	public interface IRepository<T>
	{
		T Add(T user);
		void Delete(T existing);
		T Get(Guid id);
		IEnumerable<T> GetAll();
		void Update(T updated);
	}
}