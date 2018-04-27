using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;

namespace door_reader
{
    public partial class door : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void button1_Click(object sender, EventArgs e)  
        {
            string st = @"server=.;database=db;User ID=****;Password=****;Trusted_Connection=false;";
            SqlConnection con = new SqlConnection(st);
            con.Open();

            string sql = @"exec sp;";  
            DataSet dt = new DataSet();         
            SqlDataAdapter da = new SqlDataAdapter(sql, con);          
            da.Fill(dt);          

            StringBuilder str = new StringBuilder();  //dataset 轉 stringbuilder             
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {

                if (dt.Tables[0].Rows[i][0].ToString() == string.Empty || dt.Tables[0].Rows[i][0].ToString() == null || Convert.ToDateTime(dt.Tables[0].Rows[i][0].ToString()) > DateTime.Now)
                {
                    str.Append("[A]" + "\t");
                }
                else
                {
                    str.Append("[D]" + "\t");
                }             

                str.Append(dt.Tables[0].Rows[i][2].ToString().Trim() + dt.Tables[0].Rows[i][3].ToString().Trim() + "\t"); 
                str.Append(dt.Tables[0].Rows[i][4].ToString().Trim() + "\t"); 
                str.Append(dt.Tables[0].Rows[i][5].ToString().Trim() + "\t"); 
                str.Append(dt.Tables[0].Rows[i][6].ToString().Trim() + "\t");

                if (dt.Tables[0].Rows[i][0].ToString() == string.Empty || dt.Tables[0].Rows[i][6].ToString() == null)
                {
                    str.Append(dt.Tables[0].Rows[i][0].ToString().Trim() + "\t");
                }
                else
                {
                    DateTime date2 = Convert.ToDateTime(dt.Tables[0].Rows[i][0].ToString()); //轉換有效日期格式使用： ex.2015.01.01
                    str.Append(date2.ToString("yyyy.MM.dd") + "\t"); 
                }             
                 
                str.Append(dt.Tables[0].Rows[i][2].ToString().Trim() + "\t"); 
                str.Append("\t");
                str.Append(dt.Tables[0].Rows[i][7].ToString().Trim() + "\t"); 
                str.Append(dt.Tables[0].Rows[i][8].ToString().Trim() + "\t"); 
                str.Append(dt.Tables[0].Rows[i][9].ToString().Trim() + "\t"); 
                str.Append("\t"); 
                str.Append(dt.Tables[0].Rows[i][10].ToString().Trim() + "\t"); 
                str.Append(dt.Tables[0].Rows[i][11].ToString().Trim() + "\t");
                str.Append("\t");
                str.Append(dt.Tables[0].Rows[i][12].ToString().Trim() + "\r\n");
                
            }


            /////  寫入檔案用
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            string file2 = string.Format(@"c:\\door\\" + time +".txt");  //2.修改後要寫入的檔案
            StreamWriter sw = new StreamWriter(file2, false, Encoding.GetEncoding("BIG5"));
            sw.Write(str);
            sw.Flush();
            sw.Close();

            Response.Write("<script>alert('檔案已匯出，開始執行同步!'); </script>");


        }

       
    }
}
