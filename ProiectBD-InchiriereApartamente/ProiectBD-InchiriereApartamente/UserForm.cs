﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectBD_InchiriereApartamente
{
    public partial class UserForm : Form
    {
        string Username = "";
        public UserForm(string username)
        {
            InitializeComponent();
            Username = username;
        }
    }
}