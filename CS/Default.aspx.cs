using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

public partial class _Default : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		ASPxGridView1.DataBind();
		if(!IsPostBack) {
			SetSelectedItems();
		}
	}
	void SetSelectedItems() {
		DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
		DataTable dt = dv.ToTable();
		for(int i = 0; i < dt.Rows.Count; i++) {
			bool selected = dt.Rows[i].Field<bool>("ProductSelection");
			if(selected)
				ASPxGridView1.Selection.SetSelection(i, selected);
		}
	}
	List<object> GetSelectedItemsFromDB() {
		List<object> currentIDs = new List<object>();
		DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
		DataTable dt = dv.ToTable();
		for(int i = 0; i < dt.Rows.Count; i++) {
			bool selected = dt.Rows[i].Field<bool>("ProductSelection");
			if(selected)
				currentIDs.Add(dt.Rows[i].Field<int>("ProductID"));
		}
		return currentIDs;
	}
	protected void ASPxGridView1_SelectionChanged(object sender, EventArgs e) {
		List<object> selectedIDs = ASPxGridView1.GetSelectedFieldValues("ProductID");
		List<object> lastSelectedIDs = GetSelectedItemsFromDB();
		CheckSelection(selectedIDs, lastSelectedIDs, "False");
		CheckSelection(lastSelectedIDs, selectedIDs, "True");
	}
	void CheckSelection(List<object> checkedCollection, List<object> collectionToCheck, string updateParameter) {
		for(int i = 0; i < collectionToCheck.Count; i++) {
			var currentID = collectionToCheck[i];
			if(!checkedCollection.Contains(currentID))
				UpdateDB(updateParameter, currentID.ToString());
		}
	}
	void UpdateDB(string status, string id) {
		SqlDataSource1.UpdateParameters["ProductSelection"].DefaultValue = status;
		SqlDataSource1.UpdateParameters["ProductID"].DefaultValue = id;
		//SqlDataSource1.Update(); uncomment to update DB
	}
}