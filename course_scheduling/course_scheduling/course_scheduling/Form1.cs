using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using System.Collections;

namespace course_scheduling
{
    public partial class Form1 : Form
    {
        string[] day = new string[6] { "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday" };
        string[] time = new string[8] { "08.30-09.30", "09.35-10.35", "10.40-11.40", "11.45-12.45", "01.10-02.10", "02.15-3.15", "03.20-04.20", "04.25-05.25" };
        MySqlConnection connection;
        MySqlDataReader mdr;
        MySqlCommand cmd;



        public Form1()
        {
            InitializeComponent();
            login_panel.Show();
            admin_panel.Hide();

            connection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='crm';username=root;password=");
            viewrm();
            viewcrs();
            viewassndcrs();
            viewacourse();
            viewfac();
            fac_comb();

        }

        private void login_btn_Click(object sender, EventArgs e)
        {

            try
            {
                connection.Open();
                cmd = new MySqlCommand("SELECT type FROM `login` WHERE `username` = '" + user_txt.Text + "' AND `password` = '" + pass_txt.Text + "'", connection);

                mdr = cmd.ExecuteReader();
                while (mdr.Read())
                {
                    if (mdr.GetString(0) == "admin")
                    {
                       
                        admin_panel.Show();
                        crse_pan.Hide();
                        fac_panel.Hide();
                        rm_panel.Hide();
                    }
                    else if (mdr.GetString(0) == "faculty")
                    {
                        login_panel.Hide();
                    }
                    else
                    {

                        MessageBox.Show("Invalid username or password");
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            connection.Close();
            mdr = null;
            cmd = null;
            user_txt.Text = null;
            pass_txt.Text = null;
        }

        private void add_fac_btn_Click(object sender, EventArgs e)
        {
            fac_panel.Show();
            crse_pan.Hide();
            rm_panel.Hide();
            sec_pan.Hide();
            route_pan.Hide();
			view_rout_pan.Hide();
		}

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        public void viewrm()
        {
            try
            {

                connection.Open();
                string Query = "SELECT `room` FROM `schedule` GROUP BY `room`;";

                cmd = new MySqlCommand(Query, connection);

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                room_grid.DataSource = dTable;
                connection.Close();
                mdr = null;
                cmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void viewcrs()
        {
            try
            {

                connection.Open();
                string Query = "SELECT `cid`,`credit` FROM `course`;";

                cmd = new MySqlCommand(Query, connection);

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                crse_sec_grid.DataSource = dTable;
                connection.Close();
                mdr = null;
                cmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void viewassndcrs()
        {
            try
            {

                connection.Open();
                string Query = "SELECT * FROM `routine`;";

                cmd = new MySqlCommand(Query, connection);

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                assigned_crs_grid.DataSource = dTable;
                connection.Close();
                mdr = null;
                cmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void viewfac()
        {
            try
            {

                connection.Open();
                string Query = "SELECT * FROM `faculty`;";

                cmd = new MySqlCommand(Query, connection);

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                fac_grid.DataSource = dTable;
                connection.Close();
                mdr = null;
                cmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
		public void viewrout()
		{
			try
			{

				connection.Open();
				string Query = "SELECT `routine`.`cid`,`course`.`cname`,`routine`.`section`,`routine`.`faculty`,`schedule`.`day`,`schedule`.`time`,`schedule`.`room` from `course`,`schedule`,`routine` WHERE `course`.`cid`=`routine`.`cid` AND `routine`.`r_id`=`schedule`.`r_id` AND `schedule`.`r_id` IS NOT NULL;";

				cmd = new MySqlCommand(Query, connection);

				MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
				MyAdapter.SelectCommand = cmd;
				DataTable dTable = new DataTable();
				MyAdapter.Fill(dTable);
				rout_grid.DataSource = dTable;
				connection.Close();
				mdr = null;
				cmd = null;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
		public void fac_comb()
        {

			fac_combo.Items.Clear();
            try
            {

                connection.Open();

                cmd = new MySqlCommand("Select name from faculty", connection);
                mdr = cmd.ExecuteReader();

                while (mdr.Read())
                {

                    fac_combo.Items.Add(mdr.GetString(0));

                }
                connection.Close();
                mdr = null;
                cmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "fac_combo");
            }
        }

        public void viewacourse()
        {
            try
            {

                connection.Open();
                string Query = "SELECT * FROM `course`;";

                cmd = new MySqlCommand(Query, connection);

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                crs_grid.DataSource = dTable;
                connection.Close();
                mdr = null;
                cmd = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            try
            {
                connection.Open();
                cmd = new MySqlCommand("INSERT INTO `course` (`cid`, `cname`, `credit`) VALUES ('" + cid_txt.Text + "', '" + cname_txt.Text + "', '" + credit_txt.Text + "');", connection);

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {

                    MessageBox.Show("Course information Successfully added");
                    cid_txt.Text = null;
                    cname_txt.Text = null;
                    credit_txt.Text = null;

                }
                else
                {
                    MessageBox.Show("Course information is already there");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            connection.Close();
            mdr = null;
            cmd = null;
            viewcrs();
            viewacourse();
        }

        private void add_crs_btn_Click(object sender, EventArgs e)
        {
            admin_panel.Show();
            fac_panel.Show();
            crse_pan.Show();
            rm_panel.Hide();
            sec_pan.Hide();
            route_pan.Hide();
			view_rout_pan.Hide();
		}

        private void ins_fac_btn_Click(object sender, EventArgs e)
        {

            try
            {
                connection.Open();
                cmd = new MySqlCommand("START TRANSACTION;INSERT INTO `faculty` (`id`, `Name`, `contact`, `email`, `already_assigned_cr`) VALUES ('" + id_fac_txt.Text + "', '" + name_fac_txt.Text + "', '" + cn_fac_txt.Text + "', '" + email_fac_txt.Text + "', 0);INSERT INTO `login` (`id`,`username`, `password`, `type`) VALUES ('" + id_fac_txt.Text + "','" + email_fac_txt.Text + "', '" + pass__fac_txt.Text + "', 'faculty');COMMIT; ", connection);

                int i = cmd.ExecuteNonQuery();
                if (i > 1)
                {

                    MessageBox.Show("Faculty information Successfully added");
                    id_fac_txt.Text = null;
                    name_fac_txt.Text = null;
                    cn_fac_txt.Text = null;
                    email_fac_txt.Text = null;
                    pass__fac_txt.Text = null;
                    
                }
                else
                {
                    MessageBox.Show("Faculty information is already there");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            connection.Close();
            mdr = null;
            cmd = null;
            viewfac();
			fac_comb();

		}

        private void fac_tab_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void up_fac_btn_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                cmd = new MySqlCommand("START TRANSACTION;UPDATE `faculty` SET `Name` = '" + name_fac_txt.Text + "', `email` = '" + email_fac_txt.Text + "' WHERE `faculty`.`id` = '" + id_fac_txt.Text + "';UPDATE `login` SET `username` = '" + email_fac_txt.Text + "', `password` = '" + pass__fac_txt.Text + "' WHERE `login`.`id` = '" + id_fac_txt.Text + "';COMMIT;", connection);
                int i = cmd.ExecuteNonQuery();
                if (i > 1)
                {

                    MessageBox.Show("Faculty information Successfully Updated");
                    id_fac_txt.Text = null;
                    name_fac_txt.Text = null;
                    cn_fac_txt.Text = null;
                    email_fac_txt.Text = null;
                    pass__fac_txt.Text = null;
                }
                else
                {
                    MessageBox.Show("You can't change faculty id !!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            connection.Close();
            mdr = null;
            cmd = null;
            viewfac();
			fac_comb();

		}

        private void del_fac_btn_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                cmd = new MySqlCommand("START TRANSACTION;DELETE FROM `faculty` WHERE `faculty`.`id` = '" + id_fac_txt.Text + "';DELETE FROM `login` WHERE `login`.`id` = '" + id_fac_txt.Text + "';COMMIT;", connection);
                int i = cmd.ExecuteNonQuery();
                if (i > 1)
                {

                    MessageBox.Show("Faculty information Successfully Deleted");
                    id_fac_txt.Text = null;
                    name_fac_txt.Text = null;
                    cn_fac_txt.Text = null;
                    email_fac_txt.Text = null;
                    pass__fac_txt.Text = null;
                }
                else
                {
                    MessageBox.Show("Faculty Information couldnot be found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            connection.Close();
            mdr = null;
            cmd = null;
            viewfac();
			fac_comb();
		}

        private void up_cr_btn_Click(object sender, EventArgs e)
        {

            try
            {
                connection.Open();
                cmd = new MySqlCommand("UPDATE `course` SET `cname` = '" + cname_txt.Text + "', `credit` = '" + credit_txt.Text + "' WHERE `course`.`cid` = '" + credit_txt.Text + "';", connection);

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {

                    MessageBox.Show("Course information Successfully Updated");
                    cid_txt.Text = null;
                    cname_txt.Text = null;
                    credit_txt.Text = null;

                }
                else
                {
                    MessageBox.Show("You can't change course id!!");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            connection.Close();
            mdr = null;
            cmd = null;
            viewcrs();
            viewacourse();
        }

        private void del_cr_btn_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                cmd = new MySqlCommand("DELETE FROM `course` WHERE `course`.`id` = '" + cid_txt.Text + "'", connection);

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {

                    MessageBox.Show("Course information Successfully Deleted");
                    cid_txt.Text = null;
                    cname_txt.Text = null;
                    credit_txt.Text = null;

                }
                else
                {
                    MessageBox.Show("Faculty Information couldnot be found");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            connection.Close();
            mdr = null;
            cmd = null;
            viewcrs();
            viewacourse();
        }

        private void add_rm_btn_Click(object sender, EventArgs e)
        {
            long a = 0;
            try
            {
                connection.Open();
                cmd = new MySqlCommand("SELECT count(id) FROM `schedule`;", connection);

                mdr = cmd.ExecuteReader();
                while (mdr.Read())
                {

                    a = mdr.GetInt64(0) + 1;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            connection.Close();
            mdr = null;
            cmd = null;
            MySqlCommand mcmd;
            progressBar1.Value = 0;
            progressBar1.Maximum = 47;
            progressBar1.Step = 1;


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    try
                    {
                        connection.Open();

                        mcmd = new MySqlCommand("select * from `schedule` where `day`='" + day[j] + "' and `time`='" + time[i] + "' and `room`='" + add_rm_txt.Text + "';", connection);
                        mdr = mcmd.ExecuteReader();
                        connection.Close();
                        connection.Open();
                        cmd = new MySqlCommand("START TRANSACTION;INSERT INTO `schedule` (`id`, `r_id`, `day`, `time`, `room`) VALUES ('" + a.ToString() + "',NULL, '" + day[j] + "', '" + time[i] + "','" + add_rm_txt.Text + "');COMMIT;", connection);
                        if (mdr.HasRows)
                        {
                            MessageBox.Show("Room already exists");
                            goto breakloop;

                        }
                        else
                        {

                            cmd.ExecuteNonQuery();
                            progressBar1.PerformStep();
                            a++;
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    connection.Close();
                    viewrm();

                    mdr = null;
                    cmd = null;

                }

            }
            add_rm_txt.Text = null;
        breakloop:
            connection.Close();

        }

        private void addrm_btn_Click(object sender, EventArgs e)
        {
            admin_panel.Show();
            fac_panel.Show();
            crse_pan.Show();
            rm_panel.Show();
            sec_pan.Hide();
            route_pan.Hide();
			view_rout_pan.Hide();
		}

        private void res_sec_btn_Click(object sender, EventArgs e)
        {

            connection.Open();
            cmd = new MySqlCommand("TRUNCATE routine", connection);

            cmd.ExecuteNonQuery();
            MessageBox.Show("All Data of routine successfully deleted");
            connection.Close();
            cmd = null;
            viewassndcrs();
            viewacourse();
            viewcrs();
        }

        private void save_sec_btn_Click(object sender, EventArgs e)
        {
			int pp = 0;
			int remcr=0;
			try
			{
				connection.Open();
				cmd = new MySqlCommand("SELECT `already_assigned_cr` FROM `faculty` WHERE `faculty`.`name`='"+fac_combo.Text+"';", connection);

				mdr = cmd.ExecuteReader();
				while (mdr.Read())
				{

					remcr = mdr.GetInt32(0);

				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			connection.Close();
			mdr = null;
			cmd = null;

			if ((remcr + Convert.ToInt32(cr_left_txt.Text)) > 15)
			{
				MessageBox.Show("Already assigned credit to him is reached maximum \n Please Choose another Faculty");

			}
			else
			{
				try
				{
					connection.Open();
					cmd = new MySqlCommand("SELECT * FROM `routine` WHERE `routine`.`section`='" + sec_combo.Text + "' AND `routine`.`cid`='" + cid_sec_txt.Text + "';", connection);

					mdr = cmd.ExecuteReader();
					if (mdr.HasRows)
					{

						pp = 1;

					}

				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
				connection.Close();
				mdr = null;
				cmd = null;

				if (pp == 1)
				{
					MessageBox.Show("Faculty is already assigned for the section of this course \n Please Choose another Section");
				}
				else {

					long a = 0;
					try
					{
						connection.Open();
						cmd = new MySqlCommand("SELECT count(r_id) FROM `routine`;", connection);

						mdr = cmd.ExecuteReader();
						while (mdr.Read())
						{

							a = mdr.GetInt64(0) + 1;

						}

					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString());
					}
					connection.Close();
					mdr = null;
					cmd = null;

					try
					{
						connection.Open();
						cmd = new MySqlCommand("START TRANSACTION;INSERT INTO `routine` (`r_id`,`cid`, `faculty`, `section`, `remaining_credit`) VALUES ('" + a.ToString() + "','" + cid_sec_txt.Text + "', '" + fac_combo.Text + "', '" + sec_combo.Text + "', '" + cr_left_txt.Text + "');UPDATE `faculty` SET `already_assigned_cr`= `already_assigned_cr`+" + cr_left_txt.Text + " WHERE `faculty`.`name`='" + fac_combo.Text + "';COMMIT;", connection);

						int i = cmd.ExecuteNonQuery();
						if (i > 0)
						{

							MessageBox.Show("Section,Faculty Successfully added to course");
							cid_sec_txt.Text = null;
							cr_left_txt.Text = null;


						}
						else
						{
							MessageBox.Show("Info is already there");
						}


					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString());
					}
					connection.Close();
					mdr = null;
					cmd = null;
					viewacourse();
					viewassndcrs();
					viewfac();
					viewcrs();

				}


			}
        }

        private void sec_gen_btn_Click(object sender, EventArgs e)
        {
            admin_panel.Show();
            fac_panel.Show();
            crse_pan.Show();
            rm_panel.Show();
            sec_pan.Show();
            route_pan.Hide();
			view_rout_pan.Hide();
		}

        private void crse_sec_grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cid_sec_txt.Text = crse_sec_grid.CurrentRow.Cells[0].Value.ToString();
            cr_left_txt.Text = crse_sec_grid.CurrentRow.Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin_panel.Show();
            fac_panel.Show();
            crse_pan.Show();
            rm_panel.Show();
            sec_pan.Show();
            route_pan.Show();
			view_rout_pan.Hide();
		}

        private void logout_btn_Click(object sender, EventArgs e)
        {
            login_panel.Show();
            admin_panel.Hide();
            fac_panel.Hide();
            crse_pan.Hide();
            rm_panel.Hide();
            sec_pan.Hide();
            route_pan.Hide();
			view_rout_pan.Hide();
		}

        private void auto_gen_Click(object sender, EventArgs e)
        {
            ArrayList ri = new ArrayList();
            ArrayList rc = new ArrayList();
            ArrayList rx = new ArrayList();
            try
            {
                connection.Open();
                cmd = new MySqlCommand("SELECT `id` FROM `schedule` WHERE ISNULL(`r_id`)", connection);
                mdr = cmd.ExecuteReader();

                while (mdr.Read())
                {
                    rx.Add(mdr.GetInt32(0));
                }
                connection.Close();
                cmd = null;
                MessageBox.Show("1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            mdr = null;
            int prgx = 0;
            try
            {
                connection.Open();
                cmd = new MySqlCommand("SELECT `r_id`,`remaining_credit`,SUM(`remaining_credit`) FROM `routine` WHERE `routine`.`remaining_credit` <> 0", connection);
                mdr = cmd.ExecuteReader();

                while (mdr.Read())
                {
                    ri.Add(mdr.GetInt32(0));
                    rc.Add(mdr.GetInt32(1));
                    prgx = mdr.GetInt32(1);
                }
                connection.Close();
                cmd = null;
                MessageBox.Show(prgx.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            mdr = null;
            rout_prog.Value = 0;
            rout_prog.Maximum = prgx;
            rout_prog.Step = 1;
            int l = prgx;
            int x = rx.Count;
            int p = 1;
            MessageBox.Show(ri.Count.ToString());
            for (int i = 0; i < ri.Count; i++)
            {
                for (int j = 0; j < (int)rc[i]; j++)
                {

                        try
                        {
                            connection.Open();
                            cmd = new MySqlCommand("UPDATE `schedule` SET `r_id` = '" + ri[i] + "' WHERE `schedule`.`id` = '" + rx[p] + "';UPDATE `routine` SET `remaining_credit` = `remaining_credit`-1 WHERE `routine`.`r_id` = '" + ri[i] + "';", connection);
                            cmd.ExecuteNonQuery();
                            rout_prog.PerformStep();
                            p++;
                            l--;
                            if (rout_prog.Value == prgx)
                            {
                                MessageBox.Show("Routine Generation Successful");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        
                        connection.Close();
                        mdr = null;
                        cmd = null;
                    
                }
            }
        }

		private void v_rout_Click(object sender, EventArgs e)
		{
			admin_panel.Show();
			fac_panel.Show();
			crse_pan.Show();
			rm_panel.Show();
			sec_pan.Show();
			route_pan.Show();
			view_rout_pan.Show();
			viewrout();
		}
	}
}
