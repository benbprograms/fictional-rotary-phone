Module Module1

    Structure card
        Dim Name As String
        Dim Heading As Integer
        Dim Shooting As Integer
        Dim Dribbling As Integer
        Dim Injury As Integer
    End Structure

    Dim Deck() As card
    Dim PlayersHand() As card
    Dim ComputersHand() As card

    Sub Main()
        Randomize()
        Console.ForegroundColor = ConsoleColor.White
        menu()

    End Sub

    Sub menu() 'Display the menu for the game
        ShowMenuOptions()
        Do Until GetMenuOption() = "2"
            StartGame()
            GamePlay()
            ShowMenuOptions()
        Loop
        Console.WriteLine("Press any key and enter to close the game")
        Console.ReadLine()
    End Sub

    Sub StartGame() ' This creates the deck and deals the cards to players and computer
        GetootballerNames()
        GetFootballersAttributes() 'Generates the numbers for the players
        DealHands(GetNumberOfPlayingCards())
        ShowDeckGraphic()



    End Sub

    Sub ShowMenuOptions()
        Console.WriteLine("Please select an option below")
        Console.WriteLine("-----------------------------")
        Console.WriteLine("1) Play Game")
        Console.WriteLine("2) Ouit")
    End Sub

    Function GetMenuOption() As String ' Gets the users choice for the main menu
        Dim MenuChoice As String
        MenuChoice = Console.ReadLine
        Do Until MenuOptionIsValid(MenuChoice)
            MenuChoice = Console.ReadLine
        Loop
        Return MenuChoice
    End Function

    Function MenuOptionIsValid(ByVal ChoosenOption As String) As Boolean ' Chck that the main menu option that the user has choosen is valid
        If ChoosenOption.Length < 1 Or ChoosenOption.Length > 1 Then
            Console.WriteLine("This is to long")
            Return False
        End If

        If InStr("12", ChoosenOption) < 1 Then
            Console.WriteLine("This is not a valid option")
            Return False
        End If

        Return True
    End Function

    Sub GetootballerNames() ' Goes to the file to open the list of names
        Dim counter As Integer = 0
        FileOpen(1, "S:\footballers.txt", OpenMode.Input)
        Do Until EOF(1)
            ReDim Preserve Deck(counter)
            Deck(counter).Name = LineInput(1)
            counter += 1
        Loop
        FileClose(1)
    End Sub

    Sub GetFootballersAttributes() ' Generates the attributes for the players

        For looper = 0 To Deck.Length - 1
            Deck(looper).Heading = Int(Rnd() * 5 + 1)
            Deck(looper).Shooting = Int(Rnd() * 100 + 1)
            Deck(looper).Dribbling = Int(Rnd() * 10 + 1)
            Deck(looper).Injury = Int(Rnd() * 10 + 1)
        Next
    End Sub

    Sub DealHands(ByVal PlayingSize As Integer) 'splits the deck into 2 equal decks depening on the size that the user has entered
        Dim ChoosenCard As Integer
        For looper = 0 To (PlayingSize / 2) - 1

            ChoosenCard = Int(Rnd() * Deck.Length - 1 + 1)
            ReDim Preserve PlayersHand(looper)
            PlayersHand(looper) = Deck(ChoosenCard)
            RemoveCardFromDeck(ChoosenCard)

            ChoosenCard = Int(Rnd() * Deck.Length - 1 + 1)
            ReDim Preserve ComputersHand(looper)
            ComputersHand(looper) = Deck(ChoosenCard)
            RemoveCardFromDeck(ChoosenCard)
        Next

    End Sub

    Sub RemoveCardFromDeck(ByVal ItemNumber As Integer) 'This takes cards out of the deck and places them into the player and computers hand
        For looper = ItemNumber To Deck.Length - 2
            Deck(looper) = Deck(looper + 1)
        Next
        ReDim Preserve Deck(Deck.Length - 2)
    End Sub
    Function GetNumberOfPlayingCards() As Integer 'Get the users details on how many cards he wants to use in the game

        Console.WriteLine("How many cards do you want to play with?")
        Dim PlayersChoice As String = Console.ReadLine
        Do Until IsNumberValid(PlayersChoice)
            PlayersChoice = Console.ReadLine
        Loop


        Return PlayersChoice
    End Function

    Function IsNumberValid(ByVal ChoosenOption As String) As Boolean 'Check the number is avlid and that it is in the range 4 - 22. Also checks that it is an even number
        If ChoosenOption.Length > 2 Or ChoosenOption.Length < 1 Then
            Console.WriteLine("This is the incorrect length - please try again")
            Return False
        End If

        If ChoosenOption.Length = 2 Then

            If InStr("0123456789", Mid(ChoosenOption, 1, 1)) < 1 Or InStr("0123456789", Mid(ChoosenOption, 2, 1)) < 1 Then
                Console.WriteLine("This is not a number - please try again")
                Return False
            End If

        Else
            If InStr("0123456789", ChoosenOption) < 1 Then
                Console.WriteLine("This is not a number - please try again")
                Return False
            End If
        End If

        If ChoosenOption < 4 Or ChoosenOption > 22 Then
            Console.WriteLine("This number is not between 4 or 22 - please try again")
            Return False
        End If

        If ChoosenOption Mod 2 > 0 Then
            Console.WriteLine("This number is not even - please try again")
            Return False
        End If

        Return True

    End Function

    Sub ShowDeckGraphic() ' Show a grapic of how many cards are in the users and computers hand.(This is not needed for your assessment)
        Dim Difference As Integer

        Difference = PlayersHand.Length - ComputersHand.Length
        If Difference < 0 Then Difference = Difference * -1

        If PlayersHand.Length > ComputersHand.Length Then
            For looper = 0 To ComputersHand.Length - 1
                Console.Write("__________")
                Console.Write("  ")
                Console.Write("__________")
                Console.WriteLine()
            Next
            For looper = 0 To Difference - 1
                Console.Write("__________")
                Console.WriteLine()
            Next

        ElseIf PlayersHand.Length < ComputersHand.Length Then
            For looper = 0 To PlayersHand.Length - 1
                Console.Write("__________")
                Console.Write("  ")
                Console.Write("__________")
                Console.WriteLine()
            Next
            For looper = 0 To Difference - 1
                Console.Write("            ")
                Console.Write("__________")
                Console.WriteLine()
            Next
        Else
            For looper = 0 To ComputersHand.Length - 1
                Console.Write("__________")
                Console.Write("  ")
                Console.Write("__________")
                Console.WriteLine()
            Next
        End If

    End Sub

    Sub GamePlay() ' This is the sub with the main game processes for each round
        Dim PlayersTurn As Boolean = True
        Dim TempCard As card
        Dim StatToCompare As String
        Dim RoundCounter As Integer

        Do Until IsThereAWinner() = True
            Console.WriteLine()
            RoundCounter = RoundCounter + 1
            Console.WriteLine("Round " & RoundCounter)
            Console.WriteLine()
            Console.WriteLine("Press enter to see your card")
            Console.ReadLine()

            ShowCard(True)
            'This if statment sees who turn it is and then runs the code to go with it.
            If PlayersTurn = True Then

                StatToCompare = GetUserChoice()
            Else
                Console.WriteLine()
                Console.WriteLine("Press enter to see the computers choice")
                Console.ReadLine()
                StatToCompare = GetComputerChoice()
            End If

            ' Runs the code which compares the stat and places the losers card into the winners hand. Depending on computer of player
            If DidPlayerWin(StatToCompare) Then
                ReDim Preserve PlayersHand(PlayersHand.Length)
                TempCard = PlayersHand(0)
                For looper = 0 To PlayersHand.Length - 2
                    PlayersHand(looper) = PlayersHand(looper + 1)
                Next
                PlayersHand(PlayersHand.Length - 2) = TempCard
                PlayersHand(PlayersHand.Length - 1) = ComputersHand(0)

                For looper = 0 To ComputersHand.Length - 2
                    ComputersHand(looper) = ComputersHand(looper + 1)
                Next
                ReDim Preserve ComputersHand(ComputersHand.Length - 2)
                PlayersTurn = True
            Else

                ReDim Preserve ComputersHand(ComputersHand.Length)
                TempCard = ComputersHand(0)
                For looper = 0 To ComputersHand.Length - 2
                    ComputersHand(looper) = ComputersHand(looper + 1)
                Next

                ComputersHand(ComputersHand.Length - 2) = TempCard
                ComputersHand(ComputersHand.Length - 1) = PlayersHand(0)

                For looper = 0 To PlayersHand.Length - 2
                    PlayersHand(looper) = PlayersHand(looper + 1)
                Next
                ReDim Preserve PlayersHand(PlayersHand.Length - 2)
                PlayersTurn = False
            End If

            ShowDeckGraphic()
        Loop
        DisplayWinner()
    End Sub

    Sub DisplayWinner() ' This is the winners message for the end of the game
        If ComputersHand.Length < 1 Then
            Console.WriteLine()
            Console.ForegroundColor = ConsoleColor.Green
            Console.WriteLine("Congrats you win the game")
            Console.ForegroundColor = ConsoleColor.White
            Console.WriteLine()
            Console.WriteLine("Press any button to go to the menu")
            Console.ReadLine()
        Else
            Console.WriteLine()
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("Unlucky you lose the game")
            Console.ForegroundColor = ConsoleColor.White
            Console.WriteLine()
            Console.WriteLine("Press any button to go to the menu")
            Console.ReadLine()

        End If
        Console.Clear()
    End Sub

    Sub ShowCard(ByVal PlayerTurn As Boolean) 'This will show the card as a graphic it will show computers or players depening on the boolean 
        If PlayerTurn Then
            Console.ForegroundColor = ConsoleColor.Blue
            Console.WriteLine("------------------------")
            Console.WriteLine("Your Card")
            Console.WriteLine("------------------------")
            Console.WriteLine(PlayersHand(0).Name)
            Console.WriteLine("|S| Shooting  - " & PlayersHand(0).Shooting)
            Console.WriteLine("|D| Dribbling - " & PlayersHand(0).Dribbling)
            Console.WriteLine("|H| Heading   - " & PlayersHand(0).Heading)
            Console.WriteLine("|I| Injury    - " & PlayersHand(0).Injury)
            Console.WriteLine("------------------------")
            Console.ForegroundColor = ConsoleColor.White
        Else
            Console.ForegroundColor = ConsoleColor.Yellow
            Console.WriteLine("------------------------")
            Console.WriteLine("Computers Card")
            Console.WriteLine("------------------------")
            Console.WriteLine(ComputersHand(0).Name)
            Console.WriteLine("|S| Shooting  - " & ComputersHand(0).Shooting)
            Console.WriteLine("|D| Dribbling - " & ComputersHand(0).Dribbling)
            Console.WriteLine("|H| Heading   - " & ComputersHand(0).Heading)
            Console.WriteLine("|I| Injury    - " & ComputersHand(0).Injury)
            Console.WriteLine("------------------------")
            Console.ForegroundColor = ConsoleColor.White
        End If

    End Sub

    Function GetUserChoice() ' This will get the skill that the user wants to use in the round to compare
        Dim Userchoice As String
        Console.WriteLine("Please select a Skill")
        Userchoice = UCase(Console.ReadLine)
        Do Until IsUserChoiceValid(Userchoice)
            Userchoice = UCase(Console.ReadLine)
        Loop
        Return Userchoice
    End Function

    Function IsUserChoiceValid(ByVal ChoosenOption As String) 'Will check to see if the users choice is valid. 

        If ChoosenOption.Length < 1 Or ChoosenOption.Length > 1 Then
            Console.WriteLine("Too Long - Try Again")
            Return False
        End If

        If InStr("SDHI", ChoosenOption) < 1 Then
            Console.WriteLine("Not Valid - Try Again")
            Return False
        End If
        Return True
    End Function

    Function DidPlayerWin(ByVal SelectedItem As String) As Boolean ' This will compare the stats between the computer and player and return true if the player wins. 
        ShowCard(False)
        If SelectedItem = "S" Then

            Console.ForegroundColor = ConsoleColor.DarkGreen
            Console.WriteLine("Shooting")

            Console.ForegroundColor = ConsoleColor.Gray
            Console.WriteLine("Player - " & PlayersHand(0).Shooting & " vs " & "Computer - " & ComputersHand(0).Shooting)

            If PlayersHand(0).Shooting < ComputersHand(0).Shooting Then

                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("Computer Wins")
                Console.ForegroundColor = ConsoleColor.White

                Return False
            End If

        ElseIf SelectedItem = "D" Then

            Console.ForegroundColor = ConsoleColor.DarkGreen
            Console.WriteLine("Dribbling")

            Console.ForegroundColor = ConsoleColor.Gray
            Console.WriteLine("Player - " & PlayersHand(0).Dribbling & " vs " & "Computer - " & ComputersHand(0).Dribbling)


            If PlayersHand(0).Dribbling < ComputersHand(0).Dribbling Then

                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("Computer Wins")
                Console.ForegroundColor = ConsoleColor.White

                Return False
            End If

        ElseIf SelectedItem = "H" Then

            Console.ForegroundColor = ConsoleColor.DarkGreen
            Console.WriteLine("Heading")

            Console.ForegroundColor = ConsoleColor.Gray
            Console.WriteLine("Player - " & PlayersHand(0).Heading & " vs " & "Computer - " & ComputersHand(0).Heading)

            If PlayersHand(0).Heading < ComputersHand(0).Heading Then

                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("Computer Wins the round")
                Console.ForegroundColor = ConsoleColor.White

                Return False
            End If

        ElseIf SelectedItem = "I" Then

            Console.ForegroundColor = ConsoleColor.DarkGreen
            Console.WriteLine("Injury")

            Console.ForegroundColor = ConsoleColor.Gray
            Console.WriteLine("Player - " & PlayersHand(0).Injury & " vs " & "Computer - " & ComputersHand(0).Injury)


            If PlayersHand(0).Injury > ComputersHand(0).Injury Then

                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("Computer Wins")
                Console.ForegroundColor = ConsoleColor.White

                Return False
            End If

        End If

        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine("You Win the round")
        Console.ForegroundColor = ConsoleColor.White
        Return True
    End Function

    Function GetComputerChoice() As String 'This generates the computers choice of what they want to compare
        Dim RandomNumber As Integer = Int(Rnd() * 4 + 1)
        If RandomNumber = 1 Then
            Return "S"
        ElseIf RandomNumber = 2 Then
            Return "D"
        ElseIf RandomNumber = 3 Then
            Return "H"
        Else
            Return "I"
        End If

    End Function

    Function IsThereAWinner() As Boolean 'Check to see if anyone has lost all their cards.
        If PlayersHand.Length = 0 Or ComputersHand.Length = 0 Then
            Return True
        End If
        Return False
    End Function
End Module
