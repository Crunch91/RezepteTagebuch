using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RezepteTagebuch.Models
{
    public class RecipeCookDateEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        [NotNull]
        public Guid RecipeId { get; set; }
        [NotNull]
        public DateTime CookDate { get; set; }
    }
}
