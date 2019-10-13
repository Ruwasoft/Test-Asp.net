using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
/// <summary>

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Threading.Tasks;




public partial class FrmSearchItem : System.Web.UI.Page
{
    string cs = "server=localhost;database=AmurthaPharmacyDb;" +
                "userid=root;password=;";

    int rc = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    
    protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ButtonAddToCart_Click(object sender, EventArgs e)
    {
        
        string a=TxtItemName.Text .ToString();
        string b=TxtQty.Text.ToString();
        string  C=TxtPrice.Text.ToString();

        string row= a +"    "+b+"   "+C;

        ListBox1.Items.Add(row);

       
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
             rc = ListBox1.Items.Count;


            int linspace = 5; 

            int pre_page = 10;  

            int p_size = pre_page + linspace * rc; 
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = new PaperSize("resipt", 100, p_size);

            pd.PrintPage += new PrintPageEventHandler(this.pd_printpage);
            pd.Print();
        }
        catch (Exception ex)
        {
            Response.Write(ex .Message);
        }
    }

    //code for print

    private void pd_printpage(object sender, PrintPageEventArgs ec)
    {


        ec.Graphics.DrawString("Test Application   ".ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, 0, 100);
        ec.Graphics.DrawString("Phone : 077 243 9317".ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 2, 120);
        ec.Graphics.DrawString("-----------invoice----------------".ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 0, 140);
        ec.Graphics.DrawString("Date   : ".ToString() + DateTime.Now.ToString(), new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 0, 160);

        
        ec.Graphics.DrawString("item                      qty      Amount".ToString(), new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 0, 180);

        string p_font = "Arial"; // font 
        int p_f_size = 8; // font size

        int x = 5 + 180;
        int y = 0;

        StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);

        for (int i = 0; i <= rc - 1; i++)  
        {
            
            x = x + 10;
            ec.Graphics.DrawString(ListBox1.Items[i].ToString(), new Font(p_font, p_f_size, FontStyle.Regular), Brushes.Black, y + 5, x);
           
        }

        ec.Graphics.DrawString("Total     : 1200 ".ToString() , new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 0, x + 20);

        
        ec.Graphics.DrawString("Discount  : 200 ".ToString() , new Font("Arial", 9, FontStyle.Regular), Brushes.Red, 0, x + 35);
        ec.Graphics.DrawString("Paid      : 1000 ".ToString() , new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 0, x + 50);
        ec.Graphics.DrawString("Return    : 300".ToString(), new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 0, x + 65);

        ec.Graphics.DrawString("Thank you for coming...".ToString(), new Font("Arial", 10, FontStyle.Italic), Brushes.Black, 0, x + 80);

        ec.Graphics.DrawString("---------------------------------------".ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 0, x + 90);
        ec.Graphics.DrawString("Software by:".ToString(), new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 0, x + 100);
        ec.Graphics.DrawString("ruwasoft@gmail.com".ToString(), new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 0, x + 115);
        ec.Graphics.DrawString("077-24-39-317".ToString(), new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 0, x + 130);

    }


    protected void TxtBarcode_TextChanged(object sender, EventArgs e)
    {
       // Response.Write("Its working");
        
        MySqlConnection con=new MySqlConnection(cs );
        con .Open();
       

        MySqlCommand cmd =new MySqlCommand();
       
        cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;

        cmd.CommandText = "select * From TblMedicine where Barcode='" + TxtBarcode .Text.ToString() + "'";
        cmd.ExecuteNonQuery();

        DataTable dt=new DataTable();
        MySqlDataAdapter da=new MySqlDataAdapter(cmd );
        
        da.Fill(dt);

        foreach (DataRow dr in dt.Rows)
        {

            TxtItemName.Text=(dr["Medicine"].ToString());
            TxtPrice.Text = (dr["Price"].ToString()); 
        }

        TxtQty.Focus();
    }
}