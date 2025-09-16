﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Ratings.Dtos;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Ratings.Queries.GetRatingById
{
    public class GetRatingByIdQueryHandler(ILogger<GetRatingByIdQueryHandler> logger,
     IRatingsRepository ratingsRepository,
     IRatingAuthorizationService ratingAuthorizationService,
     IMapper mapper) : IRequestHandler<GetRatingByIdQuery, RatingDto>
    {
        public async Task<RatingDto> Handle(GetRatingByIdQuery request, CancellationToken cancellationToken)
        {
            var rating = await ratingsRepository.GetByIdWithIncluded(request.Id)
                    ?? throw new NotFoundException(nameof(Rating), request.Id.ToString());

            var ratingDto = mapper.Map<RatingDto>(rating);

            //restaurantDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);
            if (!ratingAuthorizationService.Authorize(rating, ResourceOperation.Read))
                throw new ForbidException();

            logger.LogInformation("Getting rating {RatingId}", request.Id);

            return ratingDto;
        }
    }
}
