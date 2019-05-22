using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Laba
{
    class Program
    {  
        static void Main(string[] args)
        {      
            Console.WriteLine("Введите '1' - переименование; '2' - добавление отметки; '3' - сортировка:");
            string answer = Console.ReadLine();

            Console.WriteLine("Путь к папке:");
            string path = Console.ReadLine();
            //string path = @"D:\WG\PVT_C#\Laba_files\";
            DirectoryInfo dir1 = new DirectoryInfo(path);

            DirectoryInfo dir2 = new DirectoryInfo(path + new DirectoryInfo(path).Name + "_" + answer);
            dir2.Create();

            string date, year;

            foreach (FileInfo photo in dir1.GetFiles("*.jpg*"))
            {
                if (answer == "1")
                {
                    date = photo.LastWriteTime.ToShortDateString();
                    File.Copy(photo.FullName, dir2 + "\\" + date + "_" + photo.Name);
                }

                if (answer == "2")
                {
                    date = photo.LastWriteTime.ToShortDateString();
                    Image img = Image.FromFile(photo.FullName); 
                    Graphics g = Graphics.FromImage(img);

                    g.DrawString(date, new Font("Arial", 30, FontStyle.Bold),
                    new SolidBrush(Color.Red),  img.Width - 160.0F, 0);

                    img.Save(dir2 + "\\" + photo.Name, System.Drawing.Imaging.ImageFormat.Jpeg); 
                    g = null;
                    img = null;
                }
              
                if (answer == "3")
                {
                    year = photo.LastWriteTime.Year.ToString();

                    DirectoryInfo dir3 = new DirectoryInfo(dir2 + "\\" + year);
                    if (dir3.Exists == false)
                    { 
                        dir3.Create();         
                    }
                    
                    File.Copy(photo.FullName, dir2 + "\\" + year + "\\" + photo.Name);
                }
            }

            Console.ReadKey();
            Directory.Delete(Convert.ToString(dir2), true);
        }
    }
}