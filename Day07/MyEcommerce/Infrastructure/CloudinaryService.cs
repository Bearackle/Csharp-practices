using CloudinaryDotNet;
using MyEcommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEcommerce.Infrastructure
{
    public class CloudinaryService : ICloudinaryService
    {
        public  Cloudinary _cloudinary {  get; }
        public CloudinaryService(Account account)
        {
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }
        public Cloudinary GetInstance()
        {
            return _cloudinary;
        }
    }
}