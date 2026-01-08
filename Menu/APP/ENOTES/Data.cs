namespace ENOTES;

public class MenuItemModel
{
    public string CD_COM { get; set; }
    public string CD_MENU { get; set; }
    public string NM_MENU { get; set; }
    public string NM_NETWINDOW { get; set; }
    public string CD_MENU_PARENT { get; set; }
    public string FG_TYPE { get; set; }  // M = Menu, F = Form
}


public static class Data
{
    public static object GetData()
    {
        List<MenuItemModel> menuData = new List<MenuItemModel>()
    {
        // Parent 1 (Folder)
        new MenuItemModel
        {
            CD_COM = "ERP",
            CD_MENU = "P01",
            NM_MENU = "Master Data",
            NM_NETWINDOW = "",
            CD_MENU_PARENT = null,
            FG_TYPE = "F" // folder
        },
        // Children of Parent 1 (Menu/Form)
        new MenuItemModel
        {
            CD_COM = "ERP",
            CD_MENU = "C01",
            NM_MENU = "Customer",
            NM_NETWINDOW = "SMART_ERP.CustomerForm",
            CD_MENU_PARENT = "P01",
            FG_TYPE = "M"
        },
        new MenuItemModel
        {
            CD_COM = "ERP",
            CD_MENU = "C02",
            NM_MENU = "Supplier",
            NM_NETWINDOW = "SMART_ERP.SupplierForm",
            CD_MENU_PARENT = "P01",
            FG_TYPE = "M"
        },

        // Parent 2 (Folder)
        new MenuItemModel
        {
            CD_COM = "ERP",
            CD_MENU = "P02",
            NM_MENU = "Sales",
            NM_NETWINDOW = "",
            CD_MENU_PARENT = null,
            FG_TYPE = "F" // folder
        },
        // Children of Parent 2 (Menu/Form)
        new MenuItemModel
        {
            CD_COM = "ERP",
            CD_MENU = "C03",
            NM_MENU = "Sales Order",
            NM_NETWINDOW = "SMART_ERP.SalesOrderForm",
            CD_MENU_PARENT = "P02",
            FG_TYPE = "M"
        },
        new MenuItemModel
        {
            CD_COM = "ERP",
            CD_MENU = "C04",
            NM_MENU = "Sales Invoice",
            NM_NETWINDOW = "M_TEST_001",
            CD_MENU_PARENT = "P02",
            FG_TYPE = "M"
        }
    };

        return menuData;
    }


}