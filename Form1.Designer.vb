<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()

        '-- Instantiate all controls --

        ' Menu
        Me.menuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.tsmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFileSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFileSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAddTrack = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiDeleteTrack = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiHelpContents = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAbout = New System.Windows.Forms.ToolStripMenuItem()

        ' Main split (left | right)
        Me.splitMain = New System.Windows.Forms.SplitContainer()

        ' Left pane – toolbar + list + status
        Me.toolStripLeft = New System.Windows.Forms.ToolStrip()
        Me.tsbOpenFolder = New System.Windows.Forms.ToolStripButton()
        Me.tsSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbTrackChecked = New System.Windows.Forms.ToolStripButton()
        Me.tsbRebuild = New System.Windows.Forms.ToolStripButton()
        Me.tsSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbAddTrack = New System.Windows.Forms.ToolStripButton()
        Me.tsbDeleteTrack = New System.Windows.Forms.ToolStripButton()
        Me.tsSep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbHelp = New System.Windows.Forms.ToolStripButton()

        Me.lvwTracks = New System.Windows.Forms.ListView()
        Me.colName = New System.Windows.Forms.ColumnHeader()
        Me.colVersion = New System.Windows.Forms.ColumnHeader()
        Me.colStatus = New System.Windows.Forms.ColumnHeader()

        Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.sslFileName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslSpacer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslProgress = New System.Windows.Forms.ToolStripProgressBar()

        ' Context menu
        Me.ctxListMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ctxOpenHome = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxListSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ctxCopyHome = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxCopyDownload = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxListSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ctxDeleteTrack = New System.Windows.Forms.ToolStripMenuItem()

        ' Right pane – split (details top | web viewer bottom)
        Me.splitRight = New System.Windows.Forms.SplitContainer()
        Me.grpDetails = New System.Windows.Forms.GroupBox()

        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.txtVersion = New System.Windows.Forms.TextBox()
        Me.lblHomePage = New System.Windows.Forms.Label()
        Me.txtHomePage = New System.Windows.Forms.TextBox()
        Me.btnOpenHomePage = New System.Windows.Forms.Button()
        Me.lblDownloadLink = New System.Windows.Forms.Label()
        Me.txtDownloadLink = New System.Windows.Forms.TextBox()
        Me.lblTrackUrl = New System.Windows.Forms.Label()
        Me.txtTrackUrl = New System.Windows.Forms.TextBox()
        Me.btnDownloadPage = New System.Windows.Forms.Button()
        Me.btnBrowseUrl = New System.Windows.Forms.Button()
        Me.lblStartString = New System.Windows.Forms.Label()
        Me.txtStartString = New System.Windows.Forms.TextBox()
        Me.btnFindStart = New System.Windows.Forms.Button()
        Me.lblStopString = New System.Windows.Forms.Label()
        Me.txtStopString = New System.Windows.Forms.TextBox()
        Me.btnFindStop = New System.Windows.Forms.Button()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()

        ' Web viewer – find bar + rich text
        Me.pnlWebControls = New System.Windows.Forms.Panel()
        Me.lblFind = New System.Windows.Forms.Label()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.btnFindFromTop = New System.Windows.Forms.Button()
        Me.btnFindFromCaret = New System.Windows.Forms.Button()
        Me.btnFindRevBottom = New System.Windows.Forms.Button()
        Me.btnFindRevCaret = New System.Windows.Forms.Button()
        Me.lblMatches = New System.Windows.Forms.Label()
        Me.rtbContent = New System.Windows.Forms.RichTextBox()

        '-- Suspend layouts --
        Me.menuStrip1.SuspendLayout()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        Me.toolStripLeft.SuspendLayout()
        Me.statusStrip1.SuspendLayout()
        Me.ctxListMenu.SuspendLayout()
        CType(Me.splitRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitRight.Panel1.SuspendLayout()
        Me.splitRight.Panel2.SuspendLayout()
        Me.splitRight.SuspendLayout()
        Me.grpDetails.SuspendLayout()
        Me.pnlWebControls.SuspendLayout()
        Me.SuspendLayout()

        '======================================================
        ' menuStrip1
        '======================================================
        Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {
            Me.tsmiFile, Me.tsmiEdit, Me.tsmiHelp})
        Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.menuStrip1.Name = "menuStrip1"
        Me.menuStrip1.Size = New System.Drawing.Size(1200, 24)
        Me.menuStrip1.TabIndex = 0
        Me.menuStrip1.Text = "menuStrip1"

        ' File menu
        Me.tsmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {
            Me.tsmiOpenFolder, Me.tsmiFileSep1, Me.tsmiSave, Me.tsmiFileSep2, Me.tsmiExit})
        Me.tsmiFile.Name = "tsmiFile"
        Me.tsmiFile.Size = New System.Drawing.Size(37, 20)
        Me.tsmiFile.Text = "&File"

        Me.tsmiOpenFolder.Name = "tsmiOpenFolder"
        Me.tsmiOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.tsmiOpenFolder.Size = New System.Drawing.Size(210, 22)
        Me.tsmiOpenFolder.Text = "&Open Folder..."

        Me.tsmiFileSep1.Name = "tsmiFileSep1"
        Me.tsmiFileSep1.Size = New System.Drawing.Size(207, 6)

        Me.tsmiSave.Name = "tsmiSave"
        Me.tsmiSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.tsmiSave.Size = New System.Drawing.Size(210, 22)
        Me.tsmiSave.Text = "&Save Track"

        Me.tsmiFileSep2.Name = "tsmiFileSep2"
        Me.tsmiFileSep2.Size = New System.Drawing.Size(207, 6)

        Me.tsmiExit.Name = "tsmiExit"
        Me.tsmiExit.Size = New System.Drawing.Size(210, 22)
        Me.tsmiExit.Text = "E&xit"

        ' Edit menu
        Me.tsmiEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {
            Me.tsmiAddTrack, Me.tsmiDeleteTrack})
        Me.tsmiEdit.Name = "tsmiEdit"
        Me.tsmiEdit.Size = New System.Drawing.Size(39, 20)
        Me.tsmiEdit.Text = "&Edit"

        Me.tsmiAddTrack.Name = "tsmiAddTrack"
        Me.tsmiAddTrack.Size = New System.Drawing.Size(180, 22)
        Me.tsmiAddTrack.Text = "&Add Track"

        Me.tsmiDeleteTrack.Name = "tsmiDeleteTrack"
        Me.tsmiDeleteTrack.Size = New System.Drawing.Size(180, 22)
        Me.tsmiDeleteTrack.Text = "&Delete Track"

        ' Help menu
        Me.tsmiHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {
            Me.tsmiHelpContents, Me.tsmiAbout})
        Me.tsmiHelp.Name = "tsmiHelp"
        Me.tsmiHelp.Size = New System.Drawing.Size(44, 20)
        Me.tsmiHelp.Text = "&Help"

        Me.tsmiHelpContents.Name = "tsmiHelpContents"
        Me.tsmiHelpContents.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.tsmiHelpContents.Size = New System.Drawing.Size(185, 22)
        Me.tsmiHelpContents.Text = "&Help Contents"

        Me.tsmiAbout.Name = "tsmiAbout"
        Me.tsmiAbout.Size = New System.Drawing.Size(185, 22)
        Me.tsmiAbout.Text = "&About..."

        '======================================================
        ' splitMain  (Vertical = left | right)
        '======================================================
        Me.splitMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMain.Location = New System.Drawing.Point(0, 24)
        Me.splitMain.Name = "splitMain"
        Me.splitMain.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.splitMain.Size = New System.Drawing.Size(1200, 726)
        Me.splitMain.SplitterDistance = 380
        Me.splitMain.SplitterWidth = 4
        Me.splitMain.TabIndex = 1

        ' Left pane controls (Bottom first so Fill wins)
        Me.splitMain.Panel1.Controls.Add(Me.statusStrip1)
        Me.splitMain.Panel1.Controls.Add(Me.toolStripLeft)
        Me.splitMain.Panel1.Controls.Add(Me.lvwTracks)

        ' Right pane
        Me.splitMain.Panel2.Controls.Add(Me.splitRight)

        '======================================================
        ' toolStripLeft
        '======================================================
        Me.toolStripLeft.Items.AddRange(New System.Windows.Forms.ToolStripItem() {
            Me.tsbOpenFolder, Me.tsSep1,
            Me.tsbTrackChecked, Me.tsbRebuild,
            Me.tsSep2,
            Me.tsbAddTrack, Me.tsbDeleteTrack,
            Me.tsSep3,
            Me.tsbHelp})
        Me.toolStripLeft.Dock = System.Windows.Forms.DockStyle.Top
        Me.toolStripLeft.Location = New System.Drawing.Point(0, 0)
        Me.toolStripLeft.Name = "toolStripLeft"
        Me.toolStripLeft.Size = New System.Drawing.Size(380, 25)
        Me.toolStripLeft.TabIndex = 0

        Me.tsbOpenFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbOpenFolder.Name = "tsbOpenFolder"
        Me.tsbOpenFolder.Text = "Open Folder"
        Me.tsbOpenFolder.ToolTipText = "Open a folder containing SPS files (Ctrl+O)"

        Me.tsSep1.Name = "tsSep1"

        Me.tsbTrackChecked.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbTrackChecked.Name = "tsbTrackChecked"
        Me.tsbTrackChecked.Text = "Track Checked Lines"
        Me.tsbTrackChecked.ToolTipText = "Download and extract version for all checked tracks"

        Me.tsbRebuild.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbRebuild.Name = "tsbRebuild"
        Me.tsbRebuild.Text = "ReBuild"
        Me.tsbRebuild.ToolTipText = "Reload all tracks from the current folder"

        Me.tsSep2.Name = "tsSep2"

        Me.tsbAddTrack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbAddTrack.Name = "tsbAddTrack"
        Me.tsbAddTrack.Text = "Add"
        Me.tsbAddTrack.ToolTipText = "Add a new track"

        Me.tsbDeleteTrack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbDeleteTrack.Name = "tsbDeleteTrack"
        Me.tsbDeleteTrack.Text = "Delete"
        Me.tsbDeleteTrack.ToolTipText = "Delete the selected track"

        Me.tsSep3.Name = "tsSep3"

        Me.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbHelp.Name = "tsbHelp"
        Me.tsbHelp.Text = "Help"
        Me.tsbHelp.ToolTipText = "Show usage help (F1)"

        '======================================================
        ' lvwTracks
        '======================================================
        Me.lvwTracks.CheckBoxes = True
        Me.lvwTracks.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {
            Me.colName, Me.colVersion, Me.colStatus})
        Me.lvwTracks.ContextMenuStrip = Me.ctxListMenu
        Me.lvwTracks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwTracks.FullRowSelect = True
        Me.lvwTracks.GridLines = True
        Me.lvwTracks.HideSelection = False
        Me.lvwTracks.Location = New System.Drawing.Point(0, 25)
        Me.lvwTracks.MultiSelect = False
        Me.lvwTracks.Name = "lvwTracks"
        Me.lvwTracks.Size = New System.Drawing.Size(380, 679)
        Me.lvwTracks.TabIndex = 1
        Me.lvwTracks.UseCompatibleStateImageBehavior = False
        Me.lvwTracks.View = System.Windows.Forms.View.Details

        Me.colName.Text = "App Name"
        Me.colName.Width = 200

        Me.colVersion.Text = "Version"
        Me.colVersion.Width = 80

        Me.colStatus.Text = "Status"
        Me.colStatus.Width = 80

        '======================================================
        ' statusStrip1
        '======================================================
        Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {
            Me.sslFileName, Me.sslSpacer, Me.sslCount, Me.sslProgress})
        Me.statusStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.statusStrip1.Location = New System.Drawing.Point(0, 726)
        Me.statusStrip1.Name = "statusStrip1"
        Me.statusStrip1.Size = New System.Drawing.Size(380, 22)
        Me.statusStrip1.TabIndex = 2

        Me.sslFileName.Name = "sslFileName"
        Me.sslFileName.Size = New System.Drawing.Size(200, 17)
        Me.sslFileName.Spring = True
        Me.sslFileName.Text = "Ready – Open a folder to begin"
        Me.sslFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.sslSpacer.Name = "sslSpacer"
        Me.sslSpacer.Size = New System.Drawing.Size(6, 17)

        Me.sslCount.Name = "sslCount"
        Me.sslCount.Size = New System.Drawing.Size(55, 17)
        Me.sslCount.Text = "0 tracks"

        Me.sslProgress.Name = "sslProgress"
        Me.sslProgress.Size = New System.Drawing.Size(80, 16)
        Me.sslProgress.Visible = False

        '======================================================
        ' ctxListMenu
        '======================================================
        Me.ctxListMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {
            Me.ctxOpenHome,
            Me.ctxListSep1,
            Me.ctxCopyHome,
            Me.ctxCopyDownload,
            Me.ctxListSep2,
            Me.ctxDeleteTrack})
        Me.ctxListMenu.Name = "ctxListMenu"
        Me.ctxListMenu.Size = New System.Drawing.Size(200, 104)

        Me.ctxOpenHome.Name = "ctxOpenHome"
        Me.ctxOpenHome.Size = New System.Drawing.Size(199, 22)
        Me.ctxOpenHome.Text = "Open Home Page in Browser"

        Me.ctxListSep1.Name = "ctxListSep1"
        Me.ctxListSep1.Size = New System.Drawing.Size(196, 6)

        Me.ctxCopyHome.Name = "ctxCopyHome"
        Me.ctxCopyHome.Size = New System.Drawing.Size(199, 22)
        Me.ctxCopyHome.Text = "Copy Home Page URL"

        Me.ctxCopyDownload.Name = "ctxCopyDownload"
        Me.ctxCopyDownload.Size = New System.Drawing.Size(199, 22)
        Me.ctxCopyDownload.Text = "Copy Download URL"

        Me.ctxListSep2.Name = "ctxListSep2"
        Me.ctxListSep2.Size = New System.Drawing.Size(196, 6)

        Me.ctxDeleteTrack.Name = "ctxDeleteTrack"
        Me.ctxDeleteTrack.Size = New System.Drawing.Size(199, 22)
        Me.ctxDeleteTrack.Text = "Delete Track"

        '======================================================
        ' splitRight  (Horizontal = details top | web viewer bottom)
        '======================================================
        Me.splitRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitRight.Location = New System.Drawing.Point(0, 0)
        Me.splitRight.Name = "splitRight"
        Me.splitRight.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.splitRight.Size = New System.Drawing.Size(816, 726)
        Me.splitRight.SplitterDistance = 310
        Me.splitRight.SplitterWidth = 4
        Me.splitRight.TabIndex = 0

        Me.splitRight.Panel1.Controls.Add(Me.grpDetails)
        ' Bottom panel: rtbContent fills; pnlWebControls docked to Top
        Me.splitRight.Panel2.Controls.Add(Me.rtbContent)
        Me.splitRight.Panel2.Controls.Add(Me.pnlWebControls)

        '======================================================
        ' grpDetails  (Track Details group, fills upper-right pane)
        '======================================================
        Me.grpDetails.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblName, Me.txtName,
            Me.lblVersion, Me.txtVersion,
            Me.lblHomePage, Me.txtHomePage, Me.btnOpenHomePage,
            Me.lblDownloadLink, Me.txtDownloadLink,
            Me.lblTrackUrl, Me.txtTrackUrl, Me.btnDownloadPage, Me.btnBrowseUrl,
            Me.lblStartString, Me.txtStartString, Me.btnFindStart,
            Me.lblStopString, Me.txtStopString, Me.btnFindStop,
            Me.lblDescription, Me.txtDescription,
            Me.btnSave, Me.btnSaveExit, Me.btnClear})
        Me.grpDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpDetails.Location = New System.Drawing.Point(0, 0)
        Me.grpDetails.Name = "grpDetails"
        Me.grpDetails.Size = New System.Drawing.Size(816, 310)
        Me.grpDetails.TabIndex = 0
        Me.grpDetails.TabStop = False
        Me.grpDetails.Text = "Track Details"

        ' Shared layout constants: label column ends at x=112, fields start at 115
        ' Buttons: anchored to right. Row height ~26 px, top margin 22.

        ' Row 1 – App Name  (y=22)
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(8, 25)
        Me.lblName.Name = "lblName"
        Me.lblName.Text = "App Name:"

        Me.txtName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtName.Location = New System.Drawing.Point(115, 22)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(693, 20)
        Me.txtName.TabIndex = 0

        ' Row 2 – Current Version  (y=48)
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(8, 51)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Text = "Current Version:"

        Me.txtVersion.Location = New System.Drawing.Point(115, 48)
        Me.txtVersion.Name = "txtVersion"
        Me.txtVersion.Size = New System.Drawing.Size(150, 20)
        Me.txtVersion.TabIndex = 1

        ' Row 3 – Home Page URL  (y=74)
        Me.lblHomePage.AutoSize = True
        Me.lblHomePage.Location = New System.Drawing.Point(8, 77)
        Me.lblHomePage.Name = "lblHomePage"
        Me.lblHomePage.Text = "Home Page URL:"

        Me.txtHomePage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHomePage.Location = New System.Drawing.Point(115, 74)
        Me.txtHomePage.Name = "txtHomePage"
        Me.txtHomePage.Size = New System.Drawing.Size(578, 20)
        Me.txtHomePage.TabIndex = 2

        Me.btnOpenHomePage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenHomePage.Location = New System.Drawing.Point(699, 72)
        Me.btnOpenHomePage.Name = "btnOpenHomePage"
        Me.btnOpenHomePage.Size = New System.Drawing.Size(109, 24)
        Me.btnOpenHomePage.TabIndex = 3
        Me.btnOpenHomePage.Text = "Open in Browser"
        Me.btnOpenHomePage.UseVisualStyleBackColor = True

        ' Row 4 – Download URL  (y=100)
        Me.lblDownloadLink.AutoSize = True
        Me.lblDownloadLink.Location = New System.Drawing.Point(8, 103)
        Me.lblDownloadLink.Name = "lblDownloadLink"
        Me.lblDownloadLink.Text = "Download URL:"

        Me.txtDownloadLink.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDownloadLink.Location = New System.Drawing.Point(115, 100)
        Me.txtDownloadLink.Name = "txtDownloadLink"
        Me.txtDownloadLink.Size = New System.Drawing.Size(693, 20)
        Me.txtDownloadLink.TabIndex = 4

        ' Row 5 – Track URL  (y=126) – Download + Browser buttons on right
        Me.lblTrackUrl.AutoSize = True
        Me.lblTrackUrl.Location = New System.Drawing.Point(8, 129)
        Me.lblTrackUrl.Name = "lblTrackUrl"
        Me.lblTrackUrl.Text = "Track URL:"

        Me.txtTrackUrl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTrackUrl.Location = New System.Drawing.Point(115, 126)
        Me.txtTrackUrl.Name = "txtTrackUrl"
        Me.txtTrackUrl.Size = New System.Drawing.Size(466, 20)
        Me.txtTrackUrl.TabIndex = 5

        Me.btnDownloadPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDownloadPage.Location = New System.Drawing.Point(587, 124)
        Me.btnDownloadPage.Name = "btnDownloadPage"
        Me.btnDownloadPage.Size = New System.Drawing.Size(110, 24)
        Me.btnDownloadPage.TabIndex = 6
        Me.btnDownloadPage.Text = "Download"
        Me.btnDownloadPage.UseVisualStyleBackColor = True

        Me.btnBrowseUrl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseUrl.Location = New System.Drawing.Point(703, 124)
        Me.btnBrowseUrl.Name = "btnBrowseUrl"
        Me.btnBrowseUrl.Size = New System.Drawing.Size(105, 24)
        Me.btnBrowseUrl.TabIndex = 7
        Me.btnBrowseUrl.Text = "Browser"
        Me.btnBrowseUrl.UseVisualStyleBackColor = True

        ' Row 6 – Start String  (y=152) – "Find in Page" button on right
        Me.lblStartString.AutoSize = True
        Me.lblStartString.Location = New System.Drawing.Point(8, 155)
        Me.lblStartString.Name = "lblStartString"
        Me.lblStartString.Text = "Start String:"

        Me.txtStartString.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtStartString.Location = New System.Drawing.Point(115, 152)
        Me.txtStartString.Name = "txtStartString"
        Me.txtStartString.Size = New System.Drawing.Size(578, 20)
        Me.txtStartString.TabIndex = 8

        Me.btnFindStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFindStart.Location = New System.Drawing.Point(699, 150)
        Me.btnFindStart.Name = "btnFindStart"
        Me.btnFindStart.Size = New System.Drawing.Size(109, 24)
        Me.btnFindStart.TabIndex = 9
        Me.btnFindStart.Text = "Find in Page"
        Me.btnFindStart.UseVisualStyleBackColor = True

        ' Row 7 – Stop String  (y=178) – "Find in Page" button on right
        Me.lblStopString.AutoSize = True
        Me.lblStopString.Location = New System.Drawing.Point(8, 181)
        Me.lblStopString.Name = "lblStopString"
        Me.lblStopString.Text = "Stop String:"

        Me.txtStopString.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtStopString.Location = New System.Drawing.Point(115, 178)
        Me.txtStopString.Name = "txtStopString"
        Me.txtStopString.Size = New System.Drawing.Size(578, 20)
        Me.txtStopString.TabIndex = 10

        Me.btnFindStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFindStop.Location = New System.Drawing.Point(699, 176)
        Me.btnFindStop.Name = "btnFindStop"
        Me.btnFindStop.Size = New System.Drawing.Size(109, 24)
        Me.btnFindStop.TabIndex = 11
        Me.btnFindStop.Text = "Find in Page"
        Me.btnFindStop.UseVisualStyleBackColor = True

        ' Row 8 – Description  (y=204)
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(8, 207)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Text = "Description:"

        Me.txtDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescription.Location = New System.Drawing.Point(115, 204)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(693, 20)
        Me.txtDescription.TabIndex = 12

        ' Row 9 – Action buttons  (y=232)
        Me.btnSave.Location = New System.Drawing.Point(115, 232)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(120, 28)
        Me.btnSave.TabIndex = 13
        Me.btnSave.Text = "Save Changes"
        Me.btnSave.BackColor = System.Drawing.Color.DarkGreen
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.UseVisualStyleBackColor = False

        Me.btnSaveExit.Location = New System.Drawing.Point(241, 232)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(120, 28)
        Me.btnSaveExit.TabIndex = 14
        Me.btnSaveExit.Text = "Save && Exit"
        Me.btnSaveExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnSaveExit.ForeColor = System.Drawing.Color.White
        Me.btnSaveExit.UseVisualStyleBackColor = False

        Me.btnClear.Location = New System.Drawing.Point(367, 232)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 28)
        Me.btnClear.TabIndex = 15
        Me.btnClear.Text = "Clear / New"
        Me.btnClear.UseVisualStyleBackColor = True

        '======================================================
        ' pnlWebControls  (find toolbar, docked to top of web viewer)
        '======================================================
        Me.pnlWebControls.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblFind, Me.txtFind,
            Me.btnFindFromTop, Me.btnFindFromCaret,
            Me.btnFindRevBottom, Me.btnFindRevCaret,
            Me.lblMatches})
        Me.pnlWebControls.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlWebControls.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pnlWebControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlWebControls.Location = New System.Drawing.Point(0, 0)
        Me.pnlWebControls.Name = "pnlWebControls"
        Me.pnlWebControls.Size = New System.Drawing.Size(816, 34)
        Me.pnlWebControls.TabIndex = 0

        Me.lblFind.AutoSize = True
        Me.lblFind.Location = New System.Drawing.Point(5, 9)
        Me.lblFind.Name = "lblFind"
        Me.lblFind.Text = "Find:"

        Me.txtFind.Location = New System.Drawing.Point(42, 6)
        Me.txtFind.Name = "txtFind"
        Me.txtFind.Size = New System.Drawing.Size(175, 20)
        Me.txtFind.TabIndex = 0

        Me.btnFindFromTop.Location = New System.Drawing.Point(223, 4)
        Me.btnFindFromTop.Name = "btnFindFromTop"
        Me.btnFindFromTop.Size = New System.Drawing.Size(80, 24)
        Me.btnFindFromTop.TabIndex = 1
        Me.btnFindFromTop.Text = "From TOP"
        Me.btnFindFromTop.UseVisualStyleBackColor = True

        Me.btnFindFromCaret.Location = New System.Drawing.Point(309, 4)
        Me.btnFindFromCaret.Name = "btnFindFromCaret"
        Me.btnFindFromCaret.Size = New System.Drawing.Size(95, 24)
        Me.btnFindFromCaret.TabIndex = 2
        Me.btnFindFromCaret.Text = "From CARET"
        Me.btnFindFromCaret.UseVisualStyleBackColor = True

        Me.btnFindRevBottom.Location = New System.Drawing.Point(410, 4)
        Me.btnFindRevBottom.Name = "btnFindRevBottom"
        Me.btnFindRevBottom.Size = New System.Drawing.Size(130, 24)
        Me.btnFindRevBottom.TabIndex = 3
        Me.btnFindRevBottom.Text = "Rev from BOTTOM"
        Me.btnFindRevBottom.UseVisualStyleBackColor = True

        Me.btnFindRevCaret.Location = New System.Drawing.Point(546, 4)
        Me.btnFindRevCaret.Name = "btnFindRevCaret"
        Me.btnFindRevCaret.Size = New System.Drawing.Size(120, 24)
        Me.btnFindRevCaret.TabIndex = 4
        Me.btnFindRevCaret.Text = "Rev from CARET"
        Me.btnFindRevCaret.UseVisualStyleBackColor = True

        Me.lblMatches.AutoSize = True
        Me.lblMatches.ForeColor = System.Drawing.Color.Gray
        Me.lblMatches.Location = New System.Drawing.Point(672, 9)
        Me.lblMatches.Name = "lblMatches"
        Me.lblMatches.Text = ""

        '======================================================
        ' rtbContent  (fills the lower-right web-viewer pane)
        '======================================================
        Me.rtbContent.BackColor = System.Drawing.Color.Black
        Me.rtbContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbContent.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbContent.ForeColor = System.Drawing.Color.Lime
        Me.rtbContent.Location = New System.Drawing.Point(0, 34)
        Me.rtbContent.Name = "rtbContent"
        Me.rtbContent.ReadOnly = True
        Me.rtbContent.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both
        Me.rtbContent.Size = New System.Drawing.Size(816, 378)
        Me.rtbContent.TabIndex = 1
        Me.rtbContent.Text = ""
        Me.rtbContent.WordWrap = False

        '======================================================
        ' Form1
        '======================================================
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 750)
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.menuStrip1)
        Me.MainMenuStrip = Me.menuStrip1
        Me.MinimumSize = New System.Drawing.Size(900, 500)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation
        Me.Text = "SPS PortableAppTrack"

        '-- Resume layouts --
        Me.menuStrip1.ResumeLayout(False)
        Me.menuStrip1.PerformLayout()
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel1.PerformLayout()
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        Me.toolStripLeft.ResumeLayout(False)
        Me.toolStripLeft.PerformLayout()
        Me.statusStrip1.ResumeLayout(False)
        Me.statusStrip1.PerformLayout()
        Me.ctxListMenu.ResumeLayout(False)
        Me.splitRight.Panel1.ResumeLayout(False)
        Me.splitRight.Panel2.ResumeLayout(False)
        CType(Me.splitRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitRight.ResumeLayout(False)
        Me.grpDetails.ResumeLayout(False)
        Me.grpDetails.PerformLayout()
        Me.pnlWebControls.ResumeLayout(False)
        Me.pnlWebControls.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    '-- Control declarations --
    Friend WithEvents menuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents tsmiFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiFileSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiFileSep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiAddTrack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiDeleteTrack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiHelpContents As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiAbout As System.Windows.Forms.ToolStripMenuItem

    Friend WithEvents splitMain As System.Windows.Forms.SplitContainer
    Friend WithEvents toolStripLeft As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbOpenFolder As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbTrackChecked As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbRebuild As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsSep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbAddTrack As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbDeleteTrack As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsSep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents lvwTracks As System.Windows.Forms.ListView
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colVersion As System.Windows.Forms.ColumnHeader
    Friend WithEvents colStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents statusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents sslFileName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sslSpacer As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sslCount As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sslProgress As System.Windows.Forms.ToolStripProgressBar

    Friend WithEvents ctxListMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ctxOpenHome As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxListSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ctxCopyHome As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxCopyDownload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxListSep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ctxDeleteTrack As System.Windows.Forms.ToolStripMenuItem

    Friend WithEvents splitRight As System.Windows.Forms.SplitContainer
    Friend WithEvents grpDetails As System.Windows.Forms.GroupBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents txtVersion As System.Windows.Forms.TextBox
    Friend WithEvents lblHomePage As System.Windows.Forms.Label
    Friend WithEvents txtHomePage As System.Windows.Forms.TextBox
    Friend WithEvents btnOpenHomePage As System.Windows.Forms.Button
    Friend WithEvents lblDownloadLink As System.Windows.Forms.Label
    Friend WithEvents txtDownloadLink As System.Windows.Forms.TextBox
    Friend WithEvents lblTrackUrl As System.Windows.Forms.Label
    Friend WithEvents txtTrackUrl As System.Windows.Forms.TextBox
    Friend WithEvents btnDownloadPage As System.Windows.Forms.Button
    Friend WithEvents btnBrowseUrl As System.Windows.Forms.Button
    Friend WithEvents lblStartString As System.Windows.Forms.Label
    Friend WithEvents txtStartString As System.Windows.Forms.TextBox
    Friend WithEvents btnFindStart As System.Windows.Forms.Button
    Friend WithEvents lblStopString As System.Windows.Forms.Label
    Friend WithEvents txtStopString As System.Windows.Forms.TextBox
    Friend WithEvents btnFindStop As System.Windows.Forms.Button
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button

    Friend WithEvents pnlWebControls As System.Windows.Forms.Panel
    Friend WithEvents lblFind As System.Windows.Forms.Label
    Friend WithEvents txtFind As System.Windows.Forms.TextBox
    Friend WithEvents btnFindFromTop As System.Windows.Forms.Button
    Friend WithEvents btnFindFromCaret As System.Windows.Forms.Button
    Friend WithEvents btnFindRevBottom As System.Windows.Forms.Button
    Friend WithEvents btnFindRevCaret As System.Windows.Forms.Button
    Friend WithEvents lblMatches As System.Windows.Forms.Label
    Friend WithEvents rtbContent As System.Windows.Forms.RichTextBox

End Class
