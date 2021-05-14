using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TypeD.Data;
using TypeDEditor.Controller;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeDEditor.View.Forms.Dialogs
{
    public partial class AddDrawable2dDialog : Form
    {
        private class CbItem
        {
            public TypeDType TypeDType { get; set; }
            public override string ToString()
            {
                return TypeDType.FullName;
            }
        }

        public TypeDType TypeDType { get; private set; }

        public AddDrawable2dDialog()
        {
            InitializeComponent();

            var typelist = Assembly.GetAssembly(typeof(Drawable2d)).GetTypes()
              .Where(t => t.Namespace == "TypeOEngine.Typedeaf.Core.Entities.Drawables")
              .Where(t => t.IsSubclassOf(typeof(Drawable2d)))
              .Select(t => new TypeDType() { Name = t.Name, Namespace = t.Namespace, TypeInfo = t.GetTypeInfo(), TypeType = TypeDTypeType.Drawable})
              .ToList();

            foreach (var type in typelist)
            {
                cbDrawable2d.Items.Add(new CbItem() { TypeDType = type });
            }

            foreach (var typeDType in ProjectController.LoadedProject.TypeDTypes.Values)
            {
                if (typeDType.TypeType == TypeDTypeType.Drawable)
                {
                    cbDrawable2d.Items.Add(new CbItem() { TypeDType = typeDType });
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
            TypeDType = (cbDrawable2d.SelectedItem as CbItem).TypeDType;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
