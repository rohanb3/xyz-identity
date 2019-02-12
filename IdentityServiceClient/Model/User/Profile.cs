﻿using System.ComponentModel.DataAnnotations;

namespace IdentityServiceClient.Model
{
    public class Profile: BaseProfile
    {
        [Required]
        public string ObjectId { get; set; }
        [Required]
        [StringLength(64)]
        public string UserName { get; set; }
        [Required]
        [StringLength(64)]
        public string Email { get; set; }      
        [StringLength(64)]
        public string CompanyName { get; set; }
    }
}