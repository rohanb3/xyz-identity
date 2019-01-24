﻿using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace graphApiService.Dtos.User {
    public class UserProfileDto {
        [Required]
        public string ObjectId { get; set; }
        [Required]
        [StringLength(64)]
        public string GivenName { get; set; }
        [Required]
        [StringLength(64)]
        public string UserName { get; set; }
        [Required]
        [StringLength(64)]
        public string Email { get; set; }
        [Required]
        [StringLength(64)]
        public string Surname { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int RetailerId { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        [Url]
        public Url AvatarUrl { get; set; }
        [Required]
        [StringLength(64)]
        public string DisplayName { get; set; }
        [StringLength(64)]
        public string City { get; set; }
        [StringLength(64)]
        public string CompanyName { get; set; }
        [Required]
        public bool? AccountEnabled { get; set; }
    }
}