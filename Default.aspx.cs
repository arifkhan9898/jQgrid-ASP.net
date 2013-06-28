using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Json;
using System.Data.SqlClient;


public partial class _Default : System.Web.UI.Page 
{
	public string constr = @"Server=.\SQLExpress;Integrated Security=true;AttachDbFilename=|DataDirectory|oc.mdf;User Instance=true;";
	public SqlConnection conn;
	
    
    protected void Page_Load(object sender, EventArgs e)
    {
		
		if (Request.QueryString["cmd"] == "select")
		{
			int page = int.Parse (Request.QueryString["page"]);
			int limit = int.Parse(Request.QueryString["rows"]);
			// Load data from database.
			LoadTableData(page, limit, Request.QueryString["sidx"], Request.QueryString["sord"]);
			
		}
		if (Request.QueryString["cmd"] == "delete")
		{
			// Delete operation is here.
			DeleteData();
		}
    }
	
	void DeleteData()
	{
		string id = Request["id"].ToString().Trim();

		try
		{
			string msg = "This is demo.";
			
			JsonObjectCollection data = new JsonObjectCollection();
			data.Add(new JsonStringValue("result", "True"));
			data.Add(new JsonStringValue("msg", msg));
			JsonUtility.GenerateIndentedJsonText = false;
			System.Web.HttpContext.Current.Response.Write(data.ToString());
			System.Web.HttpContext.Current.Response.End();

		}
		catch (Exception e){}
	}
	void LoadTableData(int page, int limit, string sidx , string sord )
	{
		try { 
			if(sidx == "")  sidx = "id";
			if(sord == "") sord = "Asc";
			
			int totaldata = 0;
			int total_pages = 0;
			int start = 0;
			
			// Provide response in JSON format.			
			JsonObjectCollection rows = new JsonObjectCollection();			
			
			conn = new SqlConnection(constr);
            conn.Open();
			SqlCommand _cmd = conn.CreateCommand();
			
			_cmd.CommandText = "SELECT count(*) as totals FROM tbl_user_mst ";
			SqlDataReader _Reader = _cmd.ExecuteReader();

			while (_Reader.Read()){
				totaldata = Convert.ToInt32(_Reader["totals"]);
			}
			_Reader.Close();
			if (totaldata > 0)
			{
				total_pages = totaldata / limit;
				if ((double)total_pages < ((double)totaldata / (double)limit))  total_pages++;
				if (page > total_pages)  page = total_pages;
			}
			start = (limit * (page - 1));
			
			// Implement paging in query.
            _cmd.CommandText = "SELECT TOP " + limit.ToString() + " * FROM tbl_user_mst WHERE id NOT IN (SELECT TOP " + start.ToString() + " id FROM tbl_user_mst ORDER BY " + sidx + " " + sord + ") ORDER BY " + sidx + " " + sord;
			_Reader = _cmd.ExecuteReader();
			
			while (_Reader.Read())
			{
				JsonObjectCollection collection = new JsonObjectCollection();

				collection.Add(new JsonStringValue("id", _Reader["id"].ToString()));
				collection.Add(new JsonStringValue("username", _Reader["username"].ToString()));
				collection.Add(new JsonStringValue("photo", _Reader["photo"].ToString()));
				collection.Add(new JsonStringValue("comment", _Reader["comment"].ToString()));
				
				rows.Add(new JsonObjectCollection(collection));

			}
			
			// Provide response in JSON format.	
			JsonObjectCollection data = new JsonObjectCollection();
			data.Add(new JsonStringValue("page",  page.ToString()));
			data.Add(new JsonStringValue("total", total_pages.ToString()));
			data.Add(new JsonStringValue("records", totaldata.ToString()));
			data.Add(new JsonArrayCollection("rows", rows));
			JsonUtility.GenerateIndentedJsonText = false;
	

			Response.Write(data.ToString());
		}
		catch (Exception ex)
		{
			Response.Write("{\"Source\":\"Error\",\"Message\":\""+ ex.Message + "\"}"); 
		}
		
		// End of response.
		Response.End();

	}
	
	
}
