using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace WindowsFormsApp1
{
    class DD_Sql
    {

        SqlConnection cnn;
        SqlDataAdapter adapt;
        SqlCommand cmd;

        public void ConnSQL()
        {
            string connetionString;
            bool sqlconnState;
             connetionString = "Data Source=AW-PRODTS\\WINCCPLUSMIG2014;Initial Catalog=WittEyE;User ID=sa;Password=demo123-";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            if (cnn.State == System.Data.ConnectionState.Open)
            {
                sqlconnState = true;
            }
           
        }

    
        public void InsertSQLDatei(string EB_Numm, string IBC_Artnumm, int IBC_RestInh, string IBC_Sernum)
        {

            // string connetionString;
            // SqlConnection cnn;

            ConnSQL();

            if (EB_Numm != "" && IBC_Artnumm != "" && IBC_Sernum !="")
            {
                cmd = new SqlCommand("insert into IBC_EB(EB,IBC_ArtikelNummer,IBC_Restinhalt,IBC_Seriennummer) values(@EB,@IBC_ArtikelNummer,@IBC_Restinhalt,@IBC_Seriennummer)", cnn);
           //     cnn.Open();
                cmd.Parameters.AddWithValue("@EB",EB_Numm);
                cmd.Parameters.AddWithValue("@IBC_ArtikelNummer", IBC_Artnumm);
                cmd.Parameters.AddWithValue("@IBC_Restinhalt", IBC_RestInh);
                cmd.Parameters.AddWithValue("@IBC_Seriennummer", IBC_Sernum);

                cmd.ExecuteNonQuery();
               // cnn.Close();
              //  MessageBox.Show("Record Inserted Successfully");

            }
            else
            {
              //  MessageBox.Show("Please Provide Details!");
            }
            cnn.Close();
        }
    
        public DataTable DisplaySQLDatai()
        {
            ConnSQL();
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            adapt = new SqlDataAdapter("select * from IBC_EB", cnn);
            adapt.Fill(dt);
            adapt.Fill(ds);
            // iBC_EBDataGridView.DataSource = dt;
            cnn.Close();

            return dt;


           // return ds;
        }

        public DataSet DisplaySQLDataitoDS()
        {
            ConnSQL();
            DataSet ds = new DataSet();
            adapt = new SqlDataAdapter("select * from IBC_EB", cnn);
            adapt.Fill(ds);
            // iBC_EBDataGridView.DataSource = dt;
            cnn.Close();
            return ds;

            // return ds;
        }


        public void DeleteItem(int ID)
        {
            ConnSQL();

            if (ID != 0)
            {
                cmd = new SqlCommand("delete IBC_EB where ID=@id", cnn);
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
              //  cnn.Close();
                MessageBox.Show("Record Deleted Successfully!");
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
            cnn.Close();

        }
        /*
                public void ClearData()
                {
                    txt_Name.Text = "";
                    txt_State.Text = "";
                    ID = 0;
                }




            }



            private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
            {
               
                txt_Name.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_State.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            */
    }
}
