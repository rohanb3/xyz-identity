﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Xyzies.SSO.Identity.Services.Attributes;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    public class AvatarModel
    {
        [Required]
        [FileType(".jpg,.jpeg,.png,.ico", 102400)]
        public IFormFile Avatar { get; set; }
    }
}
