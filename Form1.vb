Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports System.Xml

''' <summary>
''' Main application form – single window, SplitContainer design.
''' Left pane  – ListView of SPS tracks (with checkboxes) loaded from a folder.
''' Right pane – Track details editor and web-page viewer for version tracking.
'''              All right-pane controls are disabled when no track is selected.
'''              Left-clicking a ListView item instantly populates the right pane.
''' </summary>
Public Class Form1

    '------------------------------------------------------------
    ' State
    '------------------------------------------------------------
    Private tracks As New List(Of SpsTrack)()
    Private currentTrack As SpsTrack = Nothing
    Private currentFolder As String = String.Empty

    ' Config file path (per-user AppData)
    Private ReadOnly configFilePath As String =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                     "SPS_PortableAppTrack", "config.xml")

    '============================================================
    ' Form Load / Close
    '============================================================

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Enable modern TLS for HTTPS downloads
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11
        SetRightPaneEnabled(False)
        LoadConfig()
        UpdateStatusBar()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SaveConfig()
    End Sub

    '============================================================
    ' Right-pane Enable / Disable
    '============================================================

    ''' <summary>
    ''' Enables or disables all right-pane controls.
    ''' Pass False when no track is selected; True when one is selected.
    ''' </summary>
    Private Sub SetRightPaneEnabled(enabled As Boolean)
        grpDetails.Enabled = enabled
        pnlWebControls.Enabled = enabled
        rtbContent.Enabled = enabled
    End Sub

    '============================================================
    ' Configuration – window state, splitter positions, last folder
    '============================================================

    Private Sub LoadConfig()
        Try
            If Not File.Exists(configFilePath) Then Return

            Dim doc As New XmlDocument()
            doc.Load(configFilePath)
            Dim root = doc.DocumentElement
            If root Is Nothing Then Return

            ' Window bounds
            Dim winNode = root.SelectSingleNode("Window")
            If winNode IsNot Nothing Then
                Dim left = XmlInt(winNode, "Left", Me.Left)
                Dim top = XmlInt(winNode, "Top", Me.Top)
                Dim width = XmlInt(winNode, "Width", Me.Width)
                Dim height = XmlInt(winNode, "Height", Me.Height)
                Dim maximized = XmlBool(winNode, "Maximized", False)
                Me.Left = left
                Me.Top = top
                Me.Width = Math.Max(900, width)
                Me.Height = Math.Max(500, height)
                If maximized Then Me.WindowState = FormWindowState.Maximized
            End If

            ' Splitter positions
            Dim splitNode = root.SelectSingleNode("Splitters")
            If splitNode IsNot Nothing Then
                splitMain.SplitterDistance = Math.Max(150, XmlInt(splitNode, "MainSplit", 380))
                splitRight.SplitterDistance = Math.Max(120, XmlInt(splitNode, "RightSplit", 310))
            End If

            ' Last folder
            Dim folderNode = root.SelectSingleNode("LastFolder")
            If folderNode IsNot Nothing AndAlso Directory.Exists(folderNode.InnerText) Then
                currentFolder = folderNode.InnerText
                LoadTracksFromFolder(currentFolder)
            End If

        Catch ex As Exception
            ' Config failures are non-critical – use defaults
        End Try
    End Sub

    Private Sub SaveConfig()
        Try
            Dim dir = Path.GetDirectoryName(configFilePath)
            If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)

            Dim doc As New XmlDocument()
            Dim root = doc.CreateElement("Config")
            doc.AppendChild(root)

            ' Window bounds (use RestoreBounds when maximised)
            Dim b = If(Me.WindowState = FormWindowState.Normal, Me.Bounds, Me.RestoreBounds)
            Dim winNode = AppendElement(doc, root, "Window")
            AppendElement(doc, winNode, "Left", b.Left.ToString())
            AppendElement(doc, winNode, "Top", b.Top.ToString())
            AppendElement(doc, winNode, "Width", b.Width.ToString())
            AppendElement(doc, winNode, "Height", b.Height.ToString())
            AppendElement(doc, winNode, "Maximized", (Me.WindowState = FormWindowState.Maximized).ToString())

            ' Splitter positions
            Dim splitNode = AppendElement(doc, root, "Splitters")
            AppendElement(doc, splitNode, "MainSplit", splitMain.SplitterDistance.ToString())
            AppendElement(doc, splitNode, "RightSplit", splitRight.SplitterDistance.ToString())

            ' Last folder
            AppendElement(doc, root, "LastFolder", currentFolder)

            Dim settings As New XmlWriterSettings() With {
                .Indent = True,
                .IndentChars = "  ",
                .Encoding = Encoding.UTF8
            }
            Using writer = XmlWriter.Create(configFilePath, settings)
                doc.Save(writer)
            End Using

        Catch ex As Exception
            ' Config save failure is non-critical
        End Try
    End Sub

    '------------------------------------------------------------
    ' XML helpers
    '------------------------------------------------------------

    Private Shared Function XmlInt(node As XmlNode, name As String, def As Integer) As Integer
        Dim child = node.SelectSingleNode(name)
        If child Is Nothing Then Return def
        Dim v As Integer
        Return If(Integer.TryParse(child.InnerText, v), v, def)
    End Function

    Private Shared Function XmlBool(node As XmlNode, name As String, def As Boolean) As Boolean
        Dim child = node.SelectSingleNode(name)
        If child Is Nothing Then Return def
        Dim v As Boolean
        Return If(Boolean.TryParse(child.InnerText, v), v, def)
    End Function

    Private Shared Function AppendElement(doc As XmlDocument, parent As XmlNode,
                                           name As String,
                                           Optional value As String = Nothing) As XmlElement
        Dim el = doc.CreateElement(name)
        If value IsNot Nothing Then el.InnerText = value
        parent.AppendChild(el)
        Return el
    End Function

    Private Shared Sub SetNodeText(doc As XmlDocument, parent As XmlNode,
                                    name As String, value As String)
        Dim node = parent.SelectSingleNode(name)
        If node Is Nothing Then
            node = doc.CreateElement(name)
            parent.AppendChild(node)
        End If
        node.InnerText = value
    End Sub

    Private Shared Function GetNodeText(parent As XmlNode, name As String) As String
        Dim node = parent.SelectSingleNode(name)
        Return If(node Is Nothing, String.Empty, node.InnerText.Trim())
    End Function

    '============================================================
    ' Open Folder
    '============================================================

    Private Sub OpenFolder()
        Using dlg As New FolderBrowserDialog()
            dlg.Description = "Select folder containing SPS files"
            If currentFolder <> String.Empty Then dlg.SelectedPath = currentFolder
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                currentFolder = dlg.SelectedPath
                LoadTracksFromFolder(currentFolder)
            End If
        End Using
    End Sub

    Private Sub LoadTracksFromFolder(folder As String)
        tracks.Clear()
        lvwTracks.Items.Clear()
        ClearRightPanel()
        SetRightPaneEnabled(False)

        Try
            Dim spsFiles = Directory.GetFiles(folder, "*.sps", SearchOption.TopDirectoryOnly)

            lvwTracks.BeginUpdate()
            For Each filePath In spsFiles
                Dim track = LoadSpsFile(filePath)
                If track IsNot Nothing Then
                    tracks.Add(track)
                    lvwTracks.Items.Add(BuildListViewItem(track))
                End If
            Next
            lvwTracks.EndUpdate()

            sslFileName.Text = $"Folder: {folder}"
            UpdateStatusBar()

        Catch ex As Exception
            MessageBox.Show($"Error loading folder:{Environment.NewLine}{ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            sslFileName.Text = "Error loading folder"
        End Try
    End Sub

    '============================================================
    ' SPS File – Load / Save
    '============================================================

    Private Shared Function LoadSpsFile(filePath As String) As SpsTrack
        Try
            Dim doc As New XmlDocument()
            doc.Load(filePath)

            Dim track As New SpsTrack() With {.FilePath = filePath}

            ' Support both <SPS><Package>…</Package></SPS> and flat root
            Dim pkg = doc.SelectSingleNode("//Package")
            If pkg Is Nothing Then pkg = doc.DocumentElement
            If pkg Is Nothing Then Return Nothing

            track.PackageCode = GetNodeText(pkg, "PackageCode")
            track.Title = GetNodeText(pkg, "Title")
            track.Description = GetNodeText(pkg, "Description")
            track.Category = GetNodeText(pkg, "Category")
            track.SubCategory = GetNodeText(pkg, "SubCategory")
            track.Keywords = GetNodeText(pkg, "Keywords")
            track.ExternalVersion = GetNodeText(pkg, "ExternalVersion")
            track.ExternalVersionUrl = GetNodeText(pkg, "ExternalVersionUrl")
            track.HomePage = GetNodeText(pkg, "HomePage")
            track.InfoLink = GetNodeText(pkg, "InfoLink")
            track.DownloadLink = GetNodeText(pkg, "DownloadLink")
            track.Checksum = GetNodeText(pkg, "Checksum")
            track.TrackUrl = GetNodeText(pkg, "TrackUrl")
            track.StartString = GetNodeText(pkg, "StartString")
            track.StopString = GetNodeText(pkg, "StopString")

            ' Fall back to filename when Title is absent
            If String.IsNullOrWhiteSpace(track.Title) Then
                track.Title = Path.GetFileNameWithoutExtension(filePath)
            End If

            Return track

        Catch ex As Exception
            Return Nothing   ' Skip malformed files silently
        End Try
    End Function

    Private Sub SaveSpsFile(track As SpsTrack)
        Dim doc As New XmlDocument()

        ' Load the existing file to preserve any unknown fields
        If File.Exists(track.FilePath) Then
            doc.Load(track.FilePath)
        Else
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", Nothing))
            Dim spsRoot = doc.CreateElement("SPS")
            doc.AppendChild(spsRoot)
            spsRoot.AppendChild(doc.CreateElement("Package"))
        End If

        Dim pkg = doc.SelectSingleNode("//Package")
        If pkg Is Nothing Then
            Dim spsRoot = doc.DocumentElement
            pkg = doc.CreateElement("Package")
            spsRoot.AppendChild(pkg)
        End If

        SetNodeText(doc, pkg, "Title", track.Title)
        SetNodeText(doc, pkg, "Description", track.Description)
        SetNodeText(doc, pkg, "ExternalVersion", track.ExternalVersion)
        SetNodeText(doc, pkg, "HomePage", track.HomePage)
        SetNodeText(doc, pkg, "DownloadLink", track.DownloadLink)
        SetNodeText(doc, pkg, "TrackUrl", track.TrackUrl)
        SetNodeText(doc, pkg, "StartString", track.StartString)
        SetNodeText(doc, pkg, "StopString", track.StopString)

        Dim settings As New XmlWriterSettings() With {
            .Indent = True,
            .IndentChars = "  ",
            .Encoding = New UTF8Encoding(False)   ' UTF-8 without Byte Order Mark
        }
        Using writer = XmlWriter.Create(track.FilePath, settings)
            doc.Save(writer)
        End Using
    End Sub

    '============================================================
    ' ListView helpers
    '============================================================

    Private Shared Function BuildListViewItem(track As SpsTrack) As ListViewItem
        Dim item As New ListViewItem(track.Title)
        item.SubItems.Add(track.ExternalVersion)
        item.SubItems.Add(track.Status)
        item.Tag = track
        Return item
    End Function

    '============================================================
    ' ListView – selection changed → populate / clear right pane
    '============================================================

    Private Sub lvwTracks_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles lvwTracks.SelectedIndexChanged

        If lvwTracks.SelectedItems.Count = 0 Then
            currentTrack = Nothing
            SetRightPaneEnabled(False)
            Return
        End If

        currentTrack = TryCast(lvwTracks.SelectedItems(0).Tag, SpsTrack)
        If currentTrack IsNot Nothing Then
            PopulateRightPanel(currentTrack)
            SetRightPaneEnabled(True)
        End If
    End Sub

    Private Sub lvwTracks_DoubleClick(sender As Object, e As EventArgs) _
        Handles lvwTracks.DoubleClick

        ' Double-click jumps focus to the Name field for quick editing
        If currentTrack IsNot Nothing Then
            txtName.Focus()
            txtName.SelectAll()
        End If
    End Sub

    Private Sub PopulateRightPanel(track As SpsTrack)
        txtName.Text = track.Title
        txtVersion.Text = track.ExternalVersion
        txtHomePage.Text = track.HomePage
        txtDownloadLink.Text = track.DownloadLink
        ' Pre-fill Track URL from HomePage when not yet set
        txtTrackUrl.Text = GetEffectiveTrackUrl(track)
        txtStartString.Text = track.StartString
        txtStopString.Text = track.StopString
        txtDescription.Text = track.Description

        ' Clear the web viewer when switching to a different track
        rtbContent.Clear()
        ResetSearchState()

        sslFileName.Text = $"File: {Path.GetFileName(track.FilePath)}"
    End Sub

    Private Sub ClearRightPanel()
        txtName.Text = String.Empty
        txtVersion.Text = String.Empty
        txtHomePage.Text = String.Empty
        txtDownloadLink.Text = String.Empty
        txtTrackUrl.Text = String.Empty
        txtStartString.Text = String.Empty
        txtStopString.Text = String.Empty
        txtDescription.Text = String.Empty
        txtFind.Text = String.Empty
        rtbContent.Clear()
        ResetSearchState()
        currentTrack = Nothing
    End Sub

    '============================================================
    ' Download Web Page
    '============================================================

    Private Async Sub btnDownloadPage_Click(sender As Object, e As EventArgs) _
        Handles btnDownloadPage.Click

        Dim url = txtTrackUrl.Text.Trim()
        If String.IsNullOrWhiteSpace(url) Then
            MessageBox.Show("Please enter a Track URL to download.",
                            "No URL", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim uri As Uri = Nothing
        If Not Uri.TryCreate(url, UriKind.Absolute, uri) OrElse
           (uri.Scheme <> Uri.UriSchemeHttp AndAlso uri.Scheme <> Uri.UriSchemeHttps) Then
            MessageBox.Show("Please enter a valid HTTP or HTTPS URL.",
                            "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        btnDownloadPage.Enabled = False
        btnDownloadPage.Text = "Downloading…"
        sslFileName.Text = $"Downloading: {url}"
        rtbContent.Clear()
        ResetSearchState()

        Try
            Dim client As New WebClient()
            client.Encoding = Encoding.UTF8
            client.Headers.Add(HttpRequestHeader.UserAgent,
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " &
                "AppleWebKit/537.36 (KHTML, like Gecko) " &
                "Chrome/120.0 Safari/537.36")

            Dim html = Await client.DownloadStringTaskAsync(uri)
            Dim text = StripHtml(html)

            rtbContent.Text = text
            sslFileName.Text = $"Downloaded {text.Length:N0} chars from: {url}"

        Catch ex As WebException
            sslFileName.Text = $"Download failed: {ex.Message}"
            MessageBox.Show($"Could not download the page:{Environment.NewLine}{ex.Message}",
                            "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            sslFileName.Text = $"Error: {ex.Message}"
            MessageBox.Show($"Unexpected error:{Environment.NewLine}{ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnDownloadPage.Enabled = True
            btnDownloadPage.Text = "Download"
        End Try
    End Sub

    Private Sub btnBrowseUrl_Click(sender As Object, e As EventArgs) _
        Handles btnBrowseUrl.Click
        OpenUrl(txtTrackUrl.Text.Trim())
    End Sub

    ''' <summary>Converts raw HTML to readable plain text.</summary>
    Private Shared Function StripHtml(html As String) As String
        ' Remove <script> and <style> blocks entirely
        Dim result = Regex.Replace(html, "<script[^>]*>[\s\S]*?</script>",
                                   String.Empty, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "<style[^>]*>[\s\S]*?</style>",
                                String.Empty, RegexOptions.IgnoreCase)
        ' Convert block-level tags to newlines
        result = Regex.Replace(result, "<br\s*/?>", Environment.NewLine, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "</(p|div|h[1-6]|li|tr)>",
                                Environment.NewLine, RegexOptions.IgnoreCase)
        ' Strip remaining tags
        result = Regex.Replace(result, "<[^>]+>", String.Empty)
        ' Decode common HTML entities
        result = result.Replace("&amp;", "&")
        result = result.Replace("&lt;", "<")
        result = result.Replace("&gt;", ">")
        result = result.Replace("&quot;", """")
        result = result.Replace("&#39;", "'")
        result = result.Replace("&apos;", "'")
        result = result.Replace("&nbsp;", " ")
        ' Collapse excessive blank lines
        result = Regex.Replace(result, "(\r?\n){3,}", Environment.NewLine & Environment.NewLine)
        Return result.Trim()
    End Function

    '============================================================
    ' Start/Stop String – Find in Page
    '============================================================

    Private Sub btnFindStart_Click(sender As Object, e As EventArgs) _
        Handles btnFindStart.Click
        FindStringInContent(txtStartString.Text, "Start String")
    End Sub

    Private Sub btnFindStop_Click(sender As Object, e As EventArgs) _
        Handles btnFindStop.Click
        FindStringInContent(txtStopString.Text, "Stop String")
    End Sub

    ''' <summary>
    ''' Finds the first occurrence of <paramref name="searchText"/> in the
    ''' RichTextBox, highlights it in orange, and scrolls to it.
    ''' </summary>
    Private Sub FindStringInContent(searchText As String, fieldName As String)
        If String.IsNullOrWhiteSpace(searchText) Then
            MessageBox.Show($"Please enter a {fieldName} value before searching.",
                            "No Search String", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrEmpty(rtbContent.Text) Then
            MessageBox.Show("The page content is empty. Download a page first.",
                            "No Content", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim pos = rtbContent.Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase)
        If pos >= 0 Then
            ' Reset colours, then highlight the hit
            rtbContent.SelectAll()
            rtbContent.SelectionBackColor = Drawing.Color.Black
            rtbContent.SelectionColor = Drawing.Color.Lime
            rtbContent.Select(pos, searchText.Length)
            rtbContent.SelectionBackColor = Drawing.Color.Orange
            rtbContent.SelectionColor = Drawing.Color.Black
            rtbContent.ScrollToCaret()
            sslFileName.Text = $"{fieldName} found at position {pos}"
        Else
            sslFileName.Text = $"{fieldName} not found in page content"
            MessageBox.Show($"The {fieldName} text was not found in the page content.",
                            "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    '============================================================
    ' Text Search – 4-direction find in RichTextBox
    '============================================================

    Private Sub ResetSearchState()
        lblMatches.Text = String.Empty
        lblMatches.ForeColor = Drawing.Color.Gray
    End Sub

    ''' <summary>Search forward from the very beginning of the content.</summary>
    Private Sub btnFindFromTop_Click(sender As Object, e As EventArgs) _
        Handles btnFindFromTop.Click

        Dim term = txtFind.Text
        If String.IsNullOrEmpty(term) OrElse String.IsNullOrEmpty(rtbContent.Text) Then Return

        Dim pos = rtbContent.Text.IndexOf(term, 0, StringComparison.OrdinalIgnoreCase)
        If pos >= 0 Then
            HighlightSearchResult(pos, term.Length)
            ' Place caret just after the hit so From CARET continues forward
            rtbContent.Select(pos + term.Length, 0)
            lblMatches.Text = $"Found at {pos}"
            lblMatches.ForeColor = Drawing.Color.LimeGreen
        Else
            lblMatches.Text = "Not found"
            lblMatches.ForeColor = Drawing.Color.Red
        End If
    End Sub

    ''' <summary>Search forward from the current caret position.</summary>
    Private Sub btnFindFromCaret_Click(sender As Object, e As EventArgs) _
        Handles btnFindFromCaret.Click

        Dim term = txtFind.Text
        If String.IsNullOrEmpty(term) OrElse String.IsNullOrEmpty(rtbContent.Text) Then Return

        Dim startPos = rtbContent.SelectionStart + rtbContent.SelectionLength
        If startPos >= rtbContent.Text.Length Then startPos = 0

        Dim pos = rtbContent.Text.IndexOf(term, startPos, StringComparison.OrdinalIgnoreCase)
        If pos >= 0 Then
            HighlightSearchResult(pos, term.Length)
            rtbContent.Select(pos + term.Length, 0)
            lblMatches.Text = $"Found at {pos}"
            lblMatches.ForeColor = Drawing.Color.LimeGreen
        Else
            lblMatches.Text = "Not found from caret"
            lblMatches.ForeColor = Drawing.Color.Red
        End If
    End Sub

    ''' <summary>Search backward from the very end of the content.</summary>
    Private Sub btnFindRevBottom_Click(sender As Object, e As EventArgs) _
        Handles btnFindRevBottom.Click

        Dim term = txtFind.Text
        If String.IsNullOrEmpty(term) OrElse String.IsNullOrEmpty(rtbContent.Text) Then Return

        Dim pos = rtbContent.Text.LastIndexOf(term, StringComparison.OrdinalIgnoreCase)
        If pos >= 0 Then
            HighlightSearchResult(pos, term.Length)
            rtbContent.Select(pos, 0)
            lblMatches.Text = $"Found at {pos} (from bottom)"
            lblMatches.ForeColor = Drawing.Color.LimeGreen
        Else
            lblMatches.Text = "Not found"
            lblMatches.ForeColor = Drawing.Color.Red
        End If
    End Sub

    ''' <summary>Search backward from the current caret position.</summary>
    Private Sub btnFindRevCaret_Click(sender As Object, e As EventArgs) _
        Handles btnFindRevCaret.Click

        Dim term = txtFind.Text
        If String.IsNullOrEmpty(term) OrElse String.IsNullOrEmpty(rtbContent.Text) Then Return

        Dim caretPos = rtbContent.SelectionStart
        ' If caret is at the very start, wrap to end
        If caretPos <= 0 Then caretPos = rtbContent.Text.Length

        Dim searchFrom = Math.Max(0, Math.Min(caretPos - 1, rtbContent.Text.Length - 1))
        If searchFrom < 0 Then
            lblMatches.Text = "Not found (reverse)"
            lblMatches.ForeColor = Drawing.Color.Red
            Return
        End If

        Dim pos = rtbContent.Text.LastIndexOf(term, searchFrom, StringComparison.OrdinalIgnoreCase)
        If pos >= 0 Then
            HighlightSearchResult(pos, term.Length)
            rtbContent.Select(pos, 0)
            lblMatches.Text = $"Found at {pos} (rev from caret)"
            lblMatches.ForeColor = Drawing.Color.LimeGreen
        Else
            lblMatches.Text = "Not found (reverse from caret)"
            lblMatches.ForeColor = Drawing.Color.Red
        End If
    End Sub

    ''' <summary>
    ''' Resets all text colours, then highlights the match at
    ''' <paramref name="pos"/> in yellow and scrolls to it.
    ''' </summary>
    Private Sub HighlightSearchResult(pos As Integer, length As Integer)
        rtbContent.SelectAll()
        rtbContent.SelectionBackColor = Drawing.Color.Black
        rtbContent.SelectionColor = Drawing.Color.Lime
        rtbContent.Select(pos, length)
        rtbContent.SelectionBackColor = Drawing.Color.Yellow
        rtbContent.SelectionColor = Drawing.Color.Black
        rtbContent.ScrollToCaret()
    End Sub

    ''' <summary>Enter = From CARET; Shift+Enter = Rev from CARET; Esc = reset.</summary>
    Private Sub txtFind_KeyDown(sender As Object, e As KeyEventArgs) _
        Handles txtFind.KeyDown

        Select Case e.KeyCode
            Case Keys.Enter
                If e.Shift Then
                    btnFindRevCaret_Click(sender, e)
                Else
                    btnFindFromCaret_Click(sender, e)
                End If
                e.Handled = True
            Case Keys.Escape
                ResetSearchState()
                rtbContent.SelectAll()
                rtbContent.SelectionBackColor = Drawing.Color.Black
                rtbContent.SelectionColor = Drawing.Color.Lime
                rtbContent.Select(0, 0)
                e.Handled = True
        End Select
    End Sub

    '============================================================
    ' Track Checked Lines – batch version extraction
    '============================================================

    ''' <summary>
    ''' For every checked ListView item: downloads the Track URL, extracts
    ''' the version string between StartString and StopString, updates the
    ''' ExternalVersion field, and saves the SPS file.
    ''' </summary>
    Private Async Sub TrackCheckedLines()
        Dim checkedItems = lvwTracks.Items.Cast(Of ListViewItem)().
                           Where(Function(i) i.Checked).ToList()

        If checkedItems.Count = 0 Then
            MessageBox.Show("Please check at least one track in the list.",
                            "No Tracks Checked", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(currentFolder) Then
            MessageBox.Show("Please open a folder first.",
                            "No Folder Open", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        tsbTrackChecked.Enabled = False
        sslProgress.Visible = True
        sslProgress.Minimum = 0
        sslProgress.Maximum = checkedItems.Count
        sslProgress.Value = 0

        Dim updated = 0
        Dim errors = 0

        Dim client As New WebClient()
        client.Encoding = Encoding.UTF8
        client.Headers.Add(HttpRequestHeader.UserAgent,
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " &
            "AppleWebKit/537.36 (KHTML, like Gecko) " &
            "Chrome/120.0 Safari/537.36")

        For Each item As ListViewItem In checkedItems
            Dim track = TryCast(item.Tag, SpsTrack)
            If track Is Nothing Then Continue For

            sslFileName.Text = $"Tracking: {track.Title}…"

            Try
                ' Use TrackUrl; fall back to HomePage
                Dim url = GetEffectiveTrackUrl(track)
                If String.IsNullOrWhiteSpace(url) Then Continue For

                Dim uri As Uri = Nothing
                If Not Uri.TryCreate(url, UriKind.Absolute, uri) Then Continue For

                Dim html = Await client.DownloadStringTaskAsync(uri)
                Dim text = StripHtml(html)

                If Not String.IsNullOrWhiteSpace(track.StartString) AndAlso
                   Not String.IsNullOrWhiteSpace(track.StopString) Then

                    Dim extracted = ExtractBetween(text, track.StartString, track.StopString)
                    If Not String.IsNullOrWhiteSpace(extracted) Then
                        track.ExternalVersion = extracted.Trim()
                        track.Status = "Updated"
                        item.SubItems(1).Text = track.ExternalVersion
                        item.SubItems(2).Text = track.Status
                        Try
                            SaveSpsFile(track)
                            updated += 1
                        Catch saveEx As Exception
                            track.Status = "Save Error"
                            item.SubItems(2).Text = track.Status
                            errors += 1
                        End Try
                    Else
                        track.Status = "No Match"
                        item.SubItems(2).Text = track.Status
                    End If
                End If

            Catch ex As Exception
                track.Status = "Error"
                item.SubItems(2).Text = track.Status
                errors += 1
            End Try

            sslProgress.Value = Math.Min(sslProgress.Value + 1, sslProgress.Maximum)
        Next

        sslProgress.Visible = False
        tsbTrackChecked.Enabled = True
        sslFileName.Text =
            $"Track complete: {updated} updated, {errors} error(s), {checkedItems.Count} checked"
        UpdateStatusBar()
    End Sub

    ''' <summary>
    ''' Returns the effective URL to use when tracking a version for
    ''' <paramref name="track"/>: the explicit TrackUrl when set, otherwise
    ''' the HomePage as a fallback.
    ''' </summary>
    Private Shared Function GetEffectiveTrackUrl(track As SpsTrack) As String
        Return If(String.IsNullOrWhiteSpace(track.TrackUrl), track.HomePage, track.TrackUrl)
    End Function

    ''' <summary>
    ''' Returns the text between the first occurrence of <paramref name="startMarker"/>
    ''' and the first <paramref name="stopMarker"/> that follows it.
    ''' Comparison is case-insensitive.
    ''' </summary>
    Private Shared Function ExtractBetween(content As String,
                                            startMarker As String,
                                            stopMarker As String) As String
        If String.IsNullOrEmpty(content) Then Return String.Empty

        Dim startIdx = content.IndexOf(startMarker, StringComparison.OrdinalIgnoreCase)
        If startIdx < 0 Then Return String.Empty

        startIdx += startMarker.Length

        Dim stopIdx = content.IndexOf(stopMarker, startIdx, StringComparison.OrdinalIgnoreCase)
        If stopIdx < 0 Then Return String.Empty

        Return content.Substring(startIdx, stopIdx - startIdx).Trim()
    End Function

    '============================================================
    ' Save / Save-Exit Track
    '============================================================

    Private Sub SaveCurrentTrack()
        If currentTrack Is Nothing Then
            MessageBox.Show("Please select a track from the list first.",
                            "No Track Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Copy UI values into the track object
        currentTrack.Title = txtName.Text.Trim()
        currentTrack.ExternalVersion = txtVersion.Text.Trim()
        currentTrack.HomePage = txtHomePage.Text.Trim()
        currentTrack.DownloadLink = txtDownloadLink.Text.Trim()
        currentTrack.TrackUrl = txtTrackUrl.Text.Trim()
        currentTrack.StartString = txtStartString.Text.Trim()
        currentTrack.StopString = txtStopString.Text.Trim()
        currentTrack.Description = txtDescription.Text.Trim()

        Try
            SaveSpsFile(currentTrack)

            ' Refresh the ListView row
            Dim item = lvwTracks.SelectedItems(0)
            item.Text = currentTrack.Title
            item.SubItems(1).Text = currentTrack.ExternalVersion
            item.SubItems(2).Text = currentTrack.Status

            sslFileName.Text = $"Saved: {currentTrack.Title}"

        Catch ex As Exception
            MessageBox.Show($"Error saving track:{Environment.NewLine}{ex.Message}",
                            "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Saves the current track, then deselects it and returns the
    ''' right pane to its disabled/empty state.
    ''' </summary>
    Private Sub SaveAndExit()
        SaveCurrentTrack()
        If currentTrack IsNot Nothing Then   ' SaveCurrentTrack may have failed
            lvwTracks.SelectedItems.Clear()
            ClearRightPanel()
            SetRightPaneEnabled(False)
            sslFileName.Text = "Ready"
        End If
    End Sub

    '============================================================
    ' Add / Delete Track
    '============================================================

    Private Sub AddNewTrack()
        If String.IsNullOrWhiteSpace(currentFolder) Then
            MessageBox.Show("Please open a folder first before adding a track.",
                            "No Folder Open", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        lvwTracks.SelectedItems.Clear()
        ClearRightPanel()

        Dim track As New SpsTrack() With {
            .FilePath = Path.Combine(currentFolder, "new_track.sps"),
            .Title = "New Track",
            .PackageCode = Guid.NewGuid().ToString(),
            .Status = "New"
        }
        currentTrack = track
        tracks.Add(track)

        Dim item = BuildListViewItem(track)
        lvwTracks.Items.Add(item)
        item.Selected = True
        item.EnsureVisible()

        PopulateRightPanel(track)
        SetRightPaneEnabled(True)
        txtName.Focus()
        txtName.SelectAll()

        sslFileName.Text = "New track created – fill in details and click Save"
        UpdateStatusBar()
    End Sub

    Private Sub DeleteSelectedTrack()
        If lvwTracks.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a track to delete.",
                            "No Track Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim track = TryCast(lvwTracks.SelectedItems(0).Tag, SpsTrack)
        If track Is Nothing Then Return

        Dim answer = MessageBox.Show(
            $"Are you sure you want to delete '{track.Title}'?" &
            Environment.NewLine & "The SPS file will be permanently removed from disk.",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If answer = DialogResult.Yes Then
            Try
                If File.Exists(track.FilePath) Then File.Delete(track.FilePath)
                tracks.Remove(track)
                lvwTracks.SelectedItems(0).Remove()
                ClearRightPanel()
                SetRightPaneEnabled(False)
                sslFileName.Text = $"Deleted: {track.Title}"
                UpdateStatusBar()
            Catch ex As Exception
                MessageBox.Show($"Error deleting track:{Environment.NewLine}{ex.Message}",
                                "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    '============================================================
    ' Open URL in the default browser
    '============================================================

    Private Sub OpenUrl(url As String)
        If String.IsNullOrWhiteSpace(url) Then Return
        Try
            Process.Start(New ProcessStartInfo(url) With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show($"Could not open URL:{Environment.NewLine}{ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '============================================================
    ' Status bar
    '============================================================

    Private Sub UpdateStatusBar()
        sslCount.Text = $"{tracks.Count} track{If(tracks.Count = 1, "", "s")}"
    End Sub

    '============================================================
    ' Toolbar handlers
    '============================================================

    Private Sub tsbOpenFolder_Click(sender As Object, e As EventArgs) _
        Handles tsbOpenFolder.Click
        OpenFolder()
    End Sub

    Private Sub tsbTrackChecked_Click(sender As Object, e As EventArgs) _
        Handles tsbTrackChecked.Click
        TrackCheckedLines()
    End Sub

    Private Sub tsbRebuild_Click(sender As Object, e As EventArgs) _
        Handles tsbRebuild.Click
        If String.IsNullOrWhiteSpace(currentFolder) Then
            MessageBox.Show("Please open a folder first.",
                            "No Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        LoadTracksFromFolder(currentFolder)
    End Sub

    Private Sub tsbAddTrack_Click(sender As Object, e As EventArgs) _
        Handles tsbAddTrack.Click
        AddNewTrack()
    End Sub

    Private Sub tsbDeleteTrack_Click(sender As Object, e As EventArgs) _
        Handles tsbDeleteTrack.Click
        DeleteSelectedTrack()
    End Sub

    Private Sub tsbHelp_Click(sender As Object, e As EventArgs) _
        Handles tsbHelp.Click
        ShowHelp()
    End Sub

    '============================================================
    ' Menu handlers
    '============================================================

    Private Sub tsmiOpenFolder_Click(sender As Object, e As EventArgs) _
        Handles tsmiOpenFolder.Click
        OpenFolder()
    End Sub

    Private Sub tsmiSave_Click(sender As Object, e As EventArgs) _
        Handles tsmiSave.Click
        SaveCurrentTrack()
    End Sub

    Private Sub tsmiExit_Click(sender As Object, e As EventArgs) _
        Handles tsmiExit.Click
        Me.Close()
    End Sub

    Private Sub tsmiAddTrack_Click(sender As Object, e As EventArgs) _
        Handles tsmiAddTrack.Click
        AddNewTrack()
    End Sub

    Private Sub tsmiDeleteTrack_Click(sender As Object, e As EventArgs) _
        Handles tsmiDeleteTrack.Click
        DeleteSelectedTrack()
    End Sub

    Private Sub tsmiHelpContents_Click(sender As Object, e As EventArgs) _
        Handles tsmiHelpContents.Click
        ShowHelp()
    End Sub

    Private Sub tsmiAbout_Click(sender As Object, e As EventArgs) _
        Handles tsmiAbout.Click
        MessageBox.Show(
            "SPS PortableAppTrack" & Environment.NewLine &
            "Version 1.0" & Environment.NewLine & Environment.NewLine &
            "Used alongside SyMenu / SPSBuilder to track portable" & Environment.NewLine &
            "app updates and manage SPS metadata files.",
            "About SPS PortableAppTrack",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)
    End Sub

    '============================================================
    ' Help
    '============================================================

    Private Sub ShowHelp()
        Dim helpText As String =
            "SPS PortableAppTrack – Help" & Environment.NewLine &
            Environment.NewLine &
            "LEFT PANE – Track List:" & Environment.NewLine &
            "  Open Folder        – load all .sps files from a folder" & Environment.NewLine &
            "  Track Checked Lines – download each checked track's page and" & Environment.NewLine &
            "                        extract the version between Start/Stop strings" & Environment.NewLine &
            "  ReBuild             – reload tracks from the current folder" & Environment.NewLine &
            "  Add / Delete        – create or remove individual tracks" & Environment.NewLine &
            "  Check boxes         – tick tracks for batch version tracking" & Environment.NewLine &
            "  Left-click a track  – edit its details in the right pane" & Environment.NewLine &
            Environment.NewLine &
            "RIGHT PANE – Track Details:" & Environment.NewLine &
            "  Track URL    – URL of the page to download for version checking" & Environment.NewLine &
            "  Download     – fetch the Track URL and show content below" & Environment.NewLine &
            "  Browser      – open the Track URL in the default web browser" & Environment.NewLine &
            "  Start String – text appearing just BEFORE the version number" & Environment.NewLine &
            "  Stop String  – text appearing just AFTER  the version number" & Environment.NewLine &
            "  Find in Page – locate the Start/Stop string in the content" & Environment.NewLine &
            "  Save Changes – save track data to the SPS file (Ctrl+S)" & Environment.NewLine &
            "  Save && Exit – save, then deselect and return to list view" & Environment.NewLine &
            Environment.NewLine &
            "FIND TOOLBAR (below the content area):" & Environment.NewLine &
            "  From TOP        – search forward from start of content" & Environment.NewLine &
            "  From CARET      – search forward from cursor position" & Environment.NewLine &
            "  Rev from BOTTOM – search backward from end of content" & Environment.NewLine &
            "  Rev from CARET  – search backward from cursor position" & Environment.NewLine &
            "  Enter           – From CARET" & Environment.NewLine &
            "  Shift+Enter     – Rev from CARET" & Environment.NewLine &
            "  Escape          – clear search highlight"

        MessageBox.Show(helpText, "Help – SPS PortableAppTrack",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    '============================================================
    ' Detail-pane button handlers
    '============================================================

    Private Sub btnSave_Click(sender As Object, e As EventArgs) _
        Handles btnSave.Click
        SaveCurrentTrack()
    End Sub

    Private Sub btnSaveExit_Click(sender As Object, e As EventArgs) _
        Handles btnSaveExit.Click
        SaveAndExit()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) _
        Handles btnClear.Click
        lvwTracks.SelectedItems.Clear()
        ClearRightPanel()
        SetRightPaneEnabled(False)
        sslFileName.Text = "Ready"
    End Sub

    Private Sub btnOpenHomePage_Click(sender As Object, e As EventArgs) _
        Handles btnOpenHomePage.Click
        OpenUrl(txtHomePage.Text.Trim())
    End Sub

    '============================================================
    ' Context menu – enable / disable items based on selection
    '============================================================

    Private Sub ctxListMenu_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles ctxListMenu.Opening

        Dim hasSelection = (lvwTracks.SelectedItems.Count > 0)
        ctxOpenHome.Enabled = hasSelection
        ctxCopyHome.Enabled = hasSelection
        ctxCopyDownload.Enabled = hasSelection
        ctxDeleteTrack.Enabled = hasSelection
    End Sub

    Private Sub ctxOpenHome_Click(sender As Object, e As EventArgs) _
        Handles ctxOpenHome.Click
        If currentTrack IsNot Nothing Then OpenUrl(currentTrack.HomePage)
    End Sub

    Private Sub ctxCopyHome_Click(sender As Object, e As EventArgs) _
        Handles ctxCopyHome.Click
        If currentTrack IsNot Nothing AndAlso
           Not String.IsNullOrWhiteSpace(currentTrack.HomePage) Then
            Clipboard.SetText(currentTrack.HomePage)
            sslFileName.Text = "Home Page URL copied to clipboard"
        End If
    End Sub

    Private Sub ctxCopyDownload_Click(sender As Object, e As EventArgs) _
        Handles ctxCopyDownload.Click
        If currentTrack IsNot Nothing AndAlso
           Not String.IsNullOrWhiteSpace(currentTrack.DownloadLink) Then
            Clipboard.SetText(currentTrack.DownloadLink)
            sslFileName.Text = "Download URL copied to clipboard"
        End If
    End Sub

    Private Sub ctxDeleteTrack_Click(sender As Object, e As EventArgs) _
        Handles ctxDeleteTrack.Click
        DeleteSelectedTrack()
    End Sub

End Class

'================================================================
' SpsTrack – data model for one SPS metadata file
'================================================================

Public Class SpsTrack
    Public Property FilePath As String = String.Empty
    Public Property PackageCode As String = String.Empty
    Public Property Title As String = String.Empty
    Public Property Description As String = String.Empty
    Public Property Category As String = String.Empty
    Public Property SubCategory As String = String.Empty
    Public Property Keywords As String = String.Empty
    Public Property ExternalVersion As String = String.Empty
    Public Property ExternalVersionUrl As String = String.Empty
    Public Property HomePage As String = String.Empty
    Public Property InfoLink As String = String.Empty
    Public Property DownloadLink As String = String.Empty
    Public Property Checksum As String = String.Empty
    ''' <summary>URL of the page to download to check for a new version.</summary>
    Public Property TrackUrl As String = String.Empty
    ''' <summary>Text appearing immediately before the version number on the Track URL page.</summary>
    Public Property StartString As String = String.Empty
    ''' <summary>Text appearing immediately after the version number on the Track URL page.</summary>
    Public Property StopString As String = String.Empty
    Public Property Status As String = String.Empty
    Public Property IsModified As Boolean = False
End Class
