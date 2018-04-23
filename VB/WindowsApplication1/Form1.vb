Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Repository

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits Form
		Private types() As Type = { GetType(String), GetType(Integer), GetType(Boolean), GetType(DateTime) }
		Private tbl As DataTable
				Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			tbl = New DataTable()
			tbl.Columns.Add("Object", GetType(Object))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { Nothing })
			Next i
			Return tbl
				End Function


		Public Sub New()
			InitializeComponent()
			gridControl1.DataSource = CreateTable(20)
			dataGridView1.DataSource = gridControl1.DataSource
		End Sub

		Private Function GetDataTypeByIndex(ByVal rowHandle As Integer) As Type
			Return types(rowHandle Mod types.Length)
		End Function

		Private Function GetRIByIndex(ByVal rowHandle As Integer) As RepositoryItem
			Select Case rowHandle Mod types.Length
				Case 0
					Return repositoryItemTextEdit1
				Case 1
					Return repositoryItemSpinEdit1
				Case 2
					Return repositoryItemCheckEdit1
				Case 3
					Return repositoryItemDateEdit1
				Case Else
					Return repositoryItemTextEdit1
			End Select
		End Function

		Private Sub gridView1_CustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles gridView1.CustomUnboundColumnData
			If e.IsGetData Then
				e.Value = GetDataTypeByIndex(e.ListSourceRowIndex)
			End If
		End Sub

		Private Sub gridView1_CustomRowCellEdit(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles gridView1.CustomRowCellEdit
			If e.Column IsNot colDataType Then
				e.RepositoryItem = GetRIByIndex(e.RowHandle)
			End If
		End Sub
	End Class
End Namespace