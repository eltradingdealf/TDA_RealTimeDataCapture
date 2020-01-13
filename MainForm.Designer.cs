namespace RealTimeDataCapture2
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btConnectRTS = new System.Windows.Forms.Button();
            this.btDisconnectRTS = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.btExitApplication = new System.Windows.Forms.Button();
            this.lbNumTicksCollected = new System.Windows.Forms.Label();
            this.tbNumTicksCollected = new System.Windows.Forms.TextBox();
            this.lbNumberTicksInCollection = new System.Windows.Forms.Label();
            this.tbNumberTicksInCollection = new System.Windows.Forms.TextBox();
            this.lbVolumeTotal = new System.Windows.Forms.Label();
            this.tbVolumeTotal = new System.Windows.Forms.TextBox();
            this.lbTotalAsk = new System.Windows.Forms.Label();
            this.lbTotalBid = new System.Windows.Forms.Label();
            this.tbAskTotal = new System.Windows.Forms.TextBox();
            this.tbBidTotal = new System.Windows.Forms.TextBox();
            this.tbQuerysResult = new System.Windows.Forms.TextBox();
            this.lbQuerysResult = new System.Windows.Forms.Label();
            this.lbSymbol = new System.Windows.Forms.Label();
            this.cbSymbol = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btConnectRTS
            // 
            this.btConnectRTS.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btConnectRTS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btConnectRTS.Location = new System.Drawing.Point(53, 33);
            this.btConnectRTS.Name = "btConnectRTS";
            this.btConnectRTS.Size = new System.Drawing.Size(140, 49);
            this.btConnectRTS.TabIndex = 0;
            this.btConnectRTS.Text = "Connect RealTimeServer";
            this.btConnectRTS.UseVisualStyleBackColor = false;
            this.btConnectRTS.Click += new System.EventHandler(this.btConnectRTS_Click);
            // 
            // btDisconnectRTS
            // 
            this.btDisconnectRTS.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btDisconnectRTS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDisconnectRTS.Location = new System.Drawing.Point(217, 33);
            this.btDisconnectRTS.Name = "btDisconnectRTS";
            this.btDisconnectRTS.Size = new System.Drawing.Size(133, 49);
            this.btDisconnectRTS.TabIndex = 1;
            this.btDisconnectRTS.Text = "Disconnect RealtimeServer";
            this.btDisconnectRTS.UseVisualStyleBackColor = false;
            this.btDisconnectRTS.Click += new System.EventHandler(this.btDisconnectRTS_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(372, 64);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(61, 18);
            this.lbStatus.TabIndex = 2;
            this.lbStatus.Text = "Status ";
            // 
            // tbStatus
            // 
            this.tbStatus.BackColor = System.Drawing.SystemColors.Window;
            this.tbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbStatus.Location = new System.Drawing.Point(448, 58);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ReadOnly = true;
            this.tbStatus.Size = new System.Drawing.Size(363, 24);
            this.tbStatus.TabIndex = 3;
            // 
            // btExitApplication
            // 
            this.btExitApplication.BackColor = System.Drawing.Color.Thistle;
            this.btExitApplication.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExitApplication.Location = new System.Drawing.Point(682, 369);
            this.btExitApplication.Name = "btExitApplication";
            this.btExitApplication.Size = new System.Drawing.Size(89, 39);
            this.btExitApplication.TabIndex = 4;
            this.btExitApplication.Text = "Exit";
            this.btExitApplication.UseVisualStyleBackColor = false;
            this.btExitApplication.Click += new System.EventHandler(this.btExitApplication_Click);
            // 
            // lbNumTicksCollected
            // 
            this.lbNumTicksCollected.AutoSize = true;
            this.lbNumTicksCollected.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumTicksCollected.Location = new System.Drawing.Point(50, 185);
            this.lbNumTicksCollected.Name = "lbNumTicksCollected";
            this.lbNumTicksCollected.Size = new System.Drawing.Size(186, 18);
            this.lbNumTicksCollected.TabIndex = 5;
            this.lbNumTicksCollected.Text = "Number Ticks collected";
            // 
            // tbNumTicksCollected
            // 
            this.tbNumTicksCollected.BackColor = System.Drawing.SystemColors.Window;
            this.tbNumTicksCollected.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNumTicksCollected.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbNumTicksCollected.Location = new System.Drawing.Point(242, 182);
            this.tbNumTicksCollected.Name = "tbNumTicksCollected";
            this.tbNumTicksCollected.ReadOnly = true;
            this.tbNumTicksCollected.Size = new System.Drawing.Size(84, 24);
            this.tbNumTicksCollected.TabIndex = 6;
            // 
            // lbNumberTicksInCollection
            // 
            this.lbNumberTicksInCollection.AutoSize = true;
            this.lbNumberTicksInCollection.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumberTicksInCollection.Location = new System.Drawing.Point(357, 185);
            this.lbNumberTicksInCollection.Name = "lbNumberTicksInCollection";
            this.lbNumberTicksInCollection.Size = new System.Drawing.Size(212, 18);
            this.lbNumberTicksInCollection.TabIndex = 7;
            this.lbNumberTicksInCollection.Text = "Number Ticks in Collection";
            // 
            // tbNumberTicksInCollection
            // 
            this.tbNumberTicksInCollection.BackColor = System.Drawing.SystemColors.Window;
            this.tbNumberTicksInCollection.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNumberTicksInCollection.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbNumberTicksInCollection.Location = new System.Drawing.Point(575, 182);
            this.tbNumberTicksInCollection.Name = "tbNumberTicksInCollection";
            this.tbNumberTicksInCollection.ReadOnly = true;
            this.tbNumberTicksInCollection.Size = new System.Drawing.Size(79, 24);
            this.tbNumberTicksInCollection.TabIndex = 8;
            // 
            // lbVolumeTotal
            // 
            this.lbVolumeTotal.AutoSize = true;
            this.lbVolumeTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVolumeTotal.Location = new System.Drawing.Point(50, 306);
            this.lbVolumeTotal.Name = "lbVolumeTotal";
            this.lbVolumeTotal.Size = new System.Drawing.Size(107, 18);
            this.lbVolumeTotal.TabIndex = 9;
            this.lbVolumeTotal.Text = "Total Volume";
            // 
            // tbVolumeTotal
            // 
            this.tbVolumeTotal.BackColor = System.Drawing.SystemColors.Window;
            this.tbVolumeTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVolumeTotal.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbVolumeTotal.Location = new System.Drawing.Point(53, 327);
            this.tbVolumeTotal.Name = "tbVolumeTotal";
            this.tbVolumeTotal.ReadOnly = true;
            this.tbVolumeTotal.Size = new System.Drawing.Size(100, 24);
            this.tbVolumeTotal.TabIndex = 10;
            // 
            // lbTotalAsk
            // 
            this.lbTotalAsk.AutoSize = true;
            this.lbTotalAsk.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalAsk.Location = new System.Drawing.Point(200, 306);
            this.lbTotalAsk.Name = "lbTotalAsk";
            this.lbTotalAsk.Size = new System.Drawing.Size(117, 18);
            this.lbTotalAsk.TabIndex = 11;
            this.lbTotalAsk.Text = "Total Vol Sells";
            // 
            // lbTotalBid
            // 
            this.lbTotalBid.AutoSize = true;
            this.lbTotalBid.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalBid.Location = new System.Drawing.Point(357, 306);
            this.lbTotalBid.Name = "lbTotalBid";
            this.lbTotalBid.Size = new System.Drawing.Size(117, 18);
            this.lbTotalBid.TabIndex = 12;
            this.lbTotalBid.Text = "Total Vol Buys";
            // 
            // tbAskTotal
            // 
            this.tbAskTotal.BackColor = System.Drawing.SystemColors.Window;
            this.tbAskTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAskTotal.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbAskTotal.Location = new System.Drawing.Point(203, 327);
            this.tbAskTotal.Name = "tbAskTotal";
            this.tbAskTotal.ReadOnly = true;
            this.tbAskTotal.Size = new System.Drawing.Size(100, 24);
            this.tbAskTotal.TabIndex = 13;
            // 
            // tbBidTotal
            // 
            this.tbBidTotal.BackColor = System.Drawing.SystemColors.Window;
            this.tbBidTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBidTotal.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbBidTotal.Location = new System.Drawing.Point(360, 327);
            this.tbBidTotal.Name = "tbBidTotal";
            this.tbBidTotal.ReadOnly = true;
            this.tbBidTotal.Size = new System.Drawing.Size(100, 24);
            this.tbBidTotal.TabIndex = 14;
            // 
            // tbQuerysResult
            // 
            this.tbQuerysResult.BackColor = System.Drawing.SystemColors.Window;
            this.tbQuerysResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbQuerysResult.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbQuerysResult.Location = new System.Drawing.Point(53, 258);
            this.tbQuerysResult.Name = "tbQuerysResult";
            this.tbQuerysResult.ReadOnly = true;
            this.tbQuerysResult.Size = new System.Drawing.Size(439, 24);
            this.tbQuerysResult.TabIndex = 18;
            // 
            // lbQuerysResult
            // 
            this.lbQuerysResult.AutoSize = true;
            this.lbQuerysResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQuerysResult.Location = new System.Drawing.Point(50, 237);
            this.lbQuerysResult.Name = "lbQuerysResult";
            this.lbQuerysResult.Size = new System.Drawing.Size(115, 18);
            this.lbQuerysResult.TabIndex = 17;
            this.lbQuerysResult.Text = "Querys Result";
            // 
            // lbSymbol
            // 
            this.lbSymbol.AutoSize = true;
            this.lbSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSymbol.Location = new System.Drawing.Point(50, 132);
            this.lbSymbol.Name = "lbSymbol";
            this.lbSymbol.Size = new System.Drawing.Size(64, 18);
            this.lbSymbol.TabIndex = 19;
            this.lbSymbol.Text = "Symbol";
            // 
            // cbSymbol
            // 
            this.cbSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSymbol.ForeColor = System.Drawing.Color.DarkMagenta;
            this.cbSymbol.FormattingEnabled = true;
            this.cbSymbol.Location = new System.Drawing.Point(120, 129);
            this.cbSymbol.Name = "cbSymbol";
            this.cbSymbol.Size = new System.Drawing.Size(507, 24);
            this.cbSymbol.TabIndex = 20;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 440);
            this.Controls.Add(this.cbSymbol);
            this.Controls.Add(this.lbSymbol);
            this.Controls.Add(this.tbQuerysResult);
            this.Controls.Add(this.lbQuerysResult);
            this.Controls.Add(this.tbBidTotal);
            this.Controls.Add(this.tbAskTotal);
            this.Controls.Add(this.lbTotalBid);
            this.Controls.Add(this.lbTotalAsk);
            this.Controls.Add(this.tbVolumeTotal);
            this.Controls.Add(this.lbVolumeTotal);
            this.Controls.Add(this.tbNumberTicksInCollection);
            this.Controls.Add(this.lbNumberTicksInCollection);
            this.Controls.Add(this.tbNumTicksCollected);
            this.Controls.Add(this.lbNumTicksCollected);
            this.Controls.Add(this.btExitApplication);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.btDisconnectRTS);
            this.Controls.Add(this.btConnectRTS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "RealTime Data Capture";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btConnectRTS;
        private System.Windows.Forms.Button btDisconnectRTS;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Button btExitApplication;
        private System.Windows.Forms.Label lbNumTicksCollected;
        private System.Windows.Forms.TextBox tbNumTicksCollected;
        private System.Windows.Forms.Label lbNumberTicksInCollection;
        private System.Windows.Forms.TextBox tbNumberTicksInCollection;
        private System.Windows.Forms.Label lbVolumeTotal;
        private System.Windows.Forms.TextBox tbVolumeTotal;
        private System.Windows.Forms.Label lbTotalAsk;
        private System.Windows.Forms.Label lbTotalBid;
        private System.Windows.Forms.TextBox tbAskTotal;
        private System.Windows.Forms.TextBox tbBidTotal;
        private System.Windows.Forms.TextBox tbQuerysResult;
        private System.Windows.Forms.Label lbQuerysResult;
        private System.Windows.Forms.Label lbSymbol;
        private System.Windows.Forms.ComboBox cbSymbol;
    }
}

