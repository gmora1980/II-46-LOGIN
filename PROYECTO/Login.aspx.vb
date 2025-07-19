Imports System.Data.SqlClient
Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Function VerificarUsuario(usuario As Usuario) As Boolean
        Try
            Dim helper As New DatabaseHelper()
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Email", usuario.Email),
                New SqlParameter("@Password", usuario.Password)
            }
            ' Ejecutar la consulta para verificar el usuario
            Dim query As String = "SELECT * FROM Usuarios WHERE EMAIL =   @Email   AND CONTRASEÑA = @Password;"
            ' Utilizar ExecuteQuery para obtener el DataTable
            Dim dataTable As DataTable = helper.ExecuteQuery(query, parametros)


            ' Verificar si se encontró el usuario
            If dataTable.Rows.Count > 0 Then
                ' Usuario encontrado, puedes redirigir o realizar otra acción
                usuario = usuario.dtToUsuario(dataTable)
                Session.Add("UsuarioId", usuario.Id.ToString())
                Session.Add("UsuarioNombre", usuario.Nombre.ToString())
                Session.Add("UsuarioApellido", usuario.Apellido.ToString())
                Session.Add("UsuarioEmail", usuario.Email.ToString())
                Return True
            Else
                ' Usuario no encontrado, manejar el error
                Return False

            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)

        ' Obtener los valores de los campos de entrada
        Dim usuario As New Usuario() With {
            .Email = txtEmail.Text,
            .Password = txtPass.Text
        }

        ' Validar el usuario
        If VerificarUsuario(usuario) Then
            Response.Redirect("Default.aspx")
        Else

            lblError.Text = "Correo electrónico o contraseña inválidos."
            lblError.Visible = True
        End If
    End Sub

End Class