using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace GeekBrain
{
    public partial class MainForm : Form
    {
        bool RuEng = true;
        Random rnd;
        char[] SpecialChars = new char[] {'~','`','!','@','#','$','%','^','&','*','(',')','-','_','=','+',';',':','"','/','?','.','>','<'};
        Dictionary<string, double> metric;
        string pingIP = "";
        bool pingGoEn = true;
        bool pingLoEn = true;
        string pingRespond = "Address: {0}, Time={1}ms, TTL={2}\n";
        string pingFailTO = "Ping timeout\n";
        string pingFailNA = "Network unreachable\n";
        string crcMsgCaption = "Input error";
        string crcMsgBody = "Incorrect number!";
        string pingMsgBody = "You can't ping zeroes!";
        string pingMsgCaption = "Wrong IP Address";
        
        public MainForm()
        {
            InitializeComponent();
            rnd = new Random();
            metric = new Dictionary<string, double>();
            metric.Add("mm", 1);
            metric.Add("мм", 1);
            metric.Add("cm", 10);
            metric.Add("см", 10);
            metric.Add("dm", 100);
            metric.Add("дм", 100);
            metric.Add("m", 1000);
            metric.Add("м", 1000);
            metric.Add("km", 1000000);
            metric.Add("км", 1000000);
            metric.Add("mil", 1609344);
            metric.Add("мили", 1609344);
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            if (!RuEng)
            {
                MessageBox.Show("Остановись и подумай: зачем это вообще нужно?", "Минутка псевдо-философии", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Stay awile and think: what is this all about?", "A minute of pseudo-philosophy", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void tsmiLang_Click(object sender, EventArgs e)
        {
            RuEng = !RuEng;
            if (!RuEng)
            {
                btnShiftDown.Text = "Вниз";
                btnShiftLeft.Text = "Влево";
                btnShiftRight.Text = "Вправо";
                btnShiftUp.Text = "Вверх";
                tabPage1.Text = "Сдвигатель";
                tabPage2.Text = "ГСЧ";
                tabPage3.Text = "Блокнот";
                tabPage4.Text = "ГП";
                tabPage5.Text = "Конвертер";
                lblRndFrom.Text = "От";
                lblRndTo.Text = "До";
                btnRndCount.Text = "Мне повезёт";
                btnRndClr.Text = "Очистить";
                btnRndCopy.Text = "Копировать";
                cbRndDiff.Text = "Только уникальные";
                fileToolStripMenuItem.Text = "Файл";
                notepadToolStripMenuItem.Text = "Блокнот";
                helpToolStripMenuItem.Text = "Помощь";
                tsmiAbout.Text = "О программе";
                tsmiExit.Text = "Выход";
                tsmiLang.Text = "Английский";
                tsmiNpInsDate.Text = "Вставить дату";
                tsmiNpInsTime.Text = "Вставить время";
                tsmiNpLoad.Text = "Загрузить";
                tsmiNpSave.Text = "Сохранить";
                tsmiNpPaste.Text = "Вставить";
                lblKgPlen.Text = "Длина пароля";
                clbKgOptions.Items[0] = "Цифры";
                clbKgOptions.Items[1] = "Прописные буквы";
                clbKgOptions.Items[2] = "Строчные буквы";
                clbKgOptions.Items[3] = "Специальные символы";
                btnKgGenerate.Text = "Создать пароль";
                btnConvDo.Text = "Перевести";
                switch (cbConvMetrics.Text)
                {
                    case "Lengths":
                        cbConvFrom.Items[0] = "мм";
                        cbConvFrom.Items[1] = "см";
                        cbConvFrom.Items[2] = "дм";
                        cbConvFrom.Items[3] = "м";
                        cbConvFrom.Items[4] = "км";
                        cbConvFrom.Items[5] = "мили";
                        cbConvTo.Items[0] = "мм";
                        cbConvTo.Items[1] = "см";
                        cbConvTo.Items[2] = "дм";
                        cbConvTo.Items[3] = "м";
                        cbConvTo.Items[4] = "км";
                        cbConvTo.Items[5] = "мили";
                        break;
                    case "Weights":
                        cbConvFrom.Items[0] = "гр";
                        cbConvFrom.Items[1] = "кг";
                        cbConvFrom.Items[2] = "т";
                        cbConvFrom.Items[3] = "стн";
                        cbConvFrom.Items[4] = "фнт";
                        cbConvFrom.Items[5] = "унц";
                        cbConvTo.Items[0] = "гр";
                        cbConvTo.Items[1] = "кг";
                        cbConvTo.Items[2] = "т";
                        cbConvTo.Items[3] = "стн";
                        cbConvTo.Items[4] = "фнт";
                        cbConvTo.Items[5] = "унц";
                        break;
                    default:
                        break;
                }
                cbConvMetrics.Items[0] = "Длины";
                cbConvMetrics.Items[1] = "Весы";
                btnCRC8.Text = "Рассчитать CRC8";
                rbCRCdec.Text = "Десятичный";
                rbCRChex.Text = "Шестнадцатиричный";
                tabPage7.Text = "Пинг";
                btnPing.Text = "Пнуть Google!";
                btnPingLoc.Text = "Пнуть шлюз!";
                pingRespond = "Адрес: {0}, Время={1}мс, TTL={2}\n";
                pingFailTO = "Время истекло\n";
                pingFailNA = "Сеть недоступна\n";
                crcMsgBody = "Неверный ввод числа!";
                crcMsgCaption = "Ошибка ввода";
                btnPingCustom.Text = "Пнуть!";
                pingToolStripMenuItem.Text = "Пинг";
                customToolStripMenuItem.Text = "Вручную";
                standardToolStripMenuItem.Text = "Стандартный";
                pingMsgBody = "Нельзя пнуть нули!";
                pingMsgCaption = "Неверный адрес";
                lblMTSCaption.Text = "Умножение в Сдвиг";
                tabPage8.Text = "УвС";
                btnMTScalc.Text = "Рассчитать!";

            }
            else
            {
                btnShiftDown.Text = "Down";
                btnShiftLeft.Text = "Left";
                btnShiftRight.Text = "Right";
                btnShiftUp.Text = "Up";
                tabPage1.Text = "Shifter";
                tabPage2.Text = "Randomizer";
                tabPage3.Text = "Notepad";
                tabPage4.Text = "Keygen";
                tabPage5.Text = "Converter";
                lblRndFrom.Text = "From";
                lblRndTo.Text = "To";
                btnRndCount.Text = "Get lucky";
                btnRndClr.Text = "Clear";
                btnRndCopy.Text = "Copy";
                cbRndDiff.Text = "Log unique only";
                fileToolStripMenuItem.Text = "File";
                notepadToolStripMenuItem.Text = "Notepad";
                helpToolStripMenuItem.Text = "Help";
                tsmiAbout.Text = "About";
                tsmiExit.Text = "Exit";
                tsmiLang.Text = "Russian";
                tsmiNpInsDate.Text = "Insert date";
                tsmiNpInsTime.Text = "Insert time";
                tsmiNpLoad.Text = "Load";
                tsmiNpSave.Text = "Save";
                tsmiNpPaste.Text = "Paste";
                lblKgPlen.Text = "Password length";
                btnKgGenerate.Text = "Generate";
                clbKgOptions.Items[0] = "Digits";
                clbKgOptions.Items[1] = "Big letters";
                clbKgOptions.Items[2] = "Small letters";
                clbKgOptions.Items[3] = "Special symbols";
                btnConvDo.Text = "Convert";
                switch (cbConvMetrics.Text)
                {
                    case "Длины":
                        cbConvFrom.Items[0] = "mm";
                        cbConvFrom.Items[1] = "cm";
                        cbConvFrom.Items[2] = "dm";
                        cbConvFrom.Items[3] = "m";
                        cbConvFrom.Items[4] = "km";
                        cbConvFrom.Items[5] = "mil";
                        cbConvTo.Items[0] = "mm";
                        cbConvTo.Items[1] = "cm";
                        cbConvTo.Items[2] = "dm";
                        cbConvTo.Items[3] = "m";
                        cbConvTo.Items[4] = "km";
                        cbConvTo.Items[5] = "mil";
                        break;
                    case "Weights":
                        cbConvFrom.Items[0] = "gr";
                        cbConvFrom.Items[1] = "kg";
                        cbConvFrom.Items[2] = "t";
                        cbConvFrom.Items[3] = "stn";
                        cbConvFrom.Items[4] = "lb";
                        cbConvFrom.Items[5] = "oz";
                        cbConvTo.Items[0] = "gr";
                        cbConvTo.Items[1] = "kg";
                        cbConvTo.Items[2] = "t";
                        cbConvTo.Items[3] = "stn";
                        cbConvTo.Items[4] = "lb";
                        cbConvTo.Items[5] = "oz";
                        break;
                    default:
                        break;
                }
                cbConvMetrics.Items[0] = "Lengths";
                cbConvMetrics.Items[1] = "Weights";
                btnCRC8.Text = "Count CRC8";
                rbCRCdec.Text = "Decimal";
                rbCRChex.Text = "Hexadecimal";
                tabPage7.Text = "Ping";
                btnPing.Text = "Ping Google!";
                btnPingLoc.Text = "Ping local!";
                pingRespond = "Address: {0}, Time={1}ms, TTL={2}\n";
                pingFailTO = "Ping timeout\n";
                pingFailNA = "Network unreachable\n";
                crcMsgBody = "Incorrect number!";
                crcMsgCaption = "Input error";
                btnPingCustom.Text = "Ping It!";
                pingToolStripMenuItem.Text = "Ping";
                customToolStripMenuItem.Text = "Custom";
                standardToolStripMenuItem.Text = "Standard";
                pingMsgBody = "You can't ping zeroes!";
                pingMsgCaption = "Wrong IP Address";
                lblMTSCaption.Text = "Multiply to Shift";
                tabPage8.Text = "MtS";
                btnMTScalc.Text = "Count!";
            }
        }

        private void btnShiftUp_Click(object sender, EventArgs e)
        {
            int shftrInt;
            if (tbShiftDisplay.Text[tbShiftDisplay.Text.Length - 1].ToString() == " ") { tbShiftDisplay.Text = tbShiftDisplay.Text.Remove(tbShiftDisplay.Text.Length - 1); }
            string[] shftrToChange = tbShiftDisplay.Text.Split(' ');
            tbShiftDisplay.Clear();
            for (int i = 0; i < shftrToChange.Length; i++)
            {
                int.TryParse(shftrToChange[i], out shftrInt);
                shftrToChange[i] = (shftrInt + 1).ToString();
                if (i == shftrToChange.Length-1)
                    tbShiftDisplay.AppendText(shftrToChange[i]);
                else
                    tbShiftDisplay.AppendText(shftrToChange[i] + " ");
            }

        }

        private void btnShiftDown_Click(object sender, EventArgs e)
        {
            int shftrInt;
            if (tbShiftDisplay.Text[tbShiftDisplay.Text.Length - 1].ToString() == " ") { tbShiftDisplay.Text = tbShiftDisplay.Text.Remove(tbShiftDisplay.Text.Length - 1); }
            string[] shftrToChange = tbShiftDisplay.Text.Split(' ');
            tbShiftDisplay.Clear();
            for (int i = 0; i < shftrToChange.Length; i++)
            {
                int.TryParse(shftrToChange[i], out shftrInt);
                if (shftrInt > 0) shftrToChange[i] = (shftrInt - 1).ToString(); else shftrToChange[i] = "0";
                if (i == shftrToChange.Length - 1)
                    tbShiftDisplay.AppendText(shftrToChange[i]);
                else
                    tbShiftDisplay.AppendText(shftrToChange[i] + " ");
            }
        }

        private void btnShiftRight_Click(object sender, EventArgs e)
        {
            int shftrInt;
            if (tbShiftDisplay.Text[tbShiftDisplay.Text.Length - 1].ToString() == " ") { tbShiftDisplay.Text = tbShiftDisplay.Text.Remove(tbShiftDisplay.Text.Length - 1); }
            string[] shftrToChange = tbShiftDisplay.Text.Split(' ');
            tbShiftDisplay.Clear();
            for (int i = 0; i < shftrToChange.Length; i++)
            {
                int.TryParse(shftrToChange[i], out shftrInt);
                if (shftrInt > 0) shftrToChange[i] = (shftrInt >> 1).ToString(); else shftrToChange[i] = "0";
                if (i == shftrToChange.Length - 1)
                    tbShiftDisplay.AppendText(shftrToChange[i]);
                else
                    tbShiftDisplay.AppendText(shftrToChange[i] + " ");
            }
        }

        private void btnShiftLeft_Click(object sender, EventArgs e)
        {
            int shftrInt;
            if (tbShiftDisplay.Text[tbShiftDisplay.Text.Length - 1].ToString() == " ") { tbShiftDisplay.Text = tbShiftDisplay.Text.Remove(tbShiftDisplay.Text.Length - 1); }
            string[] shftrToChange = tbShiftDisplay.Text.Split(' ');
            tbShiftDisplay.Clear();
            for (int i = 0; i < shftrToChange.Length; i++)
            {
                int.TryParse(shftrToChange[i], out shftrInt);
                if (shftrInt > 0) shftrToChange[i] = (shftrInt << 1).ToString(); else shftrToChange[i] = "0";
                if (i == shftrToChange.Length - 1)
                    tbShiftDisplay.AppendText(shftrToChange[i]);
                else
                    tbShiftDisplay.AppendText(shftrToChange[i] + " ");
            }
        }

        private void btnRndCount_Click(object sender, EventArgs e)
        {
            int n;
            if (nudRndFrom.Value > nudRndTo.Value)
                n = rnd.Next((Convert.ToInt32(nudRndTo.Value) + 1), Convert.ToInt32(nudRndFrom.Value));
            else 
                n = rnd.Next(Convert.ToInt32(nudRndFrom.Value), (Convert.ToInt32(nudRndTo.Value)+1));
            lblRndOut.Text = n.ToString();
            if (cbRndDiff.Checked)
            {
                if (tbRndLog.Text.IndexOf(n.ToString()) == -1) tbRndLog.Text = string.Format("{0} \r\n{1}", n, tbRndLog.Text);
            }
            else
            {
                tbRndLog.Text = string.Format("{0} \r\n{1}", n, tbRndLog.Text);
            }
             
        }

        private void btnRndClr_Click(object sender, EventArgs e)
        {
            tbRndLog.Clear();
        }

        private void btnRndCopy_Click(object sender, EventArgs e)
        {
            if(tbRndLog.Text != "")
                Clipboard.SetText(tbRndLog.Text);
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            notepadToolStripMenuItem.Enabled = true;
        }

        private void tabPage3_Leave(object sender, EventArgs e)
        {
            notepadToolStripMenuItem.Enabled = false;
        }

        private void tsmiNpInsDate_Click(object sender, EventArgs e)
        {
            rtbNpOut.AppendText(DateTime.Now.ToShortDateString() + " ");
        }

        private void tsmiNpInsTime_Click(object sender, EventArgs e)
        {
            rtbNpOut.AppendText(DateTime.Now.ToShortTimeString() + " ");
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (!RuEng)
            {
                MessageBox.Show("Овчинников Иван Игоревич, 26 мая 2016г.", "Минутка самолюбования", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Wayne Harry Shepard. May 26th, 2016.", "A minute of narcissism", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void tsmiNpSave_Click(object sender, EventArgs e)
        {
            try
            {
                rtbNpOut.SaveFile("notepad.dat");
            }
            catch
            {
                if (!RuEng)
                {
                    MessageBox.Show("Ошибка при сохранении", "Что-то пошло не так", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("Error saving file", "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void tsmiNpLoad_Click(object sender, EventArgs e)
        {
            try
            {
                rtbNpOut.LoadFile("notepad.dat");
            }
            catch
            {
                if (!RuEng)
                {
                    MessageBox.Show("Ошибка при загрузке", "Что-то пошло не так", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("Error loading file", "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }

        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbNpOut.AppendText(Clipboard.GetText());
        }

        private void btnKgGenerate_Click(object sender, EventArgs e)
        {
            if (clbKgOptions.CheckedItems.Count == 0)
            {
                if (!RuEng)
                {
                    MessageBox.Show("Не могу создать пустой пароль", "Что-то пошло не так", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("Can't create an empty password", "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                return;
            }
            String password = "";
            for (int i = 0; i < nudKgLength.Value; i++)
            {
                int n = rnd.Next(clbKgOptions.CheckedItems.Count);
                string s = clbKgOptions.CheckedItems[n].ToString();
                switch (s)
                {
                    case "Digits":
                    case "Цифры": password += rnd.Next(10); break;
                    case "Big letters":
                    case "Прописные буквы": password += Convert.ToChar(rnd.Next(65, 88)); break;
                    case "Small letters":
                    case "Строчные буквы": password += Convert.ToChar(rnd.Next(97, 122)); break;
                    case "Special symbols":
                    case "Специальные символы": password += SpecialChars[rnd.Next(SpecialChars.Length-1)]; break;
                    default: break;
                }
            }
            tbKgOut.Text = password;
            Clipboard.SetText(password);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            clbKgOptions.SetItemChecked(0, true);
            clbKgOptions.SetItemChecked(2, true);
            clbKgOptions.SetItemChecked(3, true);
        }

        private void btnConvDo_Click(object sender, EventArgs e)
        {
            if ((cbConvTo.Text == "") || (cbConvFrom.Text == "") || (cbConvMetrics.Text == "")) { }
            else
            {
                Double valFrom = metric[cbConvFrom.Text];
                Double valTo = metric[cbConvTo.Text];
                Double rslt = Convert.ToDouble(tbConvFrom.Text);
                tbConvTo.Text = (rslt * valFrom / valTo).ToString();
            }

        }

        private void tbConvFrom_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbConvFrom.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbConvFrom.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbConvFrom.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbConvFrom.Text = "0";
            }

            if (a > 9999)
            {
                tbConvFrom.Text = "0";
            }

        }

        private void btnConvSwap_Click(object sender, EventArgs e)
        {
            string swap = cbConvFrom.Text;
            cbConvFrom.Text = cbConvTo.Text;
            cbConvTo.Text = swap;
        }

        private void cbConvMetrics_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbConvMetrics.Text)
            {
                case "Lengths":
                case "Длины":
                    metric.Clear();
                    metric.Add("mm", 1);
                    metric.Add("мм", 1);
                    metric.Add("cm", 10);
                    metric.Add("см", 10);
                    metric.Add("dm", 100);
                    metric.Add("дм", 100);
                    metric.Add("m", 1000);
                    metric.Add("м", 1000);
                    metric.Add("km", 1000000);
                    metric.Add("км", 1000000);
                    metric.Add("mil", 1609344);
                    metric.Add("мили", 1609344);
                    if (!RuEng)
                    {
                        cbConvFrom.Items[0] = "мм";
                        cbConvFrom.Items[1] = "см";
                        cbConvFrom.Items[2] = "дм";
                        cbConvFrom.Items[3] = "м";
                        cbConvFrom.Items[4] = "км";
                        cbConvFrom.Items[5] = "мили";
                        cbConvTo.Items[0] = "мм";
                        cbConvTo.Items[1] = "см";
                        cbConvTo.Items[2] = "дм";
                        cbConvTo.Items[3] = "м";
                        cbConvTo.Items[4] = "км";
                        cbConvTo.Items[5] = "мили";
                    }
                    else
                    {
                        cbConvFrom.Items[0] = "mm";
                        cbConvFrom.Items[1] = "cm";
                        cbConvFrom.Items[2] = "dm";
                        cbConvFrom.Items[3] = "m";
                        cbConvFrom.Items[4] = "km";
                        cbConvFrom.Items[5] = "mil";
                        cbConvTo.Items[0] = "mm";
                        cbConvTo.Items[1] = "cm";
                        cbConvTo.Items[2] = "dm";
                        cbConvTo.Items[3] = "m";
                        cbConvTo.Items[4] = "km";
                        cbConvTo.Items[5] = "mil";
                    }
                    break;
                case "Weights":
                case "Весы":
                    metric.Clear();
                    metric.Add("gr", 1);
                    metric.Add("гр", 1);
                    metric.Add("кг", 1000);
                    metric.Add("kg", 1000);
                    metric.Add("t", 1000000);
                    metric.Add("т", 1000000);
                    metric.Add("stn", 6350.29);
                    metric.Add("стн", 6350.29);
                    metric.Add("lb", 453.592);
                    metric.Add("фнт", 453.592);
                    metric.Add("oz", 28.3495);
                    metric.Add("унц", 28.3495);
                    if (!RuEng)
                    {
                        cbConvFrom.Items[0] = "гр";
                        cbConvFrom.Items[1] = "кг";
                        cbConvFrom.Items[2] = "т";
                        cbConvFrom.Items[3] = "стн";
                        cbConvFrom.Items[4] = "фнт";
                        cbConvFrom.Items[5] = "унц";
                        cbConvTo.Items[0] = "гр";
                        cbConvTo.Items[1] = "кг";
                        cbConvTo.Items[2] = "т";
                        cbConvTo.Items[3] = "стн";
                        cbConvTo.Items[4] = "фнт";
                        cbConvTo.Items[5] = "унц";
                    }
                    else
                    {
                        cbConvFrom.Items[0] = "gr";
                        cbConvFrom.Items[1] = "kg";
                        cbConvFrom.Items[2] = "t";
                        cbConvFrom.Items[3] = "stn";
                        cbConvFrom.Items[4] = "lb";
                        cbConvFrom.Items[5] = "oz";
                        cbConvTo.Items[0] = "gr";
                        cbConvTo.Items[1] = "kg";
                        cbConvTo.Items[2] = "t";
                        cbConvTo.Items[3] = "stn";
                        cbConvTo.Items[4] = "lb";
                        cbConvTo.Items[5] = "oz";
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnCRC8_Click(object sender, EventArgs e)
        {
            int len = tbCRClen.Value-1;
            int fulllen = tbCRClen.Value - 1;
            int[] array = new int[7] {
                Convert.ToInt16(tbCRC0.Text),
                Convert.ToInt16(tbCRC1.Text),
                Convert.ToInt16(tbCRC2.Text),
                Convert.ToInt16(tbCRC3.Text),
                Convert.ToInt16(tbCRC4.Text),
                Convert.ToInt16(tbCRC5.Text),
                Convert.ToInt16(tbCRC6.Text)
            };
            
            int[] Crc8Table = new int[256] {
                0x00, 0x31, 0x62, 0x53, 0xC4, 0xF5, 0xA6, 0x97,
                0xB9, 0x88, 0xDB, 0xEA, 0x7D, 0x4C, 0x1F, 0x2E,
                0x43, 0x72, 0x21, 0x10, 0x87, 0xB6, 0xE5, 0xD4,
                0xFA, 0xCB, 0x98, 0xA9, 0x3E, 0x0F, 0x5C, 0x6D,
                0x86, 0xB7, 0xE4, 0xD5, 0x42, 0x73, 0x20, 0x11,
                0x3F, 0x0E, 0x5D, 0x6C, 0xFB, 0xCA, 0x99, 0xA8,
                0xC5, 0xF4, 0xA7, 0x96, 0x01, 0x30, 0x63, 0x52,
                0x7C, 0x4D, 0x1E, 0x2F, 0xB8, 0x89, 0xDA, 0xEB,
                0x3D, 0x0C, 0x5F, 0x6E, 0xF9, 0xC8, 0x9B, 0xAA,
                0x84, 0xB5, 0xE6, 0xD7, 0x40, 0x71, 0x22, 0x13,
                0x7E, 0x4F, 0x1C, 0x2D, 0xBA, 0x8B, 0xD8, 0xE9,
                0xC7, 0xF6, 0xA5, 0x94, 0x03, 0x32, 0x61, 0x50,
                0xBB, 0x8A, 0xD9, 0xE8, 0x7F, 0x4E, 0x1D, 0x2C,
                0x02, 0x33, 0x60, 0x51, 0xC6, 0xF7, 0xA4, 0x95,
                0xF8, 0xC9, 0x9A, 0xAB, 0x3C, 0x0D, 0x5E, 0x6F,
                0x41, 0x70, 0x23, 0x12, 0x85, 0xB4, 0xE7, 0xD6,
                0x7A, 0x4B, 0x18, 0x29, 0xBE, 0x8F, 0xDC, 0xED,
                0xC3, 0xF2, 0xA1, 0x90, 0x07, 0x36, 0x65, 0x54,
                0x39, 0x08, 0x5B, 0x6A, 0xFD, 0xCC, 0x9F, 0xAE,
                0x80, 0xB1, 0xE2, 0xD3, 0x44, 0x75, 0x26, 0x17,
                0xFC, 0xCD, 0x9E, 0xAF, 0x38, 0x09, 0x5A, 0x6B,
                0x45, 0x74, 0x27, 0x16, 0x81, 0xB0, 0xE3, 0xD2,
                0xBF, 0x8E, 0xDD, 0xEC, 0x7B, 0x4A, 0x19, 0x28,
                0x06, 0x37, 0x64, 0x55, 0xC2, 0xF3, 0xA0, 0x91,
                0x47, 0x76, 0x25, 0x14, 0x83, 0xB2, 0xE1, 0xD0,
                0xFE, 0xCF, 0x9C, 0xAD, 0x3A, 0x0B, 0x58, 0x69,
                0x04, 0x35, 0x66, 0x57, 0xC0, 0xF1, 0xA2, 0x93,
                0xBD, 0x8C, 0xDF, 0xEE, 0x79, 0x48, 0x1B, 0x2A,
                0xC1, 0xF0, 0xA3, 0x92, 0x05, 0x34, 0x67, 0x56,
                0x78, 0x49, 0x1A, 0x2B, 0xBC, 0x8D, 0xDE, 0xEF,
                0x82, 0xB3, 0xE0, 0xD1, 0x46, 0x77, 0x24, 0x15,
                0x3B, 0x0A, 0x59, 0x68, 0xFF, 0xCE, 0x9D, 0xAC
            };
            
            
            int crc = 255;
            //tbCRC8Result.Text = (crc ^ array[len-1]).ToString();
            while (len >= 0)
            {
                crc = Crc8Table[crc ^ array[fulllen-(len)]];
                len--;
            }
            string hexOutput = String.Format("{0:X}", crc);
            if (rbCRChex.Checked)
            {
                tbCRC8Result.Text = "0x" + hexOutput;// crc.ToString();
                Clipboard.SetText("0x" + hexOutput);//crc.ToString());
            }
            else if (rbCRCdec.Checked)
            {
                tbCRC8Result.Text = crc.ToString();
                Clipboard.SetText(crc.ToString());
            }
        }

        private void tbCRC0_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC0.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC0.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC0.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC0.Text = "0";
            }
            
            if (a > 255)
            {
                tbCRC0.Text = "0";
            }
        }

        private void tbCRC1_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC1.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC1.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC1.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC1.Text = "0";
            }

            if (a > 255)
            {
                tbCRC1.Text = "0";
            }
        }

        private void tbCRC2_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC2.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC2.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC2.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC2.Text = "0";
            }

            if (a > 255)
            {
                tbCRC2.Text = "0";
            }
        }

        private void tbCRC3_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC3.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC3.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC3.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC3.Text = "0";
            }

            if (a > 255)
            {
                tbCRC3.Text = "0";
            }
        }

        private void tbCRC4_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC4.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC4.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC4.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC4.Text = "0";
            }

            if (a > 255)
            {
                tbCRC4.Text = "0";
            }
        }

        private void tbCRC5_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC5.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC5.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC5.Text = "0";
                    a = 0;
                }              
            }
            else
            {
                a = 0;
                tbCRC5.Text = "0";
            }

            if (a > 255)
            {
                tbCRC5.Text = "0";
            }
        }

        private void tbCRC6_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC6.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC6.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC6.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC6.Text = "0";
            }

            if (a > 255)
            {
                tbCRC6.Text = "0";
            }
        }

        private void btnPing_Click(object sender, EventArgs e)
        {
            pingIP = "8.8.8.8";
            if (pingGoEn)
            {
                tmrPing.Enabled = true;
                btnPingLoc.Enabled = false;
            }
            else
            {
                tmrPing.Enabled = false;
                btnPingLoc.Enabled = true;
            }
            pingGoEn = !pingGoEn;

        }

        private void btnPingLoc_Click(object sender, EventArgs e)
        {
            pingIP = "192.168.0.1";
            if (pingLoEn)
            {
                tmrPing.Enabled = true;
                btnPing.Enabled = false;
            }
            else
            {
                tmrPing.Enabled = false;
                btnPing.Enabled = true;
            }
            pingLoEn = !pingLoEn;
        }

        private void tmrPing_Tick(object sender, EventArgs e)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 700;
            PingReply reply = pingSender.Send(pingIP, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                rtbPing.Text = (DateTime.Now.ToLongTimeString() + ": " + String.Format(pingRespond, reply.Address.ToString(), reply.RoundtripTime, reply.Options.Ttl)) + rtbPing.Text;
            }
            else if(reply.Status == IPStatus.TimedOut)
            {
                rtbPing.Text = DateTime.Now.ToLongTimeString() + ": " + pingFailTO + rtbPing.Text;
            }
            else
            {
                rtbPing.Text = DateTime.Now.ToLongTimeString() + ": " + pingFailNA + rtbPing.Text;
            }
        }

        private void tbShiftDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a;
            try
            {
                a = Convert.ToInt16(e.KeyChar.ToString());
                tbShiftDisplay.AppendText(a.ToString());
            }
            catch (Exception)
            {
                a = 0;
                if (e.KeyChar.ToString() != " ")
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                } else
                {
                    tbShiftDisplay.AppendText(" ");
                }
            } 
            
            e.Handled = true;
            
        }

        private void tbCRClen_Scroll(object sender, EventArgs e)
        {
            switch (tbCRClen.Value)
            {
                case 1: tbCRC1.Enabled = false; tbCRC2.Enabled = false; tbCRC3.Enabled = false; tbCRC4.Enabled = false; tbCRC5.Enabled = false; tbCRC6.Enabled = false; break;
                case 2: tbCRC2.Enabled = false; tbCRC1.Enabled = true; break;
                case 3: tbCRC3.Enabled = false; tbCRC2.Enabled = true; break;
                case 4: tbCRC4.Enabled = false; tbCRC3.Enabled = true; break;
                case 5: tbCRC5.Enabled = false; tbCRC4.Enabled = true; break;
                case 6: tbCRC6.Enabled = false; tbCRC5.Enabled = true; break;
                case 7: tbCRC7.Enabled = false; tbCRC6.Enabled = true; btnCRC8.Enabled = true; btnCRChex.Enabled = false; break;
                case 8: tbCRC8.Enabled = false; tbCRC7.Enabled = true; btnCRC8.Enabled = false; btnCRChex.Enabled = true; break;
                case 9: tbCRC9.Enabled = false; tbCRC8.Enabled = true; break;
                case 10: tbCRC10.Enabled = false; tbCRC9.Enabled = true; break;
                case 11: tbCRC11.Enabled = false; tbCRC10.Enabled = true; break;
                case 12: tbCRC12.Enabled = false; tbCRC11.Enabled = true; break;
                case 13: tbCRC13.Enabled = false; tbCRC12.Enabled = true; break;
                case 14: tbCRC13.Enabled = true; break;
                default: break;
            }
        }

        private void tabPage7_Leave(object sender, EventArgs e)
        {
            pingToolStripMenuItem.Enabled = false;
        }

        private void tabPage7_Enter(object sender, EventArgs e)
        {
            pingToolStripMenuItem.Enabled = true;
        }

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPingLoc.Visible = true;
            btnPing.Visible = true;
            pingGoEn = true;
            pingLoEn = true;
            btnPingLoc.Enabled = true;
            btnPing.Enabled = true;
            tbPingA1.Visible = false;
            tbPingA2.Visible = false;
            tbPingA3.Visible = false;
            tbPingA4.Visible = false;
            btnPingCustom.Visible = false;
            customToolStripMenuItem.Checked = false;
            standardToolStripMenuItem.Checked = true;
            tmrPing.Enabled = false;
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPingLoc.Visible = false;
            btnPing.Visible = false;
            tbPingA1.Visible = true;
            tbPingA2.Visible = true;
            tbPingA3.Visible = true;
            tbPingA4.Visible = true;
            btnPingCustom.Visible = true;
            customToolStripMenuItem.Checked = true;
            standardToolStripMenuItem.Checked = false;
            tmrPing.Enabled = false;
        }

        private void btnPingCustom_Click(object sender, EventArgs e)
        {
            string ip;
            if ((tbPingA1.Text == "0") || (tbPingA4.Text == "0")) {
                MessageBox.Show(pingMsgBody, pingMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                ip = "192.168.0.2";
                tbPingA1.Text = "192";
                tbPingA2.Text = "168";
                tbPingA3.Text = "0";
                tbPingA4.Text = "2";
            }
            else
            {
                ip = string.Format("{0}.{1}.{2}.{3}", tbPingA1.Text, tbPingA2.Text, tbPingA3.Text, tbPingA4.Text);
            }
            pingIP = ip;
            if (tmrPing.Enabled)
            {
                tmrPing.Enabled = false;
            }
            else
            {
                tmrPing.Enabled = true;
            }
        }

        private void tbPingA1_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbPingA1.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbPingA1.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbPingA1.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbPingA1.Text = "0";
            }

            if (a > 255)
            {
                tbPingA1.Text = "0";
            }
        }

        private void tbPingA2_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbPingA2.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbPingA2.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbPingA2.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbPingA2.Text = "0";
            }

            if (a > 255)
            {
                tbPingA2.Text = "0";
            }
        }

        private void tbPingA3_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbPingA3.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbPingA3.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbPingA3.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbPingA3.Text = "0";
            }

            if (a > 255)
            {
                tbPingA3.Text = "0";
            }
        }

        private void tbPingA4_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbPingA4.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbPingA4.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbPingA4.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbPingA4.Text = "0";
            }

            if (a > 255)
            {
                tbPingA4.Text = "0";
            }
        }

        private void btnCRChex_Click(object sender, EventArgs e)
        {
            int crc16v = 0;
            int[] arr = { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
            if (tbCRC0.Enabled != false)
                arr[0] = Convert.ToInt32(tbCRC0.Text);
            if (tbCRC1.Enabled != false)
                arr[1] = Convert.ToInt32(tbCRC1.Text);
            if (tbCRC2.Enabled != false)
                arr[2] = Convert.ToInt32(tbCRC2.Text);
            if (tbCRC3.Enabled != false)
                arr[3] = Convert.ToInt32(tbCRC3.Text);
            if (tbCRC4.Enabled != false)
                arr[4] = Convert.ToInt32(tbCRC4.Text);
            if (tbCRC5.Enabled != false)
                arr[5] = Convert.ToInt32(tbCRC5.Text);
            if (tbCRC6.Enabled != false)
                arr[6] = Convert.ToInt32(tbCRC6.Text);
            if (tbCRC7.Enabled != false)
                arr[7] = Convert.ToInt32(tbCRC7.Text);
            if (tbCRC8.Enabled != false)
                arr[8] = Convert.ToInt32(tbCRC8.Text);
            if (tbCRC9.Enabled != false)
                arr[9] = Convert.ToInt32(tbCRC9.Text);
            if (tbCRC10.Enabled != false)
                arr[10] = Convert.ToInt32(tbCRC10.Text);
            if (tbCRC11.Enabled != false)
                arr[11] = Convert.ToInt32(tbCRC11.Text);
            if (tbCRC12.Enabled != false)
                arr[12] = Convert.ToInt32(tbCRC12.Text);
            if (tbCRC13.Enabled != false)
                arr[13] = Convert.ToInt32(tbCRC13.Text);

            int var3 = 0;

            for (int j = 0; j < tbCRClen.Value; j++)
            {
                int var2 = arr[j];
                for (int i = 7; i >= 0; --i)
                {
                    var2 <<= 1;                             //var2 = var2 << 1;
                    var3 = var2 >> 8 & 1;              //var3 = (var2 >> 8) & 1;
                    if ((crc16v & 32768) != 0) //'耀' 32768
                    {
                        crc16v = (crc16v << 1) + var3 ^ 4129;
                    }
                    else
                    {
                        crc16v = (crc16v << 1) + var3;
                    }
                }
                crc16v &= '\uffff';
            }
            if (rbCRChex.Checked)
            {
                tbCRC8Result.Text = string.Format("0x{0:X}", crc16v);
            }
            else
            {
                tbCRC8Result.Text = crc16v.ToString();
            }
        }

        private void tbCRC7_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC7.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC7.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC7.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC7.Text = "0";
            }

            if (a > 255)
            {
                tbCRC7.Text = "0";
            }

        }

        private void tbCRC8_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC8.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC8.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC8.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC8.Text = "0";
            }

            if (a > 255)
            {
                tbCRC8.Text = "0";
            }

        }

        private void tbCRC9_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC9.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC9.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC9.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC9.Text = "0";
            }

            if (a > 255)
            {
                tbCRC9.Text = "0";
            }

        }

        private void tbCRC10_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC10.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC10.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC10.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC10.Text = "0";
            }

            if (a > 255)
            {
                tbCRC10.Text = "0";
            }

        }

        private void tbCRC11_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC11.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC11.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC11.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC11.Text = "0";
            }

            if (a > 255)
            {
                tbCRC11.Text = "0";
            }

        }

        private void tbCRC12_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC12.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC12.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC12.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC12.Text = "0";
            }

            if (a > 255)
            {
                tbCRC12.Text = "0";
            }

        }

        private void tbCRC13_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbCRC13.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbCRC13.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbCRC13.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbCRC13.Text = "0";
            }

            if (a > 255)
            {
                tbCRC13.Text = "0";
            }

        }

        private void tbMTSin_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (tbMTSin.Text != "")
            {
                try
                {
                    a = Convert.ToInt16(tbMTSin.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(crcMsgBody, crcMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tbMTSin.Text = "0";
                    a = 0;
                }
            }
            else
            {
                a = 0;
                tbMTSin.Text = "0";
            }

            if (a > 16777215)
            {
                tbMTSin.Text = "0";
            }
        }

        public int calcshift(int input)
        {
            int output = 0;
            int i = 0;
            while (input >= (1 << i))
            {
                i++;
                output = i - 1;
            }
            return output;
        }

        private void btnMTScalc_Click(object sender, EventArgs e)
        {
            int shiftvalue = 0; 
            int start = 0;
            int shifted = 0;
            int mod = 0;
            tbMTSout.Text = "";
            start = Convert.ToInt32(tbMTSin.Text);
            mod = start;
            if (start < 2) return;

            do
            {
                shiftvalue = calcshift(mod);
                mod = mod - (1 << shiftvalue);
                if (mod > 1)
                {
                    tbMTSout.AppendText("n<<" + shiftvalue.ToString() + " + ");
                } else {
                    tbMTSout.AppendText("n<<" + shiftvalue.ToString());
                }
                //tbMTSout.AppendText("n<<" + shiftvalue.ToString() + " + ");
            } while (mod > 1);
            if (mod == 1)
            {
                tbMTSout.AppendText(" + n");
            }
            Clipboard.SetText(tbMTSout.Text);
        }
    }
}
