using CH.Framework.Win;
using System;

namespace SN_SYS
{
    public partial class M_SYS_BAS_REG_001 : CHFormBase
    {
        M_SYS_BAS_REG_001_D _D = null;
        public M_SYS_BAS_REG_001()
        {
            InitializeComponent();
        }

        private void InitializeControl()
        {

        }

        public override async void OnSearch()
        {
            try
            {
                base.OnSearch();


            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public override void OnAddrow()
        {
            try
            {
                base.OnAddrow();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public override void OnDeleteRow()
        {
            try
            {
                base.OnDeleteRow();


            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public override void OnSave()
        {
            try
            {
                base.OnSave();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
