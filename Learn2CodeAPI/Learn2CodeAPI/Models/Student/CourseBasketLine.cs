using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class CourseBasketLine : BaseEntity
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public int CourseSubCategoryId { get; set; }
        public CourseSubCategory CourseSubCategory { get; set; }
    }
}
