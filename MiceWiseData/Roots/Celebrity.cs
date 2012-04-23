using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MiceWiseData.Roots
{
    public class Celebrity
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        public string Portfolio { get; set; }
        [Required]
        public string Email { get; set; }
        public string Mouse { get; set; }
        public string Tablet { get; set; }
        [DisplayName("Relative path to image")]
        public string Picture { get; set; }

        public bool Active { get; set; }

        public string MouseId { get; set; }
        public string TabletId { get; set; }
    }
}
