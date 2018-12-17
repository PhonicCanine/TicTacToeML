<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GameScreen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btn0 = New System.Windows.Forms.Button()
        Me.btn1 = New System.Windows.Forms.Button()
        Me.btn2 = New System.Windows.Forms.Button()
        Me.btn3 = New System.Windows.Forms.Button()
        Me.btn4 = New System.Windows.Forms.Button()
        Me.btn5 = New System.Windows.Forms.Button()
        Me.btn6 = New System.Windows.Forms.Button()
        Me.btn7 = New System.Windows.Forms.Button()
        Me.btn8 = New System.Windows.Forms.Button()
        Me.btnPractice = New System.Windows.Forms.Button()
        Me.btnMulti = New System.Windows.Forms.Button()
        Me.btnTimeAttk = New System.Windows.Forms.Button()
        Me.btnNameChange = New System.Windows.Forms.Button()
        Me.btnQuit = New System.Windows.Forms.Button()
        Me.pnlOne = New System.Windows.Forms.Panel()
        Me.lblName = New System.Windows.Forms.Label()
        Me.tmrHomeScreen = New System.Windows.Forms.Timer(Me.components)
        Me.timTimeAttk = New System.Windows.Forms.Timer(Me.components)
        Me.lblTime = New System.Windows.Forms.Label()
        Me.lblScore = New System.Windows.Forms.Label()
        Me.lblCurrentHS = New System.Windows.Forms.Label()
        Me.lblCurrentHSer = New System.Windows.Forms.Label()
        Me.pnlOne.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn0
        '
        Me.btn0.BackColor = System.Drawing.Color.LightGray
        Me.btn0.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn0.Location = New System.Drawing.Point(20, 20)
        Me.btn0.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn0.Name = "btn0"
        Me.btn0.Size = New System.Drawing.Size(112, 115)
        Me.btn0.TabIndex = 0
        Me.btn0.Tag = "0"
        Me.btn0.UseVisualStyleBackColor = False
        '
        'btn1
        '
        Me.btn1.BackColor = System.Drawing.Color.LightGray
        Me.btn1.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn1.Location = New System.Drawing.Point(141, 20)
        Me.btn1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn1.Name = "btn1"
        Me.btn1.Size = New System.Drawing.Size(112, 115)
        Me.btn1.TabIndex = 1
        Me.btn1.Tag = "1"
        Me.btn1.UseVisualStyleBackColor = False
        '
        'btn2
        '
        Me.btn2.BackColor = System.Drawing.Color.LightGray
        Me.btn2.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn2.Location = New System.Drawing.Point(262, 20)
        Me.btn2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn2.Name = "btn2"
        Me.btn2.Size = New System.Drawing.Size(112, 115)
        Me.btn2.TabIndex = 2
        Me.btn2.Tag = "2"
        Me.btn2.UseVisualStyleBackColor = False
        '
        'btn3
        '
        Me.btn3.BackColor = System.Drawing.Color.LightGray
        Me.btn3.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn3.Location = New System.Drawing.Point(20, 145)
        Me.btn3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn3.Name = "btn3"
        Me.btn3.Size = New System.Drawing.Size(112, 115)
        Me.btn3.TabIndex = 3
        Me.btn3.Tag = "3"
        Me.btn3.UseVisualStyleBackColor = False
        '
        'btn4
        '
        Me.btn4.BackColor = System.Drawing.Color.LightGray
        Me.btn4.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn4.Location = New System.Drawing.Point(141, 145)
        Me.btn4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn4.Name = "btn4"
        Me.btn4.Size = New System.Drawing.Size(112, 115)
        Me.btn4.TabIndex = 4
        Me.btn4.Tag = "4"
        Me.btn4.UseVisualStyleBackColor = False
        '
        'btn5
        '
        Me.btn5.BackColor = System.Drawing.Color.LightGray
        Me.btn5.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn5.Location = New System.Drawing.Point(262, 145)
        Me.btn5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn5.Name = "btn5"
        Me.btn5.Size = New System.Drawing.Size(112, 115)
        Me.btn5.TabIndex = 5
        Me.btn5.Tag = "5"
        Me.btn5.UseVisualStyleBackColor = False
        '
        'btn6
        '
        Me.btn6.BackColor = System.Drawing.Color.LightGray
        Me.btn6.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn6.Location = New System.Drawing.Point(20, 268)
        Me.btn6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn6.Name = "btn6"
        Me.btn6.Size = New System.Drawing.Size(112, 115)
        Me.btn6.TabIndex = 6
        Me.btn6.Tag = "6"
        Me.btn6.UseVisualStyleBackColor = False
        '
        'btn7
        '
        Me.btn7.BackColor = System.Drawing.Color.LightGray
        Me.btn7.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn7.Location = New System.Drawing.Point(141, 268)
        Me.btn7.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn7.Name = "btn7"
        Me.btn7.Size = New System.Drawing.Size(112, 115)
        Me.btn7.TabIndex = 7
        Me.btn7.Tag = "7"
        Me.btn7.UseVisualStyleBackColor = False
        '
        'btn8
        '
        Me.btn8.BackColor = System.Drawing.Color.LightGray
        Me.btn8.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn8.Location = New System.Drawing.Point(262, 268)
        Me.btn8.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn8.Name = "btn8"
        Me.btn8.Size = New System.Drawing.Size(112, 115)
        Me.btn8.TabIndex = 8
        Me.btn8.Tag = "8"
        Me.btn8.UseVisualStyleBackColor = False
        '
        'btnPractice
        '
        Me.btnPractice.Location = New System.Drawing.Point(0, 2)
        Me.btnPractice.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnPractice.Name = "btnPractice"
        Me.btnPractice.Size = New System.Drawing.Size(228, 35)
        Me.btnPractice.TabIndex = 9
        Me.btnPractice.Text = "Practice"
        Me.btnPractice.UseVisualStyleBackColor = True
        '
        'btnMulti
        '
        Me.btnMulti.Location = New System.Drawing.Point(0, 46)
        Me.btnMulti.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnMulti.Name = "btnMulti"
        Me.btnMulti.Size = New System.Drawing.Size(228, 35)
        Me.btnMulti.TabIndex = 10
        Me.btnMulti.Text = "Multiplayer"
        Me.btnMulti.UseVisualStyleBackColor = True
        '
        'btnTimeAttk
        '
        Me.btnTimeAttk.Location = New System.Drawing.Point(0, 91)
        Me.btnTimeAttk.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnTimeAttk.Name = "btnTimeAttk"
        Me.btnTimeAttk.Size = New System.Drawing.Size(228, 35)
        Me.btnTimeAttk.TabIndex = 11
        Me.btnTimeAttk.Text = "Time Attack"
        Me.btnTimeAttk.UseVisualStyleBackColor = True
        '
        'btnNameChange
        '
        Me.btnNameChange.Location = New System.Drawing.Point(0, 249)
        Me.btnNameChange.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnNameChange.Name = "btnNameChange"
        Me.btnNameChange.Size = New System.Drawing.Size(228, 35)
        Me.btnNameChange.TabIndex = 13
        Me.btnNameChange.Text = "Change Name"
        Me.btnNameChange.UseVisualStyleBackColor = True
        '
        'btnQuit
        '
        Me.btnQuit.Location = New System.Drawing.Point(386, 358)
        Me.btnQuit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(226, 35)
        Me.btnQuit.TabIndex = 14
        Me.btnQuit.Text = "Quit"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'pnlOne
        '
        Me.pnlOne.Controls.Add(Me.lblName)
        Me.pnlOne.Controls.Add(Me.btnPractice)
        Me.pnlOne.Controls.Add(Me.btnNameChange)
        Me.pnlOne.Controls.Add(Me.btnMulti)
        Me.pnlOne.Controls.Add(Me.btnTimeAttk)
        Me.pnlOne.Location = New System.Drawing.Point(386, 20)
        Me.pnlOne.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlOne.Name = "pnlOne"
        Me.pnlOne.Size = New System.Drawing.Size(228, 329)
        Me.pnlOne.TabIndex = 15
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(0, 221)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(166, 20)
        Me.lblName.TabIndex = 14
        Me.lblName.Text = "Current name: Joseph"
        '
        'tmrHomeScreen
        '
        '
        'timTimeAttk
        '
        Me.timTimeAttk.Interval = 1
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTime.Location = New System.Drawing.Point(381, 20)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(0, 46)
        Me.lblTime.TabIndex = 16
        Me.lblTime.Visible = False
        '
        'lblScore
        '
        Me.lblScore.AutoSize = True
        Me.lblScore.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScore.Location = New System.Drawing.Point(381, 69)
        Me.lblScore.Name = "lblScore"
        Me.lblScore.Size = New System.Drawing.Size(0, 46)
        Me.lblScore.TabIndex = 17
        Me.lblScore.Visible = False
        '
        'lblCurrentHS
        '
        Me.lblCurrentHS.AutoSize = True
        Me.lblCurrentHS.Location = New System.Drawing.Point(389, 281)
        Me.lblCurrentHS.Name = "lblCurrentHS"
        Me.lblCurrentHS.Size = New System.Drawing.Size(0, 20)
        Me.lblCurrentHS.TabIndex = 18
        '
        'lblCurrentHSer
        '
        Me.lblCurrentHSer.AutoSize = True
        Me.lblCurrentHSer.Location = New System.Drawing.Point(393, 312)
        Me.lblCurrentHSer.Name = "lblCurrentHSer"
        Me.lblCurrentHSer.Size = New System.Drawing.Size(0, 20)
        Me.lblCurrentHSer.TabIndex = 19
        '
        'GameScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 402)
        Me.Controls.Add(Me.lblCurrentHSer)
        Me.Controls.Add(Me.lblCurrentHS)
        Me.Controls.Add(Me.lblScore)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.btn8)
        Me.Controls.Add(Me.btn7)
        Me.Controls.Add(Me.btn6)
        Me.Controls.Add(Me.btn5)
        Me.Controls.Add(Me.btn4)
        Me.Controls.Add(Me.btn3)
        Me.Controls.Add(Me.btn2)
        Me.Controls.Add(Me.btn1)
        Me.Controls.Add(Me.btn0)
        Me.Controls.Add(Me.pnlOne)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "GameScreen"
        Me.Text = "Tic Tac Toe Advanced"
        Me.pnlOne.ResumeLayout(False)
        Me.pnlOne.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn0 As Button
    Friend WithEvents btn1 As Button
    Friend WithEvents btn2 As Button
    Friend WithEvents btn3 As Button
    Friend WithEvents btn4 As Button
    Friend WithEvents btn5 As Button
    Friend WithEvents btn6 As Button
    Friend WithEvents btn7 As Button
    Friend WithEvents btn8 As Button
    Friend WithEvents btnPractice As Button
    Friend WithEvents btnMulti As Button
    Friend WithEvents btnTimeAttk As Button
    Friend WithEvents btnNameChange As Button
    Friend WithEvents btnQuit As Button
    Friend WithEvents pnlOne As Panel
    Friend WithEvents tmrHomeScreen As Timer
    Friend WithEvents timTimeAttk As Timer
    Friend WithEvents lblTime As Label
    Friend WithEvents lblScore As Label
    Friend WithEvents lblName As Label
    Friend WithEvents lblCurrentHS As Label
    Friend WithEvents lblCurrentHSer As Label
End Class
