using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BookRight.Domain.Aggregates.Clinic;
using Microsoft.EntityFrameworkCore;
using BookRight.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace BookRight.Infrastructure.Persistence.Repositories
{
	public class TreatmentTypeRepository : ITreatmentTypeRepository
	{
		private readonly BookRightDbContext _context;

		public TreatmentTypeRepository(BookRightDbContext context)		{
			_context = context;
		}

		public async Task<TreatmentType?> GetByIdAsync(Guid id)
		{
			return await _context.TreatmentTypes.FindAsync(id);
		}

		public async Task<IEnumerable<TreatmentType>> GetAllAsync()
		{
			return await _context.TreatmentTypes.ToListAsync();
		}

		public async Task AddAsync(TreatmentType treatmentType)
		{
			await _context.TreatmentTypes.AddAsync(treatmentType);
		}

		public void Update(TreatmentType treatmentType)
		{
			_context.TreatmentTypes.Update(treatmentType);
		}

		public void Delete(TreatmentType treatmentType)
		{
			_context.TreatmentTypes.Remove(treatmentType);
		}

		public async Task<bool> SaveChangesAsync()
		{
			return (await _context.SaveChangesAsync()) > 0;
		}
	}
}