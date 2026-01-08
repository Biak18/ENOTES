using CH.Helper;
using DevExpress.XtraBars.Navigation;

using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;


namespace CH.Grid;

public partial class CHMenu : AccordionControl
{


    public CHMenu()
    {

    }

    #region property
    private string _CD_MENU;
    public string CD_MENU
    {
        get
        {
            return this._CD_MENU;
        }
        set
        {
            this._CD_MENU = value;
        }

    }

    private string _CD_MENU_PARENT;
    public string CD_MENU_PARENT
    {
        get
        {
            return this._CD_MENU_PARENT;
        }
        set
        {
            this._CD_MENU_PARENT = value;
        }

    }

    private string _NM_MENU;
    public string NM_MENU
    {
        get
        {
            return this._NM_MENU;
        }
        set
        {
            this._NM_MENU = value;
        }

    }


    private string _TP_MENU;
    public string TP_MENU
    {
        get
        {
            return this._TP_MENU;
        }
        set
        {
            this._TP_MENU = value;
        }

    }


    private string _CD_MENU_LOCATION;
    public string CD_MENU_LOCATION
    {
        get
        {
            return this._CD_MENU_LOCATION;
        }
        set
        {
            this._CD_MENU_LOCATION = value;
        }

    }

    private string _CD_MENU_CLASS;
    public string CD_MENU_CLASS
    {
        get
        {
            return this._CD_MENU_CLASS;
        }
        set
        {
            this._CD_MENU_CLASS = value;
        }

    }


    private string _LV_MENU;
    public string LV_MENU
    {
        get
        {
            return this._LV_MENU;
        }
        set
        {
            this._LV_MENU = value;
        }

    }

    private string _SEQ_MENU;
    public string SEQ_MENU
    {
        get
        {
            return this._SEQ_MENU;
        }
        set
        {
            this._SEQ_MENU = value;
        }

    }


    private string _CD_MODULE;
    public string CD_MODULE
    {
        get
        {
            return this._CD_MODULE;
        }
        set
        {
            this._CD_MODULE = value;
        }
    }


    private DataTable _DataSource;
    public DataTable DataSource
    {
        get
        {
            return this._DataSource;
        }
        set
        {
            this._DataSource = value;
        }

    }




    #endregion

    #region 강제 CapsLock 설정
    [DllImport("User32.dll")]
    //선언합니다.
    public static extern void keybd_event(
      byte bVk, // virtual-key code 
      byte bScan, // hardware scan code 
      int dwFlags, // function options 
      ref int dwExtraInfo // additional keystroke data 
     );


    [DllImport("user32.dll")]
    private static extern short GetKeyState(int keyCode);

    private void PressKey(byte _Key)
    {
        const int KEYUP = 0x0002;
        int Info = 0;

        keybd_event(_Key, 0, 0, ref Info);   // key 다운
        keybd_event(_Key, 0, KEYUP, ref Info);  // key 업

    }
    #endregion

    #region Event 모음


    #endregion

    #region Method 모음

    public virtual void Clear()
    {
        this.Elements.Clear();
    }

    #endregion

    #region Binding 


