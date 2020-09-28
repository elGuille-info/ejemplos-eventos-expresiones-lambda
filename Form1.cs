using System;
using System.Windows.Forms;

namespace Expresiones_Lambda_WinFormsApp1_cs
{
    public partial class Form1 : Form
    {
        Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            richTextBoxCodigo = new RichTextBox();
            buttonAbrir = new Button();
            buttonUndo = new Button();
            menuUndo = new ToolStripMenuItem();
        }

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private Button buttonAbrir;
        //private RichTextBox richTextBoxCodigo;
        private Button buttonUndo;
        private ToolStripMenuItem menuUndo;

        // En C# no permite declararlo fuera del cuerpo de un método
        // si no, da error de que el control no está definido (debe ser static)
        // A field initializer cannot reference the non-static field, method or property 'Form1.richTextBoxCodigo'
        // por tanto: definiendo el richtextbox como static ya funciona

        private static RichTextBox richTextBoxCodigo;

        // No hay porqué definir los tipos de los parámetros
        private EventHandler lambdaUndo = (sender, e) => { if (richTextBoxCodigo.CanUndo) richTextBoxCodigo.Undo(); };

        private void Form1_Load(object sender, EventArgs e)
        {
            // Sin usar expresión lambda
            buttonAbrir.Click += buttonAbrir_Click;
            // Usando expresión lambda
            buttonAbrir.Click += (object o, EventArgs e) => Abrir(); 

            buttonUndo.Click += variosUndo_Click;
            menuUndo.Click += variosUndo_Click;

            //EventHandler lambdaUndo = (sender, e) => { if(richTextBoxCodigo.CanUndo) richTextBoxCodigo.Undo(); };

            // Usando expresión lambda definida previamente
            buttonUndo.Click += lambdaUndo;
            menuUndo.Click += lambdaUndo;

            // Usando expresión lambda no definida previamente
            // No hay porqué definir los tipos de los parámetros
            buttonUndo.Click += (object sender, EventArgs e) => { if (richTextBoxCodigo.CanUndo) richTextBoxCodigo.Undo(); };

            menuUndo.Click += (sender, e) => { if (richTextBoxCodigo.CanUndo) richTextBoxCodigo.Undo(); };

        }

        private void buttonAbrir_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void variosUndo_Click(object sender, EventArgs e)
        {
            if (richTextBoxCodigo.CanUndo) richTextBoxCodigo.Undo();
        }

        private void Abrir()
        {
        }
    }
}
