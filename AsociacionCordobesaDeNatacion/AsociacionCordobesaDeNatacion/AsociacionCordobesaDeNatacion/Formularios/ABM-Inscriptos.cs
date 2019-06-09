﻿using AsociacionCordobesaDeNatacion.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsociacionCordobesaDeNatacion.Formularios;

namespace AsociacionCordobesaDeNatacion.Formularios
{
    public partial class ABM_Inscriptos : Form
    {

        AccesoBD _BD = new AccesoBD();
        Inscriptos inscriptos = new Inscriptos();
        Nadadores nadadores = new Nadadores();
        Torneos torneos = new Torneos();
        Especialidades especialidades = new Especialidades();
        DataTable tabla = new DataTable();



        public ABM_Inscriptos()
        {
            InitializeComponent();
            nuevo();
            cargarComboBoxEspecialidad();
            cargarComboBoxNadadores();
            cargarComboBoxTorneos();
            cargarComboBoxAnios();
            cargarTextBoxs();
        }


        private void cmd_grabar_Click(object sender, EventArgs e)
        {

            List<TextBox> array = crearArray();
            
            
                if (Utils.FormValidator.validacionesDeTextosVacios(array))
                {
                    inscriptos.cod_especialidad = this.txt_cod_especialidad.Text;
                    inscriptos.cod_torneo = this.txt_cod_torneo.Text;
                    inscriptos.cod_nadador = this.txt_cod_nadador.Text;
                    inscriptos.anio = this.cmb_anio.Text;
                    inscriptos.grabar_inscripto();
                    this.cargar_grilla();
                }
                else
                {
                    MessageBox.Show("Esta faltando seleccionar algun comboBox");
                }
        }

        private void cargar_grilla()
        {
            DataTable tabla = new DataTable();
            tabla = _BD.consulta("SELECT * FROM Inscriptos");
            if (tabla.Rows.Count == 0)
            {
                return;
            }
            dataGrid_Inscriptos.DataSource = tabla;
            dataGrid_Inscriptos.Columns[0].Width = 150;
            dataGrid_Inscriptos.Columns[1].Width = 300;
        }

        private void btn_CargarGrilla_Click(object sender, EventArgs e)
        {
            this.cargar_grilla();
        }

        private void btn_IniciarTrans_Click(object sender, EventArgs e)
        {
            _BD.iniciar_transaccion();
            this.label5.Text = "Iniciada";
        }

        private void btn_FinalizarTrans_Click(object sender, EventArgs e)
        {
            _BD.cerrar_transaccion();
            this.cargar_grilla();
            this.label5.Text = "Finalizada";
        }


        private void cmd_actualizar_Click(object sender, EventArgs e)
        {
			List<TextBox> array = crearArray();


			if (Utils.FormValidator.validacionesDeTextosVacios(array))
			{
				inscriptos.cod_especialidad = this.txt_cod_especialidad.Text;
				inscriptos.cod_torneo = this.txt_cod_torneo.Text;
				inscriptos.cod_nadador = this.txt_cod_nadador.Text;
				inscriptos.anio = this.txt_anio.Text;
				inscriptos.modificar_Inscripto(this.txt_cod_nadador.Text, this.txt_cod_especialidad.Text, this.txt_cod_torneo.Text, this.txt_anio.Text);
				this.cargar_grilla();
				this.label5.Text = "En proceso";
			}
			else
			{
				MessageBox.Show("Esta faltando seleccionar algun comboBox");
			}
		}

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            inscriptos.cod_especialidad = this.txt_cod_especialidad.Text;
            inscriptos.cod_torneo = this.txt_cod_torneo.Text;
            inscriptos.cod_nadador = this.txt_cod_nadador.Text;
            inscriptos.anio = this.txt_anio.Text;

            inscriptos.eliminar_Inscripto();


            this.label5.Text = "En proceso";
        }

        public float randomNumberMethod(Random random)
        {
            double a = 22;
            double b = 30;
            double numero = a + random.NextDouble() * (b - a);

            return (float)(numero);
        }

        private List<TextBox> crearArray()
        {
            List<TextBox> array = new List<TextBox>();
            array.Add(this.txt_cod_especialidad);
            array.Add(this.txt_cod_nadador);
            array.Add(this.txt_cod_torneo);
            return array;
        }

        public void cargarComboBoxEspecialidad()
        {
            cmb_especialidad.DataSource = _BD.consulta("SELECT * FROM Especialidad");
            cmb_especialidad.ValueMember = "cod_espec";
            cmb_especialidad.DisplayMember = "descripcion";
        }

        public void cargarComboBoxTorneos()
        {
            cmb_torneo.DataSource = _BD.consulta("SELECT * FROM Torneos");
            cmb_torneo.ValueMember = "cod_torneo";
            cmb_torneo.DisplayMember = "descripccion";

        }

        public void cargarComboBoxNadadores()
        {
            cmb_nadadores.DataSource = _BD.consulta("SELECT * FROM Nadadores");
            cmb_nadadores.ValueMember = "cod_nacional";
            cmb_nadadores.DisplayMember = "nombre";
        }

        public void cargarComboBoxAnios()
        {
            cmb_anio.DataSource = _BD.consulta("SELECT * FROM anioTorneo");
            cmb_anio.ValueMember = "anio";
            cmb_anio.DisplayMember = "anio";
        }

        private void cmb_especialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_cod_especialidad.Text = cmb_especialidad.SelectedValue.ToString();
        }

        private void cmb_torneo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_cod_torneo.Text = cmb_torneo.SelectedValue.ToString();
        }

        private void cmb_nadadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_cod_nadador.Text = cmb_nadadores.SelectedValue.ToString();
        }

        private void cmb_anio_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_anio.Text = cmb_anio.SelectedValue.ToString();
        }

        private void nuevo()
        {
            cmb_nadadores.SelectedIndex = -1;
            cmb_especialidad.SelectedIndex = -1;
            cmb_torneo.SelectedIndex = -1;
            cmb_anio.SelectedIndex = -1;
            cargarTextBoxs();
        }

        private void cmd_nuevo_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        public void cargarTextBoxs()
        {
            txt_anio.Text = "";
            txt_cod_nadador.Text = "";
            txt_cod_especialidad.Text = "";
            txt_cod_torneo.Text = "";
        }

        
    }
}
