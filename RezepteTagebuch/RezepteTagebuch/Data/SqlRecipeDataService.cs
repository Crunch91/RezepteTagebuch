using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.IO;
using RezepteTagebuch.Shared.Models;
using RezepteTagebuch.Shared.Services;
using RezepteTagebuch.Models;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(RezepteTagebuch.Data.SqlRecipeDataService))]
namespace RezepteTagebuch.Data
{
    public class SqlRecipeDataService : IRecipeDataService
    {
        static object _lock = new object();

        private SQLiteConnection _database;
        string sqliteFilename = "RezepteTagebuchSQLite.db3";

        //        string DatabasePath
        //        {
        //            get
        //            {
        //#if __IOS__
        //                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
        //                string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
        //                var path = Path.Combine(libraryPath, sqliteFilename);
        //#else
        //#if __ANDROID__
        //                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
        //                var path = Path.Combine(documentsPath, sqliteFilename);
        //#else
        //                // WinPhone
        //                var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);;
        //#endif
        //#endif
        //                return path;
        //            }
        //        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public SqlRecipeDataService()
        {
            // databasePath === "/data/data/RezepteTagebuch.Droid/files/RezepteTagebuchSQLite.db3"
            var databasePath = DependencyService.Get<IFileService>().GetDataFilePath(sqliteFilename);
            _database = new SQLiteConnection(databasePath);

            // create the tables
            //_database.DropTable<RecipeEntity>(); // Test
            //_database.DropTable<RecipeCookDateEntity>(); // Test
            _database.CreateTable<RecipeEntity>();
            _database.CreateTable<RecipeCookDateEntity>();
        }

        public IEnumerable<Recipe> GetAll()
        {
            lock (_lock)
            {
                var models = (from e in _database.Table<RecipeEntity>()
                              select e.ToModel());

                foreach (var m in models)
                {
                    m.CookDates = GetCookDatesByRecipeId(m.Id);
                    yield return m;
                }
            }
        }

        public IEnumerable<Recipe> GetByDate(DateTime date)
        {
            var test = _database.Table<RecipeCookDateEntity>().ToList();
            var recipeIds = _database.Query<RecipeCookDateEntity>("SELECT *  FROM  [RecipeCookDateEntity] WHERE [CookDate] = ?", date).Select(e => e.RecipeId).Distinct();

            var models = (from e in _database.Table<RecipeEntity>()
                          where recipeIds.Contains(e.Id)
                          select e.ToModel());

            foreach (var item in models)
            {
                item.CookDates = GetCookDatesByRecipeId(item.Id);
            }

            return models;
        }

        public IEnumerable<Recipe> GetItemsNotDone()
        {
            lock (_lock)
            {
                return _database.Query<Recipe>("SELECT * FROM [Recipe] WHERE [Done] = 0");
            }
        }

        public Recipe GetById(Guid id)
        {
            lock (_lock)
            {
                return _database.Table<RecipeEntity>().FirstOrDefault(x => x.Id == id).ToModel();
            }
        }

        public void AddOrUpdate(Recipe recipe)
        {
            lock (_lock)
            {
                if (recipe.Id != Guid.Empty)
                {
                    // Update
                    var item = recipe.ToEntity();
                    _database.Update(item);

                    var cookDates = _database.Query<RecipeCookDateEntity>("SELECT *  FROM  [RecipeCookDateEntity] WHERE [RecipeId] = ?", recipe.Id).Select(e => e.CookDate);
                    var newCookDates = recipe.CookDates.Where(d => !cookDates.Contains(d));
                    if (newCookDates.Count() > 0)
                    {
                        var cd = SQLiteMappingExtensions.CreateEntities(recipe.Id, recipe.CookDates);
                        _database.InsertAll(cd);
                    }
                }
                else
                {
                    // Insert
                    recipe.Id = Guid.NewGuid();
                    //var item = recipe.ToTable();
                    var r = recipe.ToEntity();
                    var cd = SQLiteMappingExtensions.CreateEntities(recipe.Id, recipe.CookDates);

                    _database.Insert(r);
                    _database.InsertAll(cd);
                }
            }
        }

        public int DeleteItem(int id)
        {
            lock (_lock)
            {
                return _database.Delete<Recipe>(id);
            }
        }

        private List<DateTime> GetCookDatesByRecipeId(Guid recipeId)
        {
            var query = _database.Query<RecipeCookDateEntity>("SELECT *  FROM  [RecipeCookDateEntity] WHERE [RecipeId] = ?", recipeId).Select(e => e.CookDate);
            return query.ToList();
        }
    }
}

