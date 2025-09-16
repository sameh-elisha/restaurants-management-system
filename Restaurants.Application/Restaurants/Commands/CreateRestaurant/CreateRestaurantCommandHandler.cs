﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository,
        IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("{UserEmail} [{UserId}] is creating a new restaurant {@Restaurant}",
                currentUser!.Email,
                currentUser.Id,
                request);

            logger.LogInformation("creating a new restaurant {@Restaurant}", request);

            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser.Id;

            await restaurantsRepository.AddAsync(restaurant);

            return restaurant.Id;
        }
    }

}
