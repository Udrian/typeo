using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TypeD.Types;
using TypeDEditor.Controller;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeDEditor.View.Forms.Dialogs
{
    public partial class AddDrawable2dDialog : Form
    {
        private class CbItem
        {
            public TypeOType TypeOType { get; set; }
            public override string ToString()
            {
                return TypeOType.FullName;
            }
        }

        public TypeOType TypeOType { get; private set; }

        public AddDrawable2dDialog()
        {
            InitializeComponent();

            var typelist = Assembly.GetAssembly(typeof(Drawable2d)).GetTypes()
              .Where(t => t.Namespace == "TypeOEngine.Typedeaf.Core.Entities.Drawables")
              .Where(t => t.IsSubclassOf(typeof(Drawable2d)))
              .Select(t => TypeOType.InstantiateTypeOType("Drawable", t.GetTypeInfo(), ProjectController.LoadedProject))
              .ToList();

            foreach (var type in typelist)
            {
                cbDrawable2d.Items.Add(new CbItem() { TypeOType = type });
            }

            foreach (var typeDType in ProjectController.LoadedProject.TypeOTypes.Values)
            {
                if (typeDType.TypeOBaseType == "Drawable")
                {
                    cbDrawable2d.Items.Add(new CbItem() { TypeOType = typeDType });
                }
            }

            if (cbDrawable2d.Items.Count == 0)
            {
                cbDrawable2d.Enabled = false;
                btnAdd.Enabled = false;
            }
            else
            {
                cbDrawable2d.SelectedIndex = 0;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TypeOType = (cbDrawable2d.SelectedItem as CbItem).TypeOType;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
