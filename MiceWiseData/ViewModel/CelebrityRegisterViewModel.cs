using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.Web.Mvc;

namespace MiceWiseData.ViewModel
{
    public class CelebrityRegisterViewModel
    {
        [DisplayName("Name & Surname")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Country & City")]
        [Required]
        public string Place { get; set; }
        [DisplayName("Link to your portfolio")]
        [Required]
        [Url]
        public string Portfolio { get; set; }
        [DisplayName("Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Mouse you use")]
        public string Mouse { get; set; }
        [DisplayName("Pen Tablet you use")]
        public string Tablet { get; set; }
        [DisplayName("Upload your picture")]
        [Required]
        public string Picture { get; set; }

        public Guid FileIdent { get; set; }
    }
}
