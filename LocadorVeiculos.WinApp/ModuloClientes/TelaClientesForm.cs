﻿using iText.StyledXmlParser.Jsoup.Nodes;
using LocadorAutomoveis.Dominio.ModuloClientes;
using LocadorAutomoveis.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocadorAutomoveis.WinApp.ModuloClientes
{
    public partial class TelaClientesForm : Form
    {
        private Clientes clientes;

        public event GravarRegistroDelegate<Clientes> onGravarRegistro;

        public TelaClientesForm()
        {
            InitializeComponent();
            this.ConfigurarDialog();
        }

        public Clientes ObterClientes()
        {
            clientes.NomeCliente = txtNome.Text;
            clientes.Telefone = txtTelefone.Text;
            clientes.Numero = txtNumero.Text;
            clientes.Email = txtEmail.Text;
            clientes.Cidade = txtCidade.Text;
            clientes.Cpf = txtCpf.Text;
            clientes.Cnpj = txtCnpj.Text;
            clientes.Estado = txtEstado.Text;
            clientes.Rua = txtRua.Text;
            clientes.Bairro = txtBairro.Text;

            return clientes;
        }

        public void ConfigurarClientes(Clientes clientes)
        {
            this.clientes = clientes;


            txtNome.Text = clientes.NomeCliente;
            txtBairro.Text = clientes.Bairro;
            txtCidade.Text = clientes.Cidade;
            txtCnpj.Text = clientes.Cnpj;
            txtEmail.Text = clientes.Email;
            txtCpf.Text = clientes.Cpf;
            txtNumero.Text = clientes.Numero;
            txtRua.Text = clientes.Rua;
            txtEstado.Text = clientes.Estado;
        }

        private void btninserir_Click(object sender, EventArgs e)
        {
            this.clientes = ObterClientes();

            Result resultado = onGravarRegistro(clientes);

            if (resultado.IsFailed)
            {
                string erro = resultado.Errors[0].Message;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }
    }
}
