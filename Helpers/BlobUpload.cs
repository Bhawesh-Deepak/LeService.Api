using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.Helpers
{
    public class BlobUpload
    {
        public async Task<string> UploadImage(IFormFile formFile, IHostingEnvironment _hostingEnvironment)
        {
            try
            {
                if (formFile.Length > 0)
                {
                    var upload = Path.Combine(_hostingEnvironment.WebRootPath, "Images//");
                    using (FileStream fs = new FileStream(Path.Combine(upload, formFile.FileName), FileMode.Create))
                    {
                        await formFile.CopyToAsync(fs);
                    }
                    return "/Images//" + formFile.FileName;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return string.Empty;
        }
    }
}
