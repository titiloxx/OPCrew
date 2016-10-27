﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
           
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpClient web2 = new HttpClient();
            dataGridView1.Rows.Clear();
            button1.Enabled = false;
            Dictionary<string, decimal> armas = new Dictionary<string, decimal>();//Crea el diccinoario
                armas.Clear();//Limpia el diccionario
                int count = 1;//Inicializo el contador
                string arma = textBox1.Text;
                Primero procesado = new Primero();
                HttpClient web = new HttpClient();
              
                var info = web.GetStringAsync("https://api.opskins.com/IPricing/GetAllLowestListPrices/v1/?appid=730&format=json_pretty");
                procesado = JsonConvert.DeserializeObject<Primero>(info.Result);
                var hola = JObject.Parse(info.Result);
                var propiedades = hola.Properties();
                var fecha = propiedades.Children();
                var HOLA = (JObject)hola["response"];
                foreach (var item in HOLA)
                {
				if(checkBox1.Cjecked == true)
                if (item.Key.StartsWith(arma))
                    {
                        armas.Add(item.Key, Convert.ToDecimal(item.Value["price"].ToString()));
                        count = count + 1;
                   
              

                }
            }//Imprime el diccionario
            int txtb2 = Convert.ToInt32(textBox2.Text);      // CheckBox2 valor minimo
            int txtb3 = Convert.ToInt32(textBox3.Text);      // CheckBox3 valor maximo
            foreach (KeyValuePair<string, decimal> item in armas)
                {
                decimal precio = item.Value / 100;
                if (txtb2 < precio && txtb3 > precio) { 
                    if (checkBox2.Checked == true) { 
                var procesado2 = web2.GetStringAsync("http://csgobackpack.net/api/GetItemPrice/?currency=USD&time=7&icon=1&id="+item.Key);
                var  b = JsonConvert.DeserializeObject<JObject>(procesado2.Result);
                decimal average = Convert.ToDecimal((b["average_price"]));    
                if (txtb2 < precio && txtb3 > precio) {
                    decimal dolar = Convert.ToDecimal(comboBox1.Text);
                    int argentino = Convert.ToInt32(precio * dolar);
                        int argentinoaverage = Convert.ToInt32(average * 15);
                        decimal profit = (Convert.ToDecimal(textBox4.Text)/100)+1;
                    decimal venta = Convert.ToInt32(profit*argentino);
                    int profit2 = Convert.ToInt32(venta - argentino);
                    string a = item.Key + precio;
                    dataGridView1.Rows.Add(item.Key,precio, average, argentino, argentinoaverage, venta, profit2);
                    }
                }
                else
                {
                    if (txtb2 < precio && txtb3 > precio)
                    {
                        decimal dolar = Convert.ToDecimal(comboBox1.Text);
                        int argentino = Convert.ToInt32(precio * dolar);
                        decimal profit = (Convert.ToDecimal(textBox4.Text) / 100) + 1;
                        decimal venta = Convert.ToInt32(profit * argentino);
                        int profit2 = Convert.ToInt32(venta - argentino);
                        string a = item.Key + precio;
                        dataGridView1.Rows.Add(item.Key, "-", "-", precio, argentino, venta, profit2);
                    }
            }
                if (count == 1)//Si no se encuentra el arma
                {
                    MessageBox.Show("No se encontro arma");
                }
         
            button1.Enabled = true;
        }
        }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