    public void Binding()
    {
        DataView dataView = null;

        string str_cd_menu = string.Empty;
        string str_cd_menu_p = string.Empty;
        string str_nm_menu = string.Empty;
        string str_tp_menu = string.Empty;
        string str_dll_menu = string.Empty;
        string str_class_menu = string.Empty;
        string str_cd_module = string.Empty;

        try
        {


            this.Elements.Clear();

            if (_DataSource != null)
                dataView = _DataSource.DefaultView;

            aAccordionControlElement element = null;
            AccordionControlSeparator accordionControlSeparator1 = null;
            this.BeginUpdate();

            for (int j = 0; j < 6; j++)
            {
                dataView.RowFilter = this._LV_MENU + "= " + j;
                dataView.Sort = this._SEQ_MENU;

                for (int i = 0; i < dataView.Count; i++)
                {
                    str_cd_menu = A.GetString(dataView[i][_CD_MENU]);
                    str_cd_menu_p = A.GetString(dataView[i][_CD_MENU_PARENT]);
                    str_nm_menu = A.GetString(dataView[i][_NM_MENU]);

                    str_tp_menu = A.GetString(dataView[i][_TP_MENU]);
                    //str_dll_menu = A.GetString(dataView[i][_CD_MENU_LOCATION]);
                    str_class_menu = A.GetString(dataView[i][_CD_MENU_CLASS]);

                    if (str_tp_menu == "MGM")
                    {
                        element = new aAccordionControlElement();
                        element.Style = ElementStyle.Group;
                        element.Name = str_cd_menu;
                        element.Text = str_nm_menu;
                        element.Tag = str_tp_menu;

                    }
                    else
                    {
                        element = new aAccordionControlElement();
                        element.Style = ElementStyle.Item;
                        element.Name = str_cd_menu;
                        element.Text = str_nm_menu;
                        element.CD_MENU_CLASS = str_class_menu;
                        //element.CD_MENU_LOCATION = str_dll_menu ;
                        element.Tag = str_class_menu;
                    }



                    if (j == 0)
                    {
                        accordionControlSeparator1 = new AccordionControlSeparator();
                        element.HeaderIndent = 12;
                        element.Height = 5;
                        this.Elements.Add(element);
                        //this.Elements.Add(accordionControlSeparator1);
                    }
                    else
                    {
                        this.ForEachElement((el) =>
                        {

                            if (el.Name == str_cd_menu_p)
                            {
                                element.Height = 5;
                                element.HeaderIndent = 7;
                                el.Elements.Add(element);
                            }
                        });
                    }
                }
            }


            this.EndUpdate();


        }
        catch (Exception ex)
        {
        }
    }



    #endregion


    #region AddMenu 


    public void AddMenu(string str_cd_menu_p, string str_cd_menu, string str_nm_menu, string str_class_menu, Bitmap img, Color Fore, Color Back)
    {

        aAccordionControlElement element = null;

        try
        {
            this.ForEachElement((el) =>
            {
                // 이미 추가된 것들 무시.
                if (el.Name == str_cd_menu)
                {
                    throw new Exception();
                }
            });

            this.BeginUpdate();

            element = new aAccordionControlElement();
            element.Style = ElementStyle.Item;
            element.Name = str_cd_menu;
            element.Text = str_nm_menu;
            element.CD_MENU_CLASS = str_class_menu;
            element.Tag = str_class_menu;


            this.ForEachElement((el) =>
            {
                if (el.Name == str_cd_menu_p)
                {
                    element.Height = 5;
                    element.HeaderIndent = 7;
                    element.Image = img; ;
                    element.Appearance.Normal.ForeColor = Fore;
                    element.Appearance.Normal.BackColor = Back;
                    el.Elements.Add(element);

                }
            });


            this.EndUpdate();

        }
        catch (Exception ex)
        {
        }
    }



    #endregion

    #region DeleteMenu 


    public void DeleteMenu(string str_cd_menu_p, string str_cd_menu, string str_nm_menu, string str_class_menu)
    {
        int idx = 0;

        try
        {
            this.BeginUpdate();

            this.ForEachElement((el) =>
            {
                if (el.Name == str_cd_menu)
                {
                    this.Elements[str_cd_menu_p].Elements.Remove(el);

                }
            });


            this.EndUpdate();


        }
        catch (Exception ex)
        {
        }




        #endregion

    }

    public partial class aAccordionControlElement : AccordionControlElement
    {
        #region property

        private string _CD_MENU_LOCATION;
        public string CD_MENU_LOCATION
        {
            get
            {
                return this._CD_MENU_LOCATION;
            }
            set
            {
                this._CD_MENU_LOCATION = value;
            }

        }

        private string _CD_MENU_CLASS;
        public string CD_MENU_CLASS
        {
            get
            {
                return this._CD_MENU_CLASS;
            }
            set
            {
                this._CD_MENU_CLASS = value;
            }

        }
        #endregion

    }
}
