using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Diagram_WinProg2016.Commands
{
    class SavePngCommand
    {
        private Canvas screen;
        public SavePngCommand(Canvas input)
        {
            // get canvas from input where everything is located
            screen = input;

            // Create a render bitmap and push the surface to it
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)input.ActualWidth, (int)input.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            renderBitmap.Render(screen);

            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog(); //initialize dialog
            dlg.FileName = "ScreenPrint"; // Default file name
            dlg.DefaultExt = ".png"; // Deafault file extension

            // Show savefile dialog box
            Nullable<bool> result = dlg.ShowDialog();
            string filename = null;

            // Process save file dialog box results
            if (result == true)
            {
                // get path and filename
                filename = dlg.FileName;
                string ext = Path.GetExtension(filename);

                // saving the pdf
                using (FileStream outStream = new FileStream(filename, FileMode.Create))
                {
                    switch (ext.ToLower())
                    {
                        case ".png":
                            // Use pdf encoder for our data
                            PngBitmapEncoder encoderPng = new PngBitmapEncoder();
                            // push the rendered bitmap to it
                            encoderPng.Frames.Add(BitmapFrame.Create(renderBitmap));
                            // save the data to the stream
                            encoderPng.Save(outStream);
                            break;
                        default:
                            MessageBox.Show("Not a supported file extension.");
                            break;
                    }
                }
            }
        }
    }
}
