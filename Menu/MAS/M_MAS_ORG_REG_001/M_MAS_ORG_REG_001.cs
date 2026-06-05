using CH.Framework.Common;
using CH.Framework.Win;
using CH.Framework.Win.Controls;
using CH.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CH.Helper.aGridHelper;

namespace MAS;

// Company registration
public partial class M_MAS_ORG_REG_001 : CHFormBase
{
    #region ▶ Initialize ----------
    Dictionary<string, Image> imgCollection = new Dictionary<string, Image>();
    M_MAS_ORG_REG_001_D _D = null;
    public M_MAS_ORG_REG_001()
    {
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _D = new M_MAS_ORG_REG_001_D();
        InitializeGrid();
        InitializeControl();
        InitializeEvent();

    }

    private void InitializeControl()
    {
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("CODE", typeof(string));
        dataTable.Columns.Add("NAME", typeof(string));

        for (int i = 0; i <= 1; i++)
        {
            DataRow dr = dataTable.NewRow();
            dr[0] = i == 0 ? true : false;
            dr[1] = i == 0 ? "Active" : "inactive";
            dataTable.Rows.Add(dr);
        }
        SetControl ctr = new SetControl();
        ctr.SetCombobox(lookup_Active, dataTable);

    }

    private void InitializeEvent()
    {
        btnUpload.Click += Btn_Click;
        btnView.Click += Btn_Click;
        btnDelete.Click += Btn_Click;

        gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
    }
    #endregion

    #region ▶ GridView ------------
    private void InitializeGrid()
    {
        // Visible columns
        SetColumn CD_COMPANY = new SetColumn(gridView1, "CD_COMPANY", "Company Code", 100, false);
        SetColumn NM_COMPANY = new SetColumn(gridView1, "NM_COMPANY", "Company Name", 150, false);

        // Hidden columns
        SetColumn NM_SHORT = new SetColumn(gridView1, "NM_SHORT", "Short Name", 100, false, false);
        SetColumn DC_IMAGE_URL = new SetColumn(gridView1, "DC_IMAGE_URL", "Image Url", 100, false, false);
        SetColumn DC_ADDRESS1 = new SetColumn(gridView1, "DC_ADDRESS1", "Address1", 100, false, false);
        SetColumn DC_ADDRESS2 = new SetColumn(gridView1, "DC_ADDRESS2", "Address2", 100, false, false);
        SetColumn DC_CITY = new SetColumn(gridView1, "DC_CITY", "City", 100, false, false);
        SetColumn DC_STATE = new SetColumn(gridView1, "DC_STATE", "State", 100, false, false);
        SetColumn DC_POSTAL_CODE = new SetColumn(gridView1, "DC_POSTAL_CODE", "Postal Code", 100, false, false);
        SetColumn DC_COUNTRY = new SetColumn(gridView1, "DC_COUNTRY", "Country", 100, false, false);
        SetColumn NO_PHONE = new SetColumn(gridView1, "NO_PHONE", "Phone", 100, false, false);
        SetColumn NO_FAX = new SetColumn(gridView1, "NO_FAX", "Fax", 100, false, false);
        SetColumn DC_EMAIL = new SetColumn(gridView1, "DC_EMAIL", "Email", 100, false, false);
        SetColumn DC_WEBSITE = new SetColumn(gridView1, "DC_WEBSITE", "WebSite", 100, false, false);
        SetColumn NO_TAX_ID = new SetColumn(gridView1, "NO_TAX_ID", "Tax Id", 100, false, false);
        SetColumn NO_REG_NO = new SetColumn(gridView1, "NO_REG_NO", "Reg No", 100, false, false);
        SetColumn TM_REG = new SetColumn(gridView1, "TM_REG", false);
        SetColumn TM_AMD = new SetColumn(gridView1, "TM_AMD", false);
        SetColumn FL_ACTIVE = new SetColumn(gridView1, "FL_ACTIVE", "Active", 100, false, false);

        chGrid1.VerifyNotNull = new string[] { "CD_COMPANY", "NM_COMPANY", "DC_ADDRESS1", "DC_CITY", "DC_STATE", "DC_COUNTRY", "NO_PHONE", "FL_ACTIVE", };
        SetGridStyle(chGrid1, false, false);

        chGrid1.SetBinding(panelBinding, gridView1, new object[] { txt_CdCompany, txt_NmCom });
    }
    #endregion

    #region ▶ MainButton ----------

