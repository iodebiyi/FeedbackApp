using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace FIS.Models
{
    [MetadataType(typeof(feedbackMetadata))]
    public partial class feedback
    {

    }
     

    public class feedbackMetadata { 
        [Display(Name = "Course")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Course required")]
        public string Course { get; set; }

        [Display(Name = "FeedbackMessage")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Feedback required")]
        public string FeedbackMessage { get; set; }
    }
}