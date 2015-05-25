using RezepteTagebuch.Models;
using RezepteTagebuch.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RezepteTagebuch.Data
{
    public static class SQLiteMappingExtensions
    {
        public static Recipe ToModel(this RecipeEntity entity)
        {
            return new Recipe
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                DishCategory = entity.DishCategory,
                Hint = entity.Hint,
                Tags = entity.Tags,
                FoodPicturePath = entity.FoodPicturePath,
                DescriptionPicturePath = entity.DescriptionPicturePath
            };
        }

        public static RecipeEntity ToEntity(this Recipe model)
        {
            return new RecipeEntity
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                DishCategory = model.DishCategory,
                Hint = model.Hint,
                Tags = model.Tags,
                FoodPicturePath = model.FoodPicturePath,
                DescriptionPicturePath = model.DescriptionPicturePath
            };
        }

        // TODO: Es wird keine Instanz zurückgegeben
        public static RecipeTable ToTable(this Recipe model)
        {
            var table = new RecipeTable
            {
                Recipe = ToEntity(model),
                CookDates = CreateEntities(model.Id, model.CookDates)
            };

            return table;
        }

        public static List<RecipeCookDateEntity> CreateEntities(Guid recipeId, List<DateTime> list)
        {
            return list.Select(l => new RecipeCookDateEntity { Id = Guid.NewGuid(), RecipeId = recipeId, CookDate = l }).ToList();
        }

    }
}
