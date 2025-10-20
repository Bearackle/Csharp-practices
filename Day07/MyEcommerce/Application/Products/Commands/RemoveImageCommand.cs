using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using MyEcommerce.Application.Interfaces;
using MyEcommerce.Domain.Entities;
using MyEcommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyEcommerce.Application.Products.Commands
{
    public class RemoveImageCommand : IRequest<Unit>
    {
        public int ImageId {  get; set; }
        public int pid { get; set; }
        public RemoveImageCommand(int pid, int ImageId)
        {
            this.ImageId = ImageId;
        }
        public class RemoveImageCommandHander : IRequestHandler<RemoveImageCommand, Unit>
        {
            private readonly IDbContext _context;

            private readonly ICloudinaryService _cloudinary;
            public RemoveImageCommandHander(IDbContext context,ICloudinaryService cloudinary)
            {
                _context = context;
                _cloudinary = cloudinary;
            }
            public async Task<Unit> Handle(RemoveImageCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Include(p => p.Images).
                                FirstOrDefaultAsync(p => p.ProductId == request.pid);
                var productImage = product.Images.FirstOrDefault(i => i.ImageId == request.ImageId);
                var parts = productImage.ImagePath.Split(new[] { "/upload" }, StringSplitOptions.None);
                if (parts.Length > 1)
                {
                    var pathPart = parts[1];
                    var publicIdWithExt = pathPart.Substring(pathPart.IndexOf('/') + 1); 
                    var publicId = Path.ChangeExtension(publicIdWithExt, null);
                    await _cloudinary.GetInstance().DestroyAsync(new DeletionParams(publicId));
                }
                return Unit.Value;
            }
        }
    }
}