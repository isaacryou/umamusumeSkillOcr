/*
 * Line of codes to perform OCR
 * Uses Windows OCR tool to perform OCR
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Drawing;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using System.IO;


namespace UmamusumeSkillOCR
{
    class OCR
    {
        private readonly OcrEngine ocrEngine = OcrEngine.TryCreateFromLanguage(new Windows.Globalization.Language("ja"));

        private readonly OcrEngine ocrEngineEnglish = OcrEngine.TryCreateFromLanguage(new Windows.Globalization.Language("en"));

        public async Task<string> ExtractTextAsync(Bitmap bitmap)
        {
            var text = new StringBuilder();
            using (var stream = new Windows.Storage.Streams.InMemoryRandomAccessStream())
            {
                bitmap.Save(stream.AsStream(), ImageFormat.Bmp);
                var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);
                var softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                var ocrResult = await ocrEngine.RecognizeAsync(softwareBitmap);
                var ocrResultEnglish = await ocrEngineEnglish.RecognizeAsync(softwareBitmap);

                foreach (var line in ocrResult.Lines)
                {
                    text.AppendLine(line.Text);
                }

                foreach (var line in ocrResultEnglish.Lines)
                {
                    text.AppendLine(line.Text);
                }
            }
            
            return text.ToString();
        }
    }
}
