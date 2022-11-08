using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Configuration;

namespace CRUD_Estagio
{
    public partial class Form1 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public void ShowDataOnGridView()
        {
            using(con = new SqlConnection(cs))
            {
                adapter = new SqlDataAdapter("Select * from CLIENT", con);
                dt = new DataTable();
                adapter.Fill(dt);
                dgViewClient.DataSource = dt;
            }
        }
        public void ClearAllData()
        {
            txtNome.Text = "";
            txtEmail.Text = "";
            txtTelefone.Text = "";
            txtCPF.Text = "";
        }

        public Form1()
        {
            InitializeComponent();
            ShowDataOnGridView();
        }

        private void dgViewClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNome.Text = this.dgViewClient.CurrentRow.Cells["Nome"].Value.ToString();
            txtEmail.Text = this.dgViewClient.CurrentRow.Cells["Email"].Value.ToString();
            txtTelefone.Text = this.dgViewClient.CurrentRow.Cells["Telefone"].Value.ToString();
            txtCPF.Text = this.dgViewClient.CurrentRow.Cells["CPF"].Value.ToString();

            lblCID.Text = this.dgViewClient.CurrentRow.Cells["ClientID"].Value.ToString();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using(con = new SqlConnection(cs))
            {
                con.Open();
                cmd = new SqlCommand("Insert Into CLIENT (Nome , Email , Telefone , CPF) Values (@nome , @email , @telefone , @CPF)", con);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                cmd.Parameters.AddWithValue("@CPF", txtCPF.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Dados inseridos com sucesso!");
                ShowDataOnGridView();
                ClearAllData();
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(cs))
            {
                con.Open();
                cmd = new SqlCommand("Update CLIENT Set Nome = @nome , Email = @email , Telefone = @telefone , CPF = @cpf " +
                 "Where ClientID = @clientID", con);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                cmd.Parameters.AddWithValue("@CPF", txtCPF.Text);
                cmd.Parameters.AddWithValue("@clientID", lblCID.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Dados atualizados com sucesso!");
                ShowDataOnGridView();
                ClearAllData();
            }
           
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            using(con = new SqlConnection(cs))
            {
                con.Open();
                cmd = new SqlCommand("Delete from CLIENT Where ClientID = @clientID", con);
                cmd.Parameters.AddWithValue("@clientID", lblCID.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Dados excluidos com sucesso!");
                ShowDataOnGridView();
                ClearAllData();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }
    }
}
