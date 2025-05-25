using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace etut_merkezi_uygulamasi
{
    class cls_sqlconn
    {
        public SqlConnection baglan()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-M8R9N1A\\SQLEXPRESS;Initial Catalog=etut_merkezi;Integrated Security=True;");
            conn.Open();
            return conn;
        }
        public static Image yuksekkalite(Image image, Size yeniboyut)
        {
            Bitmap yeniresim = new Bitmap(yeniboyut.Width, yeniboyut.Height);
            using (Graphics g = Graphics.FromImage(yeniresim))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(image, 0, 0, yeniboyut.Width, yeniboyut.Height);
            }
            return yeniresim;
        }
        
    }
}
