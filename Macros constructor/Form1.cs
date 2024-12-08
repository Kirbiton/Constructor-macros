using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace создание_массива_тест
{
    public partial class Form1 : Form
    {
        int indexToMove;
        string[] MacMass;
        Key MacKey = Key.P;
        bool MacEn = false;

        public Form1()
        {
            InitializeComponent();


        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            textBox1.Text = "";
        }

        private void listBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //если нажата левая кнопка мыши, начинаем Drag&Drop
            if (e.Button == MouseButtons.Left)
            {
                //индекс элемента, который мы перемещаем
                indexToMove = listBox1.IndexFromPoint(e.X, e.Y);
                listBox1.DoDragDrop(indexToMove, DragDropEffects.Move);
            }
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            //индекс, куда перемещаем
            //listBox1.PointToClient(new Point(e.X, e.Y)) - необходимо 
            //использовать поскольку в e храниться
            //положение мыши в экранных коородинатах, а эта 
            //функция позволяет преобразовать в клиентские
            int newIndex = listBox1.IndexFromPoint(listBox1.PointToClient(new Point(e.X, e.Y)));
            //если вставка происходит в начало списка
            if (newIndex == -1)
            {
                //получаем перетаскиваемый элемент
                object itemToMove = listBox1.Items[indexToMove];
                //удаляем элемент
                listBox1.Items.RemoveAt(indexToMove);
                //добавляем в конец списка
                listBox1.Items.Add(itemToMove);
            }
            //вставляем где-то в середину списка
            else if (indexToMove != newIndex)
            {
                //получаем перетаскиваемый элемент
                object itemToMove = listBox1.Items[indexToMove];
                //удаляем элемент
                listBox1.Items.RemoveAt(indexToMove);
                //вставляем в конкретную позицию
                listBox1.Items.Insert(newIndex, itemToMove);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MacEn = !MacEn;
            if(MacEn == true)
            {
                pictureBox1.BackColor = Color.Green;
                UpdateMacMass();
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled=false;
                pictureBox1.BackColor = Color.Red;
            }
        }

        private void UpdateMacMass()
        {
            MacMass = new string[listBox1.Items.Count];
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                MacMass[i] = listBox1.Items[i].ToString(); 
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(MacKey))
            {
                timer1.Enabled = !timer1.Enabled;
                for (int i = 0; i < MacMass.Length; i++)
                {
                    SendKeys.SendWait(MacMass[i]);

                }
                timer1.Enabled = !timer1.Enabled;
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

            }
        }

        private void textBox2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            MacKey = KeyInterop.KeyFromVirtualKey((int)e.KeyCode);
            label1.Text = $"Кнопка макроса: {Convert.ToString(MacKey)}";
            textBox2.Text = string.Empty;
        }

    }
}
