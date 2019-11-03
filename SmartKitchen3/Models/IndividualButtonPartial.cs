using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SmartKitchen3.Models
{
    public class IndividualButtonPartial
    {
        public string ButtonType { get; set; }
        public string Action { get; set; }
    
        public string Glyph { get; set; }
        public string Text { get; set; }

        public int? CategoryId { get; set; }
        public int? RecipeId { get; set; }
        public int? IngredientId { get; set; }

        public int? KitchenId { get; set; }

        public int? UserId { get; set; }
        public int? CalendarId { get; set; }
        public int? MembershipTypeId { get; set; }

        public string ActionParameter
        {
            get
            {
                var param = new StringBuilder(@"/");
                if(RecipeId!=null&&RecipeId>0)
                {
                    param.Append(String.Format("{0}", RecipeId));
                }
                if (CategoryId != null && CategoryId > 0)
                {
                    param.Append(String.Format("{0}", CategoryId));
                }
                if (IngredientId != null && IngredientId > 0)
                {
                    param.Append(String.Format("{0}", IngredientId));
                }
                if (UserId != null && UserId > 0)
                {
                    param.Append(String.Format("{0}", UserId));
                }
                if (KitchenId != null && KitchenId> 0)
                {
                    param.Append(String.Format("{0}", KitchenId));
                }
                if (CalendarId != null && CalendarId > 0)
                {
                    param.Append(String.Format("{0}", CalendarId));
                }
             

                return param.ToString();
            }
        }
    }
}