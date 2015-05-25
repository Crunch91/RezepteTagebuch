using RezepteTagebuch.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RezepteTagebuch.Shared.Models
{
    /// <summary>
    /// Rezept
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Titel
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Rezeptbeschreibung
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Hinweis/Tipp
        /// </summary>
        public string Hint { get; set; }

        /// <summary>
        /// Liste an Zubereitungstagen
        /// </summary>
        public List<DateTime> CookDates { get; set; }

        /// <summary>
        /// Kategorie
        /// </summary>
        public DishCategory DishCategory { get; set; }

        /// <summary>
        /// Liste an Schlagwörtern
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Rezeptfotopfad
        /// </summary>
        public string DescriptionPicturePath { get; set; }

        /// <summary>
        /// Rezeptfotopfad
        /// </summary>
        public string FoodPicturePath { get; set; }
    }
}
