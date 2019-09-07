using System;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace Neuronus
{
    public partial class MainWindow : Window
    {
        public MainWindow() { InitializeComponent(); }

        Point p1, p2;
        void Canvas_MouseMove(object sender, MouseEventArgs e)
        {//Рисует только тогда, когда нажата ЛКМ
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                p1 = e.GetPosition(canvas.Children[0]);
                canvas.Children.Add(new Line { X1 = p1.X, Y1 = p1.Y, X2 = noDraw ? p1.X : p2.X, Y2 = noDraw ? p1.Y : p2.Y, StrokeStartLineCap = PenLineCap.Round, StrokeEndLineCap = PenLineCap.Round, StrokeThickness = 4, Stroke = Brushes.Black });
                p2 = p1;
                noDraw = false;
            }
        }

        bool noDraw = true;
        void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => noDraw = true;//Отпустили ЛКМ - перестаём рисовать

        void FindeInMemory_Click(object sender, RoutedEventArgs e)
        {//Нажата кнопка распознать
            if (SelectTypeRecognitiong.SelectedIndex == 0)
            {//Через файл
                if (selectedFile.Length == 0) { MessageBox.Show("Укажите файл для распознавания"); return; }

            }
            else if (SelectTypeRecognitiong.SelectedIndex == 1)
            {//Рисуем
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)canvas.RenderSize.Width, (int)canvas.RenderSize.Height, 96d, 96d, PixelFormats.Default);
                rtb.Render(canvas);

                var crop = new CroppedBitmap(rtb, new Int32Rect(0, 0, (int)canvas.Width, (int)canvas.Height));

                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(crop));

                using (var fs = System.IO.File.OpenWrite("file.png")) { pngEncoder.Save(fs); }


            }
        }
        string selectedFile = "";
        void SelectFile_Click(object sender, RoutedEventArgs e)
        {//Выбрать картинку для распознавания
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            { filePath.Content = selectedFile = ofd.FileName; }
        }

        void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e) { while (canvas.Children.Count > 1) canvas.Children.RemoveAt(canvas.Children.Count - 1); }//Очищает поле для рисования
    }
}