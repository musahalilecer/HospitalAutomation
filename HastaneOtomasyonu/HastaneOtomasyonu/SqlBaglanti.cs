using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HastaneOtomasyonu
{
    internal class SqlBaglanti
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-VICMA4R\\SQLEXPRESS;Initial Catalog=HastaneProjesi;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
