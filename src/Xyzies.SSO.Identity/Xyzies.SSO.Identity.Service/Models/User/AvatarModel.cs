using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xyzies.SSO.Identity.Services.Attributes;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    public class AvatarModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [FileType(".jpg, .jpeg, .png, .ico", 102400)]
        public IFormFile Avatar { get; set; }
    }
}
