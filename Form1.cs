using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desafio2Ejercicio2.ClasesMonticulos;

namespace Desafio2Ejercicio2
{ 
    //Rodrigo Guillermo Trujillo Diaz TD240434
    //Isel Metzi Carrillo Mejia CM240437
    //Sofia Elizabeth Lopez Molina LM240314
    //Patricia Berenice Méndez Pérez MP241986
    public partial class Form1 : Form
    {
        Monticulo HeapMin = new Monticulo();

        Tarea[] tareasOrdenadas; // Llama al método que ordena las tareas
        public Form1()
        {
            InitializeComponent();


            tareasOrdenadas = new Tarea[0];
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Day <  DateTime.Now.Day)
            {
                MessageBox.Show("La fecha seleccionada no es valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox1.Text == "")
            {
                MessageBox.Show("El campo no puede estar vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("El campo no puede estar vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                richTextBox1.Focus();
                return;
            }
            HeapMin.InsertarValor(comboBox1.SelectedIndex, dateTimePicker1.Value, textBox1.Text, richTextBox1.Text);
            tareasOrdenadas = HeapMin.OrdenarVector();
            HeapMin.MostrarMonticuloEnGrid(dataGridView1, tareasOrdenadas);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
             

        }

        private void button2_Click(object sender, EventArgs e)
        {
            HeapMin.borrarPrimero();
            dataGridView1.Rows.Clear();
            tareasOrdenadas = HeapMin.OrdenarVector();
            HeapMin.MostrarMonticuloEnGrid(dataGridView1, tareasOrdenadas);

        }
    }
}
