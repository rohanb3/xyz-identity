﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ardas.AspNetCore.Logging.Models
{
    public class TcpLogOptions
    {
        [DefaultValue(false)]
        public bool SecureConnection { get; set; }
        [DefaultValue(true)]
        public bool Dispose { get; set; }
        [Required]
        public string Ip { get; set; }
        [Required]
        public int Port { get; set; }
    }
}
