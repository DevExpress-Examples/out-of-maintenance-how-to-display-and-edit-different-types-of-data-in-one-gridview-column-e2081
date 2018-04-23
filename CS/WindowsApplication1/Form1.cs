using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        Type[] types = new Type[] { typeof(string), typeof(int), typeof(bool), typeof(DateTime) };
        DataTable tbl;
                private DataTable CreateTable(int RowCount)
        {
            tbl = new DataTable();
            tbl.Columns.Add("Object", typeof(object));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { null });
            return tbl;
        }
        

        public Form1()
        {
            InitializeComponent();
            gridControl1.DataSource = CreateTable(20);
            dataGridView1.DataSource = gridControl1.DataSource;
        }

        Type GetDataTypeByIndex(int rowHandle)
        {
            return types[rowHandle % types.Length];
        }

        RepositoryItem GetRIByIndex(int rowHandle)
        {
            switch (rowHandle % types.Length)
            {
                case 0: return repositoryItemTextEdit1;
                case 1: return repositoryItemSpinEdit1;
                case 2: return repositoryItemCheckEdit1;
                case 3: return repositoryItemDateEdit1;
                default: return repositoryItemTextEdit1;
            }
        }

        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = GetDataTypeByIndex(e.ListSourceRowIndex);
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column != colDataType)
                e.RepositoryItem = GetRIByIndex(e.RowHandle);
        }
    }
}