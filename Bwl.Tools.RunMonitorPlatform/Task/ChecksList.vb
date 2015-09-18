Public Class ChecksList
    Inherits List(Of ITaskCheck)

    Public Function GetByType(objType As Type) As ITaskCheck()
        Dim list As New List(Of ITaskCheck)
        For Each check In Me
            If check.GetType = objType Then list.Add(check)
        Next
        Return list.ToArray
    End Function

    Public Function GetByTypeSingle(type As Type) As ITaskCheck
        Dim list = GetByType(type)
        If list.Count = 0 Then Throw New Exception("No objects with type " + type.Name + " found")
        If list.Count > 1 Then Throw New Exception("More than one objects with type " + type.Name + " found")
        Return list(0)
    End Function
End Class
