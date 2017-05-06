using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maintain_Technicians
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void techniciansBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            try
            {
                this.techniciansBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.techSupport_DataDataSet);
            }
            catch (DBConcurrencyException)
            {
                MessageBox.Show("A concurrency error occurred. " +
                    "Some rows were not updated.", "Concurrency Exception");
                this.techniciansTableAdapter.Fill(this.techSupport_DataDataSet.Technicians);
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
                techniciansBindingSource.CancelEdit();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number + ": " + ex.Message, ex.GetType().ToString());
            }
            //catch(InvalidOperationException ex)
            //{
            //    MessageBox.Show(ex.Message, ex.GetType().ToString());
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'techSupport_DataDataSet.Technicians' table. You can move, or remove it, as needed.
            try
            {
                this.techniciansTableAdapter.Fill(this.techSupport_DataDataSet.Technicians);
            }
            catch (System.Data.SqlClient.SqlException ex)//would not work when I used SqlException as written in book.  Is this correct?
            {
                MessageBox.Show("Database error # " + ex.Number + ": " + ex.Message, ex.GetType().ToString());
            }
        }
        private void techniciansDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            int row = e.RowIndex + 1;
            string errormessage = "A data entry error has occured.\nRow: " + row + "\nError: " + e.Exception.Message;
            MessageBox.Show(errormessage, "Data Error");
        }
    }
}
