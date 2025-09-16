﻿using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;

        [MaxLength(500)]
        public string? Description { get; set; }
        // Navigation Property

        public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
    }
}
