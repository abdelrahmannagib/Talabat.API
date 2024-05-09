﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
	internal static class SpecificationsEvaluator<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> spec)
		{
			var query = inputQuery;//ex DBContext.Set<Product>

			if (spec.Criteria is not null)
				query = query.Where(spec.Criteria);

			query = spec.Includes.Aggregate(query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));

			return query;
		}
	}
}
