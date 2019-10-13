using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Threading.Tasks;

public partial class FrmDeleteItem : System.Web.UI.Page
{
    string cs = "server=localhost;database=AmurthaPharmacyDb;" +
                "userid=root;password=;";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void TxtBacode_TextChanged(object sender, EventArgs e)
    {
        MySqlConnection con = new MySqlConnection(cs);
        con.Open();


        MySqlCommand cmd = new MySqlCommand();

        cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;

        cmd.CommandText = "select * From TblMedicine where Barcode='" + TxtBacode.Text.ToString() + "'";
        cmd.ExecuteNonQuery();

        DataTable dt = new DataTable();
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);

        da.Fill(dt);

        foreach (DataRow dr in dt.Rows)
        {

            TxtItemName.Text = (dr["Medicine"].ToString());
            TxtPrice.Text = (dr["Price"].ToString());
        }
    }


    protected void BtnDelete_Click(object sender, System.EventArgs e)
    {
        MySqlConnection con = new MySqlConnection(cs);
        try
        {
            con.Open();

            string myCommand = "Delete From TblMedicine where  Barcode='" + TxtBacode.Text.ToString() + "' ";
            MySqlCommand cmd = new MySqlCommand(myCommand, con);
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Item Deleted');</script>");


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