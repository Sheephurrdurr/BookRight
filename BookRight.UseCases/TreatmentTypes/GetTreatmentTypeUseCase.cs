using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.UseCases.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.TreatmentTypes
{
	public class GetTreatmentTypeUseCase
	{
		private readonly ITreatmentTypeRepository _repository;

		public GetTreatmentTypeUseCase(ITreatmentTypeRepository repository)
		{
			_repository = repository;
		}

		public async Task<TreatmentTypeDto?> Excute(Guid id)
		{
			var treatmentType = await _repository.GetByIdAsync(id);

			if (treatmentType == null)
				return null;

				//Map til en Dto for at beskytte domænemodel
				return new TreatmentTypeDto
				{
	              Id = treatmentType.Id,
				  Name = treatmentType.Name
				};
		}
	}
}
