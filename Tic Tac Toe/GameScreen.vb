Imports System.Threading
Imports System.IO

Public Class GameScreen

    ''' <summary>
    ''' The board in which all the tokens (x's and o's) are stored. Here, they are stored as 1 and -1, as this makes training the neural network more easy.
    ''' </summary>
    Dim board As New List(Of Double)

    ''' <summary>
    ''' Stores the high score
    ''' </summary>
    Dim highScore As Integer = 0

    ''' <summary>
    ''' Stores the name of the high scorer
    ''' </summary>
    Dim highScorer As String = ""

    ''' <summary>
    ''' The player's name, to be used in high scores, etc.
    ''' </summary>
    Dim playerName As String = ""

    ''' <summary>
    ''' Defines the hidden layers in the main neural network for Time Attack and Practice.
    ''' </summary>
    Dim hiddenLayers As New List(Of Integer)(New Integer() {810})

    ''' <summary>
    ''' Main neural network. Used in Time Attack, Practice and the home screen.
    ''' </summary>
    Dim network As New Neural(9, 810, 9)

    ''' <summary>
    ''' Second neural network to play on the main screen.
    ''' </summary>
    Dim homeScreenNetwork As New Neural(9, 810, 9)

    ''' <summary>
    ''' The last mode made by the player. Used in Time Attack and Practice to train the neural network defensively.
    ''' </summary>
    Dim lastMove As Integer

    ''' <summary>
    ''' The last configuration of the board, so that it can be used to train the neural network.
    ''' </summary>
    Dim lastBoard As List(Of Double)

    ''' <summary>
    ''' Stores the number of turns in a timeAttack or Practice game. Can be used to improve neural network learning rates with a PID controller.
    ''' </summary>
    Dim turns As Integer = 0

    ''' <summary>
    ''' Provides the gamemodes that can be passed around the program, in a more readable state than pure integers.
    ''' </summary>
    Enum gamemode
        none = -1
        practice = 0
        timeAttack = 1
        multiplayer = 2
    End Enum

    ''' <summary>
    ''' Stores the current game mode. Automatically set to none on the home screen.
    ''' </summary>
    Dim currentGamemode As gamemode = gamemode.none

    ''' <summary>
    ''' Calls functions to create the main neural network. Must be called each time Time Attack or Practice is launched so as to have the neural network start from a fair state.
    ''' </summary>
    Sub makeNetwork()
        'Make a new neural network from the Neural class.
        network = New Neural(9, hiddenLayers, 9)
    End Sub

    ''' <summary>
    ''' Initializes the board variable, and draws the board. Also sets turns to 0.
    ''' </summary>
    Sub InitializeBoard()
        'loop from 0-8 adding 0 to the board. Set turns to 0.
        For i As Integer = 0 To 8 Step 1
            board.Add(0)
        Next
        DrawBoard()
        turns = 0
    End Sub

    ''' <summary>
    ''' Called when the player (or X's) wins. In Practice and Time Attack this trains the neural network and restarts the game. In Multiplayer this restarts the game.
    ''' </summary>
    Sub PlayerWin()

        'If the player is in a neural network gamemode, train the network, and make a new board
        If currentGamemode = gamemode.timeAttack Or currentGamemode = gamemode.practice Then
            train()
            board = New List(Of Double)
            InitializeBoard()
            'else, the player is in multiplayer. Tell them that X has won.
        ElseIf currentGamemode = gamemode.multiplayer Then
            MsgBox("X's Wins!")
            board = New List(Of Double)
            InitializeBoard()
        End If

    End Sub

    ''' <summary>
    ''' Called when the computer (or O's) wins. In Practice, this resets the board. In time attack this invalidates the time (so that further alerts don't display), and returns the player to the main menu, as well as saving high scores. In multiplayer, this simply resets the board.
    ''' </summary>
    Sub ComputerWin()
        'if this is a playable gamemode that isn't multiplayer
        If currentGamemode = gamemode.timeAttack Or currentGamemode = gamemode.practice Then
            'invalidate the time
            time = -1
            'tell the player the computer has won
            MsgBox("The Computer won")
            'make a new board
            board = New List(Of Double)
            InitializeBoard()

            'if the player is playing timeAttack
            If currentGamemode = gamemode.timeAttack Then
                If score > highScore Then
                    'the player has achieved a high score. Tell them, and save it into the Application settings (this makes it global, IE between user accounts)
                    MsgBox("You Achieved a High Score!")
                    SaveSetting("TicTacToe", "Application", "HighScore", score.ToString())
                    SaveSetting("TicTacToe", "Application", "HighScorer", playerName)
                    highScore = score
                    highScorer = playerName
                End If
                'return to the menu, if this is time attack
                returnToMenu()
            End If

            'if this is multiplayer, a computerWin really means O's has won. Say this, and make a new board
        ElseIf currentGamemode = gamemode.multiplayer Then
            MsgBox("O's Wins!")
            board = New List(Of Double)
            InitializeBoard()
        End If

    End Sub

    ''' <summary>
    ''' Draws the board. Places X's and O's. This is done programatically.
    ''' </summary>
    Sub DrawBoard()
        For i As Integer = 0 To board.Count - 1 Step 1
            Dim marker As String = ""
            If board(i) = -1 Then
                marker = "X"
            ElseIf board(i) = 1 Then
                marker = "O"
            End If
            Me.Controls.Find("btn" + i.ToString(), True).FirstOrDefault().Text = marker
            Me.Controls.Find("btn" + i.ToString(), True).FirstOrDefault().BackColor = Color.LightGray
        Next
    End Sub

    ''' <summary>
    ''' Checks whether the board has a winning combination on it, and calls the specific function to handle which player has won
    ''' </summary>
    ''' <param name="winPosition">Variable to place the winning board position (the location of the 3 matching symbols) in</param>
    ''' <returns>True if there has been a win, False if not</returns>
    Function CheckForWin(ByRef Optional winPosition As Integer() = Nothing) As Boolean
        'Set the winposition variable if it hasn't been passed in to make sure that there are no null reference exceptions
        If winPosition Is Nothing Then
            winPosition = {0, 0, 0}
        End If
        turns += 1
        If board(0) = board(1) And board(1) = board(2) Then
            'win
            winPosition = {0, 1, 2}
            If board(0) = 1 Then
                'computer win
                ComputerWin()
                Return True
            ElseIf board(0) = -1 Then
                'player win
                PlayerWin()
                Return True
            End If
        End If
        If board(3) = board(4) And board(4) = board(5) Then
            'win
            winPosition = {3, 4, 5}
            If board(3) = 1 Then
                'computer win
                ComputerWin()
                Return True
            ElseIf board(3) = -1 Then
                'player win
                PlayerWin()
                Return True
            End If
        End If
        If board(6) = board(7) And board(7) = board(8) Then
            'win
            winPosition = {6, 7, 8}
            If board(6) = 1 Then
                'computer win
                ComputerWin()
                Return True
            ElseIf board(6) = -1 Then
                'player win
                PlayerWin()
                Return True
            End If
        End If
        If board(0) = board(3) And board(3) = board(6) Then
            'win
            winPosition = {0, 3, 6}
            If board(0) = 1 Then
                'computer win
                ComputerWin()
                Return True
            ElseIf board(0) = -1 Then
                'player win
                PlayerWin()
                Return True
            End If
        End If
        If board(1) = board(4) And board(4) = board(7) Then
            'win
            winPosition = {1, 4, 7}
            If board(1) = 1 Then
                'computer win
                ComputerWin()
                Return True
            ElseIf board(1) = -1 Then
                'player win
                PlayerWin()
                Return True
            End If
        End If
        If board(2) = board(5) And board(5) = board(8) Then
            'win
            winPosition = {2, 5, 8}
            If board(2) = 1 Then
                'computer win
                ComputerWin()
                Return True
            ElseIf board(2) = -1 Then
                'player win
                PlayerWin()
                Return True
            End If
        End If
        If board(0) = board(4) And board(4) = board(8) Then
            'win
            winPosition = {0, 4, 8}
            If board(0) = 1 Then
                'computer win
                ComputerWin()
                Return True
            ElseIf board(0) = -1 Then
                'player win
                PlayerWin()
                Return True
            End If
        End If
        If board(2) = board(4) And board(4) = board(6) Then
            'win
            winPosition = {2, 4, 6}
            If board(2) = 1 Then
                'computer win
                ComputerWin()
                Return True
            ElseIf board(2) = -1 Then
                'player win
                PlayerWin()
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' Trains the main neural network
    ''' </summary>
    Sub train()
        network.respond(lastBoard)

        ''The data to train the neural network with
        Dim trainingData As New List(Of Double)

        ''Loop through the board length, and add the last move to the training data at the specific index.
        For i As Integer = 0 To board.Count - 1 Step 1
            If i = lastMove Then
                trainingData.Add(1)
            Else
                trainingData.Add(0)
            End If
        Next i

        ''train the network based on this data.
        network.train(trainingData)
    End Sub

    ''' <summary>
    ''' trains the secondary neural network
    ''' </summary>
    Sub trainHomeScreen()
        'Respond to the last board configuration
        homeScreenNetwork.respond(lastBoard)

        'Make a new list to use to train the network
        Dim trainingData As New List(Of Double)
        For i As Integer = 0 To board.Count - 1 Step 1
            If i = lastMove Then
                trainingData.Add(1)
            Else
                trainingData.Add(0)
            End If
        Next i

        'train the network using the data
        homeScreenNetwork.train(trainingData)
    End Sub

    ''' <summary>
    ''' Gets a response from the main neural network.
    ''' </summary>
    ''' <returns>Board Position as an integer</returns>
    Function GetNeuralResponse() As Integer
        'Get the response from the neural network
        Dim response = network.respond(board)
        'Start with an invalid move
        Dim move As Integer = -1
        'Start with the highest response strength being out of the neuron bounds (below)
        Dim highestNeuron As Decimal = -1.0

        'Loop through each neuron checking whether the move is valid, and whether the neuron has responded with a higher strength than the most strong response so far (certainty)
        For i As Integer = 0 To response.Count - 1
            If response(i) > highestNeuron And board(i) = 0 Then
                highestNeuron = response(i)
                move = i
            End If
        Next

        'If there has been no move returned, look for any empty spot and return that
        If move = -1 Then
            For i As Integer = 0 To response.Count - 1
                If board(i) = 0 Then
                    move = i
                End If
            Next
        End If

        Return move
    End Function

    ''' <summary>
    ''' Gets a response from the second neural network (only available on the home screen)
    ''' </summary>
    ''' <returns>Board Position as an integer</returns>
    Function GetMainMenuResponse() As Integer
        Dim response = homeScreenNetwork.respond(board)
        Dim move As Integer = -1
        Dim highestNeuron As Decimal = -1
        For i As Integer = 0 To response.Count - 1
            If response(i) > highestNeuron And board(i) = 0 Then
                highestNeuron = response(i)
                move = i
            End If
        Next

        If move = -1 Then
            For i As Integer = 0 To response.Count - 1
                If board(i) = 0 Then
                    move = i
                End If
            Next
        End If

        Return move
    End Function

    ''' <summary>
    ''' Performs things needed at the very beginning of launching the game. This includes loading the player's name and loading any high scores.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GameScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Initialize the board variable
        InitializeBoard()
        'Start the timer on the home screen
        tmrHomeScreen.Start()

        'Check if the user has a saved username. If not, prompt them for one, with the default being their Windows username
        If GetSetting("TicTacToe", "User", "playerName") = "" Then
            playerName = InputBox("Please enter your name", "First time launch", Environment.UserName)
            SaveSetting("TicTacToe", "User", "playerName", playerName)
        Else
            playerName = GetSetting("TicTacToe", "User", "playerName")
        End If

        'Check if there are any saved high scores. If not set a dummy one that's easy to beat.
        If GetSetting("TicTacToe", "Application", "HighScore") = "" Then
            SaveSetting("TicTacToe", "Application", "HighScore", "20")
            SaveSetting("TicTacToe", "Application", "HighScorer", "Joseph")
        End If

        'Set the high score variables based on the saved values
        highScore = Integer.Parse(GetSetting("TicTacToe", "Application", "HighScore"))
        highScorer = GetSetting("TicTacToe", "Application", "HighScorer")

        'Set the name label to display the player's saved name
        lblName.Text = "Current Name: " & playerName
    End Sub

    ''' <summary>
    ''' Copies the current state of the board deeply (by value) so that it can be used to train a neural network.
    ''' </summary>
    Private Sub CopyBoard()
        'Make a new list
        lastBoard = New List(Of Double)

        'copy each value into this new list
        For i As Integer = 0 To board.Count - 1
            lastBoard.Add(board(i))
        Next
    End Sub

    ''' <summary>
    ''' Is the X the next player to move in multiplayer mode?
    ''' </summary>
    Dim nextTurnX As Boolean = True

    ''' <summary>
    ''' stores the number of times X has won in multiplayer mode.
    ''' </summary>
    Dim xwins As Integer

    ''' <summary>
    ''' stores the number of times O has won in multiplayer mode.
    ''' </summary>
    Dim owins As Integer

    ''' <summary>
    ''' Stores the default time to reset to in timeAttack mode.
    ''' </summary>
    Dim defaultTime As Integer = 500

    ''' <summary>
    ''' Stores the remaining time in timeAttack mode.
    ''' </summary>
    Dim time As Integer = defaultTime

    ''' <summary>
    ''' Stores the player's score in timeAttack mode.
    ''' </summary>
    Dim score As Integer = 0

    ''' <summary>
    ''' Stores consecutive wins in practice mode.
    ''' </summary>
    Dim consecutiveWins As Integer = 0

    ''' <summary>
    ''' Called whenever a button on the board is called. Checks which gamemode is being played, and performs the appropriate actions.
    ''' </summary>
    ''' <param name="sender">Default onClick parameter. Used to calculate cell clicked.</param>
    ''' <param name="e">Default onClick parameter</param>
    Private Sub btn_Click(sender As Object, e As EventArgs) Handles btn8.Click, btn7.Click, btn6.Click, btn5.Click, btn4.Click, btn3.Click, btn2.Click, btn1.Click, btn0.Click
        'check whether a button click is actually valid here - is the player playing a gamemode? or are they on the home screen
        If currentGamemode <> gamemode.none And currentGamemode <> gamemode.multiplayer Then
            'Find the button that the player clicked
            Dim sentBy As Button = sender
            Dim index As Integer = sentBy.Name.Replace("btn", "")

            'Make a blank variable to store the win condition in
            Dim win = {0, 0, 0}

            'If the board position clicked is blank
            If board(index) = 0 Then
                'Copy the board
                CopyBoard()
                'Set the clicked cell to a cross
                board(index) = -1
                'set lastMove to the index of the cell
                lastMove = index
                'Draw the board again
                DrawBoard()
                'Check if the player has won
                If CheckForWin(win) = True Then
                    'If so, draw a win on the board in green
                    drawWin(displayColor.green, win)
                    'Add the remaining milliseconds / 10 to the player's score (should be a two digit number)
                    score += time / 10
                    'Show the score increase
                    lblScore.Text = "Score: " + score.ToString()
                    'Set the time back to the normal time per turn
                    time = defaultTime
                    'If it's practice mode, increase the consecutive win count
                    If currentGamemode = gamemode.practice Then
                        consecutiveWins += 1
                        'Display the consecutive wins to the player
                        lblScore.Text = "Wins: " + consecutiveWins.ToString()
                    End If
                    Return
                End If

                'If the board doesn't have any free spaces, make the computer win by default
                If board.Contains(0) = False Then
                    ComputerWin()
                End If

                'If this is a gamemode that utilizes the neural network
                If currentGamemode = gamemode.timeAttack Or currentGamemode = gamemode.practice Then
                    'get a response from the network, and set the index in the list accordingly
                    board(GetNeuralResponse()) = 1
                    'draw the board again
                    DrawBoard()
                    'train the neural network based on the player's last turn
                    train()
                    'if the computer has won
                    If CheckForWin(win) = True Then
                        'draw the win in red
                        drawWin(displayColor.red, win)
                        'if it's practice mode, set consecutive wins to 0, and display this
                        If currentGamemode = gamemode.practice Then
                            consecutiveWins = 0
                            lblScore.Text = "Wins: " + consecutiveWins.ToString()
                        End If
                    End If
                End If
            End If
            'if the board has no free spots, set consecutive wins to 0, and have the computer win
            If board.Contains(0) = False Then
                ComputerWin()
                consecutiveWins = 0
                lblScore.Text = "Wins: " + consecutiveWins.ToString()
            End If

            'if we're in multiplayer mode
        ElseIf currentGamemode = gamemode.multiplayer Then
            'Get the button pressed
            Dim sentBy As Button = sender
            Dim index As Integer = sentBy.Name.Replace("btn", "")
            Dim win = {0, 0, 0}
            'if the position is free
            If board(index) = 0 Then
                'if it's x's turn
                If nextTurnX Then
                    'set the array index and redraw the board
                    board(index) = -1
                    DrawBoard()
                    'if the player has won
                    If CheckForWin(win) Then
                        'draw the win in green
                        drawWin(displayColor.green, win)
                        'increase the win counter
                        xwins += 1
                        lblTime.Text = "X wins: " + xwins.ToString()
                    End If
                    'let O play next turn
                    nextTurnX = False
                    lblCurrentHS.Text = "O's Turn"

                    'else if it is O's turn
                Else
                    'set the array index and draw the board
                    board(index) = 1
                    DrawBoard()
                    'check if O has won
                    If CheckForWin(win) Then
                        'draw this onto the board
                        drawWin(displayColor.red, win)
                        'increase O's wins
                        owins += 1
                        lblScore.Text = "O wins: " + owins.ToString()
                    End If
                    'Set the next turn to be X's
                    nextTurnX = True
                    lblCurrentHS.Text = "X's Turn"
                End If
                'if there are no free spots and there has been no win, call gameTied
                If board.Contains(0) = False And win(0) = win(1) Then
                    gameTied()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Called when a multiplayer game ties.
    ''' </summary>
    Private Sub gameTied()
        'tell the player that the game was a tie and start over
        MsgBox("The game ended in a Tie!")
        board = New List(Of Double)
        InitializeBoard()
    End Sub

    ''' <summary>
    ''' Handles UI updates for bringing the game back to its default state.
    ''' </summary>
    Private Sub returnToMenu()
        'set the gamemode to none, and hide all the labels
        currentGamemode = gamemode.none
        pnlOne.Show()
        lblTime.Hide()
        lblScore.Hide()
        lblCurrentHS.Hide()
        lblCurrentHSer.Hide()
    End Sub

    ''' <summary>
    ''' Makes necessary UI updates to begin a game of any gamemode.
    ''' </summary>
    Private Sub beginGame()
        'hide the panel of buttons
        pnlOne.Hide()
    End Sub

    ''' <summary>
    ''' Calls necessary functions, and UI updates for a practice game.
    ''' </summary>
    ''' <param name="sender">Default onClick parameter</param>
    ''' <param name="e">Default onClick parameter</param>
    Private Sub btnPractice_Click(sender As Object, e As EventArgs) Handles btnPractice.Click
        'setup the screen for practice mode, by showing the consecutive win counter, and initializing the network
        currentGamemode = gamemode.practice
        makeNetwork()
        board = New List(Of Double)
        InitializeBoard()
        DrawBoard()
        beginGame()
        lblTime.Show()
        lblScore.Show()
        lblTime.Text = "Consecutive"
        lblScore.Text = "Wins: 0"
    End Sub

    ''' <summary>
    ''' Calls necessary functions, and UI updates for a multiplayer game.
    ''' </summary>
    ''' <param name="sender">Default onClick parameter</param>
    ''' <param name="e">Default onClick parameter</param>
    Private Sub btnMulti_Click(sender As Object, e As EventArgs) Handles btnMulti.Click
        'setup the screen for multiplayer, by making a new board, resetting multiplayer variables and showing who's turn it is
        currentGamemode = gamemode.multiplayer
        board = New List(Of Double)
        InitializeBoard()
        DrawBoard()
        beginGame()
        lblTime.Text = "X wins: 0"
        lblScore.Text = "O wins: 0"
        lblScore.Show()
        lblTime.Show()
        xwins = 0
        owins = 0
        nextTurnX = False
        lblCurrentHS.Show()
        lblCurrentHS.Text = "O's Turn!"
    End Sub

    ''' <summary>
    ''' Calls necessary functions, and UI updates for a single-player time-attack game
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnTimeAttk_Click(sender As Object, e As EventArgs) Handles btnTimeAttk.Click
        'Make a neural netowrk, start the timer, set the highscore showing labels
        currentGamemode = gamemode.timeAttack
        makeNetwork()
        board = New List(Of Double)
        InitializeBoard()
        DrawBoard()
        beginGame()
        timTimeAttk.Start()
        lblTime.Show()
        lblScore.Show()
        lblScore.Text = "Score: 0"
        time = defaultTime
        lblCurrentHS.Show()
        lblCurrentHSer.Show()
        highScore = Integer.Parse(GetSetting("TicTacToe", "Application", "HighScore"))
        highScorer = GetSetting("TicTacToe", "Application", "HighScorer")
        lblCurrentHS.Text = "Current High Score: " & highScore
        lblCurrentHSer.Text = highScorer
    End Sub

    ''' <summary>
    ''' Provides readability for which color will be used when drawing a win onto the board.
    ''' </summary>
    Enum displayColor
        green = True
        red = False
    End Enum

    ''' <summary>
    ''' Draws a win onto the board.
    ''' </summary>
    ''' <param name="displayColor">The color that the win will be displayed as. Must be from the enum displayColor.</param>
    ''' <param name="winType">The board condition that led to a win. As an array of integers.</param>
    Private Sub drawWin(displayColor As displayColor, winType As Integer())
        For i As Integer = 0 To winType.Count - 1 Step 1
            'if red, loop through the buttons in the win array, and set them to red. Else do the same for green.
            If displayColor = displayColor.red Then
                Me.Controls.Find("btn" + winType(i).ToString(), True).FirstOrDefault().BackColor = Color.OrangeRed
            Else
                Me.Controls.Find("btn" + winType(i).ToString(), True).FirstOrDefault().BackColor = Color.LawnGreen
            End If
        Next
    End Sub

    ''' <summary>
    ''' Stores whether the last game (on the homescreen) has been won. This allows it to pause for 1 cycle so that the user can see that there has been a win.
    ''' </summary>
    Dim lastGameWon As Boolean = True

    ''' <summary>
    ''' Runs a game pitting one neural network against another. Used to run the demo on the home screen of the game.
    ''' </summary>
    Private Sub mainMenuDemo()
        'Quit the function if we're in a game
        If currentGamemode <> gamemode.none Then
            Return
        End If

        'If the game was won last time, reset the board
        If lastGameWon Then
            board = New List(Of Double)
            InitializeBoard()
            lastGameWon = False
        End If

        'Train the home screen neural network if it can be trained
        If lastBoard IsNot Nothing Then
            trainHomeScreen()
        End If

        'get a response from the main menu neural network
        Dim index As Integer = GetMainMenuResponse()
        CopyBoard()

        If index > -1 Then
            board(index) = -1
            lastMove = index
        End If

        Dim win = {0, 0, 0}

        'check for a win, and display it if it happens
        If CheckForWin(win) Then
            DrawBoard()
            drawWin(displayColor.green, win)
            lastGameWon = True
            Return
        End If

        'train the main network
        train()
        index = GetNeuralResponse()
        CopyBoard()

        'if the move is -1 (the board has no valid moves), pause the game then reset it next loop
        If index > -1 Then
            board(index) = 1
            lastMove = index
        Else
            lastGameWon = True
        End If

        'if the main network has won, display it and pause (through lastGameWon)
        If CheckForWin(win) Then
            DrawBoard()
            drawWin(displayColor.red, win)
            lastGameWon = True
            Return
        End If
        DrawBoard()
    End Sub

    ''' <summary>
    ''' Calls the mainMenuDemo function when a gamemode is not selected (the user is on the home screen).
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tmrHomeScreen_Tick(sender As Object, e As EventArgs) Handles tmrHomeScreen.Tick
        'only call the mainMenuDemo if the player is not in a game
        If currentGamemode = gamemode.none Then
            mainMenuDemo()
        End If

    End Sub

    ''' <summary>
    ''' Quits the current gamemode if it isn't none. This means resetting the UI, and returning to the home screen. Otherwise quits the game.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        'if the player is at the homescreen, quit the game. Else return to the homescreen.
        If currentGamemode = gamemode.none Then
            Me.Close()
        Else
            returnToMenu()
        End If
    End Sub

    ''' <summary>
    ''' Runs functions necessary to running the time attack mode. Essentially, lowers the time, and causes a game end if the remaining time reaches 0.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub timTimeAttk_Tick(sender As Object, e As EventArgs) Handles timTimeAttk.Tick
        'if the player is playing time attack, decrease the time remaining and display this.
        If currentGamemode = gamemode.timeAttack And time > 0 Then
            time -= 1
            lblTime.Text = "Time: " + (Double.Parse(time) / 100).ToString()
            'if the time remaining is less than or equal to 0, tell the player they're out of time, and return to the menu
            If time <= 0 Then
                MsgBox("You ran out of time!")
                ComputerWin()
                returnToMenu()
            End If
        End If
    End Sub

    Private Sub btnNameChange_Click(sender As Object, e As EventArgs) Handles btnNameChange.Click
        'get an input box to prompt for a name change. Then set the default to the result, and update UI.
        playerName = InputBox("Please enter your name", "Change your name", GetSetting("TicTacToe", "User", "playerName"))
        SaveSetting("TicTacToe", "User", "playerName", playerName)
        lblName.Text = "Current Name: " & playerName
    End Sub
End Class
