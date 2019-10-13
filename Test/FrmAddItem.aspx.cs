using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


public partial class FrmAddItem : System.Web.UI.Page
{
    string cs = "server=localhost;database=AmurthaPharmacyDb;" +
                "userid=root;password=;";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (TxtBarcode.Text.Trim() == string.Empty || TxtItemName.Text.Trim() == string.Empty || TxtPrice.Text.Trim() == string.Empty)
        {
            if (TxtBarcode.Text.Trim() == string.Empty)
            {
                Response.Write("<script>alert('Barcode is empty');</script>");
            }


            else if (TxtItemName.Text.Trim() == string.Empty)
            {
                Response.Write("<script>alert('Item Name is empty');</script>");
            }

            else
            {
                Response.Write("<script>alert('Price is empty');</script>");
            }

        }

        else
        {
            MySqlConnection con = new MySqlConnection(cs);
            try
            {
                con.Open();

                string myCommand = "INSERT INTO `AmurthaPharmacyDb`.`TblMedicine` (`barcode`, `medicine`, `Price`) VALUES ('" + TxtBarcode.Text + "', '" + TxtItemName.Text + "', '" + 34 + "'); ";
                MySqlCommand cmd = new MySqlCommand(myCommand, con);
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Item Inserted');</script>");


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
   }// end of btn add
}   //end of class