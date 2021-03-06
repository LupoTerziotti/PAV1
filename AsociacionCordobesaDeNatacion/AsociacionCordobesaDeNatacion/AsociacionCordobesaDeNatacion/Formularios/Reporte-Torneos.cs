﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AsociacionCordobesaDeNatacion.Clases;

namespace AsociacionCordobesaDeNatacion.Formularios
{
    internal partial class Reporte_Torneos : Form
    {
        AccesoBD _BD;

        public Reporte_Torneos(AccesoBD BD)
        {
            InitializeComponent();
            _BD = BD;
            cargarComboBoxAnios();
        }

        private void btn_CargarGrilla_Click(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();

            dataGrid_Torneos.DataSource=null;

            tabla = _BD.consulta("SELECT Inscriptos.cod_torneo, Torneos.descripcion, count (*) as cantidad, Min(Inscriptos.tiempo) as 'Mejor Tiempo', Avg(Inscriptos.tiempo) as 'Tiempo Promedio'   " +
                                 "FROM Inscriptos , Torneos " +
                                 "WHERE Inscriptos.cod_torneo = Torneos.cod_torneo " +
                                 "AND anio =" + cmb_anio.SelectedValue +
                                 "GROUP BY Torneos.descripcion , Inscriptos.cod_torneo");

            if (tabla.Rows.Count == 0)
            {
                MessageBox.Show("No hay TORNEOS registrados del AÑO seleccionado");
                return;
            }

            dataGrid_Torneos.DataSource = tabla;
            dataGrid_Torneos.Columns[0].HeaderText = "Código";
            dataGrid_Torneos.Columns[1].HeaderText = "Torneo";
            dataGrid_Torneos.Columns[2].HeaderText = "Inscripciones";
			dataGrid_Torneos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dataGrid_Torneos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

			string[] x = new string[tabla.Rows.Count];
            int[] y = new int[tabla.Rows.Count];

            for (int i = 0 ; i < tabla.Rows.Count ; i ++)
            {
                x[i] = tabla.Rows[i][1].ToString();
                y[i] = Convert.ToInt32(tabla.Rows[i][2].ToString());
            }

            chart1.Series[0].Points.DataBindXY(x, y);
            chart1.Series[0].ChartType = SeriesChartType.Pie;


        }

        public void cargarComboBoxAnios()
        {
            cmb_anio.DataSource = _BD.consulta("SELECT * FROM anioTorneo");
            cmb_anio.ValueMember = "anio";
            cmb_anio.DisplayMember = "anio";
        }
    }
}
