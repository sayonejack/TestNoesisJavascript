using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Noesis.Javascript;

namespace TestNoesisJavascript
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (JavascriptContext context = new JavascriptContext())
            {

                Stopwatch sw = new Stopwatch();
                sw.Start();
                int i = 1000000;
                while (i-- > 0)
                {
                    context.SetParameter("ii", 1);
                    context.SetParameter("jj", 2);
                    context.SetParameter("sum", 0);
                    string js = string.Format(@"sum=ii+jj;");
                    context.Run(js);
                    int sum = (int)context.GetParameter("sum");
                }
                sw.Stop();
                long useJS_Total = sw.ElapsedMilliseconds;
                float useJS_Average = ((float)sw.ElapsedMilliseconds) / 1000000;
                sw.Reset();
                sw.Start();
                i = 1000000;
                while (i-- > 0)
                {
                    int ii = 1;
                    int jj = 2;
                    int sum = 0;
                    sum = ii + jj;
                }
                sw.Stop();
                long nouseJS_Total = sw.ElapsedMilliseconds;
                float nouseJS_Average = ((float)sw.ElapsedMilliseconds) / 1000000;
                string msg = "";
                msg += string.Format("useJS_Total: {0}/r/n", useJS_Total);
                msg += string.Format("useJS_Average: {0}/r/n/r/n/r/n", useJS_Average);
                msg += string.Format("nouseJS_Total: {0}/r/n", nouseJS_Total);
                msg += string.Format("nouseJS_Average: {0}/r/n/r/n/r/n", nouseJS_Average);
                MessageBox.Show(msg);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (JavascriptContext context = new JavascriptContext())
            {
                context.SetParameter("value", 1);

                string js = Properties.Resources.JavaScript1;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine(js);
                
                string s = "超細纖維四季沙灘巾 / 浴巾 / 毛巾 5色可選1入-賣點購物22";
                s = Jayrock.Json.JsonString.Enquote(s);
                sb.AppendLine("var value=e(" + s + ")");

                context.Run(sb.ToString());
                string value = context.GetParameter("value").ToString();

                MessageBox.Show(value);
            }
        }
    }
}
