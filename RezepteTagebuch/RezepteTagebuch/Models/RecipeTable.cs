using System;
using System.Collections.Generic;
using System.Text;

namespace RezepteTagebuch.Models
{
    public class RecipeTable
    {
        public RecipeEntity Recipe { get; set; }
        public List<RecipeCookDateEntity> CookDates { get; set; }
    }
}
