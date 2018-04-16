Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Web.UI

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        ASPxGridView1.DataBind()
        If Not IsPostBack Then
            SetSelectedItems()
        End If
    End Sub
    Private Sub SetSelectedItems()
        Dim dv As DataView = DirectCast(SqlDataSource1.Select(DataSourceSelectArguments.Empty), DataView)
        Dim dt As DataTable = dv.ToTable()
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim selected As Boolean = dt.Rows(i).Field(Of Boolean)("ProductSelection")
            If selected Then
                ASPxGridView1.Selection.SetSelection(i, selected)
            End If
        Next i
    End Sub
    Private Function GetSelectedItemsFromDB() As List(Of Object)
        Dim currentIDs As New List(Of Object)()
        Dim dv As DataView = DirectCast(SqlDataSource1.Select(DataSourceSelectArguments.Empty), DataView)
        Dim dt As DataTable = dv.ToTable()
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim selected As Boolean = dt.Rows(i).Field(Of Boolean)("ProductSelection")
            If selected Then
                currentIDs.Add(dt.Rows(i).Field(Of Integer)("ProductID"))
            End If
        Next i
        Return currentIDs
    End Function
    Protected Sub ASPxGridView1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim selectedIDs As List(Of Object) = ASPxGridView1.GetSelectedFieldValues("ProductID")
        Dim lastSelectedIDs As List(Of Object) = GetSelectedItemsFromDB()
        CheckSelection(selectedIDs, lastSelectedIDs, "False")
        CheckSelection(lastSelectedIDs, selectedIDs, "True")
    End Sub
    Private Sub CheckSelection(ByVal checkedCollection As List(Of Object), ByVal collectionToCheck As List(Of Object), ByVal updateParameter As String)
        For i As Integer = 0 To collectionToCheck.Count - 1
            Dim currentID = collectionToCheck(i)
            If Not checkedCollection.Contains(currentID) Then
                UpdateDB(updateParameter, currentID.ToString())
            End If
        Next i
    End Sub
    Private Sub UpdateDB(ByVal status As String, ByVal id As String)
        SqlDataSource1.UpdateParameters("ProductSelection").DefaultValue = status
        SqlDataSource1.UpdateParameters("ProductID").DefaultValue = id
        'SqlDataSource1.Update(); uncomment to update DB
    End Sub
End Class