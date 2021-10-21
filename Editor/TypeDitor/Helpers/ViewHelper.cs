using System.Linq;
using TypeD.View;

namespace TypeDitor.Helpers
{
    public static class ViewHelper
    {
        public static void InitMenu(System.Windows.Controls.ItemsControl currentMenu, MenuItem item, object model)
        {
            var newMenuItem = new System.Windows.Controls.MenuItem() { Header = item.Name };
            if (item.Click != null)
            {
                newMenuItem.Click += (object sender, System.Windows.RoutedEventArgs e) =>
                {
                    object param = null;
                    if (!string.IsNullOrEmpty(item.ClickParameter))
                    {
                        var type = model.GetType();
                        param = type.GetProperties().FirstOrDefault(p => p.Name == item.ClickParameter).GetValue(model);
                    }
                    item.Click(param);
                };
            }
            currentMenu.Items.Add(newMenuItem);
            currentMenu = newMenuItem;

            foreach (var menu in item.Items)
            {
                InitMenu(currentMenu, menu, model);
            }
        }
    }
}
