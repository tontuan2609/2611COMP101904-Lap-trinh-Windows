using System.Windows.Forms;

namespace Lab07.Forms
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
            btnClose.Click += (s, e) => this.Close();
        }
    }
}