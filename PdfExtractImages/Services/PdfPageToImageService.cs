using Docnet.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Docnet.Core.Models;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace PdfExtractImages.Services
{
    internal class PdfPageToImageService
    {
        public string FilePath { get; set; }
        public PdfPageToImageService(string FilePath_)
        {

            FilePath = FilePath_;

        }

    
        /// <summary>
        /// 获取图片数量（页面数量）
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetPdfPageCount()
        {
            var pdfBytes = await File.ReadAllBytesAsync(FilePath);

            var DocReader = DocLib.Instance.GetDocReader(pdfBytes, new PageDimensions(1080, 1920));

            return DocReader.GetPageCount();

        }
        /// <summary>
        /// 读取图片
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<Stream> ReadPdfPagesImages()
        {
            var pdfBytes = await File.ReadAllBytesAsync(FilePath);
            var DocReader = DocLib.Instance.GetDocReader(pdfBytes, new PageDimensions(1080, 1920));

            for (int i = 0; i < DocReader.GetPageCount(); i++)
            {
                using var PageReader = DocReader.GetPageReader(i);
                var rawImageData = PageReader.GetImage(flags: RenderFlags.RenderAnnotations | RenderFlags.RenderForPrinting);

                int W = PageReader.GetPageWidth();
                int H = PageReader.GetPageHeight();

                using var tempBmp = new Bitmap(W, H, PixelFormat.Format32bppArgb);
                var bmpData = tempBmp.LockBits(new Rectangle(0, 0, W, H), ImageLockMode.WriteOnly, tempBmp.PixelFormat);
                Marshal.Copy(rawImageData, 0, bmpData.Scan0, rawImageData.Length);
                tempBmp.UnlockBits(bmpData);


                using var finalBmp = new Bitmap(W, H, PixelFormat.Format24bppRgb);
                using (var g = Graphics.FromImage(finalBmp))
                {
                    g.Clear(Color.White);
                    g.DrawImageUnscaled(tempBmp, 0, 0);
                }

                var ImageStream = new MemoryStream();
                finalBmp.Save(ImageStream, ImageFormat.Png);
                ImageStream.Position = 0;

                yield return ImageStream;
            }
        }


    }
}
