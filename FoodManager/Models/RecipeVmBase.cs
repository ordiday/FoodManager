using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodManager.Web.Models
{
    public abstract class RecipeVmBase
    {
        public abstract bool IsYummly { get; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
    }
}