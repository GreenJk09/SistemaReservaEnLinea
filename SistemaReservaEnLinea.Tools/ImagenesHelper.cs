using System;
using System.IO;
using System.Threading.Tasks;
using Imageflow.Fluent;
using Microsoft.AspNetCore.Http;
using SistemaReservaEnLinea.Tools.Services;

namespace SistemaReservaEnLinea.Tools
{
    public class ImagenesHelper
    {

        public static async Task<string> UploadAsync(string path, IFormFile file, int idUser, IEmailSender _emailSender)
        {
            string strFile = "";
            try
            {
                if (file != null && file.Length > 0)
                {
                    strFile = Generics.NameFile() + "_U_" + idUser.ToString() + System.IO.Path.GetExtension(file.FileName);
                    using (var stream = new FileStream(Path.Combine(path, strFile), FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                await _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error", ex.ToString());
                strFile = "";
            }

            return strFile;
        }


        public static async Task<bool> ResizeSaveImage(string pathSource, string pathDestino, uint width, uint height, IEmailSender _emailSender)
        {
            try
            {
                byte[] image = System.IO.File.ReadAllBytes(pathSource);
                image = await ResizeImageBytes(image, width, height, _emailSender);
                System.IO.File.WriteAllBytes(pathDestino, image);
            }
            catch (Exception ex)
            {
                await _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error",ex.ToString());
                return false;
            }
            return true;
        }

        protected static async Task<byte[]> ResizeImageBytes(byte[] imageData, uint? desiredWidth, uint? desiredHeight, IEmailSender _emailSender)
        {
            try
            {
                using (var job = new FluentBuildJob())
                {
                    var res = await job.Decode(imageData).ConstrainWithin(desiredWidth, desiredHeight)
                        .EncodeToBytes(new LibJpegTurboEncoder()).Finish().InProcessAsync();
                    var bytes = res.First.TryGetBytes();
                    return bytes.HasValue ? bytes.Value.Array : new byte[] { };
                }
            }
            catch (Exception ex)
            {
                await _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error", ex.ToString());
                return null;
            }
        }

    }
}
