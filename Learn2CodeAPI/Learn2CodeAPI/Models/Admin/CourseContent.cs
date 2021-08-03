using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
    public class CourseContent : BaseEntity
    {
        public string Filepath { get; set; }

        public byte[] DataFiles { get; set; }
        public int CourseSubCategoryId { get; set; }

        [ForeignKey("CourseSubCategoryId")]
        public CourseSubCategory CourseSubCategory { get; set; }

        public int ContentTypeId { get; set; }

        [ForeignKey("ContentTypeId")]
        public ContentType ContentType { get; set; }

    }
}
