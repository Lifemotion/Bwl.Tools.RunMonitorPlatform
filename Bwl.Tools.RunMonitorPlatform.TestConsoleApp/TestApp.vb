Module TestApp

    Sub Main()
        Console.WriteLine("Test Console App!")
        Console.WriteLine("")
        Do
            Console.WriteLine("1. Test Input")
            Console.WriteLine("0. Exit")
            Console.WriteLine("Select:")
            Dim read = Console.ReadLine
            If read = "0" Then End
            If read = "1" Then
                Console.WriteLine("Input: ")
                Dim read2 = Console.ReadLine
                Console.WriteLine(read.ToUpper)
            End If
        Loop


    End Sub

End Module
