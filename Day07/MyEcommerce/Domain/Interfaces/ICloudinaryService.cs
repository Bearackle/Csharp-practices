using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEcommerce.Domain.Interfaces
{
    public interface ICloudinaryService
    {
        Cloudinary GetInstance();
    }
}