using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Xyzies.SSO.Identity.Services.Attributes
{
    /// <summary>
    /// Validate file
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileTypeAttribute : ValidationAttribute
    {
        private int _maxSize { get; set; }

        private List<string> _extensionList { get; set; }

        private const string SIZE_ERROE_MESSAGE = "Invalid size: {0}, should be < {1}";
        private const string EXTENSION_ERROE_MESSAGE = "Invalid extension: {0}, should be: {1}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="maxSize"></param>
        public FileTypeAttribute(string extension, int maxSize)
        {
            _maxSize = maxSize;
            _extensionList = new List<string>();
            _extensionList.AddRange(extension.Split(','));
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxSize)
                {
                    return new ValidationResult(string.Format(SIZE_ERROE_MESSAGE, file.Length, _maxSize));
                }

                string fileExtension = Path.GetExtension(file.FileName);
                if (!_extensionList.Contains(fileExtension))
                {
                    return new ValidationResult(string.Format(EXTENSION_ERROE_MESSAGE, fileExtension, string.Join(", ", _extensionList.ToArray())));
                }
            }
            return ValidationResult.Success;
        }
    }
}
