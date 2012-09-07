using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MongoDBDemo.Code;
using MongoDBDemo.Entity;

namespace MongoDBDemo
{
    public partial class frmMongoDBDemo : Form
    {
        private DBUtility _objDBUtility;

        public frmMongoDBDemo()
        {
            InitializeComponent();

            _objDBUtility = new DBUtility();
        }

        #region Events

        /// <summary>
        /// Gets triggered when the form gets loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMongoDBDemo_Load(object sender, EventArgs e)
        {
            bindDBData();
        }

        /// <summary>
        /// Gets triggered when the user changes the selection of database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drpDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDBName.Text = drpDB.SelectedItem.ToString();
            bindTableData();
        }

        /// <summary>
        /// Gets triggered when the user changes the table 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drpTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTable.Text = drpTable.SelectedItem.ToString();
            bindData();
        }
        
        /// <summary>
        /// Gets triggered when the user selects the new DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtDBName.Clear();
            txtDBName.Visible = true;
            drpDB.Visible = false;
            txtDBName.Focus();
            radioButton3.Checked = true;
        }

        /// <summary>
        /// Gets triggered when the user selects the existing DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtDBName.Visible = false;
            drpDB.Visible = true;
            radioButton4.Checked = true;
            bindDBData();
        }

        /// <summary>
        /// Gets triggered when the user selects new Table 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            txtTable.Clear();
            txtTable.Visible = true;
            drpTable.Visible = false;
            txtTable.Focus();
        }

        /// <summary>
        /// Gets triggered when the user selects existing table option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            txtTable.Visible = false;
            drpTable.Visible = true;
            bindTableData();
        }

        /// <summary>
        /// Gets triggered when the user clicks on the add data button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            addData();            
            bindData();

            txtFirstName.Clear();
            txtLastName.Clear();
        }

        #endregion


        #region Private Methods


        /// <summary>
        /// Binds the Database list
        /// </summary>
        private void bindDBData()
        {
            drpDB.DataSource = _objDBUtility.getDataBaseNames();

            if (drpDB.SelectedItem != null)
                txtDBName.Text = drpDB.SelectedItem.ToString();
        }

        /// <summary>
        /// Binds the list of table for the selected database
        /// </summary>
        private void bindTableData()
        {
            if(txtDBName.Text != String.Empty)
                drpTable.DataSource = _objDBUtility.getTableNames(txtDBName.Text);

            if (drpTable.SelectedItem != null)
                txtTable.Text = drpTable.SelectedItem.ToString();        
        }

        /// <summary>
        /// Binds the row of data for the selected table in the selected database
        /// </summary>
        private void bindData()
        {
            //Declarations
            String strDatabaseName = txtDBName.Text;
            String strTableName = txtTable.Text;

            //Set the data source
            gvData.DataSource = _objDBUtility.getData(strDatabaseName, strTableName);

        }

        /// <summary>
        /// Adds the data to the table
        /// </summary>
        private void addData()
        {
            //Declarations
            User objData = new User { FirstName = txtFirstName.Text, LastName = txtLastName.Text };

            if (!_objDBUtility.addData(txtDBName.Text, txtTable.Text, objData))
                MessageBox.Show("Unable to add!", frmMongoDBDemo.ActiveForm.Text);

        }


        #endregion

    }








}
