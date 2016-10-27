using System;
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
using System.Globalization;
using System.IO;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        int contador = 0;
        public Form1()
        {
           
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (contador == 0)//Agarra la money que tenemos en op
            {
                HttpClient web3 = new HttpClient();
                
                var info2 = web3.GetStringAsync("https://opskins.com/api/user_api.php?request=GetOP&key=b1f9845c63204b889c198d5de6bc67");
                var procesado3 = JObject.Parse(info2.Result);
                label8.Text = "$ " + Convert.ToString(Convert.ToDecimal((string)procesado3["result"]["op"]) / 100);
                contador = 1;
            }
                decimal average;
            Dictionary<string, decimal> dicsteam = new Dictionary<string, decimal>();//Crea el diccinoario de steam
            string text = System.IO.File.ReadAllText(@"steam.json");//Importar precio steam
            var steam = JsonConvert.DeserializeObject<JObject>(text);
            foreach (var item in steam)
            {
                dicsteam.Add(item.Key, Convert.ToDecimal(item.Value.ToString()));
            }
            HttpClient web2 = new HttpClient();
            dataGridView1.Rows.Clear();
            button1.Enabled = false;
            Dictionary<string, decimal> armas = new Dictionary<string, decimal>();//Crea el diccinoario
            armas.Clear();//Limpia el diccionario
            int count = 1;//Inicializo el contador para ver si cargo algo el diccionario o si no encontro armas
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            Primero procesado = new Primero();
            HttpClient web = new HttpClient();
            var info = web.GetStringAsync("https://api.opskins.com/IPricing/GetAllLowestListPrices/v1/?appid=730&format=json_pretty");
            procesado = JsonConvert.DeserializeObject<Primero>(info.Result);
            var hola = JObject.Parse(info.Result);
            var HOLA = (JObject)hola["response"];
            foreach (var item in HOLA)
            {
                string arma = ti.ToTitleCase(textBox1.Text);
                if (textBox1.Text.Equals("cuchi", StringComparison.OrdinalIgnoreCase))//Busca cuchillo
                {
                    arma = "\u2605 ";
                }
                else { 
                if (arma.StartsWith("\u2605")){//Le saca el \u2605
                    arma = arma.Substring(2, arma.Length-2);
                }
                if (checkBox1.Checked == true) {
                    arma = "StatTrak\u2122 " + arma;//Si tiene StatTrak
                }
                }
                if (item.Key.StartsWith(arma, StringComparison.OrdinalIgnoreCase)|| item.Key.Contains(arma))//Busca sin importar el Case
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
                if (txtb2 < precio && txtb3 > precio)
                {
                   foreach (KeyValuePair<string, decimal> item2 in dicsteam)
                        {
                            string string1 = item.Key;
                            string string2 = item2.Key;
                            if (item2.Key.StartsWith("?"))
                            {//Le saca el \u2605
                                 string2 = item2.Key.Substring(1, item2.Key.Length -1);
                            }
                            if (item.Key.StartsWith("\u2605"))
                            {//Le saca el \u2605
                                string1 = item.Key.Substring(1, item.Key.Length - 1);
                            }
                            if (string1.StartsWith(string2, StringComparison.OrdinalIgnoreCase))
                            {
                            decimal venta;
                                average = item2.Value;
                                decimal dolar = Convert.ToDecimal(comboBox1.Text);
                                int argentino = Convert.ToInt32(precio * dolar);
                                int argentinoaverage = Convert.ToInt32(average * 15);
                                decimal profit = (Convert.ToDecimal(textBox4.Text) / 100) + 1;
                            if (radioButton3.Checked == true)// Check Radius
                            {
                                venta = Convert.ToInt32(textBox4.Text);

                            }
                            else { 
                            venta = Convert.ToInt32(profit * argentino);
                            }
                            int profit2 = Convert.ToInt32(venta - argentino);
                                string a = item.Key + precio;
                            if (radioButton1.Checked == true)// Check Radius real
                            {
                                profit2 = Convert.ToInt32(textBox4.Text);
                                venta = argentino + profit2;
                            }
                            if (radioButton3.Checked == true)// Check Radius forzar venta
                            {
                                venta = Convert.ToInt32(textBox4.Text);
                                profit2 = Convert.ToInt32(venta - argentino);
                                if (profit2 < 0)// Para que no tire valores negativos
                                {
                                    profit2 = 0;
                                }
                            }
                            if (radioButton4.Checked == true)// Check Radius predecir
                            {
                                if (precio <= 1 && precio >0)
                                {
                                    profit2 = 20;
                                }
                                if (precio <= 5 && precio > 1)
                                {
                                    profit2 = 40;
                                }
                                if (precio <= 10 && precio > 5)
                                {
                                    profit2 = 70;
                                }
                                if (precio <= 15 && precio > 10)
                                {
                                    profit2 = 75;
                                }
                                if (precio <= 20 && precio > 15)
                                {
                                    profit2 = 90;
                                }
                                if (precio <= 25 && precio > 20)
                                {
                                    profit2 = 160;
                                }
                                if (precio <= 30 && precio > 25)
                                {
                                    profit2 = 200;
                                }
                                if (precio <= 35 && precio > 30)
                                {
                                    profit2 = 225;
                                }
                                if (precio <= 45 && precio > 35)
                                {
                                    profit2 = 250;
                                }
                                if (precio <= 55 && precio > 45)
                                {
                                    profit2 = 310;
                                }
                                if (precio <= 65 && precio > 55)
                                {
                                    profit2 = 350;
                                }
                                if (precio <= 75 && precio > 65)
                                {
                                    profit2 = 370;
                                }
                                if (precio <= 85 && precio > 75)
                                {
                                    profit2 = 410;
                                }
                                if (precio <= 95 && precio > 85)
                                {
                                    profit2 = 450;
                                }
                                if (precio <= 105 && precio > 95)
                                {
                                    profit2 = 465;
                                }
                                if (precio <= 105 && precio > 95)
                                {
                                    profit2 = 500;
                                }
                                if (precio <= 125 && precio > 105)
                                {
                                    profit2 = 550;
                                }
                                if (precio <= 155 && precio > 125)
                                {
                                    profit2 = 630;
                                }
                                if (precio <= 175 && precio > 155)
                                {
                                    profit2 = 700;
                                }
                                if (precio <= 200 && precio > 175)
                                {
                                    profit2 = 770;
                                }
                                if (precio <= 250 && precio > 200)
                                {
                                    profit2 = 850;
                                }
                                if (precio <= 300 && precio > 250)
                                {
                                    profit2 = 1100;
                                }
                                if (precio <= 350 && precio > 300)
                                {
                                    profit2 = 1400;
                                }
                                if (precio <= 400 && precio > 350)
                                {
                                    profit2 = 1600;
                                }
                                if (precio <= 450 && precio > 400)
                                {
                                    profit2 = 1800;
                                }
                                if (precio <= 500 && precio > 450)
                                {
                                    profit2 = 1900;
                                }
                                if (precio <= 550 && precio > 500)
                                {
                                    profit2 = 2000;
                                }
                                if (precio <= 600 && precio > 550)
                                {
                                    profit2 = 2100;
                                }
                                if (precio <= 700 && precio > 600)
                                {
                                    profit2 = 2200;
                                }
                                if (precio <= 800 && precio > 700)
                                {
                                    profit2 = 2300;
                                }
                                if (precio > 800)
                                {
                                    profit2 = 2500;
                                }
                                venta = argentino + profit2;

                            }
                            int porcent = Convert.ToInt32(100 - (precio * 100 / average));
                            if (porcent < 1)// Posible error en los precios de steam que da valores negativos
                            {
                                porcent = 0;
                                average = precio;

                            }
                                dataGridView1.Rows.Add(item.Key, precio, average,porcent , argentino, argentinoaverage, venta, profit2, "https://opskins.com/?loc=shop_search&app=730_2&sort=lh&search_item="+ item.Key);
                            }
                            else
                            {
                             average = 0;
                            }

                        }
                             
                }
            }
            if (count == 1)//Si no se encuentra el arma
            {
                MessageBox.Show("No se encontro arma");
            }   
            button1.Enabled = true;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
                label1.Visible = false;
                textBox4.Visible = true;
                label7.Visible = true;
            }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = true;
            textBox4.Visible = true;
            label7.Visible = true;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBox4.Visible = true;
            label7.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
  

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBox4.Visible = false;
            label7.Visible = false;
        }
    }
}