    public override void OnSearch()
    {
        try
        {
            base.OnSearch();

            DataTable dataTable = _D.Search(new object[] { txt_s_Company.Text });
            chGrid1.Binding(dataTable);
            _ = LoadImageFromGrid(dataTable);
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
            gridView1.AddNewRow();
            gridView1.UpdateCurrentRow();
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
            gridView1.DeleteRow(gridView1.FocusedRowHandle);

        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public override async void OnSave()
    {
        try
        {
            base.OnSave();
            DataTable dtSave = chGrid1.GetChanges();

            HashSet<string> imgUrlToDel = new HashSet<string>(); // To prevent duplicate url

            if (dtSave != null)
            {
                foreach (DataRow dataRow in dtSave.Rows)
                {
                    if (dataRow.RowState != DataRowState.Deleted) continue;

                    string imgUrl = A.GetString(dataRow["DC_IMAGE_URL", DataRowVersion.Original]);
                    if (string.IsNullOrEmpty(imgUrl)) continue;
                    imgUrlToDel.Add(imgUrl);
                }
            }


            bool result = _D.Save(dtSave);

            if (!result)
            {
                ShowMessageBox("Failed to save.", CH.Framework.Common.MessageType.Error);
                return;
            }
            if (imgUrlToDel.Any())
            {
                foreach (string url in imgUrlToDel)
                {
                    await FH.DeleteFileAsync(url);
                }
            }

            ShowMessageBox("Save successfully.", CH.Framework.Common.MessageType.Information);
            chGrid1.AcceptChanges();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
    #endregion

    #region ▶ Event ---------------
    private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
    {
        string url = A.GetString(gridView1.GetRowCellValue(e.FocusedRowHandle, "DC_IMAGE_URL"));

        if (string.IsNullOrEmpty(url))
        {
            pictureEdit1.Image = null;
            return;
        }

        if (imgCollection.TryGetValue(url, out var img))
        {
            pictureEdit1.Image = img;
            return;
        }

        _ = LoadSingleImage(url);
    }

    private async void Btn_Click(object sender, EventArgs e)
    {
        CHRoundButton btn = sender as CHRoundButton;
        try
        {
            switch (btn.Name)
            {
                case "btnUpload":
                    {
                        using var dialog = new OpenFileDialog
                        {
                            Multiselect = false,
                            Filter = "Picture file (*.jpg, *.gif, *.bmp, *.png) | *.jpg; *.gif; *.bmp; *.png; | All files (*.*) | *.*",
                            Title = "Please select the file you want to upload",
                            RestoreDirectory = true,
                        };

                        if (dialog.ShowDialog() != DialogResult.OK) return;

                        string strFullPathFile = dialog.FileName;
                        string fileName = Path.GetFileName(dialog.FileName);
                        pictureEdit1.LoadAsync(strFullPathFile);
                        var imgUrl = await FH.UploadFileAsync(strFullPathFile, fileName, "companies");
                        if (string.IsNullOrEmpty(imgUrl))
                        {
                            ShowMessageBox("Upload failed.", MessageType.Error);
                            return;
                        }
                        gridView1.SetFocusedRowCellValue("DC_IMAGE_URL", imgUrl);
                        OnSave();
                    }
                    break;

                case "btnView":
                    if (gridView1.RowCount == 0 || string.IsNullOrWhiteSpace(A.GetString(gridView1.GetFocusedRowCellValue("DC_IMAGE_URL")))) return;
                    string imageUrl = A.GetString(gridView1.GetFocusedRowCellValue("DC_IMAGE_URL"));
                    await FH.SaveRun(imageUrl);
                    break;

                case "btnDelete":
                    if (gridView1.RowCount == 0 || string.IsNullOrWhiteSpace(A.GetString(gridView1.GetFocusedRowCellValue("DC_IMAGE_URL")))) return;
                    pictureEdit1.Image = null;
                    await FH.DeleteFileAsync(A.GetString(gridView1.GetFocusedRowCellValue("DC_IMAGE_URL")));
                    gridView1.SetFocusedRowCellValue("DC_IMAGE_URL", "");
                    OnSave();
                    break;
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
    #endregion

    #region ▶ Method --------------

    private async Task LoadImageFromGrid(DataTable dataTable)
    {
        if (dataTable == null) return;

        imgCollection.Clear();

        var tasks = dataTable.AsEnumerable()
            .Select(row => A.GetString(row["DC_IMAGE_URL"]))
            .Where(url => !string.IsNullOrEmpty(url))
            .Distinct()
            .Select(async url =>
            {
                try
                {
                    using var http = new HttpClient();
                    var bytes = await http.GetByteArrayAsync(url);
                    var ms = new MemoryStream(bytes);
                    var image = Image.FromStream(ms);
                    imgCollection[url] = image;
                }
                catch { }
            });

        await Task.WhenAll(tasks);


        UpdatePictureEdit();
    }

    private void UpdatePictureEdit()
    {
        string url = A.GetString(gridView1.GetFocusedRowCellValue("DC_IMAGE_URL"));

        pictureEdit1.Image = (!string.IsNullOrEmpty(url) && imgCollection.TryGetValue(url, out var img)) ? img : null;
    }

    private async Task LoadSingleImage(string url)
    {
        try
        {
            using var http = new HttpClient();
            var bytes = await http.GetByteArrayAsync(url);
            var ms = new MemoryStream(bytes);
            var image = Image.FromStream(ms);
            imgCollection[url] = image;

            pictureEdit1.Invoke(() => pictureEdit1.Image = image);
        }
        catch { }
    }
    #endregion
}
