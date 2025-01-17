﻿using System;
using System.Collections.Generic;
using System.Linq; 
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository.IRepository;

public class Repository<T> : IRepository<T> where T : class
{
	private readonly ApplicationDbContext _db;
	internal DbSet<T> dbSet; 
    public Repository(ApplicationDbContext db)
    {
		_db = db;
		this.dbSet = _db.Set<T>();
    }
    public void Add(T entity)
	{
		dbSet.Add(entity);
	}

	public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includeproperties = null)
	{
		IQueryable<T> query = dbSet;
		query = query.Where(filter);
		return query.FirstOrDefault();
	}

	public IEnumerable<T> GetAll(string? includeproperties = null)
	{
		IQueryable<T> query = dbSet;
		return query.ToList();
	}

	public void Remove(T entity)
	{
		dbSet.Remove(entity);
	}


	public void RemoveRange(IEnumerable<T> entity)
	{
		dbSet.RemoveRange(entity);
	}
}
