using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;

using System.Drawing;

using System.Text;
using System.Threading.Tasks;

public partial class FrmChangePrice : System.Web.UI.Page
{
    string cs = "server=localhost;database=AmurthaPharmacyDb;" +
                "userid=root;password=;";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void TxtBarcode_TextChanged(object sender, EventArgs e)
    {
        MySqlConnection con = new MySqlConnection(cs);
        con.Open();


        MySqlCommand cmd = new MySqlCommand();

        cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;

        cmd.CommandText = "select * From TblMedicine where Barcode='" + TxtBarcode.Text.ToString() + "'";
        cmd.ExecuteNonQuery();

        DataTable dt = new DataTable();
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);

        da.Fill(dt);

        foreach (DataRow dr in dt.Rows)
        {

            TxtItem.Text = (dr["Medicine"].ToString());
            TxtPrice.Text = (dr["Price"].ToString());
        }

        
       

    }
    protected void BtnChange_Click(object sender, EventArgs e)
    {
        MySqlConnection con = new MySqlConnection(cs);
        try
        {
            con.Open();

            string myCommand = "Update TblMedicine SET Price='"+TxtNewPrice .Text +"' where  Barcode='" + TxtBarcode .Text .ToString()+ "' ";
            MySqlCommand cmd = new MySqlCommand(myCommand, con);
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Item Price Updated');</script>");


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

        }

        finally
        {
            con.Close();
        }
    }
}