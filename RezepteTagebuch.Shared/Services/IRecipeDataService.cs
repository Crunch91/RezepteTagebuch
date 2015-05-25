using RezepteTagebuch.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezepteTagebuch.Shared.Services
{
    public interface IRecipeDataService
    {
        IEnumerable<Recipe> GetAll();
        IEnumerable<Recipe> GetByDate(DateTime date);
        Recipe GetById(Guid id);
        void AddOrUpdate(Recipe item);
    }
}
