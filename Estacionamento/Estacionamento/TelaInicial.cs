using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Estacionamento
{
    public partial class TelaInicial : Form
    {
        string RxString;
        string rxString;

        public TelaInicial()
        {
            InitializeComponent();
        }


        private void atualizaListaCOMs()
        {
            int i;
            bool quantDiferente; //flag para sinalizar que a quantidade de portas mudou

            i = 0;
            quantDiferente = false;

            //se a quantidade de portas mudou
            if (cbPortas.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (cbPortas.Items[i++].Equals(s) == false)
                    {
                        quantDiferente = true;
                    }
                }
            }
            else
            {
                quantDiferente = true;
            }

            //Se não foi detectado diferença
            if (quantDiferente == false)
            {
                return;                     //retorna
            }

            //limpa comboBox
            cbPortas.Items.Clear();

            //adiciona todas as COM diponíveis na lista
            foreach (string s in SerialPort.GetPortNames())
            {
                cbPortas.Items.Add(s);
            }
            //seleciona a primeira posição da lista
            cbPortas.SelectedIndex = 0;
        }

        

        private void btCom_Click(object sender, EventArgs e)
        {
            atualizaListaCOMs();
        }

        private void btConectar_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.PortName = cbPortas.Items[cbPortas.SelectedIndex].ToString();
                    serialPort1.Open();

                }
                catch
                {
                    return;

                }
                if (serialPort1.IsOpen)
                {
                    btConectar.Text = "Desconectar";
                    cbPortas.Enabled = false;

                }
            }
            else
            {

                try
                {
                    serialPort1.DiscardInBuffer();
                    serialPort1.Close();
                    cbPortas.Enabled = true;
                    btConectar.Text = "Conectar";
                }
                catch
                {
                    return;
                }

            }
        }

        private void TelaInicial_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)  // se porta aberta
                serialPort1.Close();         //fecha a porta
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            RxString = "";
            RxString = serialPort1.ReadExisting();              //le o dado disponível na serial
            this.Invoke(new EventHandler(trataDadoRecebido));   //chama outra thread para escrever o dado no text box
        }

        private void trataDadoRecebido(object sender, EventArgs e)
        {
            

            rxString = RxString;

            switch (rxString)
            {
                case "A":
                    LivreA1.Visible = false;
                    break;

                case "B":
                    LivreA2.Visible = false;
                    break;
                case "C":
                    LivreA3.Visible = false;
                    break;
                case "D":
                    LivreA4.Visible = false;
                    break;
                case "E":
                    LivreB1.Visible = false;
                    
                    break;
                case "F":
                    LivreB2.Visible = false;
                    break;
                case "G":
                    LivreB3.Visible = false;
                    break;
                case "H":
                    LivreB4.Visible = false;
                    break;


                case "I":
                    LivreA1.Visible = true;
                    break;

                case "J":
                    LivreA2.Visible = true;
                    break;
                case "K":
                    LivreA3.Visible = true;
                    break;
                case "L":
                    LivreA4.Visible = true;
                    break;
                case "M":
                    
                    LivreB1.Visible = true;
                    break;
                case "N":
                    LivreB2.Visible = true;
                    break;
                case "O":
                    LivreB3.Visible = true;
                    break;
                case "P":
                    LivreB4.Visible = true;
                    break;

                default:
                    break;
            }


        }

        
    }
}
