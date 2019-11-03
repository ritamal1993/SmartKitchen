using SmartKitchen3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartKitchen3.Extensions
{
    public  static class ThumbnailExtensions
    {public static IEnumerable<ThumbnailModel> GetRecipeThumbnail(this List<ThumbnailModel> thumbnails, ApplicationDbContext db=null,string search=null)
        {
            try
            {
                if (db == null)
                {

                    db = ApplicationDbContext.Create();

                }
                thumbnails = (from b in db.Recipes
                              select new ThumbnailModel
                              {
                                  Recipeid = b.RecipeId,
                                  Title = b.Title,
                                  Instructions = b.Instructions,
                                  ImageUrl = b.ImageUrl,
                                  Link = "/RecipeDetail/Index/" + b.RecipeId
                              }).ToList();

                if (search != null)
                    return thumbnails.Where(t => t.Title.ToLower().Contains(search.ToLower())).OrderBy(t => t.Title);
            }
            catch(Exception ex)
            {

            }
            return thumbnails.OrderBy(b => b.Title);
        }
    }
}