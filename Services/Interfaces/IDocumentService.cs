namespace RecruitmentServer.Services.Interfaces
{
    public interface IDocumentService
    {
        string SavePdfAsTmpImage(string filePath);

        string ExtractCandidateFace(string filePath);

        string ExtractTextFromNonReadablePdf(string filePath);

        string ExtractTextFromPdf(string filepath);

       Task<string> SaveTmpPDF(IFormFile file);
    }
}
