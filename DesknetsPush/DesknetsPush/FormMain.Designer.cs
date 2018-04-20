namespace DesknetsPush
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.notifyIconEvent = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerPush = new System.Windows.Forms.Timer(this.components);
            this.listBoxPushSchedules = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // notifyIconEvent
            // 
            this.notifyIconEvent.BalloonTipTitle = "desknet\'s NEO Push";
            this.notifyIconEvent.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconEvent.Icon")));
            this.notifyIconEvent.Text = "desknet\'s NEO Push";
            this.notifyIconEvent.Visible = true;
            this.notifyIconEvent.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconEvent_MouseClick);
            // 
            // timerPush
            // 
            this.timerPush.Interval = 60000;
            this.timerPush.Tick += new System.EventHandler(this.timerPush_Tick);
            // 
            // listBoxPushSchedules
            // 
            this.listBoxPushSchedules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPushSchedules.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBoxPushSchedules.FormattingEnabled = true;
            this.listBoxPushSchedules.ItemHeight = 20;
            this.listBoxPushSchedules.Location = new System.Drawing.Point(0, 0);
            this.listBoxPushSchedules.Name = "listBoxPushSchedules";
            this.listBoxPushSchedules.Size = new System.Drawing.Size(384, 204);
            this.listBoxPushSchedules.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 204);
            this.Controls.Add(this.listBoxPushSchedules);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.ShowInTaskbar = false;
            this.Text = "desknet\'s NEO Push";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIconEvent;
        private System.Windows.Forms.Timer timerPush;
        private System.Windows.Forms.ListBox listBoxPushSchedules;
    }
}

