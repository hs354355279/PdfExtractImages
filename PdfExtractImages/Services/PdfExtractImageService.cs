using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace PdfExtractImages.Services
{
    internal class PdfExtractImageService
    {
        public string FilePath { get; set; }

        public string SaveFilePath { get; set; }

        public PdfExtractImageService(string FilePath_, string SaveFilePath_)
        {
            FilePath = FilePath_;
            SaveFilePath = SaveFilePath_;
        }

        public IEnumerable<int> ExtractFirstImagePerPage()
        {

            string FileName = System.IO.Path.GetFileNameWithoutExtension(FilePath);


            using (var reader = new PdfReader(FilePath))
            {
                int numPages = reader.NumberOfPages;

                for (int pageNum = 1; pageNum <= numPages; pageNum++)
                {
                    var strategy = new MyImageRenderListener(SaveFilePath, FileName, pageNum);
                    PdfContentStreamProcessor processor = new PdfContentStreamProcessor(strategy);
                    PdfDictionary pageDic = reader.GetPageN(pageNum);
                    PdfDictionary resourcesDic = pageDic.GetAsDict(PdfName.RESOURCES);
                    processor.ProcessContent(ContentByteUtils.GetContentBytesForPage(reader, pageNum), resourcesDic);
                    yield return pageNum; // 返回处理过的页码
                }
            }
        }
    }
    public class MyImageRenderListener : IRenderListener
    {
        private readonly string SaveFilePath;
        private readonly int PageNum;
        private string FileName { get; set; }
        private int ImageNum;
        public MyImageRenderListener(string SaveFilePath_, string FileName_, int PageNum_)
        {
            SaveFilePath = SaveFilePath_;
            PageNum = PageNum_;
            FileName = FileName_;
        }

        public async void RenderImage(ImageRenderInfo renderInfo)
        {
            ImageNum++;
            try
            {
                var imageObj = renderInfo.GetImage();
                if (imageObj == null) return;

                var imgBytes = imageObj.GetImageAsBytes();

                using var ms = new MemoryStream(imgBytes);
                try
                {
                    using var img = SixLabors.ImageSharp.Image.Load(ms); // ImageSharp 会抛异常
                    ms.Position = 0;
                    string ImageName = $"{FileName}_{PageNum}_{ImageNum}.png";

                    string imgPath = System.IO.Path.Combine(SaveFilePath, ImageName);

                    img.Save(imgPath);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void BeginTextBlock() { }
        public void EndTextBlock() { }
        public void RenderText(TextRenderInfo renderInfo) { }
    }
}
