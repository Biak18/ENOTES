using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ENOTES.Deploy
{
    public partial class DeployForm : XtraForm
    {
        //private const string STORAGE_BASE = "https://hfdxxjngsdhwczpusnlj.supabase.co/storage/v1/object/public/updates/";
        private List<string> _selectedFiles = new();
        private SupabaseUploader _uploader;

        public DeployForm()
        {
            InitializeComponent();
            _uploader = new SupabaseUploader();
            InitializeEvent();
        }

        private void InitializeEvent()
        {
            btnBrowse.Click += BtnBrowse_Click;
            btnUpload.Click += BtnUpload_Click;
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "DLL Files (*.dll)|*.dll",
                Title = "Select DLLs to deploy"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            _selectedFiles = dialog.FileNames.ToList();

            // Show in checklist
            checkedListBox1.Items.Clear();
            foreach (var file in _selectedFiles)
            {
                var info = FileVersionInfo.GetVersionInfo(file);
                string label = $"{Path.GetFileName(file)}  v{info.FileVersion}";
                checkedListBox1.Items.Add(label, true); // checked by default
            }
        }

        private async void BtnUpload_Click(object sender, EventArgs e)
        {
            await _uploader.InitializeAsync();
            // Get only checked files
            var toUpload = _selectedFiles
                .Where((f, i) => checkedListBox1.GetItemChecked(i))
                .ToList();

            if (toUpload.Count == 0)
            {
                MessageBox.Show("No files selected.");
                return;
            }

            btnUpload.Enabled = false;
            btnBrowse.Enabled = false;
            progressBar1.EditValue = 0;

            var progress = new Progress<(string message, int percent)>(report =>
            {
                lblStatus.Text = report.message;
                progressBar1.EditValue = Math.Clamp(report.percent, 0, 100);
            });

            try
            {
                // 1. Upload each DLL
                int i = 0;
                foreach (var filePath in toUpload)
                {
                    i++;
                    int pct = (int)((i / (double)(toUpload.Count + 1)) * 90);

                    var fileProgress = new Progress<(string message, int percent)>(r =>
                    {
                        lblStatus.Text = r.message;
                        progressBar1.EditValue = pct;
                    });

                    await _uploader.UploadFileAsync(
      filePath,
      Path.GetFileName(filePath),
      percent =>
      {
          progressBar1.EditValue = (int)percent;
          lblStatus.Text = $"Uploading {Path.GetFileName(filePath)}... {percent:0}%";
      });
                }

                // 2. Build and upload manifest
                ((IProgress<(string, int)>)progress).Report(
                    ("Updating manifest.json...", 95));

                var manifest = ManifestBuilder.BuildFromFiles(toUpload);
                await _uploader.UploadManifestAsync(manifest, progress =>
                {

                });

                ((IProgress<(string, int)>)progress).Report(
                    ("Deploy complete!", 100));

                MessageBox.Show(
                    $"Successfully deployed {toUpload.Count} module(s)!\n\n" +
                    $"Manifest version: {manifest.Version}",
                    "Deploy Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Deploy failed:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnUpload.Enabled = true;
                btnBrowse.Enabled = true;
                progressBar1.EditValue = 0;
                lblStatus.Text = "";
            }
        }
    }
}
