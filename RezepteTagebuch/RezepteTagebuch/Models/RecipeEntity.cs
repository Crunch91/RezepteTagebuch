using RezepteTagebuch.Shared.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RezepteTagebuch.Models
{
    public class RecipeEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        [NotNull]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Hint { get; set; }
        //public List<DateTime> CookDates { get; set; }
        [NotNull]
        public DishCategory DishCategory { get; set; }
        public string Tags { get; set; }
        public string DescriptionPicturePath { get; set; }
        public string FoodPicturePath { get; set; }
    }
}
