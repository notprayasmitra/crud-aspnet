using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace crud_aspnet
{
    public partial class _Default : Page
    {
        private string connStr = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearForm();
                BindGrid();
            }
        }

        // READ
        private void BindGrid()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                string query = "SELECT id, first_name, last_name, email, major FROM students";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvStudents.DataSource = dt;
                        gvStudents.DataBind();
                    }
                }
            }
        }

        // CREATE AND UPDATE
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query;

                if (string.IsNullOrEmpty(hfStudentId.Value))
                {
                    query = "INSERT INTO students (first_name, last_name, email, major) VALUES (@FirstName, @LastName, @Email, @Major)";
                }
                else
                {
                    query = "UPDATE students SET first_name=@FirstName, last_name=@LastName, email=@Email, major=@Major WHERE id=@Id";
                }

                using (MySqlCommand cmd = new MySqlCommand( query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Major", txtMajor.Text.Trim());

                    if (!string.IsNullOrEmpty(hfStudentId.Value))
                    {
                        cmd.Parameters.AddWithValue("@Id", hfStudentId.Value);
                    }

                    try
                    {
                        cmd.ExecuteNonQuery();

                        // If execution succeeds, clear form and refresh list
                        ClearForm();
                        BindGrid();
                    }
                    catch (MySqlException ex)
                    {
                        // Number 1062 is MySQL's specific code for duplicate entry
                        if (ex.Number == 1062)
                        {
                            lblStatus.Text = "Error: A student with this email address already exists!";
                        }
                        else
                        {
                            // For handling any other error messages
                            lblStatus.Text = "A database error occurred: " + ex.Message;
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            ClearForm();
            BindGrid();
        }

        // EDIT SELECTOR
        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(gvStudents.DataKeys[rowIndex].Value);
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    string query = "SELECT id, first_name, last_name, email, major FROM students WHERE id=@Id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                hfStudentId.Value = dt.Rows[0]["id"].ToString();
                                txtFirstName.Text = dt.Rows[0]["first_name"].ToString();
                                txtLastName.Text = dt.Rows[0]["last_name"].ToString();
                                txtEmail.Text = dt.Rows[0]["email"].ToString();
                                txtMajor.Text = dt.Rows[0]["major"].ToString();
                                btnSave.Text = "Update";
                            }
                        }
                    }
                }
            }
        }

        // DELETE
        protected void gvStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvStudents.DataKeys[e.RowIndex].Value);
            
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                string query = "DELETE FROM students WHERE id=@Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            hfStudentId.Value = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtMajor.Text = "";
            btnSave.Text = "Save";
        }
    }
}