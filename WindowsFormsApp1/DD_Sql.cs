using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class DD_Sql
    {

        SqlConnection cnn;
        SqlDataAdapter adapt;
        SqlCommand cmd;

        public void ConnSQL(bool sqlconnState)
        {
            string connetionString;
           
            connetionString = "Data Source=AW-PRODTS\\WINCCPLUSMIG2014;Initial Catalog=WittEyE;User ID=sa;Password=demo123-";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            if (cnn.State == System.Data.ConnectionState.Open)
            {
                sqlconnState = true;
            }
        }

    
        public void InsertSQLDatei(string EB_Numm, string IBC_Artnumm, string IBC_RestInh, string IBC_Sernum)
        {
           
           // string connetionString;
           // SqlConnection cnn;
           
            if (EB_Numm != "" && IBC_Artnumm != "" && IBC_RestInh != "" && IBC_Sernum !="")
            {
                cmd = new SqlCommand("insert into IBC_EB(EB_Nummer,IBC_ArtikelNummer,IBC_Restinhalt,IBC_Seriennummer) values(@EB_Nummer,@IBC_ArtikelNummer,@IBC_Restinhalt)", cnn);
                cnn.Open();
                cmd.Parameters.AddWithValue("@EB_Nummer", eB_NummerTextBox.Text);
                cmd.Parameters.AddWithValue("@IBC_ArtikelNummer", iBC_ArtikelNummerTextBox.Text);


                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("Record Inserted Successfully");

            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }

        }
    /*
        public void DisplaySQLDatai()
        {
            string connetionString;
            SqlConnection cnn;
            SqlDataAdapter adapt;
            connetionString = "Data Source=AW-PRODTS\\WINCCPLUSMIG2014;Initial Catalog=WittEyE;User ID=sa;Password=demo123-";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from IBC_EB", cnn);
            adapt.Fill(dt);
            iBC_EBDataGridView.DataSource = dt;
            cnn.Close();

        }

        public void ClearData()
        {
            txt_Name.Text = "";
            txt_State.Text = "";
            ID = 0;
        }

        public void DeleteItem()
        {
            SqlCommand cmd;
            string connetionString;
            SqlConnection cnn;
            SqlDataAdapter adapt;
            connetionString = "Data Source=AW-PRODTS\\WINCCPLUSMIG2014;Initial Catalog=WittEyE;User ID=sa;Password=demo123-";
            cnn = new SqlConnection(connetionString);

            if (ID != 0)
            {
                cmd = new SqlCommand("delete IBC_EB where ID=@id", cnn);
                cnn.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("Record Deleted Successfully!");


            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
           
        }


    }


   
    private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
        txt_Name.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        txt_State.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
    }
    */
    }
}
