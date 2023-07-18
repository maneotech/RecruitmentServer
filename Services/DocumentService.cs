using IronOcr;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using RecruitmentServer.Services.Interfaces;
using System.Text;
using ImageMagick;
using Emgu.CV.Structure;
using Emgu.CV;
using iText.IO.Image;
using Microsoft.IdentityModel.Tokens;

namespace RecruitmentServer.Services
{
    public class DocumentService : IDocumentService
    {
        public string ExtractCandidateFace(string filePath)
        {
            //safe pdf as an image
            string faceImagePath = String.Empty;
            string tmpImagePath = SavePdfAsTmpImage(filePath);
            if (tmpImagePath.IsNullOrEmpty())
            {
                return String.Empty;
            }


            //extract face emgu
            var imageCV = new Image<Bgr, byte>(tmpImagePath);
            var faceCascade = new CascadeClassifier(Constants.ProcessingFaceFile);
            // Detect faces in the image
            var faces = faceCascade.DetectMultiScale(imageCV);
            if (faces.Length > 0)
            {
                var faceRect = faces[0];
                Image<Bgr, byte> faceImage = imageCV.Copy(faceRect);

                // Save the face image
                string faceDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.FacesFilesDirectory);
                string filename = $"{Guid.NewGuid()}.png";
                faceImagePath = Path.Combine(faceDirectory, filename);

                //create directory if not exist
                if (!Directory.Exists(faceDirectory))
                {
                    Directory.CreateDirectory(faceDirectory);
                }
                faceImage.Save(faceImagePath);
            }
            else
            {
                return String.Empty;
            }

            //delete tmp file
            if (File.Exists(tmpImagePath))
            {
                File.Delete(tmpImagePath);
            }

            return faceImagePath;
        }

        public string ExtractTextFromNonReadablePdf(string filePath)
        {
            StringBuilder text = new StringBuilder();

            IronTesseract ocr = new();
            var input = new OcrInput();
           
            input.AddPdf(filePath);
            OcrResult result = ocr.Read(input);
            text.Append(result.Text);
           

            return text.ToString();
        }
        public string ExtractTextFromPdf(string filepath)
        {

            StringBuilder text = new StringBuilder();

            var pdfDocument = new PdfDocument(new PdfReader(filepath));
            var strategy = new LocationTextExtractionStrategy();

            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
            {
                var page = pdfDocument.GetPage(i);
                text.Append(PdfTextExtractor.GetTextFromPage(page, strategy));
                //string processed = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
            }
            pdfDocument.Close();

            return text.ToString();
        }

        public string SavePdfAsTmpImage(string filePath)
        {
            string imagepath = String.Empty;

            // Settings the density to 300 dpi will create an image with a better quality
            var settings = new MagickReadSettings
            {
                Density = new Density(300)
            };

            MagickImageCollection images = new MagickImageCollection();
            images.Read(filePath, settings);


            if (images != null && images.Count > 0)
            {
                MagickImage? image = images.ElementAt(0) as MagickImage;
                if (image != null)
                {
                    string outputPath = Constants.PdfFilesTmpImgDirectory + "/" + Guid.NewGuid() + ".png";
                    image.Format = MagickFormat.Png;
                    image.Density = new Density(300, 300);

                    // Set the output file path and format

                    //create directory if not exist
                    if (!Directory.Exists(Constants.PdfFilesTmpImgDirectory))
                    {
                        Directory.CreateDirectory(Constants.PdfFilesTmpImgDirectory);
                    }

                    // Save the image
                    image.Write(outputPath);
                    imagepath = outputPath;
                }
            }

            return imagepath;
        }

        public async Task<string> SaveTmpPDF(IFormFile file)
        {
            // Generate a unique file name
            var fileName = $"{Guid.NewGuid()}.pdf";

            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.PdfFilesDirectory);
            // Define the file path to save on the server
            var filePath = Path.Combine(directoryPath, fileName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            // Save the file to the server
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return filePath;
        }
    }
}
