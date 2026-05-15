using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Infrastructure.Persistence.Repositories
{
	public class TreatmentTypeRepository : ITreatmentTypeRepository
	{
		private readonly MyDbContext _context;

		public TreatmentTypeRepository(MyDbContext context)
		{
			_context = context;
		}

		public async Task<TreatmentType?> GetByIdAslync(int id)
		{
			return await _context.TreatmentTypes.FindAsync(id);
		}

		public async Task<IEnumerable<TreatmentType>> GetallAsync()
		{
			return await _context.TreatmentTypes.ToListAsync();
		}

		public async Task AddAsync(TreatmentType treatmentType)
		{
			await _context.Treatmenttypes.Addasync(treatmentType);
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