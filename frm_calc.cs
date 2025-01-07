﻿using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    public partial class frmCalc : Form
    {
        float num1 = 0;
        float num2 = 0;
        string op = "";
        const string divError = "This is a very complex question. Many people say it's undefinined, but what is that? Well, it can also be considered indeterminent, but that's just the begining. There are many odd results when you get to the smallest part";
        bool firstnum = false; //flag for if it's the first number
        public frmCalc()
        {
            InitializeComponent();


        }
        bool debug = true;
        string confirmClose = "please confirm your desire to leave"; //custom text for confirming close
        private void frmCalc_Load(object sender, EventArgs e)
        {
            
            if( debug == true)
            {
                txt_debug.Visible = true;
            } //if debug on see list
            else {
                this.Size = new Size(816, 489);
            }// if deug off, shrink form for conscisness;

            string closeText = bttnClose.Text;
            

            addItem(debug.ToString(),false); //debug mode?
            addItem(closeText, true); //close text
            addItem(confirmClose, false); //confirm close
        }

        const string closeText = "Close";
        const string confirmText = "Confirm";

        private string addItem(string item, bool newEntry)
        {
            string empty = Environment.NewLine;
            if (newEntry == true)
            {
                txt_debug.AppendText(empty);
            }
            else
            {
            }
            txt_debug.AppendText(item);
            txt_debug.AppendText(empty);
            return txt_debug.Text.ToString();
        }
        private void bttnClose_Click(object sender, EventArgs e)
        {
            if ( bttnClose.Text == closeText) // next name
            {
                bttnClose.Text = confirmText;
            }
            else {
                Application.Exit();
            } // close
            tmr_ConfirmClose.Start();
            
        }

        private void txt_debug_TextChanged(object sender, EventArgs e)
        {

        }

        private void tmr_ConfirmClose_Tick(object sender, EventArgs e)
        {
            tmr_ConfirmClose.Stop();
            bttnClose.Text = closeText;
        }

        private void bttn_num_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (rtb_ans.Text == "0" || firstnum)
            {
                rtb_ans.Text = "";
            }
            else
            {
                rtb_ans.Text += btn.Text;
            };
            // also the following works (but sometimes it works in cases like strings to integers. Sometimes it works, sometimes it doesn't.
            // Button btn = (Button)sender;
            
            rtb_ans.Text += btn.Text;
        }

        private void op_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            op = btn.Text;
            num1 = float.Parse(rtb_ans.Text);
            rtb_ans.Text = "0"; //set to 0 as after hitting operator you need 0, si?
            firstnum = false;

        }

        private void clear_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text == "Clear Equation") //ensure it's clear equation (no operators, both nums are 0, 0 as answer)
            {
                num1 = 0;
                num2 = 0;
                op = "";
            } 
            rtb_ans.Text = "0";
            firstnum = false;
        }

        private void bttn_equal_Click(object sender, EventArgs e)
        {
            num2 = float.Parse(rtb_ans.Text);

            switch ( op ) //runs only exact kinda like an if and an equal, with in-built if-else
            {
                case "+":
                    rtb_ans.Text = (num1 + num2).ToString();
                    break;
                case "-":
                    rtb_ans.Text = (num1 - num2).ToString();
                    break;
                case "*":
                        rtb_ans.Text = (num1 * num2).ToString();
                    break;
                case "/":
                    if (num2 != 0)
                    {
                        rtb_ans.Text = (num1 / num2).ToString();
                    } else {
                        MessageBox.Show("error");
                    }
                    break;
                case "%":
                    if (num2 != 0)
                    {
                        rtb_ans.Text = (num1 % num2).ToString();
                    } else
                    {
                        MessageBox.Show("Divide by 0 error");
                    }
                    break;
                default:
                    {
                        rtb_ans.Text = num1.ToString();
                        break;
                    }
         
            }
            num1 = 0;
            num2 = 0;
            op = "";
            firstnum = true;
        }
    }
}
