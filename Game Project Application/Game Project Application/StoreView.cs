﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Project_Application
{
    public partial class StoreView : Form
    {
        public StoreView()
        {
            InitializeComponent();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStoreInfo_Click(object sender, EventArgs e)
        {
            if(uxStoreId.Text != "")
            {
                int storeId = Convert.ToInt32(uxStoreId.Text);
                string location;
                string hours;
                string numOfGames;
                string totalSales;

                string[] output = getStoreInfo(storeId);

                string s = "Location: " +  output[0] + "\n" +
                           "Weekday Hours: " + output[1] + "\n" +
                           "Saturday Hours: " + output[2] + "\n" +
                           "Sunday Hours: " + output[3] + "\n" +
                           "Number of Games in Store: " + output[4] + "\n" +
                           "Total Sales: " + output[5] + "\n";
                MessageBox.Show(s);
            }
            else
            {
                MessageBox.Show("Please Insert a Store Id.");
            }

        }

        private string[] getStoreInfo(int id)
        {
            string connectionString = "Server=mssql.cs.ksu.edu;Database=cis560_team21; Integrated Security=true";
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("GameStore.GetStoreInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("StoreId", id);
                    connection.Open();

                    var k = command.ExecuteReader();


                    string address ="";
                    string city ="";
                    string state ="";
                    string weekdayHours = "";
                    string saturdayHours = "";
                    string sundayHours = "";
                    string numOfGames = "";
                    string totalSales = "";
                    int hours;
                    int minute;
                    string m;

                    while (k.Read())
                    {
             
                        address = k.GetString(k.GetOrdinal("Address"));
                        city = k.GetString(k.GetOrdinal("City"));
                        state = k.GetString(k.GetOrdinal("State"));

                        if(k.GetString(k.GetOrdinal("Description")).Equals("Monday to Friday Hours"))
                        {

                            hours = k.GetInt32(k.GetOrdinal("StartHour"));
                            minute = k.GetInt32(k.GetOrdinal("StartMinute"));
                            
                            if(minute < 10)
                            {
                                m = "0" + minute.ToString();
                            }
                            else
                            {
                                m = minute.ToString();
                            }

                            if(hours < 12)
                            {
                                
                                weekdayHours = hours.ToString() + ":" + m + " AM";
                            }
                            else
                            {
                                
                                weekdayHours = (hours % 12).ToString() + ":" + m + " PM";
                            }

                        }
                        else if(k.GetString(k.GetOrdinal("Description")).Equals("Saturday Hours"))
                        {
                            hours = k.GetInt32(k.GetOrdinal("StartHour"));
                            minute = k.GetInt32(k.GetOrdinal("StartMinute"));

                            if (minute < 10)
                            {
                                m = "0" + minute.ToString();
                            }
                            else
                            {
                                m = minute.ToString();
                            }

                            if (hours < 12)
                            {

                                saturdayHours = hours.ToString() + ":" + m + " AM";
                            }
                            else
                            {

                                saturdayHours = (hours % 12).ToString() + ":" + m + " PM";
                            }
                        }
                        else if(k.GetString(k.GetOrdinal("Description")).Equals("Sunday Hours"))
                        {
                            hours = k.GetInt32(k.GetOrdinal("StartHour"));
                            minute = k.GetInt32(k.GetOrdinal("StartMinute"));

                            if (minute < 10)
                            {
                                m = "0" + minute.ToString();
                            }
                            else
                            {
                                m = minute.ToString();
                            }

                            if (hours < 12)
                            {

                                sundayHours = hours.ToString() + ":" + m + " AM";
                            }
                            else
                            {

                                sundayHours = (hours % 12).ToString() + ":" + m + " PM";
                            }
                        }


                        numOfGames = k.GetInt32(k.GetOrdinal("NumGames")).ToString();
                        totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();

                    }

                    string location = address + ", " + city + ", " + state;

                    
                    k.Close();

                    string[] output = { location, weekdayHours, saturdayHours, sundayHours, numOfGames, totalSales };
                    return output;
                }
            }
        }

        /// <summary>
        /// Returns the total number of orders made and total sales of month to date and year to date
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        private string[] getSalesInfo(int storeId, int month)
        {
            string connectionString = "Server=mssql.cs.ksu.edu;Database=cis560_team21; Integrated Security=true";
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("GameStore.GetSalesInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("StoreId", storeId);
                    command.Parameters.AddWithValue("Month", month);
                    connection.Open();

                    var k = command.ExecuteReader();

                    string totalSales = "";
                    string orders = "";

                    while (k.Read())
                    {
                        switch (month)
                        {
                            case 1:
                                if (k.GetString(k.GetOrdinal("Month")).Equals("January"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                            case 2:
                                if (k.GetString(k.GetOrdinal("Month")).Equals("February"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                            case 3:
                                if (k.GetString(k.GetOrdinal("Month")).Equals("March"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                            case 4:
                                if (k.GetString(k.GetOrdinal("Month")).Equals("April"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                            case 5:
                                if (k.GetString(k.GetOrdinal("Month")).Equals("May"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                            case 6:
                                if (k.GetString(k.GetOrdinal("Month")).Equals("June"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                            case 7:
                                if (k.GetString(k.GetOrdinal("Month")).Equals("July"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                            case 8:
                                if (k.GetString(k.GetOrdinal("Month")).Equals("August"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                            case 9:
                                if (k.GetString(k.GetOrdinal("Month")).Equals("September"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                            case 10:
                                if (k.GetString(k.GetOrdinal("Month")).Equals("October"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                            case 11:
                                if (k.GetString(k.GetOrdinal("Month")).Equals("November"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                            case 12:
                                if (k.GetString(k.GetOrdinal("Month")).Equals("December"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                            default:
                                if (k.GetString(k.GetOrdinal("Year")).Equals("2019"))
                                {
                                    orders = k.GetInt32(k.GetOrdinal("Orders")).ToString();
                                    totalSales = k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();
                                }
                                break;
                        }

                    }

                    k.Close();

                    string[] output = { orders, totalSales };
                    return output;
                }
            }
        }


        private void btnSales_Click(object sender, EventArgs e)
        {
            if (uxStoreId.Text != "")
            {
                int storeId = Convert.ToInt32(uxStoreId.Text);
                StringBuilder s = new StringBuilder();

                string[] output;

                for(int i = 1; i <= 12; i++)
                {
                    output = getSalesInfo(storeId, i);
                    switch (i)
                    {
                        case 1:
                            s.Append("Month: January\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 2:
                            s.Append("Month: February\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 3:
                            s.Append("Month: March\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 4:
                            s.Append("Month: April\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 5:
                            s.Append("Month: May\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 6:
                            s.Append("Month: June\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 7:
                            s.Append("Month: July\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 8:
                            s.Append("Month: August\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");        
                            break;
                        case 9:
                            s.Append("Month: September\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 10:
                            s.Append("Month: October\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 11:
                            s.Append("Month: November\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 12:
                            s.Append("Month: December\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n\n");
                            break;
                    }

                }

                output = getSalesInfo(storeId, 2019);
                s.Append("2019 Yearly Orders: " + output[0] + "\n" +
                         "2019 Yearly Sales:  " + output[1]);

                MessageBox.Show(s.ToString());
            }
            else
            {
                MessageBox.Show("Please Insert a Store Id.");
            }
        }
    }
}
