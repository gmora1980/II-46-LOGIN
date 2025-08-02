Public Class Usuario

    Public Property Id As Integer
    Public Property Nombre As String
    Public Property Apellido As String
    Public Property Email As String
    Public Property Password As String
    ' Constructor por defecto
    Public Sub New()
    End Sub

    ' Método para validar el usuario (ejemplo simple)
    Public Function Validar() As Boolean
        Return Not String.IsNullOrEmpty(Email) AndAlso Not String.IsNullOrEmpty(Password)
    End Function

    ' Método para convertir un DataTable a un objeto Usuario
    Public Function dtToUsuario(dataTable As DataTable) As Usuario
        If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then
            Return Nothing
        End If
        Dim row = dataTable.Rows(0)
        Dim usuarios As New Usuario() With {
            .Id = Convert.ToInt32(row("ID")),
            .Nombre = Convert.ToString(row("NOMBRE")),
            .Apellido = Convert.ToString(row("APELLIDOS")),
            .Email = Convert.ToString(row("EMAIL")),
            .Password = Convert.ToString(row("CONTRASEÑA"))
        }
        Return usuarios
    End Function


End Class