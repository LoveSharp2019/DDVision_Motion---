using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VControls
{
    public partial class VDataGridView : System.Windows.Forms.DataGridView
    {
        public VDataGridView()
        {
            InitializeComponent();
        }
      
        public VDataGridView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
