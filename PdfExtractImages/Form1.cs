using PdfExtractImages.Services;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace PdfExtractImages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string FilePath { get; set; }

        private string SaveFilePath { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();

            ofd.Title = "请选择PDF文件";
            ofd.Filter = "PDF 文件 (*.pdf)|*.pdf";
            ofd.Multiselect = false;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FilePath = ofd.FileName;
                label2.Text = ofd.FileName;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();

            fbd.Description = "请选择保存位置";
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                SaveFilePath = fbd.SelectedPath;
                label1.Text = fbd.SelectedPath;

            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            string FileName = Path.GetFileNameWithoutExtension(FilePath);

            PdfImageExtractorService pdfImageExtractorService = new PdfImageExtractorService(FilePath);

            int ImgCount = await pdfImageExtractorService.GetPdfPageCount();

            listBox1.Items.Clear();
            listBox1.Items.Add($"加载文件:{FileName}");
            listBox1.Items.Add($"页面个数:{ImgCount}");

            await Task.Run(async () =>
            {
                int Index = 1;
                await foreach (var item in pdfImageExtractorService.ReadPdfPagesImages())
                {
                    string imgName = $"{FileName}_{Index}.png";

                    string imgPath = Path.Combine(SaveFilePath, imgName);

                    using var fs = File.Create(imgPath);

                    await item.CopyToAsync(fs);
                    item.Dispose();
                    this.BeginInvoke(() =>
                    {
                        listBox1.Items.Add($"处理完成第{Index}页");
                        listBox1.TopIndex = listBox1.Items.Count - 1;
                        Index++;
                    });
                   
                }
            });

            listBox1.Items.Add("处理完成");
            listBox1.TopIndex = listBox1.Items.Count - 1;

            button1.Enabled = true;
        }
    }
}
