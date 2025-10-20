using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using MyEcommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyEcommerce.Application.Products.Commands
{
    public class UploadFileCloudinaryCommand : IRequest<List<string>>
    {
        public HttpPostedFileBase[] File { get; set; }
        public UploadFileCloudinaryCommand(HttpPostedFileBase[] file)
        {
            this.File = file;
        }
        public class UploadFileCloudinaryHandler : IRequestHandler<UploadFileCloudinaryCommand, List<string>>
        {
            private readonly ICloudinaryService _cloudinary;
            public UploadFileCloudinaryHandler(ICloudinaryService cloudinary)
            {
                _cloudinary = cloudinary;
            }

            public async Task<List<string>> Handle(UploadFileCloudinaryCommand request, CancellationToken cancellationToken)
            {
                if(request.File == null)
                {
                    return null;
                }
                List<string> url = new List<string>();
                foreach(HttpPostedFileBase f in request.File)
                {
                    var uploadParam = new ImageUploadParams
                    {
                        File = new FileDescription(f.FileName, f.InputStream),
                        Folder = "products"
                    };
                    var uploadResult = await _cloudinary.GetInstance().UploadAsync(uploadParam);
                    url.Add(uploadResult.SecureUrl.ToString());
                }
                return url;
            }
        }
    }
}