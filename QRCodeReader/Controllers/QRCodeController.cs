using System;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.IO;
using QRCoder;

namespace QRCodeReader.Controllers
{
    
    [ApiController]
    [Route("QRCode/GetQRCodeFor")]
    public class QRCodeController : ControllerBase
    {
        

        [HttpGet]
        
        public Byte[] Get(string FileName)
        {
            string qrText = ReadFile(FileName);
            try
            {


                if (qrText != null)
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();

                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText,
                    QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20);
                    qrCodeImage.Save("test", System.Drawing.Imaging.ImageFormat.Png);


                    return BitmapToBytes(qrCodeImage);
                }
                return null;
            }
            catch(Exception e)
            {
                throw;
            }


        }
        private string ReadFile( string fileName)
        {
            try
            {   // Open the text file using a stream reader.
                string data;
                fileName = "text.txt";
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a str ing, and write the string to the console.
                     data = sr.ReadToEnd();
                    
                }
                return data;
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);return null;
            }
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
               
                return stream.ToArray();
            }
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
