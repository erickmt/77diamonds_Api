using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace DiamondApi.Controllers
{
    public class PhotoController : ApiController
    {

        public HttpResponseMessage Get(string FileName)
        {
            if (string.IsNullOrEmpty(FileName))
                FileName = "no_picture";

            string filePath = $@"{AppContext.BaseDirectory}Assets\Images\{FileName}.jpeg";

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                Image image = Image.FromStream(fileStream);
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Jpeg);

                result.Content = new ByteArrayContent(memoryStream.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                return result;
            }
        }
    }
}
